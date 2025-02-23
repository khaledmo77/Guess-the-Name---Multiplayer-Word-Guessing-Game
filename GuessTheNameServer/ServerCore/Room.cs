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
        public List<char> GuessedLetters { get; set; } = new();
        public bool IsGameActive { get; set; }
        public Player? CurrentPlayer { get; set; }
        public int CurrentPlayerIndex { get; set; }

        public int NumberOfPlayers = 0;

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
            string command = PrepareCommand();
            SendDataToOne(player, command);
        }
        public bool AskToJoin(Player player)
        {
            var joinRequest = new GameCommand { Action = "JOIN_REQUEST", Data = $"{player.Name} want to join the game" };
            var command = JsonConvert.SerializeObject(joinRequest);
            Players[0].Writer.WriteLine(command);
            return Players[0].ListenToJoinRequest();
        }
        public string PrepareCommand()
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

            return JsonConvert.SerializeObject(command);
        }
        public void SendDataToAll(string command)
        {
            foreach (var player in Players)
            {
                player?.Writer.WriteLine(JsonConvert.SerializeObject(command));
            }
            foreach (var watcher in Watchers)
            {
                watcher?.Writer.WriteLine(JsonConvert.SerializeObject(command));
            }
        }
        public void SendDataToOne(Player player, string command)
        {
            player.Writer.WriteLine(JsonConvert.SerializeObject(command));
        }

        public void Join(Player player)
        {
            Players[1] = player;
            NumberOfPlayers++;
            IsGameActive = true;
            player.Guess += CheckTheGuessing;
            string command = PrepareCommand();
            SendDataToAll(command);
        }
        public void Watch(Player watcher)
        {
            Watchers.Add(watcher);
            string command = PrepareCommand();
            SendDataToOne(watcher, command);
        }

        public void CheckTheGuessing(string letter)
        {
            int index = GameLogic.CheckLetter(SecretWord, char.ToUpper(letter[0]));

            if (index >= 0)
            {
                GuessedLetters[index] = letter[0];
            }
            else
            {
                CurrentPlayerIndex = (CurrentPlayerIndex + 1) % 2;
                CurrentPlayer = Players[CurrentPlayerIndex];
            }
            GameState state = new() { RevealedLetters = GuessedLetters, CurrentPlayer = this.CurrentPlayer.Name };
            foreach (var player in Players)
            {
                player?.Writer.WriteLine(JsonConvert.SerializeObject(state));
            }
            foreach (var watcher in Watchers)
            {
                watcher?.Writer.WriteLine(JsonConvert.SerializeObject(state));
            }
            if (!GuessedLetters.Contains('_'))
            {
                var winnerMessage = new GameCommand
                {
                    Action = "GAME_END",
                    Data = "Congratulation!\nPlay again?"
                };
                CurrentPlayer.Writer.WriteLine(JsonConvert.SerializeObject(winnerMessage));
                var loserMessage = new GameCommand
                {
                    Action = "GAME_END",
                    Data = "Sorry!\nPlay again?"
                };
                Players[(CurrentPlayerIndex + 1) % 2].Writer.WriteLine(JsonConvert.SerializeObject(winnerMessage));
            }
        }
        public void NewGame()
        {
            SecretWord = new StringBuilder(GameLogic.GetRandomWord(Category));
            GuessedLetters.Clear();
            GuessedLetters = Enumerable.Repeat('_', SecretWord.Length).ToList();
            string command = PrepareCommand();
            SendDataToAll(command);
        }
    }
}