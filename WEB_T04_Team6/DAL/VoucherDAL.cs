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
    public class VoucherDAL
    {
        private IConfiguration Configuration { get; }
        private SqlConnection conn;
        //Constructor
        public VoucherDAL()
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
        public List<Voucher> GetAllCashVoucher()
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify the SELECT SQL statement 
            cmd.CommandText = @"SELECT * FROM CashVoucher ORDER BY IssuingID";
            //Open a database connection
            conn.Open();
            //Execute the SELECT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();
            List<Voucher> voucherList = new List<Voucher>();
            while (reader.Read())
            {
                voucherList.Add(
                new Voucher
                {
                    IssuingID = reader.GetInt32(0), 
                    MemberID = reader.GetString(1), 
                    Amount = reader.GetDecimal(2), 
                    MonthIssuedFor = reader.GetInt32(3),  
                    YearIssuedFor = reader.GetInt32(4),
                    DateTimeIssued = reader.GetDateTime(5), 
                    VoucherSN = !reader.IsDBNull(6) ?
                                reader.GetString(6) : (string?)null,
                    Status = reader.GetString(7), 
                    DateTimeRedeemed = !reader.IsDBNull(8) ?
                                        reader.GetDateTime(8) : (DateTime?)null,
                }
                );
            }
            reader.Close();
            conn.Close();
            return voucherList;
        }
        public List<Voucher> GetCashVoucher()
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT * FROM CashVoucher WHERE Status = '0'";
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            List<Voucher> voucherList = new List<Voucher>();
            while (reader.Read())
            {
                voucherList.Add(
                    new Voucher
                    {
                        IssuingID = reader.GetInt32(0),
                        MemberID = reader.GetString(1),
                        Amount = reader.GetDecimal(2),
                        MonthIssuedFor = reader.GetInt32(3),
                        YearIssuedFor = reader.GetInt32(4),
                        DateTimeIssued = reader.GetDateTime(5),
                        VoucherSN = !reader.IsDBNull(6) ?
                                reader.GetString(6) : (string?)null,
                        Status = reader.GetString(7),
                        DateTimeRedeemed = !reader.IsDBNull(8) ?
                                        reader.GetDateTime(8) : (DateTime?)null,

                    }
                  );
            }
            reader.Close();
            conn.Close();
            return voucherList;
        }
        public Voucher GetDetails(int id)
        {
            Voucher voucher = new Voucher();
            SqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = @"SELECT * FROM CashVoucher WHERE IssuingID = @selectedID";
            cmd.Parameters.AddWithValue("@selectedID", id);
            //Open a database connection
            conn.Open();
            //Execute SELCT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                //Read the record from database
                while (reader.Read())
                {
                    voucher.IssuingID = id;
                    voucher.MemberID = !reader.IsDBNull(1) ?
                                    reader.GetString(1) : string.Empty;
                    voucher.Amount = !reader.IsDBNull(2) ?
                                     reader.GetDecimal(2) : (Decimal)0.00;
                    voucher.MonthIssuedFor = !reader.IsDBNull(3) ?
                                        reader.GetInt32(3) : 0;
                    voucher.YearIssuedFor = !reader.IsDBNull(4) ?
                                       reader.GetInt32(4) : 0;
                    voucher.DateTimeIssued = !reader.IsDBNull(5) ?
                                       reader.GetDateTime(5) : DateTime.Today;
                    voucher.VoucherSN = !reader.IsDBNull(6) ?
                                    reader.GetString(6) : (string?)null;
                    voucher.Status = !reader.IsDBNull(7) ?
                                   reader.GetString(7) : string.Empty;
                    voucher.DateTimeRedeemed = !reader.IsDBNull(8) ?
                                                reader.GetDateTime(8) : (DateTime?) null;
                }
            }
            //Close data reader
            reader.Close();
            //Close database connection
            conn.Close();
            return voucher;
        }
        public int Update(Voucher voucher)
        {
            SqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = @"UPDATE CashVoucher SET VoucherSN = @serialno, Status = @status WHERE IssuingID = @selectedid";

            cmd.Parameters.AddWithValue("@serialno", voucher.VoucherSN);
            cmd.Parameters.AddWithValue("@status", voucher.Status[0]);
            cmd.Parameters.AddWithValue("@selectedid", voucher.IssuingID);
            //Open a database connection
            conn.Open();

            //ExecuteNonQuery is used for UPDATE and DELETE
            int count = cmd.ExecuteNonQuery();
            //Close the database connection
            conn.Close();

            return count;
        }

        public Voucher GetMemberDetails(int id)
        {
            Voucher voucher = new Voucher();
            SqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = @"SELECT * FROM CashVoucher WHERE IssuingID = @selectedID AND MemeberID = @selectedmemberID";
            cmd.Parameters.AddWithValue("@selectedID", id);
            cmd.Parameters.AddWithValue("@selectedmemberID", voucher.MemberID);
            //Open a database connection
            conn.Open();
            //Execute SELCT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                //Read the record from database
                while (reader.Read())
                {
                    voucher.IssuingID = id;
                    voucher.MemberID = !reader.IsDBNull(1) ?
                                    reader.GetString(1) : string.Empty;
                    voucher.Amount = !reader.IsDBNull(2) ?
                                     reader.GetDecimal(2) : (Decimal)0.00;
                    voucher.MonthIssuedFor = !reader.IsDBNull(3) ?
                                        reader.GetInt32(3) : 0;
                    voucher.YearIssuedFor = !reader.IsDBNull(4) ?
                                       reader.GetInt32(4) : 0;
                    voucher.DateTimeIssued = !reader.IsDBNull(5) ?
                                       reader.GetDateTime(5) : DateTime.Today;
                    voucher.VoucherSN = !reader.IsDBNull(6) ?
                                    reader.GetString(6) : (string?)null;
                    voucher.Status = !reader.IsDBNull(7) ?
                                   reader.GetString(7) : string.Empty;
                    voucher.DateTimeRedeemed = !reader.IsDBNull(8) ?
                                                reader.GetDateTime(8) : (DateTime?)null;
                }
            }
            //Close data reader
            reader.Close();
            //Close database connection
            conn.Close();
            return voucher;
        }

        public int Collect(Voucher voucher)
        {
            SqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = @"UPDATE CashVoucher SET VoucherSN = @serialno, Status = 1 WHERE IssuingID = @id";

            cmd.Parameters.AddWithValue("@serialno", voucher.VoucherSN);
            cmd.Parameters.AddWithValue("@id", voucher.IssuingID);
            //Open a database connection
            conn.Open();

            int count = cmd.ExecuteNonQuery();
            conn.Close();

            return count;
        }
        public int Redeem(Voucher voucher)
        {
            SqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = @"UPDATE CashVoucher SET Status = 2, DateTimeRedeemed = @datetime WHERE IssuingID = @id";

            cmd.Parameters.AddWithValue("@datetime", DateTime.Now);
            cmd.Parameters.AddWithValue("@id", voucher.IssuingID);
            //Open a database connection
            conn.Open();
            int count = cmd.ExecuteNonQuery();
            conn.Close();

            return count;
        }
        public bool Redeemable(string vouchersn)
        {
            bool redeemable = false;
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT * FROM CashVoucher WHERE DATEDIFF(MONTH, DateTimeIssued, GETDATE()) < 12";
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            List<Voucher> voucherList = new List<Voucher>();
            while (reader.Read())
            {
                voucherList.Add(
                new Voucher
                {
                    IssuingID = reader.GetInt32(0),
                    MemberID = reader.GetString(1),
                    Amount = reader.GetDecimal(2),
                    MonthIssuedFor = reader.GetInt32(3),
                    YearIssuedFor = reader.GetInt32(4),
                    DateTimeIssued = reader.GetDateTime(5),
                    VoucherSN = !reader.IsDBNull(6) ?
                                reader.GetString(6) : (string?)null,
                    Status = reader.GetString(7),
                    DateTimeRedeemed = !reader.IsDBNull(8) ?
                                        reader.GetDateTime(8) : (DateTime?)null,
                }
                );
            }
            reader.Close();
            conn.Close();
            for (int i = 0; i < voucherList.Count; i++)
            {
                if (voucherList[i].VoucherSN == vouchersn)
                {
                    redeemable = true;
                }
            }
            return redeemable;
        }
        public Voucher GetSelectedCashVoucher(string VoucherSN)
        {
            Voucher voucher = new Voucher();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT * FROM CashVoucher WHERE VoucherSN = @serialno";
            cmd.Parameters.AddWithValue("@serialno", VoucherSN);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                voucher.IssuingID = !reader.IsDBNull(0) ?
                                reader.GetInt32(0) : 0;
                voucher.MemberID = !reader.IsDBNull(1) ?
                                reader.GetString(1) : string.Empty;
                voucher.Amount = !reader.IsDBNull(2) ?
                                 reader.GetDecimal(2) : (Decimal)0.00;
                voucher.MonthIssuedFor = !reader.IsDBNull(3) ?
                                    reader.GetInt32(3) : 0;
                voucher.YearIssuedFor = !reader.IsDBNull(4) ?
                                   reader.GetInt32(4) : 0;
                voucher.DateTimeIssued = !reader.IsDBNull(5) ?
                                   reader.GetDateTime(5) : DateTime.Today;
                voucher.VoucherSN = !reader.IsDBNull(6) ?
                                reader.GetString(6) : (string?)null;
                voucher.Status = !reader.IsDBNull(7) ?
                               reader.GetString(7) : string.Empty;
                voucher.DateTimeRedeemed = !reader.IsDBNull(8) ?
                                            reader.GetDateTime(8) : (DateTime?)null;
            }
            conn.Close();
            return voucher;
        }

    }

}
