﻿using System;

namespace GameServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Game Server";

            Server.Start(6, 26950);

            Console.ReadKey(); 
        }
    }
}
