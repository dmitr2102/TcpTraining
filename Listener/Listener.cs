using System;
using System.Net;
using System.Net.Sockets;

namespace ListenerNamespace
{
    public class Listener
    {
        TcpListener server;
        
        public void Start(){
            server = null;
            // Set the TcpListener on port 13000.
            int port = 13000;
            IPAddress localAddr = IPAddress.Parse("127.0.0.1");

            // TcpListener server = new TcpListener(port);
            server = new TcpListener(localAddr, port);

            // Start listening for client requests.
            server.Start();
        }

        private void StartWaitingForConnnections()
        {
            // Buffer for reading data
            Byte[] bytes = new Byte[256];
            String data = null;

            // Enter the listening loop.
            while (true)
            {
                Console.Write("Waiting for a connection... ");

                // Perform a blocking call to accept requests.
                // You could also use server.AcceptSocket() here.
                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine("Connected!");
            }
        }
    }
}