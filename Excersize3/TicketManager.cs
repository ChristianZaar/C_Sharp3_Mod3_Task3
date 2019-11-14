using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excersize3
{
    class TicketManager
    {
        private Func<List<string>> GetCountriesFromList;
        private Func<List<string>> GetCustomerNamesFromList;
        private Func<List<string>> GetAdminNamesFromList;
        private Func<List<string>> GetAdminOfficeNamesFromList;
        private Data data;
        private List<Ticket> tickets;
        private Action filterTicketsAction;

        public TicketManager()
        {

            data = new Data();
            tickets = data.Tickets;

            filterTicketsAction += CountryFilter;
            filterTicketsAction += CostumerFilter;
            filterTicketsAction += AdminFilter;
            filterTicketsAction += AdminOfficeFilter;

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
        private void CountryFilter(string choice)
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
        private void CostumerFilter()
        {
            string choice = GetSelectedItem(customerComboBox);

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
        private void AdminFilter()
        {
            string choice = GetSelectedItem(adminComboBox);

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
        private void AdminOfficeFilter()
        {
            string choice = GetSelectedItem(officeAdminComboBox);

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
    }
}
