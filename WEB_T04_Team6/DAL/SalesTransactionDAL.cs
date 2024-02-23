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
    public class SalesTransactionDAL
    {
        private IConfiguration Configuration { get; }
        private SqlConnection conn;

        //Constructor
        public SalesTransactionDAL()
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
            cmd.CommandText = @"SELECT * FROM SalesTransaction ORDER BY TransactionID";
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
                        TransactionID = reader.GetInt32(0),
                        StoreID = reader.GetString(1), 
                        MemberID = !reader.IsDBNull(2) ? reader.GetString(2) : string.Empty, 
                        SubTotal = reader.GetDecimal(3), 
                        Tax = reader.GetFloat(4),
                        DiscountPercent = reader.GetDecimal(5),
                        DiscountAmt = reader.GetDecimal(6),
                        Total = reader.GetDecimal(7),
                        DateCreated = reader.GetDateTime(8)
                    }
                );
            }
            //Close DataReader
            reader.Close();

            //Close the database connection
            conn.Close();

            return transactionList;
        }

    }
}

