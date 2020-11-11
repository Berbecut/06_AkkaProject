using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WU18.Akka.MovieStreaming.Core.Messages;

namespace WU18.Akka.MovieStreaming.Core.Actors
{
    public class UserActor : ReceiveActor
    {
        private string currentlyWatching;

        private readonly int userId;

        public UserActor(int userId)
        {
            this.userId = userId;
            Stopped();
        }

        public UserActor()
        {
            Console.WriteLine("Creating a UserActor...");

            Console.WriteLine("Setting the initial behaviour to stopped.");
            Stopped();
        }

        // This method represents the playing behaviour.
        private void Playing()
        {
            Receive<PlayMovieMessage>(
                message => System.Console.WriteLine(
                @"UserActor {0} error: Cannot start playing another movie 
                    before stopping the current one.", userId));

            Receive<StopMovieMessage>(message => StopPlayingCurrentMovie());

            System.Console.WriteLine("UserActor {0} is in playing state.", userId);
        }

        // This method represents the stopped behaviour.
        private void Stopped()
        {
            // Handle the start movie request message.
            Receive<PlayMovieMessage>(
                message => StartPlayingMovie(message.MovieTitle));

            // Handle the stop movie request message.
            Receive<StopMovieMessage>(
                message => Console.WriteLine(@"UserActor {0} error: Cannot stop 
                    if nothing currently playing.", userId));

            System.Console.WriteLine("UserActor {0} is now in stopped state.", userId);
        }

        private void StartPlayingMovie(string title)
        {
            currentlyWatching = title;

            System.Console.WriteLine("UserActor {0} is currently watching the movie '{1}'.",
                userId, currentlyWatching);

            Context.ActorSelection("/user/Playback/PlaybackStatistics/MoviePlayCounter").Tell(new IncrementPlayCountMessage(title));

            // Switch from the initital stopped state to the started state.
            Become(Playing);
        }

        private void StopPlayingCurrentMovie()
        {
            System.Console.WriteLine("UserActor {0} has stopped watching the movie '{1}'.",
                userId,
                currentlyWatching);

            currentlyWatching = null;

            // Switch from the started state to the stopped state.
            Become(Stopped);
        }

        protected override void PreStart()
        {
            Console.WriteLine("UserActor PreStart()");
            base.PreStart();
        }

        protected override void PostStop()
        {
            Console.WriteLine("UserActor PostStop()");
            base.PostStop();
        }

        protected override void PreRestart(Exception reason, object message)
        {
            Console.WriteLine("UserActor PreStart()");

            // The PreStart has an implementation so we need to pass on the call.
            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            Console.WriteLine("UserActor PostRestart()");

            // The PostStart has an implementation so we need to pass on the call.
            base.PostRestart(reason);
        }
    }
}
