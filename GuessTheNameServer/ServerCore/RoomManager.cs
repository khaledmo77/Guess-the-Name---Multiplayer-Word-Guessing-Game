<<<<<<< HEAD
=======

>>>>>>> 83cfaf253fc697a51ae00ec4b583e8176d14411f
﻿using GuessTheNameServer.Utilities;
using Newtonsoft.Json;
using Shared.ProtocolModels;

<<<<<<< HEAD
=======

>>>>>>> 83cfaf253fc697a51ae00ec4b583e8176d14411f
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
<<<<<<< HEAD
=======

>>>>>>> 83cfaf253fc697a51ae00ec4b583e8176d14411f
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

<<<<<<< HEAD
=======

>>>>>>> 83cfaf253fc697a51ae00ec4b583e8176d14411f
        // In GuessTheNameServer/ServerCore/RoomManager.cs
        public void ProcessCommand(Player player, GameCommand? command)
        {
            if (command == null) return;
<<<<<<< HEAD
=======

>>>>>>> 83cfaf253fc697a51ae00ec4b583e8176d14411f
            if (command.Action == "TEST_SERIALIZATION")
            {
                Logger.Log($"Received test data: {command.Data}");
            }
            switch (command.Action)
            {
                case "TEST_SERIALIZATION":
                    Logger.Log($"Server Received: {command.Data}");
                    // Echo back to client
                    SendToPlayer(player, new GameCommand
                    {
                        Action = "TEST_RESPONSE",
                        Data = $"Echo: {command.Data}"
                    });
                    break;
<<<<<<< HEAD
=======

>>>>>>> 83cfaf253fc697a51ae00ec4b583e8176d14411f
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