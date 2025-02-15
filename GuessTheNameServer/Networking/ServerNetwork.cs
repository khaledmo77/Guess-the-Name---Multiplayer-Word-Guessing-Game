using System.Net;
using System.Net.Sockets;
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
                Thread t = new Thread(()=> Task.Run(() => HandleClient(client)));
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
                    if (command?.Action == "TEST_SERIALIZATION")
                    {
                        // Log received command
                        Logger.Log($"[TEST] Received: {command.Data}");

                        if (player.Writer != null)
                        {
                            var response = new GameCommand
                            {
                                Action = "TEST_RESPONSE",
                                Data = $"Received: {command.Data}"
                            };
                            await player.Writer.WriteLineAsync(JsonConvert.SerializeObject(response));
                            await player.Writer.FlushAsync();
                        }
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