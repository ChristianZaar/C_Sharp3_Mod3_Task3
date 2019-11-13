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
using System.Windows.Shapes;

namespace Excersize3
{
    /// <summary>
    /// Interaction logic for ViewTicket.xaml
    /// </summary>
    public partial class ViewTicket : Window
    {
        private event Func<String> GetTicketInfo;
        public event Action<Ticket> DeleteTicket;
        private Ticket ticket;

        public ViewTicket(Ticket ticket, Func<String> user, Func<String> admin)
        {
            InitializeComponent();
            GetTicketInfo += user;
            TicketTextBlock.Text = GetTicketInfo();
            GetTicketInfo += admin;
            TicketTextBlock.Text += GetTicketInfo();
            this.ticket = ticket;
        }

        private void DeleteTicketBtn_Click(object sender, RoutedEventArgs e)
        {
            DeleteTicket(ticket);
            this.Close();
        }
    }
}
