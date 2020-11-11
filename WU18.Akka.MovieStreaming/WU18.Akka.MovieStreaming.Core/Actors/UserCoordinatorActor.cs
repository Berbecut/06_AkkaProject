using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;

namespace WU18.Akka.MovieStreaming.Core.Actors
{
    public class UserCoordinatorActor:ReceiveActor
    {
        private readonly Dictionary<int, IActorRef> users;

        public UserCoordinatorActor()
        {
            users = new Dictionary<int, IActorRef>();

            Receive<PlayMovieMessage>(message =>
            {
                CreateChildIfSuchDoesNotExist(message.UserId);

                IActorRef childActorRef = users[message.UserId];
                childActorRef.Tell(message);
            });

            Receive<StopMovieMessage>(message =>
            {
                CreateChildIfSuchDoesNotExist(message.UserId);

                IActorRef childActorRef = users[message.UserId];
                childActorRef.Tell(message);
            });
        }

        private void CreateChildIfSuchDoesNotExist(int userId)
        {
            if (!users.ContainsKey(userId))
            {
                var userActorName = string.Format("User{0}", userId);
                IActorRef newChildActorRef
                    = Context.ActorOf(
                        Props.Create(() => new UserActor(userId)), userActorName);

                users.Add(userId, newChildActorRef);

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(
                    @"UserCoordinatorActor created a new child UserActor for {0}. Total users: {1}",
                    userId,
                    users.Count);
            }
        }

        #region "Lifecycle Hooks"

        // Should all these bellow methods be deleted ???
        protected override void PreStart()
        {
            Console.WriteLine("UserCoordinatorActor calling PreStart()");
            base.PreStart();
        }

        protected override void PostStop()
        {
            Console.WriteLine("UserCoordinatorActor calling PostStop()");
            base.PostStop();
        }

        protected override void PreRestart(Exception reason, object message)
        {
            Console.WriteLine("UserCoordinatorActor calling PreStart()");

            // The PreStart has an implementation so we need to pass on the call.
            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            Console.WriteLine("UserCoordinatorActor calling PostRestart()");

            // The PostStart has an implementation so we need to pass on the call.
            base.PostRestart(reason);
        }

        #endregion
    }
}
