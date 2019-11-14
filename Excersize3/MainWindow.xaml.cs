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
/// <summary>
/// Application for keeping track of support tickets. By selecting filter criteria in comboboxes data is queried. Showing only relevant issues and options.
/// Main point is testing lambdas and delegates
/// </summary>
namespace Excersize3
{
    public delegate void Testa();
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        private string defultChoice = "All";
        
        private bool blockSelectionChanged = false;
        private Ticket selectedTicket;
        private Func<List<string>> GetCountriesFromList;
        private Func<List<string>> GetCustomerNamesFromList;
        private Func<List<string>> GetAdminNamesFromList;
        private Func<List<string>> GetAdminOfficeNamesFromList;
        private Func<ComboBox, string> GetSelectedItem;

        public MainWindow()
        {
            InitializeComponent();
            
            

            // Get selected item Combobox
            GetSelectedItem = (ComboBox cb) =>cb.SelectedItem as String;

            FilterTickets();

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

        /// <summary>
        /// Clixk event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewTicketBtn_Click(object sender, RoutedEventArgs e)
        {
            selectedTicket = ticketListBox.SelectedItem as Ticket;

            if (selectedTicket != null)
            {
                ViewTicket viewTicket = new ViewTicket(selectedTicket, GetTicketAuthorPart, GetTicketAdminPart);
                viewTicket.DeleteTicket += DeleteSelectedTicket;
                viewTicket.ShowDialog();
            }
        }


        /// <summary>
        /// Gets text for ticket
        /// </summary>
        /// <returns></returns>
        private string GetTicketAuthorPart()
        {
            string author = data.Customers.Find(c => c.ID == selectedTicket.PosterId).Name;
            
             return   "Author: " + "\t" + author + "\n"
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

        private void DeleteSelectedTicket(Ticket ticket)
        {
            data.Tickets.Remove(ticket);
            FilterTickets();//.Remove(ticket);
        }
    }
}
