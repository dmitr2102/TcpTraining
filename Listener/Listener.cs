using System;
using System.Net;
using System.Net.Sockets;

namespace ListenerNamespace
{
    public class Listener
    {
        TcpListener _server;
        
        public void Start(){
            _server = null;
            // Set the TcpListener on port 13000.
            int port = 13000;
            IPAddress localAddr = IPAddress.Parse("127.0.0.1");

            // TcpListener server = new TcpListener(port);
            _server = new TcpListener(localAddr, port);

            // Start listening for client requests.
            _server.Start();
            StartWaitingForConnnections();
        }

        private void StartWaitingForConnnections()
        {
            // Enter the listening loop.
            while (true)
            {
                Console.Write("Waiting for a connection... ");
                TcpClient client = _server.AcceptTcpClient();
                Console.WriteLine("Connected!");
            }
        }
    }
}