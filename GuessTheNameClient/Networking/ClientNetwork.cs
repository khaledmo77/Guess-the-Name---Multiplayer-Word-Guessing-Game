using System.Net.Sockets;
using Newtonsoft.Json;
using Shared.ProtocolModels;

namespace GuessTheNameClient.Networking
{
    public class ClientNetwork
    {
        private TcpClient? _client;
        private StreamReader? _reader;
        private StreamWriter? _writer;

        public bool IsConnected => _client?.Connected == true && _writer != null;
<<<<<<< HEAD
<<<<<<< HEAD
=======

>>>>>>> 83cfaf253fc697a51ae00ec4b583e8176d14411f
=======

>>>>>>> 405cf6c7783d56e19bb324c14c224a4283a0d1c2
        public async Task TestSerializationAsync()
        {
            if (!IsConnected)
                throw new InvalidOperationException("Not connected to server");

            try
            {
                var testCommand = new GameCommand
                {
                    Action = "TEST_SERIALIZATION",
                    Data = "Hello Server!"
                };

                await _writer!.WriteLineAsync(JsonConvert.SerializeObject(testCommand));
                await _writer.FlushAsync();
            }
            catch
            {
                throw new InvalidOperationException("Failed to send test command");
            }
        }
<<<<<<< HEAD
<<<<<<< HEAD
=======

>>>>>>> 83cfaf253fc697a51ae00ec4b583e8176d14411f
=======

>>>>>>> 405cf6c7783d56e19bb324c14c224a4283a0d1c2
        public async Task ConnectAsync(string ip, int port = 8888)
        {
            try
            {
                _client = new TcpClient();
                await _client.ConnectAsync(ip, port);
                _reader = new StreamReader(_client.GetStream());
                _writer = new StreamWriter(_client.GetStream());
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Connection failed", ex);
            }
        }

        public async Task SendCommandAsync(GameCommand command)
        {
            if (!IsConnected || _writer == null)
                throw new InvalidOperationException("Not connected to server");

            await _writer.WriteLineAsync(JsonConvert.SerializeObject(command));
            await _writer.FlushAsync();
        }
    }
}