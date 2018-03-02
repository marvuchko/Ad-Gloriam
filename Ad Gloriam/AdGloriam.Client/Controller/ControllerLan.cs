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

namespace Ad_Gloriam.Controller
{
    class ControllerLan : IController
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
        private TcpClient _sendClient;
        private Socket _listenSocket;
        private Socket _gameServerSock;
        private UdpClient _gameListenerUdpClient;
        private int _me;
        private bool _canPlay = false;
        private bool _recv = false;
        private bool _ended = false;
        #endregion

        #region Threads
        private Thread gl;
        #endregion

        #region Constructors
        public ControllerLan(IModel model, IView view)
        {
            this._model = model;
            this._view = view;
        }
        #endregion        

        #region Members
        public void SendMessage(string message)
        {
            if (!_joined) return;
            _view.DisplayMessage("Player " + _me + ": " + message);
            try
            {
                _sendClient.Client.Send(Encoding.ASCII.GetBytes(message));
            }

            catch(Exception ex)
            {
                EndGame();
                return;
            }
        }

        public void CreateGame()
        {
            _canPlay = true;
            _view.DisplayMessage("Waiting for player 2...");
            Thread gs = new Thread(new ThreadStart(GameServer));
            gs.IsBackground = true;
            gs.Start();
            gl = new Thread(new ThreadStart(GameListener));
            gl.IsBackground = true;
            gl.Start();
        }

        public List<GameItem> FindGames()
        {
            _found = new List<GameItem>();
            Thread gs = new Thread(new ThreadStart(GameSearcher));
            gs.IsBackground = true;
            gs.Start();
            Thread.Sleep(3000);

            _found = _found.GroupBy(i => i.IpAddress).Select(g => g.First()).ToList();
            return _found;
        }

        public bool JoinGame(GameItem gItem, string pwd)
        {
            _gItem = gItem;
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(gItem.IpAddress), int.Parse(ConfigurationManager.AppSettings["GameServerPort"]));
            TcpClient tcp = new TcpClient();
            try
            {
                tcp.Connect(ipep);
            }
            catch(Exception)
            {
                EndGame();
            }
            string message = ConfigurationManager.AppSettings["JoinRequestMessage"] + "-" + pwd;
            _server = tcp;
            _view.DisplayMessage("Connecting...");
            try
            {
                tcp.Client.Send(Encoding.ASCII.GetBytes(message));
            }

            catch (Exception ex)
            {
                EndGame();
                return false;
            }
            

            byte[] response = new byte[1024];

            try
            {
                tcp.Client.Receive(response);
            }
            catch (Exception ex)
            {
                EndGame();
                return false;
            }
            

            if (Encoding.ASCII.GetString(response).TrimEnd('\0') == ConfigurationManager.AppSettings["SuccessMessage"])
            {
                _joined = true;
                _me = 2;

                var ip = (_server.Client.RemoteEndPoint as IPEndPoint).Address.ToString();

                //IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ip), int.Parse(ConfigurationManager.AppSettings["MessageListenerPort"]));
                _sendClient = new TcpClient();
                var result = _sendClient.BeginConnect(ip, int.Parse(ConfigurationManager.AppSettings["MessageListenerPort"]), null, null);
                result.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(5));
                if(!_sendClient.Connected)
                {
                    throw new Exception("Client not connected!");
                }

                IPEndPoint ipep2 = new IPEndPoint(IPAddress.Any, int.Parse(ConfigurationManager.AppSettings["MessageSenderPort"]));
                Socket newsocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                newsocket.Bind(ipep2);
                newsocket.Listen(int.Parse(ConfigurationManager.AppSettings["MessageSenderPort"]));
                _listenSocket = newsocket.Accept();

                Thread listener = new Thread(new ThreadStart(ListenMessagesThread));
                listener.IsBackground = true;
                listener.Start();

