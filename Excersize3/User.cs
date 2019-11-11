using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excersize3
{
    class User
    {
        public int ID { get; set; } = 0;
        public string Name { get; set; } = "";

        public User(int iD, string name)
        {
            ID = iD;
            Name = name;
        }

        public User()
        {
        }

    }
}
