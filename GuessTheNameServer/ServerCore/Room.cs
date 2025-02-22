using System.Numerics;
using System.Text;
using System.Text.Json;
using GuessTheNameServer.Utilities;
using Newtonsoft.Json;
using Shared.ProtocolModels;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace GuessTheNameServer.ServerCore
{
    public class Room
    {
        public string Category { get; set; } = null!;
        public Player[] Players { get; set; } = new Player[2];
        public StringBuilder SecretWord { get; set; }
        public List<char> GuessedLetters { get; } = new();
        public bool IsGameActive { get; set; }
        public Player? CurrentPlayer { get; set; }
        public int CurrentPlayerIndex { get; set; }

        private int NumberOfPlayers = 0;

        public List<Player> Watchers { get; set; } = new();
        public Room(Player player, string category)
        {
            Players[0] = player;
            Category = category;
            SecretWord = new StringBuilder(GameLogic.GetRandomWord(category));
            GuessedLetters = Enumerable.Repeat('_', SecretWord.Length).ToList();
            player.Guess += CheckTheGuessing;
            CurrentPlayer = Players[0];
            NumberOfPlayers++;
            SendDataToOne(player);
        }
        
        public GameCommand PrepareCommand()
        {
            var gameData = new
            {
                Player1 = this.Players[0].Name,
                Player2 = this.Players[1]?.Name,
                CurrentPlayer = this.CurrentPlayer?.Name,
                GuessedLetters = this.GuessedLetters
            };
            var data = JsonConvert.SerializeObject(gameData);
            var command = new GameCommand
            {
                Action = "CURRENT_GAME",
                Data = data
            };
            return command;
        }
        public void SendDataToAll()
        {
            var command = PrepareCommand();
            foreach (var player in Players)
            {
                player?.Writer.WriteLine(JsonConvert.SerializeObject(command));
            }
            foreach (var watcher in Watchers)
            {
                watcher?.Writer.WriteLine(JsonConvert.SerializeObject(command));
            }
        }
        public void SendDataToOne(Player player)
        {
            var command = PrepareCommand();
            player.Writer.WriteLine(JsonConvert.SerializeObject(command));
        }

        public void Join(Player player)
        {
            if (NumberOfPlayers < 2)
            {
                Players[1] = player;
                NumberOfPlayers++;
                IsGameActive = true;
                player.Guess += CheckTheGuessing;
                SendDataToAll();
            }
            else
            {
                var command = new GameCommand
                {
                    Action = "CAN_NOT_JOIN",
                    Data = "The room is full"
                };
                player.Writer.WriteLine(JsonConvert.SerializeObject(command));
            }
        }
        public void Watch(Player watcher)
        {
            Watchers.Add(watcher);
            PrepareCommand();
            SendDataToOne(watcher);
        }

        public void CheckTheGuessing(string letter)
        {
            int index = GameLogic.CheckLetter(SecretWord, char.ToUpper(letter[0]));
            string _state = "playing";

            if (index >= 0)
            {
                GuessedLetters[index] = letter[0];
                Console.WriteLine($"{string.Join(" ", GuessedLetters)}");
                if (GuessedLetters.Contains('_'))
                    _state = "Finished";
            }
            else
            {
                Console.WriteLine("wrong guess");
                CurrentPlayerIndex = (CurrentPlayerIndex + 1) % 2;
                CurrentPlayer = Players[CurrentPlayerIndex];
            }
            GameState state = new GameState { RevealedLetters = GuessedLetters, CurrentPlayer = this.CurrentPlayer.Name, State = _state };
            foreach (var player in Players)
            {
                player?.Writer.WriteLine(JsonConvert.SerializeObject(state));
            }
            foreach (var watcher in Watchers)
            {
                watcher?.Writer.WriteLine(JsonConvert.SerializeObject(state));
            }
        }

    }
}