using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CemeterySoul.Models
{
    public class Deceased
    {
        public int ID { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Jewish_Date_Of_Demise { get; set; }
        //public DateTime Gregorian_Date_Of_Demise { get; set; }
        public string Gregorian_Date_Of_Demise { get; set; }

        public string City { get; set; }
        public string Cemetery_Name { get; set; }
        public int Row_Num { get; set; }
        public int Plot_Num { get; set; }
        public int Block_Num { get; set; }
        public string Date_Of_Burial { get; set; }
        public string Mother_First_Name { get; set; }
        public string Father_First_Name { get; set; }
        public string GPS_Location { get; set; }
        public string Link_To_Video_Pictures { get; set; }
        public decimal Deceased_Latitude { get; set; }
        public decimal Deceased_Longitude { get; set; }


        public Deceased() { }

        public Deceased(int id,
            string first_name,
            string last_name,
            string jewish_date_of_demise,
            //DateTime gregorian_date_of_demise,
            string gregorian_date_of_demise,
            string city,
            string cemetery_name,
            int row_num,
            int plot_num,
            int block_num,
            string date_of_burial,
            string mother_first_name,
            string father_first_name
            //decimal deceased_latitude,
            //decimal deceased_longitude
            )
        {
            ID = id;
            First_Name = first_name;
            Last_Name = last_name;
            Jewish_Date_Of_Demise = jewish_date_of_demise;
            Gregorian_Date_Of_Demise = gregorian_date_of_demise;
            City = city;
            Cemetery_Name = cemetery_name;
            Row_Num = row_num;
            Plot_Num = plot_num;
            Block_Num = block_num;
            Date_Of_Burial = date_of_burial;
            Mother_First_Name = mother_first_name;
            Father_First_Name = father_first_name;
            //Deceased_Latitude = deceased_latitude;
            //Deceased_Longitude = deceased_longitude;
        }


        public Deceased(int id, decimal deceased_latitude , decimal deceased_longitude)
        {
            ID = id;
            Deceased_Latitude = deceased_latitude;
            Deceased_Longitude = deceased_longitude;
        }


        public override string ToString()
        {
            return $"{ID}, {First_Name}, {Last_Name}, {Jewish_Date_Of_Demise}, {Gregorian_Date_Of_Demise}, {City}, {Cemetery_Name}, {Row_Num}, {Plot_Num}, {Block_Num}, {Date_Of_Burial}, {Mother_First_Name}, {Father_First_Name}";

        }



    }
}