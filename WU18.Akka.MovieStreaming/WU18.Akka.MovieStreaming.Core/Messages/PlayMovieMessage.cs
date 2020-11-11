using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WU18.Akka.MovieStreaming.Core
{
    public class PlayMovieMessage
    {
        public string MovieTitle { get; private set; }
        public int UserId { get; private set; }

        public PlayMovieMessage(string movieTitle, int userId)
        {
            MovieTitle = movieTitle;
            UserId = userId;
        }
    }
}
