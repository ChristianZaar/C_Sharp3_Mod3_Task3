using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excersize3
{
    class TicketManager
    {
        public Func<List<string>> GetCountriesFromList;
        public Func<List<string>> GetCustomerNamesFromList;
        public Func<List<string>> GetAdminNamesFromList;
        public Func<List<string>> GetAdminOfficeNamesFromList;
        private Data data;
        private List<Ticket> tickets;
        private Action<string,string> FilterTicketsAction;

        public TicketManager()
        {

            data = new Data();
            tickets = data.Tickets;

            FilterTicketsAction += CountryFilter;
            FilterTicketsAction += CostumerFilter;
            FilterTicketsAction += AdminFilter;
            FilterTicketsAction += AdminOfficeFilter;

            // Get selectable countries
            GetCountriesFromList = () => tickets.Join(
                    data.Customers, t => t.PosterId,
                    c => c.ID,
                    (t, c) => c.Country)
                    .Distinct().ToList();

            // Get selectable customer names
            GetCustomerNamesFromList = () => tickets.Join(
                    data.Customers, t => t.PosterId,
                    c => c.ID,
                    (t, c) => c.Name)
                    .Distinct().ToList();

            // Get selectable admin names
            GetAdminNamesFromList = () => tickets.SelectMany(t => t.Answers)
                    .ToList().Select(a => a.Key)
                    .Distinct().ToList()
                    .Join(data.Admins, i => i, a => a.ID, (i, a) => a.Name)
                    .ToList();

            // Get selectable office admin names
            GetAdminOfficeNamesFromList = () => tickets.SelectMany(t => t.Answers)
                    .ToList().Select(a => a.Key)
                    .Distinct().ToList()
                    .Join(data.Admins, i => i, a => a.ID, (i, a) => a.Office)
                    .ToList();
         

        }

        /// <summary>
        /// Filters countries
        /// </summary>
        private void CountryFilter(string choice, string defultChoice)
        {

            if (choice != null && !choice.Equals(defultChoice))
            {
                tickets = data.Customers
                    .Where(c => c.Country.Equals(choice))
                    .Join(tickets, c => c.ID, t => t.PosterId, (c, t) => t)
                    .ToList();
            }
        }

        /// <summary>
        /// Filters Customers
        /// </summary>
        private void CostumerFilter(string choice, string defultChoice)
        {
            if (choice != null && !choice.Equals(defultChoice))
            {
                tickets = data.Customers
                    .Where(c => c.Name.Equals(choice))
                    .Join(tickets, c => c.ID, t => t.PosterId, (c, t) => t)
                    .ToList();
            }
        }

        /// <summary>
        /// Filter admins
        /// </summary>
        private void AdminFilter(string choice, string defultChoice)
        {
            if (choice != null && !choice.Equals(defultChoice))
            {
                Admin admin = data.Admins.Find(a => a.Name.Equals(choice));
                List<Ticket> tmp = new List<Ticket>();
                tickets = FindAdminAnsweredTicket(admin);
            }
        }

        /// <summary>
        /// Admin office filter
        /// </summary>
        private void AdminOfficeFilter(string choice, string defultChoice)
        {
            if (choice != null && !choice.Equals(defultChoice))
            {
                Admin admin = data.Admins.Find(a => a.Office.Equals(choice));
                List<Ticket> tmp = new List<Ticket>();
                tickets = FindAdminAnsweredTicket(admin);
            }
        }

        /// <summary>
        /// Find ticket that is answered by admin
        /// </summary>
        /// <param name="admin"></param>
        /// <returns></returns>
        private List<Ticket> FindAdminAnsweredTicket(Admin admin)
        {
            List<Ticket> tmp = new List<Ticket>();
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
        private string GetTicketAuthorPart()
        {
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
        private string GetTicketAdminPart()
        {
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

        public void DeleteSelectedTicket(Ticket ticket)
        {
            data.Tickets.Remove(ticket);
            FilterTickets();//.Remove(ticket);
        }
    }
}
