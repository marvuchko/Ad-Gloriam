using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Configuration;

/*Todo: 
-Disconnect handling
*/
namespace AdGloriam.Server
{
    class Program
    {
        private static List<Game> _games;
        private static Thread _listenerGamesList;
        private static Thread _listenerCreateGame;
        private static Thread _listenerJoinGame;
        private static Thread _listenerGameMove;
        private static Thread _listenerMessages;
        private static DBConnect _db;

        static void Main(string[] args)
        {
            Log("Server starting");

            _games = new List<Game>();

            Log("Thread for creating games starting");
            _listenerCreateGame = new Thread(new ThreadStart(CreateGame));
            _listenerCreateGame.Start();

            Log("Thread for game creating starting");
            _listenerGamesList = new Thread(new ThreadStart(GetGames));
            _listenerGamesList.Start();

            Log("Thread for game joining starting");
            _listenerJoinGame = new Thread(new ThreadStart(JoinGame));
            _listenerJoinGame.Start();

            Log("Thread for game moves starting");
            _listenerGameMove = new Thread(new ThreadStart(SendMove));
            _listenerGameMove.Start();

            Log("Thread for messages starting");
            _listenerMessages = new Thread(new ThreadStart(ListenForMessages));
            _listenerMessages.Start();

            Log("Database service starting");
            _db = new DBConnect();


            while (_listenerCreateGame.IsAlive && _listenerGameMove.IsAlive && _listenerGamesList.IsAlive && _listenerJoinGame.IsAlive && _listenerMessages.IsAlive) { }

            Log("Server crushed!");
        }

        static void ListenForMessages()
        {
            IPEndPoint _ipEndPoint = new IPEndPoint(IPAddress.Any, int.Parse(ConfigurationManager.AppSettings["InternetMessageListenerPort"]));
            Socket newsock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                newsock.Bind(_ipEndPoint);
                newsock.Listen(100);
            }
            catch(Exception ex)
            {
                Log("Thread for messages failed: " + ex.Message);
                return;
            }


            while (true)
            {
                Socket client = newsock.Accept();
                Game game = null;
                try
                {
                    var sender = (client.RemoteEndPoint as IPEndPoint).Address;
                    Log("Server is receiving a new message from " + client.RemoteEndPoint);
                    byte[] data = new byte[1024];
                    client.Receive(data);
                    string message = Encoding.ASCII.GetString(data).TrimEnd('\0');
                
                    game = _games.First(g => g.Player1.ToString() == sender.ToString() || g.Player2.ToString() == sender.ToString());  

                    var receiver = (game.Player1.ToString() == sender.ToString()) ? game.Player2 : game.Player1;

                    IPEndPoint ipep = new IPEndPoint(receiver, int.Parse(ConfigurationManager.AppSettings["InternetMessageSenderPort"]));
                    Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    sock.Connect(ipep);
                    sock.Send(Encoding.ASCII.GetBytes(message));
                    if (message == ConfigurationManager.AppSettings["GameOverMessage"])
                    {
                        _games.Remove(game);
                        Log("Game " + game + "removed from queue");
                    }
                    sock.Close();
                    //
                }
                catch (Exception ex)
                {
                    Log(ex.ToString());
                    if(game != null)
                    {
                        _games.Remove(game);
                    }
                }
                finally
                {
                    client.Close();
                }
                
            }
        }

        static void SendMove()
        {
            IPEndPoint _ipEndPoint = new IPEndPoint(IPAddress.Any, int.Parse(ConfigurationManager.AppSettings["ServerListenerMovePort"]));
            Socket newsock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            
            try
            {
                newsock.Bind(_ipEndPoint);
                newsock.Listen(100);
            }
            catch(Exception ex)
            {
                Log("Thread for game moves failed: " + ex.Message);
                return;
            }
            

            while(true)
            {
                Socket client = newsock.Accept();
                Game game = null;
                try
                {
                    var sender = (client.RemoteEndPoint as IPEndPoint).Address;
                    Log("Server is receiving a new game move from " + client.RemoteEndPoint);
                    byte[] data = new byte[1024];
                    client.Receive(data);
                    string message = Encoding.ASCII.GetString(data).TrimEnd('\0');

                
                    game = _games.First(g => g.Player1.ToString() == sender.ToString() || g.Player2.ToString() == sender.ToString());
                    var receiver = (game.Player1.ToString() == sender.ToString()) ? game.Player2 : game.Player1;
                    Log(client.RemoteEndPoint.ToString() + " sent move: " + message);

                    Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    sock.Connect(new IPEndPoint(receiver, int.Parse(ConfigurationManager.AppSettings["ClientListenerMovePort"])));
                    sock.Send(Encoding.ASCII.GetBytes(message));

                    sock.Close();

                }
                catch(Exception ex)
                {
                    Log(ex.Message);
                    if(game != null)
                    {
                        _games.Remove(game);
                    }
                }
                finally
                {
                    client.Close();
                }
                
            }
        }

        static void JoinGame()
        {
            IPEndPoint _ipEndPoint = new IPEndPoint(IPAddress.Any, int.Parse(ConfigurationManager.AppSettings["JoinGamePort"]));
            Socket newsock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                newsock.Bind(_ipEndPoint);
                newsock.Listen(100);
            }
            catch(Exception ex)
            {
                Log("Thread for game joining failed: " + ex.Message);
                return;
            }
           

