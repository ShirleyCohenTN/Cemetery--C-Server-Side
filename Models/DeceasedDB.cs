using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;


namespace CemeterySoul.Models
{
    public class DeceasedDB
    {
        static bool local = false;
        static string strCon = null;
        static string strConLocal = @"Data Source=LAPTOP-8GON88TL\SQLEXPRESS;Initial Catalog=CEMETERY1;Integrated Security=True";
        static string strConLIVEDNS = @"Data Source=185.60.170.14;Integrated Security=False;User ID=site08;Password=i*8qEb42;";

        static DeceasedDB()
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

        //LIST ALL
        public static List<Deceased> GetAllDeceased()
        {
            List<Deceased> dl = new List<Deceased>();
            SqlConnection con = new SqlConnection(strCon);
            SqlCommand comm = new SqlCommand("SELECT * FROM Deceased", con);
            comm.Connection.Open();
            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                Deceased d = new Deceased(
                    (int)reader["id"],
                    (string)reader["first_name"],
                    (string)reader["last_name"],
                    (string)reader["jewish_date_of_demise"],
                    //(DateTime)reader["gregorian_date_of_demise"],
                    (string)reader["gregorian_date_of_demise"],
                    (string)reader["city"],
                    (string)reader["cemetery_name"],
                    (int)reader["row_num"],
                    (int)reader["plot_num"],
                    (int)reader["block_num"],
                    (string)reader["date_of_burial"],
                    (string)reader["mother_first_name"],
                    (string)reader["father_first_name"]
                    //(decimal)reader["deceased_latitude"],
                    //(decimal)reader["deceased_longitude"]
                    );
                dl.Add(d);
            }
            comm.Connection.Close();
            return dl;
        }



        //LIST BY FIRST AND LAST NAME
        public static Deceased GetDeceasedByFirstAndLastName(string first_name, string last_name)
        {
            Deceased d = null;
            SqlConnection con = new SqlConnection(strCon);
            SqlCommand comm = new SqlCommand(
                $" SELECT * FROM Deceased " +
                $" WHERE Cast(first_name AS nvarchar(50) ) = N'{first_name}' AND Cast(last_name AS nvarchar(50) ) = N'{last_name}'", con);
            comm.Connection.Open();
            SqlDataReader reader = comm.ExecuteReader();
            if (reader.Read())
            {
                d = new Deceased(
                    (int)reader["id"],
                    (string)reader["first_name"],
                    (string)reader["last_name"],
                    (string)reader["jewish_date_of_demise"],
                    //(DateTime)reader["gregorian_date_of_demise"],
                    (string)reader["gregorian_date_of_demise"],
                    (string)reader["city"],
                    (string)reader["cemetery_name"],
                    (int)reader["row_num"],
                    (int)reader["plot_num"],
                    (int)reader["block_num"],
                    (string)reader["date_of_burial"],
                    (string)reader["mother_first_name"],
                    (string)reader["father_first_name"]
                    //(decimal)reader["deceased_latitude"],
                    //(decimal)reader["deceased_longitude"]
                    );
            }
            comm.Connection.Close();
            return d;
        }




        public static Deceased GetLanAndLong(int id)
        {
            Deceased d = null;
            SqlConnection con = new SqlConnection(strCon);
            SqlCommand comm = new SqlCommand(
                $" SELECT * FROM Deceased " +
                //$" WHERE Cast(first_name AS nvarchar(50) ) = N'{first_name}' AND Cast(last_name AS nvarchar(50) ) = N'{last_name}'", con);
                $" WHERE id ={ id }", con);

            comm.Connection.Open();
            SqlDataReader reader = comm.ExecuteReader();
            if (reader.Read())
            {
                d = new Deceased(
                    (int)reader["id"],
                    (decimal)reader["deceased_latitude"],
                    (decimal)reader["deceased_longitude"]
                    );
            }
            comm.Connection.Close();
            return d;
        }


        //BY ID
        public static Deceased GetDeceasedByID(int id)
        {
            Deceased d = null;
            SqlConnection con = new SqlConnection(strCon);
            SqlCommand comm = new SqlCommand(
                $" SELECT * FROM Deceased " +
                $" WHERE id={id}", con);
            comm.Connection.Open();
            SqlDataReader reader = comm.ExecuteReader();
            if (reader.Read())
            {
                d = new Deceased(
                    (int)reader["id"],
                    (string)reader["first_name"],
                    (string)reader["last_name"],
                    (string)reader["jewish_date_of_demise"],
                    //(DateTime)reader["gregorian_date_of_demise"],
                    (string)reader["gregorian_date_of_demise"],
                    (string)reader["city"],
                    (string)reader["cemetery_name"],
                    (int)reader["row_num"],
                    (int)reader["plot_num"],
                    (int)reader["block_num"],
                    (string)reader["date_of_burial"],
                    (string)reader["mother_first_name"],
                    (string)reader["father_first_name"]
                    //(decimal)reader["deceased_latitude"],
                    //(decimal)reader["deceased_longitude"]

                    );
            }
            comm.Connection.Close();
            return d;
        }



