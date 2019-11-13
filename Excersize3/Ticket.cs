using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excersize3
{
    class Ticket
    {
        public int PosterId { get; set; } = 0;
        public string Title { get; set; } = "";
        public string Desc { get; set; } = "";
        public bool IsSolved { get; set; } = false;
        public DateTime Date { get; set; } = DateTime.Now;
        public Dictionary<int, Answer> Answers { get; set; } = new Dictionary<int, Answer>();


        public Ticket(int posterId, string title, string desc, bool isSolved, DateTime date)
        {
            PosterId = posterId;
            Title = title;
            Desc = desc;
            IsSolved = isSolved;
            Date = date;
        }

        public Ticket(int posterId, string title, string desc, bool isSolved, DateTime date, Dictionary<int, Answer> answers)
        {
            PosterId = posterId;
            Title = title;
            Desc = desc;
            IsSolved = isSolved;
            Date = date;
            Answers = answers;
        }

        public override string ToString()
        {
            return Title;
        }
    }
}
