using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CemeterySoul.Models
{
    public class Users
    {
        public string Email_Address { get; set; }
        public string Pass { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }


        public Users() { }

        public Users(string email_address, string pass, string first_name, string last_name)
        {
            Email_Address = email_address;
            Pass = pass;
            First_Name = first_name;
            Last_Name = last_name;
        }

        public override string ToString()
        {
            return $"{Email_Address}, {Pass}, {First_Name}, {Last_Name}";
        }
    }
}