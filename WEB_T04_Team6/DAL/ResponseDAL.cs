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
    public class ResponseDAL
    {
        private IConfiguration Configuration { get; }
        private SqlConnection conn;

        //Constructor
        public ResponseDAL()
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

        public List<Response> GetAllResponse()
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify the SELECT SQL statement
            cmd.CommandText = @"SELECT * FROM Response ORDER BY ResponseID";
            //Open a database connection
            conn.Open();
            //Execute the SELECT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();

            //Read all records until the end, save data into a feedback list
            List<Response> responseList = new List<Response>();
            while (reader.Read())
            {
                responseList.Add(
                    new Response
                    {
                        ResponseID = reader.GetInt32(0), 
                        FeedbackID = reader.GetInt32(1),
                        DateTimePosted = reader.GetDateTime(4), 
                        Text = reader.GetString(5), 
                        //if null value in db, assign integer null value
                        MemberID = !reader.IsDBNull(2) ? reader.GetString(2) : (string?)null,
                        StaffID = !reader.IsDBNull(3) ? reader.GetString(3) : (string?)null,
                    }
                );
            }
            //Close DataReader
            reader.Close();

            //Close the database connection
            conn.Close();

            return responseList;
        }

        public List<Response> GetMemberResponse(string memberID)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify the SELECT SQL statement
            cmd.CommandText = @"SELECT * FROM Response WHERE MemberID = @selectedmemberID";
            cmd.Parameters.AddWithValue("@selectedmemberID", memberID);
            //Open a database connection
            conn.Open();
            //Execute the SELECT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();

            //Read all records until the end, save data into a feedback list
            List<Response> responseList = new List<Response>();
            while (reader.Read())
            {
                responseList.Add(
                    new Response
                    {
                        ResponseID = reader.GetInt32(0),
                        FeedbackID = reader.GetInt32(1),
                        DateTimePosted = reader.GetDateTime(4),
                        Text = reader.GetString(5),
                        //if null value in db, assign integer null value
                        MemberID = !reader.IsDBNull(2) ? reader.GetString(2) : (string?)null,
                        StaffID = !reader.IsDBNull(3) ? reader.GetString(3) : (string?)null,
                    }
                );
            }
            //Close DataReader
            reader.Close();

            //Close the database connection
            conn.Close();

            return responseList;
        }

        public void PostResponse(Response response)
        {
            SqlCommand cmd = conn.CreateCommand();
            //Specify the SELECT SQL statement
            cmd.CommandText = @"INSERT INTO Response (FeedbackID, StaffID, Text) VALUES (@Feedbackid, @Staffid, @text)";
            cmd.Parameters.AddWithValue("@Feedbackid", response.FeedbackID);
            cmd.Parameters.AddWithValue("@Staffid", response.StaffID);
            cmd.Parameters.AddWithValue("@text", response.Text);
            //Open a database connection
            conn.Open();
            //Execute the SELECT SQL through a DataReader
            cmd.ExecuteNonQuery();

            //Close the database connection
            conn.Close();

        }

        public Response GetDetails(int responseId)
        {
            Response response = new Response();

            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();

            //Specify the SELECT SQL statement that
            //retrieves all attributes of a staff record.
            cmd.CommandText = @"SELECT * FROM Response 
                                WHERE ResponseID = @selectedResponseID";

            //Define the parameter used in SQL statement, value for the
            //parameter is retrieved from the method parameter “memberId”.
            cmd.Parameters.AddWithValue("@selectedResponseID", responseId);

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
                    //C-sharp condition statement
                    response.ResponseID = responseId;
                    response.FeedbackID = !reader.IsDBNull(1) ?
                    reader.GetInt32(1) : 1;
                    
                    //feedback.DateTimePosted = (DateTime)reader.IsDBNull(1) ?
                    //reader.GetDateTime(1) : (DateTime?)null;
                    response.MemberID = !reader.IsDBNull(2) ?
                    reader.GetString(2) : string.Empty;
                    response.StaffID = !reader.IsDBNull(3) ?
                    reader.GetString(3) : string.Empty;
                    response.Text = !reader.IsDBNull(5) ?
                    reader.GetString(5) : string.Empty;
                    response.DateTimePosted = !reader.IsDBNull(4) ?
                    reader.GetDateTime(4) : DateTime.Today;

                }
            }
            //Close data reader
            reader.Close();

            //Close database connection
            conn.Close();

            return response;
        }

        //public List<Response> GetFeedbackResponse(int responseid)
        //{
        //    //Create a SqlCommand object from connection object
        //    SqlCommand cmd = conn.CreateCommand();

        //    //Specify the SQL statement that select all branches
        //    cmd.CommandText = @"SELECT * FROM Response WHERE ResponseID = @selectedFeedback";

        //    //Define the parameter used in SQL statement, value for the
        //    //parameter is retrieved from the method parameter “branchNo”.
        //    cmd.Parameters.AddWithValue("@selectedFeedback", responseid);

        //    //Open a database connection
        //    conn.Open();

        //    //Execute SELCT SQL through a DataReader
        //    SqlDataReader reader = cmd.ExecuteReader();

        //    List<Response> responseList = new List<Response>();
        //    while (reader.Read())
        //    {
        //        responseList.Add(
        //            new Response
        //            {
        //                ResponseID = reader.GetInt32(0), 
        //                FeedbackID = reader.GetInt32(1), 
        //                //Get the first character of a string
        //                MemberID = !reader.IsDBNull(2) ? reader.GetString(2) : string.Empty, 
        //                StaffID = !reader.IsDBNull(3) ? reader.GetString(3) : string.Empty,
        //                DateTimePosted = reader.GetDateTime(4), 
        //                Text = reader.GetString(5),
        //            }
        //        );
        //    }
        //    //Close DataReader
        //    reader.Close();

        //    //Close database connection
        //    conn.Close();

        //    return responseList;
        //}
    }
}
