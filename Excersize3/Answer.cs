using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excersize3
{
    class Answer
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }

        public Answer(string title, string message, DateTime date)
        {
            Title = title;
            Message = message;
            Date = date;
        }
    }
}
