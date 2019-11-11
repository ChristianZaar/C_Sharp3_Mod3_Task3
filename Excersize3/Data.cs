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

        /// <summary>
        /// Adding ticket data
        /// </summary>
        private void AddTickets()
        {
            Func<string, string, Answer> createAnswer = (title, msg) => new Answer(title, msg, DateTime.Now);
            Func<string, int> getAdminId = (name) => Admins.Find(a => a.Name.Equals(name)).ID;
            Func<string, int> getCustomerId = (name) => Customers.Find((Customer c) => c.Name.Equals(name)).ID;

            //Ticket 1
            Tickets.Add(new Ticket(
                getCustomerId("Bengt-Göran Karlsson"),
                "Kaffekokaren fungerar inte.",
                "Den startar inte!!",
                false,
                DateTime.Now));

            //Ticket 2
            Dictionary<int, Answer> answer = new Dictionary<int, Answer>()
            {
                {   getAdminId("Harald Denon"), createAnswer("Konstigt!", "Dra urtt sladden")}
            };

            Tickets.Add(new Ticket(
                getCustomerId("Gunilla Svensson"),
                "Datorn låter.",
                "Ett surrande ljud som är super högt.",
                true,
                DateTime.Now,
                answer));


            //Ticket 3
            answer = new Dictionary<int, Answer>()
            {
                {   getAdminId("Evan Almighty"), createAnswer("hmm!", "Vet inte varför") },
                {   getAdminId("Harald Denon"), createAnswer("kolla fläkten", "Dra urtt sladden")}
            };

            Tickets.Add(new Ticket(
                getCustomerId("Yilmaz Mourad"),
                "Doft.",
                "Det stinker vid datorn!!",
                true,
                DateTime.Now,
                answer));

            //Ticket 4
            Tickets.Add(new Ticket(
                getCustomerId("Dorris Gran"),
                "Glassen.",
                "Glassen är slut",
                false,
                DateTime.Now));

            //Ticket 5
            Tickets.Add(new Ticket(
                getCustomerId("Törje Hansson"),
                "Kaffekokaren fungerar inte.",
                "Den startar inte!!",
                false,
                DateTime.Now));

            //Ticket 6
            answer = new Dictionary<int, Answer>()
            {
                {   getAdminId("Christian Zaar"), createAnswer("lösenord", "Kontrollera ditt lösenord") }
            };
            Tickets.Add(new Ticket(
               getCustomerId("Karl Fontän"),
                "Inloggning.",
                "Kan inte logga in på canvas",
                true,
                DateTime.Now,
                answer));

            //Ticket 7
            Tickets.Add(new Ticket(
                getCustomerId("Mohammad Ali"),
                "blabla.",
                "blblbla bla bla.",
                false,
                DateTime.Now));

            //Ticket 8
            Tickets.Add(new Ticket(
                getCustomerId("Mohammad Ali"),
                "Kaffekokaren fungerar inte.",
                "Den startar inte!!",
                false,
                DateTime.Now));

            //Ticket 9
            answer = new Dictionary<int, Answer>()
            {
                {   getAdminId("Evan Almighty"), createAnswer("hm", "Det blir 3.") },
                {   getAdminId("Harald Denon"), createAnswer("nej", "Det blir 4") },
                {   getAdminId("Christian Zaar"), createAnswer("ehh nej", "svaret är 2.") }
            };
            Tickets.Add(new Ticket(
                getCustomerId("Lisbet Werland"),
                "matte problem.",
                "Vad blir 1+1?",
                true,
                DateTime.Now,
                answer));

            //Ticket 10
            Tickets.Add(new Ticket(
                getCustomerId("Serjon Lind"),
                "Fråga.",
                "Är detta rätt?",
                false,
                DateTime.Now));

            //Ticket 11
            answer = new Dictionary<int, Answer>()
            {
                {   getAdminId("Evan Almighty"), createAnswer("jadu", "inte jag heller") },
                {   getAdminId("Harald Denon"), createAnswer("...", "Det har du rätt i") }
            };
            Tickets.Add(new Ticket(
                getCustomerId("Harriet Redig"),
                "lite oklart",
                "Kan inte formulera en fråga!",
                true,
                DateTime.Now,
                answer));
        }

        private int GetID()
        {
            return IDs++;
        }
    }
}