                Thread gc = new Thread(new ThreadStart(GameClient));
                gc.IsBackground = true;
                gc.Start();
                return true;
            }

            return false;
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
                if(!_recv)
                {
                    try
                    {
                        if (_me == 1) _client.Send(Encoding.ASCII.GetBytes(_model.GetSelectedCoordinates().X + "" + _model.GetSelectedCoordinates().Y + "" + x + "" + y));
                        else _server.Client.Send(Encoding.ASCII.GetBytes(_model.GetSelectedCoordinates().X + "" + _model.GetSelectedCoordinates().Y + "" + x + "" + y));
                    }

                    catch (Exception ex)
                    {
                        EndGame();
                        return;
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
                        FormMessage fm = new FormMessage("Winner: Player 1!  Result: " + _model.GetPointsPlayer1() + ":" + _model.GetPointsPlayer2());
                        fm.Opacity = 0;
                        fm.ShowDialog();
                    }
                    if (res == 2)
                    {
                        FormMessage fm = new FormMessage("Winner: Player 2!  Result: " + _model.GetPointsPlayer1() + ":" + _model.GetPointsPlayer2());
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

            foreach (var point in surrounding)
            {
                var f = _model.GetField(point.X, point.Y);
                if (f != 0 && (int)f != _model.GetPlayer())
                {
                    _model.SetField(point.X, point.Y);
                }
            }
        }

        #endregion

        #region Lan 
        private void GameServer()
        {
            IPEndPoint _ipEndPoint = new IPEndPoint(IPAddress.Any, int.Parse(ConfigurationManager.AppSettings["GameServerPort"]));
            _gameServerSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                _gameServerSock.Bind(_ipEndPoint);
                _gameServerSock.Listen(100);
            }
            catch (Exception)
            {
                EndGame();
            }

            byte[] data = new byte[1024];
            while (true)
            {
                try
                {
                    _client = _gameServerSock.Accept();
                    _client.Receive(data);
                }
                catch (Exception ex)
                {
                    _client.Close();
                    continue;
                }
                string[] message = Encoding.ASCII.GetString(data).TrimEnd('\0').Split('-');

                if (message[0] == ConfigurationManager.AppSettings["JoinRequestMessage"] && message[1] == _model.GetGame().Password)
                {
                    try
                    {
                        _client.Send(Encoding.ASCII.GetBytes(ConfigurationManager.AppSettings["SuccessMessage"]));
                    }

                    catch (Exception ex)
                    {
                        _client.Close();
                        continue;
                    }

                    _view.DisplayMessage("Player 2 connected!");
                    _joined = true;
                    _me = 1;
                    gl.Suspend();
                    var ip = (_client.RemoteEndPoint as IPEndPoint).Address.ToString();

                    IPEndPoint ipep = new IPEndPoint(IPAddress.Any, int.Parse(ConfigurationManager.AppSettings["MessageListenerPort"]));
                    Socket newsocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    newsocket.Bind(ipep);
                    newsocket.Listen(100);
                    _listenSocket = newsocket.Accept();

                    Thread listener = new Thread(new ThreadStart(ListenMessagesThread));
                    listener.IsBackground = true;
                    listener.Start();


                    IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ip), int.Parse(ConfigurationManager.AppSettings["MessageSenderPort"]));
                    _sendClient = new TcpClient();
                    var result = _sendClient.BeginConnect(ip, int.Parse(ConfigurationManager.AppSettings["MessageSenderPort"]), null, null);
                    result.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(5));
                    if (!_sendClient.Connected)
                    {
                        EndGame();
                    }

                    while (true)
                    {
                        try
                        {
                            _client.Receive(data);
                        }
                        catch (Exception ex)
                        {
                            EndGame();
                        }

                        string tmp = Encoding.ASCII.GetString(data).TrimEnd('\0');
                        _model.SetSelected(int.Parse(tmp[0].ToString()), int.Parse(tmp[1].ToString()));
                        _selected = true;
                        _recv = true;
                        FieldClicked(int.Parse(tmp[2].ToString()), int.Parse(tmp[3].ToString()));
                        _recv = false;
                    }
                }
            }

        }

        private void GameListener()
        {
            byte[] data = new byte[1024];
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, int.Parse(ConfigurationManager.AppSettings["BroadcastPort"]));
            try
            {
                _gameListenerUdpClient = new UdpClient(ipep);
            }
            catch (Exception)
            {
                EndGame();
            }
            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);

            while (!_joined)
            {
                try
                {
                    data = _gameListenerUdpClient.Receive(ref sender);
                }
                catch (Exception ex)
                {
                    continue;
                }
                
                string recv = Encoding.ASCII.GetString(data);
                if (recv == ConfigurationManager.AppSettings["CheckIfHostMessage"])
                {
                    var game = _model.GetGame();
                    string message = game.GameName + " " + GetWorkingIP() + " " + (game.Password != "");
                    try
                    {
                        _gameListenerUdpClient.Send(Encoding.ASCII.GetBytes(message), message.Length, sender);
                    }

                    catch (Exception ex)
                    {
                        continue;
                    }
                    
                }
            }
        }

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

        private void GameSearcher()
        {
            byte[] recv = new byte[1024];
            IPAddress tmp = GetWorkingIP();
            UdpClient client = new UdpClient(new IPEndPoint(tmp, 0));
            IPEndPoint ip = new IPEndPoint(IPAddress.Broadcast, int.Parse(ConfigurationManager.AppSettings["BroadcastPort"]));
            byte[] bytes = Encoding.ASCII.GetBytes(ConfigurationManager.AppSettings["CheckIfHostMessage"]);
            try
            {
                client.Send(bytes, bytes.Length, ip);
            }

            catch (Exception ex)
            {
                EndGame();
                return;
            }
            

            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);

            while (true)
            {
                try
                {
                    recv = client.Receive(ref sender);
                }
                catch (Exception ex)
                {
                    EndGame();
                    return;
                }
                
                string[] data = Encoding.ASCII.GetString(recv).Split(' ');
                if (data.Length != 3) continue;
                GameItem gItem = new GameItem(data[0], data[1], data[2]);
                _found.Add(gItem);
            }
        }

        private void GameClient()
        {
            byte[] data = new byte[1024];

            while (true)
            {
                try
                {
                    _server.Client.Receive(data);
                }
                catch(Exception ex)
                {
                    EndGame();
                    return;
                }
                string message = Encoding.ASCII.GetString(data).TrimEnd('\0');
                data = new byte[1024];
                _model.SetSelected(int.Parse(message[0].ToString()), int.Parse(message[1].ToString()));
                _selected = true;
                _recv = true;
                FieldClicked(int.Parse(message[2].ToString()), int.Parse(message[3].ToString()));
                _recv = false;
            }
        }

        private void ListenMessagesThread()
        {
            byte[] data = new byte[1024];
            while (true)
            {
                try
                {
                    _listenSocket.Receive(data);
                }
                catch (Exception ex)
                {
                    EndGame();
                    return;
                }
                
                string message = Encoding.ASCII.GetString(data).TrimEnd('\0');
                _view.DisplayMessage("Player " + (_me ^ 11 - 8) + ": " + message);
                data = new byte[1024];
            }
        }

        public void EndGame()
        {
            if (!_ended)
            {
                FormMessage fm = new FormMessage("Game ended!");
                fm.Opacity = 0;
                fm.ShowDialog();
            }
            _ended = true;
            Application.Restart();
        }
        #endregion
    }
}
