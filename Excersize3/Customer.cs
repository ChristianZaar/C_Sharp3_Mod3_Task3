using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excersize3
{
    public class Customer : User
    {
        public string Country { get; set; }
        public string City { get; set; }

        public Customer(string country, string city,int iD, string name) :base(iD, name)
        {
            Country = country;
            City = city;
        }
    }
}
