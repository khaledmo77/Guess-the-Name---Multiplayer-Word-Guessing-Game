using Shared.ProtocolModels;
namespace GuessTheNameServer.ServerCore
{
    public class Room
    {
        public string Category { get; set; } = null!;
        public Player Player1 { get; set; } = null!;
        public Player? Player2 { get; set; }
        public string SecretWord { get; set; } = null!;
        public List<char> GuessedLetters { get; } = new();
        public bool IsGameActive { get; set; }
        public Player? CurrentPlayer { get; set; }
    }
}