            while(true)
            {
                Socket client = newsock.Accept();
                try
                {
                    Log("Server is receiving a request to join the game from " + client.RemoteEndPoint);
                    byte[] data = new byte[1024];
                    client.Receive(data);

                    string recv = Encoding.ASCII.GetString(data).TrimEnd('\0');
                    if (recv == ConfigurationManager.AppSettings["RequestJoinMessage"])
                    {
                        client.Send(Encoding.ASCII.GetBytes(ConfigurationManager.AppSettings["SuccessMessage"]));
                        data = new byte[1024];
                        client.Receive(data);

                        string[] request = Encoding.ASCII.GetString(data).TrimEnd('\0').Split('-');
                        Log(client.RemoteEndPoint + " wants to join a game: " + request[0]);
                        var message = ConfigurationManager.AppSettings["FailMessage"];


                        var found = _games.First(g => g.GameName == request[0]);
                        if (found.Password == request[1] && !found.Joined)
                        {
                            message = ConfigurationManager.AppSettings["SuccessMessage"];
                            found.Player2 = (client.RemoteEndPoint as IPEndPoint).Address;
                            //
                            _db.Insert(found.Player1.ToString(), found.Player2.ToString(), found.GameName);
                            found.Joined = true;
                            //
                            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                            IPEndPoint receiver = new IPEndPoint(found.Player1, int.Parse(ConfigurationManager.AppSettings["InternetMessageSenderPort"]));
                            sock.Connect(receiver);
                            sock.Send(Encoding.ASCII.GetBytes(ConfigurationManager.AppSettings["PlayerJoinedMessage"]));

                            sock.Close();
                        }

                        client.Send(Encoding.ASCII.GetBytes(message));
                    }
                }
                catch (Exception ex)
                {
                    Log(ex.Message);
                }
                finally
                {
                    client.Close();
                }
                
            }
        }

        static void CreateGame()
        {
            IPEndPoint _ipEndPoint = new IPEndPoint(IPAddress.Any, int.Parse(ConfigurationManager.AppSettings["CreateGamePort"]));
            Socket newsock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                newsock.Bind(_ipEndPoint);
                newsock.Listen(100);
            }
            catch(Exception ex)
            {
                Log("Thread for game creating failed: " + ex.Message);
                return;
            } 
            

            while(true)
            {
                Socket client = newsock.Accept();
                Game game = null;
                try
                {                
                    Log("Server is receiving a new request to create a game from " + client.RemoteEndPoint);
                    byte[] data = new byte[1024];
                    client.Receive(data);

                    if(Encoding.ASCII.GetString(data).TrimEnd('\0') == ConfigurationManager.AppSettings["RequestToCreateGameMessage"])
                    {
                        data = new byte[1024];
                        client.Send(Encoding.ASCII.GetBytes(ConfigurationManager.AppSettings["SuccessMessage"]));

                        Log(client.RemoteEndPoint + " wants to create a game");
                        client.Receive(data);

                        string[] request = (Encoding.ASCII.GetString(data)).Split('-');

                        var found = _games.Where(g => g.GameName == request[0]);

                        var message = ConfigurationManager.AppSettings["FailMessage"];

                        if (found.Count() == 0)
                        {
                            game = new Game(request[0], request[1].TrimEnd('\0'), (client.RemoteEndPoint as IPEndPoint).Address);
                            _games.Add(game);

                            Log("Server created game " + game + " for " + client.RemoteEndPoint);
                            message = ConfigurationManager.AppSettings["SuccessMessage"];
                        }
                        else Log("Game " + request[0] + " is already created");

                        client.Send(Encoding.ASCII.GetBytes(message));
                    }
                }
                catch(Exception ex)
                {
                    Log(ex.Message);
                    if (game != null)
                    {
                        _games.Remove(game);
                    }
                }
                finally
                {
                    client.Close();
                }
                
            }
        }

        static void GetGames()
        {
            IPEndPoint _ipEndPoint = new IPEndPoint(IPAddress.Any, int.Parse(ConfigurationManager.AppSettings["GetGamesPort"]));
            Socket newsock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                newsock.Bind(_ipEndPoint);
                newsock.Listen(100);
            }
            catch(Exception ex)
            {
                Log("Thread for getting created games failed: " + ex.Message);
                return;
            }
            

            while(true)
            {
                Socket client = newsock.Accept();

                try
                {
                    Log("Server is receiving a list of created games from " + client.RemoteEndPoint);
                    byte[] data = new byte[1024];
                    client.Receive(data);

                    if (Encoding.ASCII.GetString(data).TrimEnd('\0') == ConfigurationManager.AppSettings["RequestGamesMessage"])
                    {
                        string response = "";
                        foreach (var game in _games)
                        {
                            if (game.Joined) continue;
                            response += game.ToString();
                            response += "|";
                        }

                        if(_games.Count > 0)
                        response = response.Substring(0, response.Length - 1);

                        client.Send(Encoding.ASCII.GetBytes(response));
                        Log("Server is sending a list of created games to " + client.RemoteEndPoint);
                    }
                }
                catch(Exception ex)
                {
                    Log(ex.Message);
                }
                finally
                {
                    client.Close();
                }
                
            }
        }

        static void Log(string message)
        {
            Console.WriteLine("[{0}] {1}", DateTime.Now.ToString(), message);
        }
    }
}
