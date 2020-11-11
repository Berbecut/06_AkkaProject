using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WU18.Akka.MovieStreaming.Core.Messages
{
    public class IncrementPlayCountMessage
    {
        public string Title { get; set; }
        public IncrementPlayCountMessage(string title)
        {
            Title = title;
        }
    }
}
