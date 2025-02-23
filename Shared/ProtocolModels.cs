using Newtonsoft.Json;

namespace Shared.ProtocolModels
{
    public class GameCommand
    {
        [JsonProperty("action")]
        public string Action { get; set; } = null!;

        [JsonProperty("data")]
        public string Data { get; set; } = null!;

        public static GameCommand Parse(string raw) =>
            JsonConvert.DeserializeObject<GameCommand>(raw)!;
    }

    public class GameState
    {
        [JsonProperty("revealed")]
        public List<char> RevealedLetters { get; set; } = null!;

        [JsonProperty("currentPlayer")]
        public string CurrentPlayer { get; set; } = null!;

    }
}