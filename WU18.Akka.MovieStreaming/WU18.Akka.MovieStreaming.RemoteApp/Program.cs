﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;

namespace WU18.Akka.MovieStreaming.RemoteApp
{
    class Program
    {
        private static ActorSystem actorSystem;
        static void Main(string[] args)
        {
            Console.WriteLine("Creating the remote system.");
            actorSystem = ActorSystem.Create("MovieStreamingActorSystem");

            var task = actorSystem.WhenTerminated;
            Task.WaitAny(task);

        }
    }
}
