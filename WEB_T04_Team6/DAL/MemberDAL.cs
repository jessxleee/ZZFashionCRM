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
    public class MemberDAL
    {
        private IConfiguration Configuration { get; }
        private SqlConnection conn;
        
        //Constructor
        public MemberDAL()
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
        
        public List<Member> GetAllMember()
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            
            //Specify the SELECT SQL statement
            cmd.CommandText = @"SELECT * FROM Customer ORDER BY MemberID";
            
            //Open a database connection
            conn.Open();

            //Execute the SELECT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();
            //Read all records until the end, save data into a staff list
            List<Member> memberList = new List<Member>();
            while (reader.Read())
            {
                memberList.Add(
                new Member
                {
                    MemberID = reader.GetString(0), 
                    MName = reader.GetString(1),
                    MGender = reader.GetString(2)[0],
                    MBirthDate = reader.GetDateTime(3), 
                    MAddress = !reader.IsDBNull(4) ?
                                reader.GetString(4) : (string?)null,
                    MCountry = reader.GetString(5),
                    MTelNo = !reader.IsDBNull(6) ?
                                reader.GetString(6) : (string?)null,
                    MEmailAddr = !reader.IsDBNull(7) ?
                                reader.GetString(7) : (string?)null,
                    MPassword = reader.GetString(8)
                }
                );
            }

            //Close DataReader
            reader.Close();

            //Close the database connection
            conn.Close();
            return memberList;
        }

        public Member GetSelectedMember(string id)
        {
            Member member = new Member();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT * FROM Customer WHERE MemberID = @selectedID";
            cmd.Parameters.AddWithValue("@selectedID", id);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    member.MemberID = reader.GetString(0);
                    member.MName = reader.GetString(1);
                    member.MGender = reader.GetString(2)[0];
                    member.MBirthDate = reader.GetDateTime(3);
                    member.MAddress = !reader.IsDBNull(4) ?
                                reader.GetString(4) : (string?)null;
                    member.MCountry = reader.GetString(5);
                    member.MTelNo = !reader.IsDBNull(6) ?
                                reader.GetString(6) : (string?)null;
                    member.MEmailAddr = !reader.IsDBNull(7) ?
                                reader.GetString(7) : (string?)null;
                    member.MPassword = reader.GetString(8);

                }
            }
            reader.Close();
            conn.Close();
            return member;
        }


        public Member memberLogin(string username, string password)
        {
            Member member = new Member();

            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();

            //Specify the SELECT SQL statement that
            //retrieves all attributes of a staff record.
            cmd.CommandText = @"SELECT * FROM Customer 
                                WHERE MemberID = @selectedMemberID AND MPassword = @selectedMPassword";

            //Define the parameter used in SQL statement, value for the
            //parameter is retrieved from the method parameter “staffId”.
            cmd.Parameters.AddWithValue("@selectedMemberID", username);
            cmd.Parameters.AddWithValue("@selectedMPassword", password);

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
                    member.MemberID = reader.GetString(0);
                    member.MName = reader.GetString(1);
                    member.MGender = reader.GetString(2)[0];
                    member.MBirthDate = reader.GetDateTime(3);
                    member.MAddress = !reader.IsDBNull(4) ?
                    reader.GetString(4) : string.Empty;
                    member.MCountry = reader.GetString(5);
                    member.MTelNo = !reader.IsDBNull(6) ?
                    reader.GetString(6) : string.Empty;
                    member.MEmailAddr = !reader.IsDBNull(7) ?
                    reader.GetString(7) : string.Empty;
                    member.MPassword = reader.GetString(8);
                }
            }
            //Close data reader
            reader.Close();

            //Close the database connection
            conn.Close();

            return member;
        }
        public string Add(Member member)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify an INSERT SQL statement which will
            //return the auto-generated StaffID after insertion
            cmd.CommandText = @"INSERT INTO Customer (MemberID, MName, MGender, MBirthDate, 
                                MAddress, MCountry, MTelNo, MEmailAddr) 
                                VALUES(@id, @name, @gender, @dob, @address,
                                @country, @telno, @email)";
            //Define the parameters used in SQL statement, value for each parameter
            //is retrieved from respective class's property.
            cmd.Parameters.AddWithValue("@id", member.MemberID);
            cmd.Parameters.AddWithValue("@name", member.MName);
            cmd.Parameters.AddWithValue("@gender", member.MGender);
            cmd.Parameters.AddWithValue("@dob", member.MBirthDate);
            if (String.IsNullOrEmpty(member.MAddress))
            {
                cmd.Parameters.AddWithValue("@address", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@address", member.MAddress);
            }
            cmd.Parameters.AddWithValue("@country", member.MCountry);
            if (String.IsNullOrEmpty(member.MTelNo))
            {
                cmd.Parameters.AddWithValue("@telno", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@telno", member.MTelNo);
            }
            if (String.IsNullOrEmpty(member.MEmailAddr))
            {
                cmd.Parameters.AddWithValue("@email", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@email", member.MEmailAddr);
            }
            conn.Open();
            cmd.ExecuteScalar();
            //A connection should be closed after operations.
            conn.Close();
            //Return id when no error occurs.
            return member.MemberID;
        }
        public bool IsTelNoExist(string telno, string memberId)
        {
            bool telNoFound = false;
            //Create a SqlCommand object and specify the SQL statement 
            //to get a staff record with the email address to be validated
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT MemberID FROM Customer WHERE MTelNo=@selectedno";
            cmd.Parameters.AddWithValue("@selectedno", telno);
            //Open a database connection and execute the SQL statement
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            { //Records found
                while (reader.Read())
                {
                    if (reader.GetString(0) != memberId)
                        //The email address is used by another staff
                        telNoFound = true;
                }
            }
            else
            { //No record
                telNoFound = false; // The email address given does not exist
            }
            reader.Close();
            conn.Close();
            return telNoFound;
        }

        public bool IsEmailExist(string email, string memberId)
        {
            bool emailFound = false;
            //Create a SqlCommand object and specify the SQL statement 
            //to get a staff record with the email address to be validated
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT MemberID FROM Customer WHERE MEmailAddr = @selectedEmail";
            cmd.Parameters.AddWithValue("@selectedEmail", email);
            //Open a database connection and execute the SQL statement
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            { //Records found
                while (reader.Read())
                {
                    if (reader.GetString(0) != memberId)
                        //The email address is used by another staff
                        emailFound = true;
                }
            }
            else
            { //No record
                emailFound = false; // The email address given does not exist
            }
            reader.Close();
            conn.Close();
            return emailFound;
        }
        public Member GetDetails(string memberID)
        {
            Member member = new Member();
            SqlCommand cmd = conn.CreateCommand();
           
            cmd.CommandText = @"SELECT * FROM Customer WHERE MemberID = @selectedID";
            cmd.Parameters.AddWithValue("@selectedID", memberID);
            //Open a database connection
            conn.Open();
            //Execute SELCT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                //Read the record from database
                while (reader.Read())
                {
                    // Fill member object with values from the data reader 
                    member.MemberID = memberID;
                    member.MName = !reader.IsDBNull(1) ?
                                    reader.GetString(1) : string.Empty;
                    member.MGender = !reader.IsDBNull(2) ?
                                        reader.GetString(2)[0] : (char)0;
                    member.MBirthDate = !reader.IsDBNull(3) ?
                                        reader.GetDateTime(3): DateTime.Today;
                    member.MAddress = !reader.IsDBNull(4) ?
                                       reader.GetString(4) : (string?)null;
                    member.MCountry = !reader.IsDBNull(5) ?
                                       reader.GetString(5) : string.Empty;
                    member.MTelNo = !reader.IsDBNull(6) ?
                                    reader.GetString(6) : (string?)null;
                    member.MEmailAddr = !reader.IsDBNull(7) ?
                                   reader.GetString(7) : (string?)null;
                }
            }
            //Close data reader
            reader.Close();
            //Close database connection
            conn.Close();
            return member;
        }

        public Member MemberDetails(string memberID)
        {
            Member member = new Member();
            SqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = @"SELECT * FROM Customer WHERE MemberID = @selectedID";
            cmd.Parameters.AddWithValue("@selectedID", memberID);
            //Open a database connection
            conn.Open();
            //Execute SELCT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                //Read the record from database
                while (reader.Read())
                {
                    // Fill member object with values from the data reader 
                    member.MemberID = memberID;
                    member.MAddress = !reader.IsDBNull(4) ?
                                       reader.GetString(4) : (string?)null;
                    member.MTelNo = !reader.IsDBNull(6) ?
                                    reader.GetString(6) : (string?)null;
                    member.MEmailAddr = !reader.IsDBNull(7) ?
                                   reader.GetString(7) : (string?)null;
                    member.MPassword = !reader.IsDBNull(8) ?
                                   reader.GetString(8) : string.Empty;
                }
            }
            //Close data reader
            reader.Close();
            //Close database connection
            conn.Close();
            return member;
        }
        public List<string> GetMemberID()
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT MemberID FROM Customer";
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            List<string> memberidList = new List<string>();
            while (reader.Read())
            {
                memberidList.Add(
              
                  reader.GetString(0)
                                
                );
            }
            reader.Close();
            conn.Close();
            return memberidList;

        }
        public int UpdateMember(Member member)
        {
            //Member member = new Member();
            SqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = @"UPDATE Customer SET MAddress = @mAddress, MEmailAddr = @mEmailAddr, MTelNo = @mTelNo, MPassword = @mpassword
                                WHERE MemberID = @selectedMemberID";

            cmd.Parameters.AddWithValue("@mAddress", member.MAddress);
            cmd.Parameters.AddWithValue("@mEmailAddr", member.MEmailAddr);
            cmd.Parameters.AddWithValue("@mTelNo", member.MTelNo);
            cmd.Parameters.AddWithValue("@mpassword", member.MPassword);
            cmd.Parameters.AddWithValue("@selectedMemberID", member.MemberID);

            //Open a database connection
            conn.Open();

            //ExecuteNonQuery is used for UPDATE and DELETE
            int count = cmd.ExecuteNonQuery();
            
            //Close the database connection
            conn.Close();

            return count;
        }

    }
}
