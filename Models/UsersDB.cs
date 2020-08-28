using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace CemeterySoul.Models
{
    public class UsersDB    
    {
        static bool local = false;
        static string strCon = null;
        static string strConLocal = @"Data Source=LAPTOP-8GON88TL\SQLEXPRESS;Initial Catalog=CEMETERY1;Integrated Security=True";
        static string strConLIVEDNS = @"Data Source=185.60.170.14;Integrated Security=False;User ID=site08;Password=i*8qEb42;";

        static UsersDB()
        {
            if (local)
            {
                strCon = strConLocal;
            }
            else
            {
                strCon = strConLIVEDNS;
            }
        }


        public static List<Users> GetAllUsers()
        {
            List<Users> ul = new List<Users>();
            SqlConnection con = new SqlConnection(strCon);
            SqlCommand comm = new SqlCommand("SELECT * FROM Users", con);
            comm.Connection.Open();
            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                Users u = new Users(
                    (string)reader["email_address"],
                    (string)reader["pass"],
                    (string)reader["first_name"],
                    (string)reader["last_name"]
                    );
                ul.Add(u);
            }
            comm.Connection.Close();
            return ul;
        }




        public static Users GetUserByEmailAndPassword(string email, string password)
        {
            Users u = null;
            SqlConnection con = new SqlConnection(strCon);
            SqlCommand comm = new SqlCommand(
                $" SELECT * FROM Users " +
                $" WHERE email_address='{email}' AND pass='{password}'", con);
            comm.Connection.Open();
            SqlDataReader reader = comm.ExecuteReader();
            if (reader.Read())
            {
                u = new Users(
                    (string)reader["email_address"],
                    (string)reader["pass"],
                    (string)reader["first_name"],
                    (string)reader["last_name"]);
            }
            comm.Connection.Close();
            return u;
        }



        public static Users GetUserByEmail(string email)
        {
            Users u = null;
            SqlConnection con = new SqlConnection(strCon);
            SqlCommand comm = new SqlCommand(
                $" SELECT * FROM Users " +
                $" WHERE email_address='{email}'", con);
            comm.Connection.Open();
            SqlDataReader reader = comm.ExecuteReader();
            if (reader.Read())
            {
                u = new Users(
                    (string)reader["email_address"],
                    (string)reader["pass"],
                    (string)reader["first_name"],
                    (string)reader["last_name"]);
            }
            comm.Connection.Close();
            return u;
        }



        public static Users InsertUserToDb(Users val)
        {
            if (GetUserByEmail(val.Email_Address) != null) return null;

            Users u = null;
            SqlConnection con = new SqlConnection(strCon);
            SqlCommand comm = new SqlCommand(
                $" INSERT INTO Users(email_address, pass, first_name, last_name) " +
                $" VALUES('{val.Email_Address}', '{val.Pass}', '{val.First_Name}', '{val.Last_Name}')", con);
            comm.Connection.Open();
            int res = comm.ExecuteNonQuery();
            comm.Connection.Close();
            if (res == 1)
            {
                u = GetUserByEmail(val.Email_Address);
            }
            return u;
        }



        public static int DeleteUserByEmail(string email_address)
        {
            string strComm =
                    $" DELETE Users " +
                    $" WHERE email_address={email_address}";

            return ExcNonQ(strComm);
        }


        private static int ExcNonQ(string comm2Run)
        {
            SqlConnection con = new SqlConnection(strCon);
            SqlCommand comm = new SqlCommand(comm2Run, con);
            comm.Connection.Open();
            int res = comm.ExecuteNonQuery();
            comm.Connection.Close();
            return res;
        }


        public static List<Users> ExcReader(string comm2Run)
        {
            List<Users> ul = new List<Users>();
            SqlConnection con = new SqlConnection(strCon);
            SqlCommand comm = new SqlCommand(comm2Run, con);
            comm.Connection.Open();
            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                Users u = new Users(
                    (string)reader["email_address"],
                    (string)reader["pass"],
                    (string)reader["first_name"],
                    (string)reader["last_name"]);
                ul.Add(u);
            }
            comm.Connection.Close();
            return ul;
        }
    }
}