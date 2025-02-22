
using System.ComponentModel;
using GuessTheNameServer.Utilities;
using Newtonsoft.Json;
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

        private void SendToPlayer(Player player, GameCommand command)
        {
            try
            {
                if (player.Client.Connected && player.Writer != null)
                {
                    player.Writer.WriteLine(JsonConvert.SerializeObject(command));
                    player.Writer.Flush();
                }
            }
            catch (Exception ex)
            {
                Logger.Log($"Error sending to player {player.Name}: {ex.Message}");
            }
        }


        // In GuessTheNameServer/ServerCore/RoomManager.cs

        public void Login(Player player, string name)
        {
            player.Name = name;

            //send list of existing room
            if (_rooms.Count != 0)
            {
                List<object> existingRooms = new();
                foreach (Room room in _rooms)
                {
                    var roomData = new
                    {
                        Player1 = room.Players[0].Name,
                        Player2 = room.Players[1]?.Name,
                        Category = room.Category
                    };
                    existingRooms.Add(roomData);
                }
                var roomsData = JsonConvert.SerializeObject(existingRooms);
                SendToPlayer(player, new GameCommand
                {
                    Action = "EXISTING_ROOMS",
                    Data = roomsData
                });
            }
        }


        public void CreateRoom(Player player, string category)
        {
            var room = new Room(player, category);
            _rooms.Add(room);
        }
        public void JoinRoom(Player player, int index)
        {
            _rooms[index].Join(player);
        }
        public void Watch(Player watcher, int index)
        {
            _rooms[index].Watch(watcher);
        }

    }
}