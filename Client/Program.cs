﻿using System;
using System.Net.Sockets;
using ClientClassNamespace;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            //Connect("127.0.0.1", "String message");

            ClientClass client = new ClientClass("127.0.0.1", 13000);
            client.Connect(new TcpClient());
            client.OnMessageRecevd += (message)=> {};
            client.SendMessage("Hello world!");
            Console.ReadLine();
            client.Disconnect();
            Console.ReadLine();
        }

        // Functions:
        // Connect()
        // SendMessage()
        // (priv) StartListening()
        // + OnMessageReceived
        // StopListening()
        // Disconnect()

        static void Connect(String server, String message)
        {
            try
            {
                #region sConnect
                // Create a TcpClient.
                // Note, for this client to work you need to have a TcpServer
                // connected to the same address as specified by the server, port
                // combination.
                Int32 port = 13000;
                TcpClient client = new TcpClient(server, port);

                // Get a client stream for reading and writing.
                //  Stream stream = client.GetStream();

                NetworkStream stream = client.GetStream();
                #endregion Connect

                #region SendMessage
                // Translate the passed message into ASCII and store it as a Byte array.
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

                // Send the message to the connected TcpServer.
                stream.Write(data, 0, data.Length);

                Console.WriteLine("Sent: {0}", message);
                #endregion SendMessage

                // Receive the TcpServer.response.

                // Buffer to store the response bytes.
                data = new Byte[256];

                // String to store the response ASCII representation.
                String responseData = String.Empty;

                // Read the first batch of the TcpServer response bytes.
                Int32 bytes = stream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                Console.WriteLine("Received: {0}", responseData);

                // Close everything.
                stream.Close();
                client.Close();
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }

            Console.WriteLine("\n Press Enter to continue...");
            Console.Read();
        }
    }
}