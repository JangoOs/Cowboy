﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cowboy.Sockets;

namespace Cowboy.Codec.WebSocket
{
    public sealed class AsyncWebSocketSession : IAsyncTcpSocketServerMessageDispatcher
    {
        public async Task OnSessionStarted(AsyncTcpSocketSession session)
        {
            //Console.WriteLine(string.Format("TCP session {0} has connected {1}.", session.RemoteEndPoint, session));
            await Task.CompletedTask;
        }

        public async Task OnSessionDataReceived(AsyncTcpSocketSession session, byte[] data, int offset, int count)
        {
            var text = Encoding.UTF8.GetString(data, offset, count);
            //Console.Write(string.Format("Client : {0} --> ", session.RemoteEndPoint));
            //Console.WriteLine(text);

            await session.SendAsync(Encoding.UTF8.GetBytes(text));
        }

        public async Task OnSessionClosed(AsyncTcpSocketSession session)
        {
            //Console.WriteLine(string.Format("TCP session {0} has disconnected.", session));
            await Task.CompletedTask;
        }
    }
}
