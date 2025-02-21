using System.Net.Sockets;

namespace GuessTheNameServer.ServerCore
{
    public class Player : IDisposable
    {
        public TcpClient Client { get; }
        public string Name { get; set; } = null!;
        public StreamWriter Writer { get; }
        public StreamReader Reader { get; }
        private bool _disposed = false;
        //bool inGame = false;  

        public Player(TcpClient client)
        {
            Client = client;
            Writer = new StreamWriter(client.GetStream());
            Reader = new StreamReader(client.GetStream());
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