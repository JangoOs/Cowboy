﻿using System;
using System.Text;
using Cowboy.Logging.NLogIntegration;

namespace Cowboy.Sockets.TestServer
{
    class Program
    {
        static TcpSocketServer _server;

        static void Main(string[] args)
        {
            NLogLogger.Use();

            StartServer();

            Console.WriteLine("TCP server has been started.");
            Console.WriteLine("Type something to send to client...");
            while (true)
            {
                try
                {
                    string text = Console.ReadLine();
                    _server.SendToAll(Encoding.UTF8.GetBytes(text));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private static void StartServer()
        {
            _server = new TcpSocketServer(22222);
            _server.ClientConnected += server_ClientConnected;
            _server.ClientDisconnected += server_ClientDisconnected;
            _server.ClientDataReceived += server_ClientDataReceived;
            _server.Start();
        }

        static void server_ClientConnected(object sender, TcpClientConnectedEventArgs e)
        {
            Console.WriteLine(string.Format("TCP client {0} has connected.", e.Session.RemoteEndPoint.ToString()));
        }

        static void server_ClientDisconnected(object sender, TcpClientDisconnectedEventArgs e)
        {
            Console.WriteLine(string.Format("TCP client {0} has disconnected.", e.Session.RemoteEndPoint.ToString()));
        }

        static void server_ClientDataReceived(object sender, TcpClientDataReceivedEventArgs e)
        {
            var text = Encoding.UTF8.GetString(e.Data, e.DataOffset, e.DataLength);
            Console.Write(string.Format("Client : {0} --> ", e.Session.RemoteEndPoint.ToString()));
            Console.WriteLine(string.Format("{0}", text));
            _server.SendToAll(Encoding.UTF8.GetBytes(text));
        }
    }
}