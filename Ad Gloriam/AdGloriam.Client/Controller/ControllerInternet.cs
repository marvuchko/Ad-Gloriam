using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Configuration;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Ad_Gloriam.Model;
using Ad_Gloriam.View;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

/* TODO
 * 
 */
namespace Ad_Gloriam.Controller
{
    class ControllerInternet : IController
    {
        #region Attributes
        private IModel _model;
        private IView _view;
        private bool _selected = false;
        private bool _over = false;
        private List<GameItem> _found = new List<GameItem>();
        private bool _joined = false;
        private GameItem _gItem;
        private Socket _client;
        private TcpClient _server;
        private TcpClient _serverClient;
        private TcpClient _sendClient;
        private Socket _listenSocket;
        private int _me;
        private bool _canPlay = false;
        private bool _recv = false;
        #endregion

        #region Threads
        private Thread gl;
        #endregion

        #region Constructors
        public ControllerInternet(IModel model, IView view)
        {
            this._model = model;
            this._view = view;
        }
        #endregion        

        #region Members

        public void SendMessage(string message)
        {
            if (!_joined) return;

            try
            {
                IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(ConfigurationManager.AppSettings["ServerIPAddress"]), int.Parse(ConfigurationManager.AppSettings["InternetMessageListenerPort"]));
                //
                Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                sock.Connect(ipep);
                sock.Send(Encoding.ASCII.GetBytes(message));
                sock.Close();
                _view.DisplayMessage("Player " + _me + ": " + message);
                //
            }
            catch (Exception ex)
            {
                EndGame();   
            }
        }

        public void CreateGame()
        {
            _canPlay = true;
            IPAddress serverIP = IPAddress.Parse(ConfigurationManager.AppSettings["ServerIPAddress"]);
            int serverPort = int.Parse(ConfigurationManager.AppSettings["CreateGamePort"]);
            IPEndPoint serverEP = new IPEndPoint(serverIP, serverPort);

            _server = new TcpClient();
            
            _server.Client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _view.DisplayMessage("Connecting to server...");

            try
            {
                _server.Client.Connect(serverEP);
                _server.Client.Send(Encoding.ASCII.GetBytes(ConfigurationManager.AppSettings["RequestToCreateGameMessage"]));
            }
            catch(Exception ex)
            {
                EndGame();
                return;
            }


            string response;
            byte[] data = new byte[1024];
            try
            {
                _server.Client.Receive(data);
                response = Encoding.ASCII.GetString(data).TrimEnd('\0');
                if (response == ConfigurationManager.AppSettings["SuccessMessage"])
                {
                    string gn = _model.GetGame().GameName;
                    string pwd = _model.GetGame().Password;

                    _server.Client.Send(Encoding.ASCII.GetBytes(gn + "-" + pwd));

                    response = Encoding.ASCII.GetString(data).TrimEnd('\0');

                    if (response != ConfigurationManager.AppSettings["SuccessMessage"]) throw new Exception("Not created");
                    _me = 1;

                    _view.DisplayMessage("Waiting for player 2...");
                    Thread listener = new Thread(new ThreadStart(ListenMessagesThread));
                    listener.IsBackground = true;
                    listener.Start();

                    Thread client = new Thread(new ThreadStart(GameClient));
                    client.IsBackground = true;
                    client.Start();
                }
            }
            catch (Exception ex)
            {
                EndGame();
            }
        }

        public List<GameItem> FindGames()
        {
            _found = new List<GameItem>();

            IPAddress serverIP = IPAddress.Parse(ConfigurationManager.AppSettings["ServerIPAddress"]);
            int serverPort = int.Parse(ConfigurationManager.AppSettings["GetGamesPort"]);
            IPEndPoint serverEP = new IPEndPoint(serverIP, serverPort);

            try
            {
                Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                sock.Connect(serverEP);
                sock.Send(Encoding.ASCII.GetBytes(ConfigurationManager.AppSettings["RequestGamesMessage"]));

                string response;
                byte[] data = new byte[1024];
                sock.Receive(data);
                sock.Close();
                response = Encoding.ASCII.GetString(data);

                string[] games = response.Split('|');
                foreach (var g in games)
                {
                    var gameSplitted = g.Split('-');
                    if (gameSplitted.Length != 2) continue;
                    var game = new GameItem(gameSplitted[0].TrimEnd('\0'), "INTERNET", gameSplitted[1].TrimEnd('\0'));
                    _found.Add(game);
                }

                Thread.Sleep(2000);
            }
            catch (Exception ex)
            {
                EndGame();
            }
            return _found;
        }

