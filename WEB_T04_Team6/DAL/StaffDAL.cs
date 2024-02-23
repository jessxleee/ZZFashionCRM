using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Data.SqlClient;
using WEB_T04_Team6.Models;

namespace WEB_T04_Team6.DAL
{
    public class StaffDAL
    {
        private IConfiguration Configuration { get; }
        private SqlConnection conn;

        //Constructor
        public StaffDAL()
        {
            //Read ConnectionString from appsettings.json file
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
            string strConn = Configuration.GetConnectionString(
            "ZZFashionConnectionString");
            //Instantiate a SqlConnection object with the
            //Connection String read.
            conn = new SqlConnection(strConn);
        }

        public List<Staff> GetAllStaff()
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify the SELECT SQL statement
            cmd.CommandText = @"SELECT * FROM Staff ORDER BY StaffID";
            //Open a database connection
            conn.Open();
            //Execute the SELECT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();

            //Read all records until the end, save data
            List<Staff> staffList = new List<Staff>();
            while (reader.Read())
            {
                staffList.Add(
                    new Staff
                    {
                        StaffID = reader.GetString(0), //0: 1st column
                        SName = reader.GetString(2), //1: 2nd column
                        SGender = reader.GetString(3), //2: 3rd column
                        SAppt = reader.GetString(4), //3: 4th column
                        STelNo = reader.GetString(5), //4: 5th column
                        SEmailAddr = reader.GetString(6), //5: 6th column
                        SPassword = reader.GetString(7), //6: 7th column
                        //if null value in db, assign integer null value
                        StoreID = !reader.IsDBNull(1) ? reader.GetString(1) : (string?)null,

                    }
                );
            }
            //Close DataReader
            reader.Close();

            //Close the database connection
            conn.Close();

            return staffList;
        }


        public Staff staffLogin(string username, string password)
        {
            Staff staff = new Staff();

            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();

            //Specify the SELECT SQL statement that
            //retrieves all attributes of a staff record.
            cmd.CommandText = @"SELECT * FROM Staff 
                                WHERE StaffID = @selectedStaffID AND SPassword = @selectedSPassword";

            //Define the parameter used in SQL statement, value for the
            //parameter is retrieved from the method parameter “staffId”.
            cmd.Parameters.AddWithValue("@selectedStaffID", username);
            cmd.Parameters.AddWithValue("@selectedSPassword", password);

            //Open a database connection
            conn.Open();

            //Execute SELCT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                //Read the record from database
                while (reader.Read())
                {
                    // Fill staff object with values from the data reader
                    staff.StaffID = reader.GetString(0);
                    staff.StoreID = !reader.IsDBNull(1) ?
                    reader.GetString(1) : string.Empty;
                    staff.SName = reader.GetString(2);
                    staff.SGender = reader.GetString(3);
                    staff.SAppt = reader.GetString(4);
                    staff.STelNo = reader.GetString(5);
                    staff.SEmailAddr = reader.GetString(6);
                    staff.SPassword = reader.GetString(7);
                }
            }
            //Close data reader
            reader.Close();

            //Close the database connection
            conn.Close();

            return staff;
        }
    }
}
