using System.Net;
using System.Net.Sockets;
using System.Xml.Linq;
using GuessTheNameServer.ServerCore;
using GuessTheNameServer.Utilities;
using Newtonsoft.Json;
using Shared.ProtocolModels;

namespace GuessTheNameServer.Networking
{
    public class ServerNetwork
    {
        private readonly TcpListener _listener;
        private readonly RoomManager _roomManager;

        public ServerNetwork(int port, RoomManager roomManager)
        {
            _listener = new TcpListener(IPAddress.Any, port);
            _roomManager = roomManager;
        }

        public void StartListening()
        {
            _listener.Start();
            while (true)
            {
                var client = _listener.AcceptTcpClient();
                Thread t = new Thread(() => Task.Run(() => HandleClient(client)));
                t.Start();
            }

        }

        private async Task HandleClient(TcpClient client)
        {
            using var player = new Player(client);
            try
            {
                while (client.Connected)
                {
                    var message = await player.Reader.ReadLineAsync();
                    if (string.IsNullOrEmpty(message)) continue;

                    var command = JsonConvert.DeserializeObject<GameCommand>(message);

                    switch (command.Action)
                    {
                        case "LOGIN":
                            _roomManager.Login(player, command.Data);
                            break;

                        case "CREATE_ROOM":
                            if (!string.IsNullOrEmpty(command.Data))
                            {
                                _roomManager.CreateRoom(player, command.Data);
                                while (player.state == "Waiting other player") ;
                            }
                            break;
                        case "JOIN_ROOM":
                            if (!string.IsNullOrEmpty(command.Data))
                            {
                                int index = Convert.ToInt32(command.Data);
                                _roomManager.JoinRoom(player, index);
                            }
                            break;
                        case "GUESS":
                            if (!string.IsNullOrEmpty(command.Data))
                            {
                                _roomManager.CreateRoom(player, "Animals");
                                player.SendGuess(command.Data);
                            }
                            break;
                        case "WATCH":
                            if (!string.IsNullOrEmpty(command.Data))
                            {
                                int index = Convert.ToInt32(command.Data);
                                _roomManager.Watch(player, index);
                            }
                            break;
                        case "PLAY_AGAIN":
                            if (!string.IsNullOrEmpty(command.Data))
                            {
                                int index = player.RoomIndex;
                                _roomManager.NewGame(index);
                            }
                            break;
                        case "END_GAME":
                            if (!string.IsNullOrEmpty(command.Data))
                            {
                                int index = player.RoomIndex;
                                _roomManager.NewGame(index);
                            }
                            break;

                            // Add other commands
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log($"Connection error: {ex.Message}");
            }
            finally
            {

                Logger.Log($"Client disconnected: {client.Client.RemoteEndPoint}");



            }
        }
    }
}