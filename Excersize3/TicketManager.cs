using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excersize3
{
    class TicketManager
    {
        public Func<List<ITicket>,List<string>> GetCountriesFromList;
        public Func<List<ITicket>, List<string>> GetCustomerNamesFromList;
        public Func<List<ITicket>, List<string>> GetAdminNamesFromList;
        public Func<List<ITicket>, List<string>> GetAdminOfficeNamesFromList;
        private Data data;
        public event Action TicketDeleted;

        public TicketManager()
        {

            data = new Data();

            // Get selectable countries
            GetCountriesFromList = (List<ITicket> tickets) => tickets.ConvertAll(new Converter<ITicket, Ticket>(( t ) => (Ticket) t )).Join(
                    data.Customers, t => t.PosterId,
                    c => c.ID,
                    (t, c) => c.Country)
                    .Distinct().ToList();

            // Get selectable customer names
            GetCustomerNamesFromList = (List<ITicket> tickets) => tickets.ConvertAll(new Converter<ITicket, Ticket>((t) => (Ticket)t)).Join(
                    data.Customers, t => t.PosterId,
                    c => c.ID,
                    (t, c) => c.Name)
                    .Distinct().ToList();

            // Get selectable admin names
            GetAdminNamesFromList = (List<ITicket> tickets) => tickets.ConvertAll(new Converter<ITicket, Ticket>((t) => (Ticket)t)).SelectMany(t => t.Answers)
                    .ToList().Select(a => a.Key)
                    .Distinct().ToList()
                    .Join(data.Admins, i => i, a => a.ID, (i, a) => a.Name)
                    .ToList();

            // Get selectable office admin names
            GetAdminOfficeNamesFromList = (List<ITicket> tickets) => tickets.ConvertAll(new Converter<ITicket, Ticket>((t) => (Ticket)t)).SelectMany(t => t.Answers)
                    .ToList().Select(a => a.Key)
                    .Distinct().ToList()
                    .Join(data.Admins, i => i, a => a.ID, (i, a) => a.Office)
                    .ToList();
         

        }

        public List<ITicket> GetAllTickets()
        {
            return data.Tickets.ConvertAll(new Converter<Ticket, ITicket>(t=> (ITicket)t));
        }

        /// <summary>
        /// Filters countries
        /// </summary>
        public List<ITicket> CountryFilter(string choice, string defultChoice, List<ITicket> tickets)
        {

            if (choice != null && !choice.Equals(defultChoice))
            {
                tickets = data.Customers
                    .Where(c => c.Country.Equals(choice))
                    .Join(tickets, c => c.ID, t => t.PosterId, (c, t) => t)
                    .ToList();
            }
            return tickets;
        }

        /// <summary>
        /// Filters Customers
        /// </summary>
        public List<ITicket> CostumerFilter(string choice, string defultChoice, List<ITicket> tickets)
        {
            if (choice != null && !choice.Equals(defultChoice))
            {
                tickets = data.Customers
                    .Where(c => c.Name.Equals(choice))
                    .Join(tickets, c => c.ID, t => t.PosterId, (c, t) => t)
                    .ToList();
            }
            return tickets;
        }

        /// <summary>
        /// Filter admins
        /// </summary>
        public List<ITicket> AdminFilter(string choice, string defultChoice, List<ITicket> tickets)
        {
            if (choice != null && !choice.Equals(defultChoice))
            {
                Admin admin = data.Admins.Find(a => a.Name.Equals(choice));
                List<Ticket> tmp = new List<Ticket>();
                tickets = FindAdminAnsweredTicket(admin, tickets);
            }
            return tickets;
        }

        /// <summary>
        /// Admin office filter
        /// </summary>
        public List<ITicket> AdminOfficeFilter(string choice, string defultChoice, List<ITicket> tickets)
        {
            if (choice != null && !choice.Equals(defultChoice))
            {
                Admin admin = data.Admins.Find(a => a.Office.Equals(choice));
                List<Ticket> tmp = new List<Ticket>();
                tickets = FindAdminAnsweredTicket(admin, tickets);
            }

            return tickets;
        }

        /// <summary>
        /// Find ticket that is answered by admin
        /// </summary>
        /// <param name="admin"></param>
        /// <returns></returns>
        private List<ITicket> FindAdminAnsweredTicket(Admin admin, List<ITicket> tickets)
        {
            List<ITicket> tmp = new List<ITicket>();
            foreach (Ticket t in tickets)
            {
                foreach (KeyValuePair<int, Answer> entry in t.Answers)
                {
                    if (entry.Key == admin.ID)
                    {
                        tmp.Add(t);
                        break;
                    }
                }
            }
            return tickets = tmp;
        }

        /// <summary>
        /// Gets text for ticket
        /// </summary>
        /// <returns></returns>
        public string GetTicketAuthorPart(ITicket ticket)
        {
            if (!(ticket is Ticket selectedTicket))
                return "";

            string author = data.Customers.Find(c => c.ID == selectedTicket.PosterId).Name;

            return "Author: " + "\t" + author + "\n"
           + "Date: " + "\t" + selectedTicket.Date.ToString("yyyyMMdd") + "\n"
           + "Title: " + "\t" + selectedTicket.Title + "\n"
           + "Description: " + selectedTicket.Desc + "\n\n";
        }

        /// <summary>
        /// Gets text for ticket
        /// </summary>
        /// <returns></returns>
        public string GetTicketAdminPart(ITicket ticket)
        {
            if (!(ticket is Ticket selectedTicket))
                return "";

            string answers = "";

            foreach (KeyValuePair<int, Answer> entry in selectedTicket.Answers)
            {
                Admin admin = data.Admins.Find(a => a.ID == entry.Key);
                answers += "Helper: " + admin.Name + " Office: " + admin.Office + "\n"
                    + "Date: " + "\t" + entry.Value.Date.ToString("yyyyMMdd") + "\n"
                    + "Title: " + "\t" + entry.Value.Title + "\n"
                    + "Description: " + entry.Value.Message + "\n\n";
            }

            return answers;
        }

        /// <summary>
        /// Delete ticket
        /// </summary>
        /// <param name="ticket"></param>
        public void DeleteTicket(ITicket ticket)
        {
            data.Tickets.Remove((Ticket)ticket);
            TicketDeleted();
        }
    }
}
