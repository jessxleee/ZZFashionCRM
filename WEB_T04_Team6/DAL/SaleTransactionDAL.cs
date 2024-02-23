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
    public class SaleTransactionDAL
    {
        private IConfiguration Configuration { get; }
        private SqlConnection conn;

        //Constructor
        public SaleTransactionDAL()
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

        public List<SaleTransaction> GetAllSalesTransaction()
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify the SELECT SQL statement
            cmd.CommandText = @"SELECT MemberID, sum(Total) as 'Total' FROM SalesTransaction WHERE DATEPART(m,DateCreated) = DATEPART(m,DATEADD(m,-1,GETDATE())) GROUP BY MemberID ORDER BY Total DESC";
            //Open a database connection
            conn.Open();
            //Execute the SELECT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();

            //Read all records until the end, save data
            List<SaleTransaction> transactionList = new List<SaleTransaction>();
            while (reader.Read())
            {
                transactionList.Add(
                    new SaleTransaction
                    {
                        MemberID = !reader.IsDBNull(0) ? reader.GetString(0) : "n/a",
                        Total = reader.GetDecimal(1),
                    }
                );
            }
            //Close DataReader
            reader.Close();

            //Close the database connection
            conn.Close();

            return transactionList;
        }

        public SaleTransaction GetSpecificTransaction(string memberid)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();

            //Specify the SELECT SQL statement
            cmd.CommandText = @"SELECT MemberID, sum(Total) as 'Total' FROM SalesTransaction WHERE (DATEPART(m,DateCreated) = DATEPART(m,DATEADD(m,-1,GETDATE()))) And (MemberID = @MemberID) GROUP BY MemberID ORDER BY Total DESC";
            cmd.Parameters.AddWithValue("@MemberID", memberid);
            //Open a database connection
            conn.Open();
            //Execute the SELECT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();

            SaleTransaction transaction = new SaleTransaction();
            while (reader.Read())
            {

                transaction.MemberID = !reader.IsDBNull(0) ? reader.GetString(0) : string.Empty;
                transaction.Total = reader.GetDecimal(1);

            }

            //Close the database connection
            conn.Close();

            return transaction;
        }

        public void GetVoucher(SaleTransaction transaction)
        {
            int amount = 0;
            if (transaction.Total < 200)
            {
                amount = 0;
            }
            else if (transaction.Total < 500)
            {
                amount = 20;
            }
            else if (transaction.Total < 1000)
            {
                amount = 40;
            }
            else if (transaction.Total < 1500)
            {
                amount = 80;
            }
            else
            {
                amount = 160;
            }

            if (amount == 0) return;
            else
            {
                int times = amount / 20;

                int month = DateTime.Now.Month;
                month--;
                if (month == 0)
                    month = 11;
                int year = DateTime.Now.Year;
                if (month == 11)
                    year--;

                //Create a SqlCommand object from connection object
                SqlCommand cmd = conn.CreateCommand();

                for (int i = 0; i < times; i++)
                {
                    //Specify the Insert SQL statement
                    cmd.CommandText = @"INSERT INTO CashVoucher (MemberID, MonthIssuedFor, YearIssuedFor) VALUES(@MemberID, @MonthIssuedFor, @YearIssuedFor)";
                    cmd.Parameters.AddWithValue("@MemberID", transaction.MemberID);
                    cmd.Parameters.AddWithValue("@MonthIssuedFor", month);
                    cmd.Parameters.AddWithValue("@YearIssuedFor", year);

                    //Open a database connection
                    conn.Open();

                    //Execute the SELECT SQL through a DataReader
                    cmd.ExecuteNonQuery();

                    //Open a database connection
                    conn.Close();
                    cmd.Parameters.Clear();
                }
            }
        }


        public bool ValidateRepeat(string id)
        {
            bool status = false;

            int month = DateTime.Now.Month;
            month--;
            if (month == 0)
                month = 11;
            int year = DateTime.Now.Year;
            if (month == 11)
                year--;


            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();

            //Specify the Select SQL statement
            cmd.CommandText = @"SELECT * from CashVoucher where MemberID = @MemberID 
                                AND YearIssuedFor = @YearIssuedFor AND MonthIssuedFor = @MonthIssuedFor";
            cmd.Parameters.AddWithValue("@MemberID", id);
            cmd.Parameters.AddWithValue("@YearIssuedFor", year);
            cmd.Parameters.AddWithValue("@MonthIssuedFor", month);

            //Open a database connection
            conn.Open();
            //Execute SELCT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                status = true;
            }
            //Close data reader
            reader.Close();

            //Close database connection
            conn.Close();
            return status;

        }
    }
}

