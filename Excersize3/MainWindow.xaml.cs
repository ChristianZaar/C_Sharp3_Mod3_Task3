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
        //private Func<List<string>> GetCountriesFromList;
        //private Func<List<string>> GetCustomerNamesFromList;
        //private Func<List<string>> GetAdminNamesFromList;
        //private Func<List<string>> GetAdminOfficeNamesFromList;
        private Func<ComboBox, string> GetSelectedItem;
        TicketManager ticketManager = new TicketManager();

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
            SetCBSource(countryComboBox, ticketManager.GetCountriesFromList());
            SetCBSource(customerComboBox, ticketManager.GetCustomerNamesFromList());
            SetCBSource(adminComboBox, ticketManager.GetAdminNamesFromList());
            SetCBSource(officeAdminComboBox, ticketManager.GetAdminOfficeNamesFromList());
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

        
    }
}