        public bool JoinGame(GameItem gItem, string pwd)
        {
            _gItem = gItem;
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(ConfigurationManager.AppSettings["ServerIPAddress"]), int.Parse(ConfigurationManager.AppSettings["JoinGamePort"]));
            TcpClient tcp;
            tcp = new TcpClient();
            _view.DisplayMessage("Connecting to server...");
            try
            {
                tcp.Connect(ipep);
                string message = ConfigurationManager.AppSettings["RequestJoinMessage"];
                _server = tcp;
                tcp.Client.Send(Encoding.ASCII.GetBytes(message));

                byte[] response = new byte[1024];

                tcp.Client.Receive(response);

                string recv = Encoding.ASCII.GetString(response).TrimEnd('\0');
                if (recv == ConfigurationManager.AppSettings["SuccessMessage"])
                {
                    string request = gItem.GameName + "-" + pwd;
                    tcp.Client.Send(Encoding.ASCII.GetBytes(request));

                    tcp.Client.Receive(response);

                    if (Encoding.ASCII.GetString(response).TrimEnd('\0') != ConfigurationManager.AppSettings["SuccessMessage"]) return false;

                    _view.DisplayMessage("You joined the game!");
                    _joined = true;
                    _me = 2;

                    Thread listener = new Thread(new ThreadStart(ListenMessagesThread));
                    listener.IsBackground = true;
                    listener.Start();

                    Thread gc = new Thread(new ThreadStart(GameClient));
                    gc.IsBackground = true;
                    gc.Start();
                    return true;
                }
            }
            catch (Exception ex)
            {
                EndGame();
            }

