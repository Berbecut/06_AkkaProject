using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using WU18.Akka.MovieStreaming.Core.Actors;
using WU18.Akka.MovieStreaming.Core.Exceptions;

namespace WU18.Akka.MovieStreaming.Core
{
    public class PlaybackActor : ReceiveActor
    {
        public PlaybackActor()
        {
            Context.ActorOf(Props.Create<UserCoordinatorActor>(), "UserCoordinator");
            Context.ActorOf(Props.Create<PlaybackStatisticsActor>(), "PlaybackStatistics");
        }

        //private void HandlePlayMovieMessage(PlayMovieMessage message)
        //{
        //    //Every time we get a PlayMovieMessage message,
        //    // we print the message contents.
        //    Console.WriteLine(
        //        "Received a play movie message: {0} {1}",
        //        message.MovieTitle,
        //        message.UserId);
        //}

            //xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        protected override SupervisorStrategy SupervisorStrategy()
        {
            return new OneForOneStrategy(exception =>
            {
                if (exception is SimulatedExtraBadMovieException)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("Well, that escalated quickly!");
                }
                return Directive.Restart;
            });
        }
        //xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx

        protected override void PreStart()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("PlaybackActor calling PreStart()");
        }

        protected override void PostStop()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("PlaybackActor calling PostStop()");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("PlaybackActor calling PreStart() because: {0}", reason);

            // The PreStart has an implementation so we need to pass on the call.
            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("PlaybackActor calling PostRestart() because: {0}", reason);

            // The PostStart has an implementation so we need to pass on the call.
            base.PostRestart(reason);
        }
    }
}
