using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WU18.Akka.MovieStreaming.Core.Exceptions;
using WU18.Akka.MovieStreaming.Core.Messages;
using Akka.Actor;

namespace WU18.Akka.MovieStreaming.Core.Actors
{
    public class MoviePlayCounterActor: ReceiveActor
    {
        private readonly Dictionary<string, int> moviePlayCounts;
        public MoviePlayCounterActor()
        {
            moviePlayCounts = new Dictionary<string, int>();
            Receive<IncrementPlayCountMessage>(message => HandleIncrementMessage(message));
        }


        private void HandleIncrementMessage(IncrementPlayCountMessage message)
        {
            if (moviePlayCounts.ContainsKey(message.Title))
            {
                moviePlayCounts[message.Title]++;
            }
            else
            {
                moviePlayCounts.Add(message.Title, 1);
            }

            //simulated bug
            if (moviePlayCounts[message.Title] > 3)
            {
                throw new SimulatedCorruptStateException();
            }

            //simulated bug
            if(message.Title == "Terminator")
            {
                throw new SimulatedBadMovieException();
            }

            if (message.Title == "Terminator2")
            {
                throw new SimulatedExtraBadMovieException();
            }


            var numberOfPlays = moviePlayCounts[message.Title];
            Console.WriteLine("Movie Play Counter: " + 
                               "The movie {0} has been played {1} times.",
                               message.Title, numberOfPlays);
        }

        protected override void PreStart()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("MoviePlayCounterActor calling PreStart()");
        }

        protected override void PostStop()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("MoviePlayCounterActor calling PostStop()");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("MoviePlayCounterActor calling PreStart() because: " + reason);
            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("MoviePlayCounterActor calling PostRestart() because: " + reason);
            base.PostRestart(reason);
        }

    }
}
