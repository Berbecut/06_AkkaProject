using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WU18.Akka.MovieStreaming.Core
{
    public class StopMovieMessage
    {
        public int UserId { get; private set; }
        public StopMovieMessage(int userId)
        {
            UserId = userId;
        }
    }
}
