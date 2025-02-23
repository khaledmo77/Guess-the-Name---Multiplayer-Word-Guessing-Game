using System.Net.Sockets;
using GuessTheNameServer.Utilities;
using System.Numerics;
using Shared.ProtocolModels;
using System.Collections.Specialized;
using System.Threading.Tasks.Dataflow;
using Newtonsoft.Json;

namespace GuessTheNameServer.ServerCore
{
    public class Player : IDisposable
    {
        public event Action<string> Guess;
        public TcpClient Client { get; }
        public string Name { get; set; } = null!;
        public StreamWriter Writer { get; }
        public StreamReader Reader { get; }
        public string state { get; set; }
        public int RoomIndex { get; set; }

        private bool _disposed = false;

        public Player(TcpClient client)
        {
            Client = client;
            Writer = new StreamWriter(client.GetStream());
            Reader = new StreamReader(client.GetStream());
            Writer.AutoFlush = true;
        }
        public void SendGuess(string letter)
        {
            if (Guess != null)
            {
                Guess(letter);
            }
        }

        public bool ListenToJoinRequest()
        {
            var message = Reader.ReadLine();
            if (string.IsNullOrEmpty(message))
                return false;
            var ressponse = JsonConvert.DeserializeObject<GameCommand>(message);
            if (ressponse?.Action == "ACCEPT")
            {
                state = "Playing";
                return true;
            }
            else
            {
                return false;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Writer.Dispose();
                    Reader.Dispose();
                    Client.Dispose();
                }
                _disposed = true;
            }
        }
    }
}