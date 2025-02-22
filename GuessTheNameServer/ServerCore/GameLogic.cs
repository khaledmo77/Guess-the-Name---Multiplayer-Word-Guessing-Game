using System.IO;
using System.Text;

namespace GuessTheNameServer.ServerCore
{
    public static class GameLogic
    {
        public static string GetRandomWord(string category)
        {
            var path = Path.Combine("Models", "Categories", $"{category}.txt");
            var words = File.ReadAllLines(path);
            return words[new Random().Next(words.Length)].ToUpper();
        }
        public static int CheckLetter(StringBuilder word, char letter)
        {

            for (int i = 0; i < word.Length; i++)
            {
                if (letter == word[i])
                {
                    word[i] = '#';
                    return i;
                }
            }
            return -1;
        }
    }
}