using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Excersize3
{
    public delegate void Testa();
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Action filterTicketsAction;
        private string defultChoice = "All";
        private Data data;
        private List<Ticket> tickets;
        private bool blockSelectionChanged = false;

        public MainWindow()
        {
            InitializeComponent();
            data = new Data();
            tickets = data.Tickets; 

            filterTicketsAction += CountryFilter;
            filterTicketsAction += CostumerFilter;
            filterTicketsAction += AdminFilter;
            filterTicketsAction += AdminOfficeFilter;
           
            FilterTickets();
        }

        /// <summary>
        /// Get selectable countries
        /// </summary>
        /// <returns></returns>
        private List<string> GetCountriesFromList()
        {
            return tickets.Join(
                    data.Customers, t => t.PosterId,
                    c => c.ID,
                    (t, c) => c.Country)
                    .Distinct().ToList();
        }
        
        /// <summary>
        /// Get selectable customer names
        /// </summary>
        /// <returns></returns>
        private List<String> GetCustomerNamesFromList()
        {
            return tickets.Join(
                    data.Customers, t => t.PosterId,
                    c => c.ID,
                    (t, c) => c.Name)
                    .Distinct().ToList();
        }

        /// <summary>
        /// Get selectable admin names
        /// </summary>
        /// <returns></returns>
        private List<String> GetAdminNamesFromList()
        {
            return tickets.SelectMany(t => t.Answers)
                .ToList().Select(a => a.Key)
                .Distinct().ToList()
                .Join(data.Admins, i => i, a => a.ID, (i, a) => a.Name)
                .ToList();
        }

        /// <summary>
        /// Get selectable office admin names
        /// </summary>
        /// <returns></returns>
        private List<String> GetAdminOfficeNamesFromList()
        {
            return tickets.SelectMany(t => t.Answers)
                .ToList().Select(a => a.Key)
                .Distinct().ToList()
                .Join(data.Admins, i => i, a => a.ID, (i, a) => a.Office)
                .ToList();
        }

        /// <summary>
        /// Filter tickets
        /// </summary>
        private void FilterTickets()
        {
            //Reset tickets
            tickets = data.Tickets;
            //Filter tickets
            filterTicketsAction();

            //Set selectables with options from remaining tickets
            SetCBSource(countryComboBox, GetCountriesFromList());
            SetCBSource(customerComboBox, GetCustomerNamesFromList());
            SetCBSource(adminComboBox, GetAdminNamesFromList());
            SetCBSource(officeAdminComboBox, GetAdminOfficeNamesFromList());
            PopulateListBox();
        }

        /// <summary>
        /// Filters countries
        /// </summary>
        private void CountryFilter()
        {
            string choice = GetSelectedItem(countryComboBox);
            
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

        /// <summary>
        /// Get selected item Combobox
        /// </summary>
        /// <param name="cb"></param>
        /// <returns></returns>
        private string GetSelectedItem(ComboBox cb)
        {
            return cb.SelectedItem as String;
        }

        /// <summary>
        /// Set source comboBoxes
        /// </summary>
        /// <param name="cb"></param>
        /// <param name="source"></param>
        private void SetCBSource(ComboBox cb, List<string> source)
        {
            string selectedItem = cb.SelectedItem as string;
            
            source.Insert(0, defultChoice);

            blockSelectionChanged = true;
            cb.ItemsSource = null;
            cb.ItemsSource = source;

            blockSelectionChanged = true;
            if (selectedItem != null)
                cb.SelectedValue = selectedItem;
            else
                cb.SelectedValue = defultChoice;
        }

        /// <summary>
        /// Popolate listbox
        /// </summary>
        private void PopulateListBox()
        {
            ticketListBox.ItemsSource = null;
            ticketListBox.ItemsSource = tickets;
        }

        /// <summary>
        /// Change listener
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!blockSelectionChanged)
                FilterTickets();
            blockSelectionChanged = false;
        }
    }
}
