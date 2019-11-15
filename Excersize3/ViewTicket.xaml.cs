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
        private event Func<ITicket,String> GetTicketInfo;
        public event Action<ITicket> DeleteTicket;
        private ITicket ticket;

        public ViewTicket(ITicket ticket, Func<ITicket, string> user, Func<ITicket,String> admin)
        {
            InitializeComponent();
            GetTicketInfo += user;
            TicketTextBlock.Text = GetTicketInfo(ticket);
            GetTicketInfo += admin;
            TicketTextBlock.Text += GetTicketInfo(ticket);
            this.ticket = ticket;
        }

        private void DeleteTicketBtn_Click(object sender, RoutedEventArgs e)
        {
            DeleteTicket(ticket);
            this.Close();
        }
    }
}