            return false;
        }

        private void GameClient()
        {
            byte[] data = new byte[1024];
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, int.Parse(ConfigurationManager.AppSettings["ClientListenerMovePort"]));
     
            _serverClient = new TcpClient();
            try
            {
                _serverClient.Client.Bind(ipep);
                _serverClient.Client.Listen(100);
            }
            catch(Exception ex)
            {
                EndGame();
            }


            while (true)
            {
                try
                {
                    Socket sock = _serverClient.Client.Accept();
                    sock.Receive(data);
                    sock.Close();
                }
                catch (Exception ex)
                {

                    EndGame();
                }
                string message = Encoding.ASCII.GetString(data).TrimEnd('\0');
                if (message == ConfigurationManager.AppSettings["SuccessMessage"]) continue;
                _model.SetSelected(int.Parse(message[0].ToString()), int.Parse(message[1].ToString()));
                _selected = true;
                _recv = true;
                FieldClicked(int.Parse(message[2].ToString()), int.Parse(message[3].ToString()));
                _recv = false;
            }
        }

        public Point GetSelectedCoordinates()
        {
            return _model.GetSelectedCoordinates();
        }

        public Board.Field[,] GetTableFields()
        {
            return _model.GetTable();
        }

        public List<Point> GetAvailableFieldsCoordinates(int x, int y)
        {
            List<Point> fields = new List<Point>();
            for (int i = -2; i < 3; i++)
            {
                for (int j = -2; j < 3; j++)
                {
                    int newX = x + i;
                    int newY = y + j;
                    var f = _model.GetField(newX, newY);
                    if (f == Board.Field.Free)
                        fields.Add(new Point(newX, newY));
                }
            }
            return fields;
        }

        public void FieldClicked(int x, int y)
        {
            if (!_joined) return;
            if (_over) return;
            if (!_canPlay && !_recv) return;

            Board.Field clickedField = _model.GetField(x, y);

            if (clickedField == (Board.Field)_me)
            {
                _selected = true;
                _model.SetSelected(x, y);
            }
            else if (clickedField == Board.Field.Free)
            {
                if (!_selected) return;
                _canPlay = !_canPlay;
                if (!_recv)
                {
                    IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(ConfigurationManager.AppSettings["ServerIPAddress"]), int.Parse(ConfigurationManager.AppSettings["ServerListenerMovePort"]));
                    TcpClient tcp = new TcpClient();
                    try
                    {
                        tcp.Client.Connect(ipep);
                        tcp.Client.Send(Encoding.ASCII.GetBytes(_model.GetSelectedCoordinates().X + "" + _model.GetSelectedCoordinates().Y + "" + x + "" + y));
                        tcp.Close();
                    }
                    catch (Exception ex)
                    {
                        EndGame();
                    }
                }

                var coords = _model.GetSelectedCoordinates();
                int x2 = coords.X;
                int y2 = coords.Y;

                if (Math.Abs(x2 - x) > 2 || Math.Abs(y2 - y) > 2) return;

                _model.SetField(x, y);

                _model.SetSelected(-1, -1);
                _selected = false;

                if (Math.Abs(x2 - x) == 2 || Math.Abs(y2 - y) == 2)
                {
                    _model.ReleaseField(x2, y2);
                }

                Conversion(x, y);

                _model.NextPlayer();

                int res = CheckGameOver();
                if (_over)
                {
                    if (res == 1)
                    {
                        FormMessage fm = new FormMessage("Winner: Player 1!  Resault: " + _model.GetPointsPlayer1() + ":" + _model.GetPointsPlayer2());
                        fm.Opacity = 0;
                        fm.ShowDialog();
                    }
                    if (res == 2)
                    {
                        FormMessage fm = new FormMessage("Winner: Player 2!  Resault: " + _model.GetPointsPlayer1() + ":" + _model.GetPointsPlayer2());
                        fm.Opacity = 0;
                        fm.ShowDialog();
                    }
                    if (res == 0)
                    {
                        FormMessage fm = new FormMessage("Draw!");
                        fm.Opacity = 0;
                        fm.ShowDialog();
                    }
                }
            }


            _view.Redraw();
        }
        #endregion

        #region Methods

        private List<Point> GetSurroundingFields(int x, int y)
        {
            List<Point> fields = new List<Point>();
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    int newX = x + i;
                    int newY = y + j;
                    if (newX > -1 && newX < 8 && newY > -1 && newY < 8)
                        fields.Add(new Point(newX, newY));
                }
            }
            return fields;
        }

        private int CheckGameOver()
        {
            int c1 = 0; //broj mogucih poteza za p1
            int c2 = 0; //broj mogucih poteza za p2

            int p1 = 0; //broj polja za p1
            int p2 = 0; //broj polja za p2

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    var listMoves = GetAvailableFieldsCoordinates(i, j);
                    if (_model.GetField(i, j) == Board.Field.Player1)
                    {
                        c1 += listMoves.Count;
                        p1++;
                    }
                    if (_model.GetField(i, j) == Board.Field.Player2)
                    {
                        c2 += listMoves.Count;
                        p2++;
                    }
                }
            }

            if (c1 == 0)
            {
                p2 = 64 - p1;
                _over = true;
            }
            if (c2 == 0)
            {
                p1 = 64 - p2;
                _over = true;
            }

            _model.SetPointsPlayer1(p1);
            _model.SetPointsPlayer2(p2);

            _view.UpdatePoints(p1, p2);
            _view.UpdatePlayerTurn(_model.GetPlayer());

            if (p1 > p2) return 1;
            if (p1 < p2) return 2;

            return 0;
        }

        private void Conversion(int x, int y)
        {
            var surrounding = GetSurroundingFields(x, y);
            int player = _model.GetPlayer();

            foreach (var point in surrounding)
            {
                var f = _model.GetField(point.X, point.Y);
                if (f != 0 && (int)f != player)
                {
                    _model.SetField(point.X, point.Y);
                }
            }
        }

        #endregion

        #region Internet 

        private IPAddress GetWorkingIP()
        {
            IPAddress gw = NetworkInterface
                .GetAllNetworkInterfaces()
                .Where(n => n.OperationalStatus == OperationalStatus.Up)
                .Where(n => n.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                .SelectMany(n => n.GetIPProperties()?.GatewayAddresses)
                .Where(n => n.Address.AddressFamily == AddressFamily.InterNetwork)
                .Select(g => g?.Address)
                .FirstOrDefault(a => a != null);

            List<NetworkInterface> interfejsi = NetworkInterface
                .GetAllNetworkInterfaces()
                .Where(n => n.OperationalStatus == OperationalStatus.Up)
                .Where(n => n.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                .ToList();

            foreach (var n in interfejsi)
            {
                List<GatewayIPAddressInformation> gws = n.GetIPProperties().GatewayAddresses.ToList();
                foreach (var z in gws)
                {
                    if (z.Address.Equals(gw))
                    {
                        return n.GetIPProperties().UnicastAddresses.ToList()[1].Address;
                    }
                }
            }

            return null;
        }

        private void ListenMessagesThread()
        {
            IPEndPoint ipep2 = new IPEndPoint(IPAddress.Any, int.Parse(ConfigurationManager.AppSettings["InternetMessageSenderPort"]));
            Socket newsocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                newsocket.Bind(ipep2);
                newsocket.Listen(100);
            }
            catch (Exception ex)
            {
                EndGame();
            }

            byte[] data = new byte[1024];
            while (true)
            {
                try
                {
                    _listenSocket = newsocket.Accept();
                    _listenSocket.Receive(data);
                    string message = Encoding.ASCII.GetString(data).TrimEnd('\0');
                    _listenSocket.Close();
                    data = new byte[1024];
                    if (message == ConfigurationManager.AppSettings["PlayerJoinedMessage"])
                    {
                        _joined = true;
                        _view.DisplayMessage("Player 2 joined!");
                        continue;
                    }
                    else if (message == ConfigurationManager.AppSettings["GameOverMessage"])
                    {
                        EndGame();
                        continue;
                    }
                    _view.DisplayMessage("Player " + (_me ^ 11 - 8) + ": " + message);
                }
                catch (Exception ex)
                {
                    EndGame();
                }            
            }
        }

        public void EndGame()
        {
            SendMessage(ConfigurationManager.AppSettings["GameOverMessage"]);
            FormMessage fm = new FormMessage("Game ended!");
            Application.Restart();
        }

        #endregion
    }
}
