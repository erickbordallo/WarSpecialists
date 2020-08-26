﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer
{
    class ServerHandle
    {
        public static void WelcomeReceived(int _fromClient, Packet _packet)
        {
            int _clientIdCheck = _packet.ReadInt();
            string _username = _packet.ReadString();

            Console.WriteLine($"{Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint} connected successfully and it's now player {_fromClient}.");
            if(_fromClient != _clientIdCheck)
            {
                Console.WriteLine($"Player \"{_username}\" (ID: {_fromClient}) has assumed the wrong client ID ({_clientIdCheck})!");
            }
            //TODO: Send Player into Game
        }

        public static void UDPTestReceived(int _fromClient, Packet _packet)
        {
            string msg = _packet.ReadString();
            Console.WriteLine($"Received packet from UDP. Contains message: {msg}");
        }
    }
}
