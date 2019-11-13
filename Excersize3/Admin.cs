using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excersize3
{
    public class Admin:User
    {
        public string Office { get; set; }

        public Admin(string office, int iD, string name):base(iD, name)
        {
            Office = office;
        }
    }
}
