using GuessTheNameServer.ServerCore;
using GuessTheNameServer.Utilities;

namespace GuessTheNameServer
{
    class Program
    {
        static void Main()
        {
            Logger.Log("Starting server...");
            try
            {
                new GameServer().Start();
                Logger.Log("Server ready for connections");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Logger.Log($"Critical error: {ex.Message}");
                Logger.Log($"Stack trace: {ex.StackTrace}");
            }
        }
    }
}