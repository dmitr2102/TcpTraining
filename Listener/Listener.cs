using System.Net;
using System.Net.Sockets;

namespace ListenerNamespace
{
    public class Listener
    {
        public void Start(){
            TcpListener server = null;
            // Set the TcpListener on port 13000.
            int port = 13000;
            IPAddress localAddr = IPAddress.Parse("127.0.0.1");

            // TcpListener server = new TcpListener(port);
            server = new TcpListener(localAddr, port);

            // Start listening for client requests.
            server.Start();
        }
    }
}