using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using WU18.Akka.MovieStreaming.Core.Actors;

namespace WU18.Akka.MovieStreaming.Core
{
    public class Program
    {
        private static ActorSystem actorSystem;
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            //Define our Actor system.
            Console.WriteLine("Creating the system");
            actorSystem = ActorSystem.Create("MovieStreamingActorSystem");

            // Creating the Actor which is supervisor. I created reference directly
            Console.WriteLine("Creating the actor supervisory hierarchy");
            actorSystem.ActorOf(Props.Create<PlaybackActor>(), "Playback");

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Enter a command (play, [USER ID], [MOVIE TITLE] | stop, [USER ID] | exit):");

            do
            {
                var commandText = Console.ReadLine();

                if (commandText.StartsWith("play"))
                {
                    int userId = int.Parse(commandText.Split(',')[1]);
                    string movieTitle = commandText.Split(',')[2];

                    var message = new PlayMovieMessage(movieTitle, userId);
                    actorSystem.ActorSelection("/user/Playback/UserCoordinator").Tell(message);
                }
                if (commandText.StartsWith("stop"))
                {
                    int userId = int.Parse(commandText.Split(',')[1]);

                    var message = new StopMovieMessage(userId);
                    actorSystem.ActorSelection("/user/Playback/UserCoordinator").Tell(message);
                }
                if (commandText == "exit")
                {
                    actorSystem.Terminate();
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("The actor system is now down. Press any key to exit the application");
                    Console.ReadKey();

                    Environment.Exit(1);
                }
            }
            while (true);
        }
    }
}