        //public static Deceased InsertDeceasedToDb(Deceased val)
        //{
        //    Deceased d = null;
        //    SqlConnection con = new SqlConnection(strCon);
        //    SqlCommand comm = new SqlCommand(
        //        $" INSERT INTO Deceased(email_address, pass, first_name, last_name) " +
        //        $" VALUES('{val.Email_Address}', '{val.Pass}', '{val.First_Name}', '{val.Last_Name}')", con);
        //    comm.Connection.Open();
        //    int res = comm.ExecuteNonQuery();
        //    comm.Connection.Close();
        //    if (res == 1)
        //    {
        //        u = GetUserByEmail(val.Email_Address);
        //    }
        //    return u;
        //}


        //INSERT
        public static int InsertDeceased(Deceased val)
        {
            string strComm =
                $" INSERT INTO Deceased(first_name, last_name, jewish_date_of_demise, gregorian_date_of_demise, city, cemetery_name, row_num, plot_num, block_num, date_of_burial, mother_first_name, father_first_name) VALUES(" +
                $" '{val.First_Name}'," +
                $" '{val.Last_Name}'," +
                $" '{val.Jewish_Date_Of_Demise}'," +
                $" '{val.Gregorian_Date_Of_Demise}'," +
                $" '{val.City}'," +
                $" '{val.Cemetery_Name}'," +
                $" '{val.Row_Num}'," +
                $" '{val.Plot_Num}'," +
                $" '{val.Block_Num}'," +
                $" '{val.Date_Of_Burial}'," +
                $" '{val.Mother_First_Name}'," +
                $" '{val.Father_First_Name}'); ";

            strComm +=
                " SELECT SCOPE_IDENTITY() AS[SCOPE_IDENTITY]; ";

            return ExcReaderInsertDeceased(strComm);
        }


        public static int ExcReaderInsertDeceased(string comm2Run)
        {
            int DeceasedID = -1;
            SqlConnection con = new SqlConnection(strCon);
            SqlCommand comm = new SqlCommand(comm2Run, con);
            comm.Connection.Open();
            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                DeceasedID = int.Parse(reader["SCOPE_IDENTITY"].ToString());
            }
            comm.Connection.Close();
            return DeceasedID;
        }


        //DELETE
        public static int DeleteDeceasedByID(int id)
        {
            string strComm =
                    $" DELETE Deceased " +
                    $" WHERE id={id}";

            return ExcNonQ(strComm);
        }


        //UPDATE
        public static int UpdateDeceased(Deceased d)
        {
            string strComm =
                  $" UPDATE Deceased SET " +
                  $" first_name='{d.First_Name}' , " +
                  $" last_name='{d.Last_Name}' , " +
                  $" jewish_date_of_demise='{d.Jewish_Date_Of_Demise}' , " +
                  $" gregorian_date_of_demise='{d.Gregorian_Date_Of_Demise}' , " +//in the end i did  put ''
                  $" city='{d.City}' , " +
                  $" cemetery_name='{d.Cemetery_Name}' , " +
                  $" row_num={d.Row_Num} , " +
                  $" plot_num={d.Plot_Num} , " +
                  $" block_num={d.Block_Num} , " +
                  $" date_of_burial='{d.Date_Of_Burial}' , " +
                  $" mother_first_name='{d.Mother_First_Name}' , " +
                  $" father_first_name='{d.Father_First_Name}' " +
                  $" WHERE id={d.ID}";

            return ExcNonQ(strComm);
        }




        //UPDATE LAT AND LONG
        public static int UpdateLatAndLong(Deceased d)
        {
            string strComm =
                  $" UPDATE Deceased SET " +
                  $" deceased_latitude={d.Deceased_Latitude} , " +
                  $" deceased_longitude={d.Deceased_Longitude}  " +
         
                  $" WHERE id={d.ID}";

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


        //public static List<Users> ExcReader(string comm2Run)
        //{
        //    List<Users> ul = new List<Users>();
        //    SqlConnection con = new SqlConnection(strCon);
        //    SqlCommand comm = new SqlCommand(comm2Run, con);
        //    comm.Connection.Open();
        //    SqlDataReader reader = comm.ExecuteReader();
        //    while (reader.Read())
        //    {
        //        Users u = new Users(
        //            (string)reader["email_address"],
        //            (string)reader["pass"],
        //            (string)reader["first_name"],
        //            (string)reader["last_name"]);
        //        ul.Add(u);
        //    }
        //    comm.Connection.Close();
        //    return ul;
        //}
    }
}