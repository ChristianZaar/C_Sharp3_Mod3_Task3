using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excersize3
{
    class Data
    {
        private static int IDs = 1;
        public List<Customer> Customers { get; set; }
        public List<Admin> Admins { get; set; }
        public List<Ticket> Tickets { get; set; }
        public List<string> Countries { get; set; }

        public Data()
        {
            Customers = new List<Customer>();
            Admins = new List<Admin>();
            Tickets = new List<Ticket>();
            Countries = new List<string>() { "Sweden", "Denmark", "Norway"};
            CreateAdmins();
            CreateCustomers();
            AddTickets();
        }

        private void CreateCustomers()
        {
            Customers.Add(new Customer("Sweden", "Hässleholm", GetID(), "Bengt-Göran Karlsson"));
            Customers.Add(new Customer("Sweden", "Malmö", GetID(), "Gunilla Svensson"));
            Customers.Add(new Customer("Sweden", "Hässleholm", GetID(), "Törje Hansson"));
            Customers.Add(new Customer("Sweden", "Kristianstad", GetID(), "Dorris Gran"));
            Customers.Add(new Customer("Norway", "Oslo", GetID(), "Harriet Redig"));
            Customers.Add(new Customer("Norway", "Kristiansand", GetID(), "Karl Fontän"));
            Customers.Add(new Customer("Norway", "Trondheim", GetID(), "Serjon Lind"));
            Customers.Add(new Customer("Norway", "Oslo", GetID(), "Mohammad Ali"));
            Customers.Add(new Customer("Denmark", "Helsingör", GetID(), "Yilmaz Mourad"));
            Customers.Add(new Customer("Denmark", "Roskilde", GetID(), "Roger Öhman"));
            Customers.Add(new Customer("Denmark", "Roskilde", GetID(), "Lisbet Werland"));
        }

        private void CreateAdmins()
        {
            Admins.Add(new Admin("Ney York", GetID(), "Harald Denon"));
            Admins.Add(new Admin("Eden", GetID(), "Evan Almighty"));
            Admins.Add(new Admin("Hässleholm", GetID(), "Christian Zaar"));
        }

        private void AddTickets()
        {
            //Ticket 1
            Tickets.Add(new Ticket(
                Customers.Find(c => c.Name.Equals("Bengt-Göran Karlsson")).ID,
                "Kaffekokaren fungerar inte.", 
                "Den startar inte!!",
                false, 
                DateTime.Now));

            //Ticket 2
            Dictionary<int, Answer> answer = new Dictionary<int, Answer>();
            answer.Add(Admins.Find(a => a.Name.Equals("Harald Denon")).ID, new Answer("Konstigt!", "Dra urtt sladden", DateTime.Now));

            Tickets.Add(new Ticket(
                Customers.Find((Customer c) => c.Name.Equals("Gunilla Svensson")).ID,
                "Datorn låter.",
                "Ett surrande ljud som är super högt.",
                true,
                DateTime.Now,
                answer));


            //Ticket 3
            Tickets.Add(new Ticket(
                Customers.Find((Customer c) => c.Name.Equals("Bengt-Göran Karlsson")).ID,
                "Kaffekokaren fungerar inte.",
                "Den startar inte!!",
                false,
                DateTime.Now));

            //Ticket 4
            Tickets.Add(new Ticket(
                Customers.Find((Customer c) => c.Name.Equals("Bengt-Göran Karlsson")).ID,
                "Kaffekokaren fungerar inte.",
                "Den startar inte!!",
                false,
                DateTime.Now));

            //Ticket 5
            Tickets.Add(new Ticket(
                Customers.Find((Customer c) => c.Name.Equals("Bengt-Göran Karlsson")).ID,
                "Kaffekokaren fungerar inte.",
                "Den startar inte!!",
                false,
                DateTime.Now));

            //Ticket 6
            Tickets.Add(new Ticket(
                Customers.Find((Customer c) => c.Name.Equals("Bengt-Göran Karlsson")).ID,
                "Kaffekokaren fungerar inte.",
                "Den startar inte!!",
                false,
                DateTime.Now));

            //Ticket 7
            Tickets.Add(new Ticket(
                Customers.Find((Customer c) => c.Name.Equals("Bengt-Göran Karlsson")).ID,
                "Kaffekokaren fungerar inte.",
                "Den startar inte!!",
                false,
                DateTime.Now));

            //Ticket 8
            Tickets.Add(new Ticket(
                Customers.Find((Customer c) => c.Name.Equals("Bengt-Göran Karlsson")).ID,
                "Kaffekokaren fungerar inte.",
                "Den startar inte!!",
                false,
                DateTime.Now));

            //Ticket 9
            Tickets.Add(new Ticket(
                Customers.Find((Customer c) => c.Name.Equals("Bengt-Göran Karlsson")).ID,
                "Kaffekokaren fungerar inte.",
                "Den startar inte!!",
                false,
                DateTime.Now));

            //Ticket 10
            Tickets.Add(new Ticket(
                Customers.Find((Customer c) => c.Name.Equals("Bengt-Göran Karlsson")).ID,
                "Kaffekokaren fungerar inte.",
                "Den startar inte!!",
                false,
                DateTime.Now));

            //Ticket 11
            Tickets.Add(new Ticket(
                Customers.Find((Customer c) => c.Name.Equals("Bengt-Göran Karlsson")).ID,
                "Kaffekokaren fungerar inte.",
                "Den startar inte!!",
                false,
                DateTime.Now));
        }

        private int GetID()
        {
            return IDs++;
        }
    }
}
