using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using WU18.Akka.MovieStreaming.Core.Exceptions;

namespace WU18.Akka.MovieStreaming.Core.Actors
{
    public class PlaybackStatisticsActor : ReceiveActor
    {
        public PlaybackStatisticsActor()
        {
            Context.ActorOf(Props.Create<MoviePlayCounterActor>(), "MoviePlayCounter");
        }

        protected override SupervisorStrategy SupervisorStrategy()
        {
            return new OneForOneStrategy(exception =>
            { 
                if(exception is SimulatedCorruptStateException)
                {
                    return Directive.Stop;
                }
                if(exception is SimulatedBadMovieException)
                {
                    return Directive.Resume;
                }
                if(exception is SimulatedExtraBadMovieException)
                {
                    return Directive.Escalate;
                }
                return Directive.Restart;
            });
        }

        protected override void PreStart()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("PlaybackStatisticsActor calling PreStart()");
        }

        protected override void PostStop()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("PlaybackStatisticsActor calling PostStop()");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("PlaybackStatisticsActor calling PreRestart() because: " + reason);
            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("PlaybackStatisticsActor calling PostRestart() because: " + reason);
            base.PostRestart(reason);
        }
    }
}
