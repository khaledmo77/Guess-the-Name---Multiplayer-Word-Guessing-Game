using Shared.ProtocolModels;

namespace GuessTheNameServer.ServerCore
{
    public class RoomManager
    {
        private readonly List<Room> _rooms = new();
        private readonly object _lock = new();

        public void AddPlayer(Player player)
        {
            lock (_lock)
            {
                // Add player to available room or create new
            }
        }

        // In GuessTheNameServer/ServerCore/RoomManager.cs
        public void ProcessCommand(Player player, GameCommand? command)
        {
            if (command == null) return;

            switch (command.Action)
            {
                case "CREATE_ROOM":
                    if (!string.IsNullOrEmpty(command.Data))
                        CreateRoom(player, command.Data);
                    break;
                    // Add other commands
            }
        }

        private void CreateRoom(Player player, string category)
        {
            lock (_lock)
            {
                var room = new Room
                {
                    Category = category,
                    Player1 = player,
                    SecretWord = GameLogic.GetRandomWord(category)
                };
                _rooms.Add(room);
            }
        }
    }
}