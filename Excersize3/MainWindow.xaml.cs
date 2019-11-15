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
        
        private string defaultChoice = "All";
        private List<ITicket> tickets = new List<ITicket>();  
        private bool blockSelectionChanged = false;
        private Ticket selectedTicket;
        private Func<string, string, List<ITicket>, List<ITicket>> FilterTicketsByCountry;
        private Func<string, string, List<ITicket>, List<ITicket>> FilterTicketsByCostumer;
        private Func<string, string, List<ITicket>, List<ITicket>> FilterTicketsByAdmin;
        private Func<string, string, List<ITicket>, List<ITicket>> FilterTicketsByAdminOffice;
        private Func<ComboBox, string> GetSelectedItem;
        TicketManager ticketManager = new TicketManager();

        public MainWindow()
        {
            InitializeComponent();
            ticketManager.TicketDeleted += FilterTickets;
            FilterTicketsByCountry += ticketManager.CountryFilter;
            FilterTicketsByCostumer += ticketManager.CostumerFilter;
            FilterTicketsByAdmin += ticketManager.AdminFilter;
            FilterTicketsByAdminOffice += ticketManager.AdminOfficeFilter;
            // Get selected item Combobox
            GetSelectedItem = (ComboBox cb) =>cb.SelectedItem as String;
            //SetComboboxSources();
            FilterTickets();

        }

        /// <summary>
        /// Filter tickets
        /// </summary>
        private void FilterTickets()
        {
            //Reset tickets
            tickets = ticketManager.GetAllTickets(); ;
            //Filter tickets
            Filter();
        
            SetComboboxSources(); 

            PopulateListBox();
        }

        private void SetComboboxSources()
        {
            //Set selectables with options from remaining tickets
            SetCBSource(countryComboBox, ticketManager.GetCountriesFromList(tickets));
            SetCBSource(customerComboBox, ticketManager.GetCustomerNamesFromList(tickets));
            SetCBSource(adminComboBox, ticketManager.GetAdminNamesFromList(tickets));
            SetCBSource(officeAdminComboBox, ticketManager.GetAdminOfficeNamesFromList(tickets));
        }

        private void Filter()
        {
            tickets = FilterTicketsByCountry(GetSelectedItem(countryComboBox), defaultChoice, tickets);
            tickets = FilterTicketsByCostumer(GetSelectedItem(customerComboBox), defaultChoice, tickets);
            tickets = FilterTicketsByAdmin(GetSelectedItem(adminComboBox), defaultChoice, tickets);
            tickets = FilterTicketsByAdminOffice(GetSelectedItem(officeAdminComboBox), defaultChoice, tickets);
        }

        

        /// <summary>
        /// Set source comboBoxes
        /// </summary>
        /// <param name="cb"></param>
        /// <param name="source"></param>
        private void SetCBSource(ComboBox cb, List<string> source)
        {
            string selectedItem = cb.SelectedItem as string;
            
            source.Insert(0, defaultChoice);

            blockSelectionChanged = true;
            cb.ItemsSource = null;
            cb.ItemsSource = source;

            blockSelectionChanged = true;
            if (selectedItem != null)
                cb.SelectedValue = selectedItem;
            else
                cb.SelectedValue = defaultChoice;
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
                ViewTicket viewTicket = new ViewTicket(selectedTicket, ticketManager.GetTicketAuthorPart, ticketManager.GetTicketAdminPart);
                viewTicket.DeleteTicket += ticketManager.DeleteTicket;
                viewTicket.ShowDialog();
            }
        }

        
    }
}
