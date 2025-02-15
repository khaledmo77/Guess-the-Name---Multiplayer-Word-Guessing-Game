using System.IO;

namespace GuessTheNameServer.ServerCore
{
    public static class GameLogic
    {
        public static string GetRandomWord(string category)
        {
            var path = Path.Combine("Data", "Categories", $"{category}.txt");
            var words = File.ReadAllLines(path);
            return words[new Random().Next(words.Length)].ToUpper();
        }
    }
}