using GuessTheNameServer.Networking;
using GuessTheNameServer.Utilities;
using Shared.ProtocolModels;
namespace GuessTheNameServer.ServerCore
{
    public class GameServer
    {
        private readonly ServerNetwork _network;
        private static readonly RoomManager _roomManager = new();

        public GameServer() =>
            _network = new ServerNetwork(Config.Port, _roomManager);

        public void Start()
        {
            _network.StartListening();
            Logger.Log($"Server started on port {Config.Port}");
        }
    }
}