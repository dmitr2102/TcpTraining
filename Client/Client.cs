using System.IO;
using System;
using System.Net.Sockets;
using System.Threading;

namespace ClientClassNamespace
{
    public class ClientClass 
    {
        private readonly string _serverAddress;
        private readonly int _port;
        private NetworkStream _stream;
        private Thread _listneningThread;

        private bool _isListening = false;
        private TcpClient _client;

        public ClientClass(string serverAddress, int port)
        {
            _serverAddress = serverAddress;
            _port = port;
        }

        public void Connect()
        {
            TcpClient client = new TcpClient(_serverAddress, _port);
            _stream = client.GetStream();
            StartListening();
        }

        public void SendMessage(string message)
        {
            Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
            _stream.Write(data, 0, data.Length);
        }

        public event Action<string> OnMessageRecevd;

        private void StartListening(){
            _isListening = true;
            _listneningThread = new Thread(() => 
            {
                //todo fix infinite loop
                while(true)
                {
                    byte[] data = new byte[256];
                    Int32 bytes = _stream.Read(data, 0, data.Length);
                    //event
                    string message = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                    OnMessageRecevd?.Invoke(message);
                }
            });

            _listneningThread.Start();
        }

        private void StopListen(){
            _isListening = false;
        }

        public void Disconnect(){
            StopListen();
            _stream.Close();
            _listneningThread.Abort();
            _client.Close();
        }
    }
}