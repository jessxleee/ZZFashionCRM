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
    public class FeedbackDAL
    {
        private IConfiguration Configuration { get; }
        private SqlConnection conn;

        //Constructor
        public FeedbackDAL()
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

        public List<Feedback> GetAllFeedback()
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify the SELECT SQL statement
            cmd.CommandText = @"SELECT * FROM Feedback ORDER BY DateTimePosted DESC";
            //Open a database connection
            conn.Open();
            //Execute the SELECT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();

            //Read all records until the end, save data into a feedback list
            List<Feedback> feedbackList = new List<Feedback>();
            while (reader.Read())
            {
                feedbackList.Add(
                    new Feedback
                    {
                        FeedbackID = reader.GetInt32(0), //0: 1st column
                        MemberID = reader.GetString(1), //1: 2nd column
                        DateTimePosted = reader.GetDateTime(2), //2: 4th column
                        Title = reader.GetString(3), //3: 4th column
                        //if null value in db, assign integer null value
                        Text = !reader.IsDBNull(4) ? reader.GetString(4) : (string?)null,
                        ImageFileName = !reader.IsDBNull(5) ? reader.GetString(5) : (string?)null,
                    }
                );
            }
            //Close DataReader
            reader.Close();

            //Close the database connection
            conn.Close();

            return feedbackList;
        }

        public List<Feedback> GetMemberFeedback(string memberID)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();

            //Specify the SQL statement that select all memberID
            cmd.CommandText = @"SELECT * FROM Feedback WHERE MemberID = @selectedmemberID";
            cmd.Parameters.AddWithValue("@selectedmemberID", memberID);

            //Open a database connection
            conn.Open();

            //Execute SELCT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();

            List<Feedback> feedbackList = new List<Feedback>();
            while (reader.Read())
            {
                feedbackList.Add(
                new Feedback
                {
                    FeedbackID = reader.GetInt32(0), //0: 1st column
                    MemberID = reader.GetString(1), //1: 2nd column
                    DateTimePosted = reader.GetDateTime(2), //2: 4th column
                    Title = reader.GetString(3), //3: 4th column
                    Text = !reader.IsDBNull(4) ? reader.GetString(4) : (string?)null,
                    ImageFileName = !reader.IsDBNull(5) ? reader.GetString(5) : (string?)null,
                }
                );
            }
            //Close DataReader
            reader.Close();
            //Close database connection
            conn.Close();
            return feedbackList;
        }

        public int Add(Feedback feedback)
        {
            DateTime dateposted = DateTime.Now;

            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();

            //Specify an INSERT SQL statement which will
            //return the auto-generated StaffID after insertion
            cmd.CommandText = @"INSERT INTO Feedback (MemberID, DateTimePosted, Title,Text)
             OUTPUT INSERTED.FeedbackID VALUES(@memberID, @datetimeposted, @title, @text)";

            //Define the parameters used in SQL statement, value for each parameter
            //is retrieved from respective class's property.
            cmd.Parameters.AddWithValue("@memberID", feedback.MemberID);
            cmd.Parameters.AddWithValue("@datetimeposted", dateposted);
            cmd.Parameters.AddWithValue("@title", feedback.Title);
            cmd.Parameters.AddWithValue("@text", feedback.Text);

            //A connection to database must be opened before any operations made.
            conn.Open();

            //ExecuteScalar is used to retrieve the auto-generated
            //StaffID after executing the INSERT SQL statement
            feedback.FeedbackID = (int)cmd.ExecuteScalar();

            //A connection should be closed after operations.
            conn.Close();
            //Return id when no error occurs.
            return feedback.FeedbackID;
        }

        // Return number of row updated
        public int Update(Feedback feedback)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();

            //Specify an UPDATE SQL statement
            cmd.CommandText = @"UPDATE Feedback SET Title=@title, Text = @text
                                WHERE MemberID = @selectedMemberID";

            //Define the parameters used in SQL statement, value for each parameter
            //is retrieved from respective class's property.
            cmd.Parameters.AddWithValue("@salary", feedback.Title);
            cmd.Parameters.AddWithValue("@status", feedback.Text);
            cmd.Parameters.AddWithValue("@selectedStaffID", feedback.FeedbackID);
            
            //Open a database connection
            conn.Open();

            //ExecuteNonQuery is used for UPDATE and DELETE
            int count = cmd.ExecuteNonQuery();

            //Close the database connection
            conn.Close();

            return count;

        }

        /*
        public int ChangePassword(Feedback feedback)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();

            //Specify an UPDATE SQL statement
            cmd.CommandText = @"UPDATE Feedback SET Title=@title, Text = @text
                                WHERE MemberID = @selectedMemberID";

            //Define the parameters used in SQL statement, value for each parameter
            //is retrieved from respective class's property.
            cmd.Parameters.AddWithValue("@salary", feedback.Title);
            cmd.Parameters.AddWithValue("@status", feedback.Text);
            cmd.Parameters.AddWithValue("@selectedStaffID", feedback.FeedbackID);

            //Open a database connection
            conn.Open();

            //ExecuteNonQuery is used for UPDATE and DELETE
            int count = cmd.ExecuteNonQuery();

            //Close the database connection
            conn.Close();

            return count;

        }
        */

        public int Delete(int feedbackID)
        {
            //Feedback feedback = new Feedback();
            //Instantiate a SqlCommand object, supply it with a DELETE SQL statement
            //to delete a staff record specified by a Staff ID
            SqlCommand cmd = conn.CreateCommand();
            //SqlCommand cmd1 = conn.CreateCommand();

            cmd.CommandText = @"DELETE FROM Feedback WHERE FeedbackID = @selectfeedbackID";
            cmd.Parameters.AddWithValue("@selectfeedbackID", feedbackID);

            //Open a database connection
            conn.Open();
            int rowAffected = 0;

            //Execute the DELETE SQL to remove the staff record
            rowAffected += cmd.ExecuteNonQuery();
            //rowAffected += cmd1.ExecuteNonQuery();

            //Close database connection
            conn.Close();

            return rowAffected;
        }


        public Feedback GetDetails(string memberId)
        {
            Feedback feedback = new Feedback();

            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();

            //Specify the SELECT SQL statement that
            //retrieves all attributes of a staff record.
            cmd.CommandText = @"SELECT * FROM Feedback 
                                WHERE MemberID = @selectedMemberID";

            //Define the parameter used in SQL statement, value for the
            //parameter is retrieved from the method parameter “memberId”.
            cmd.Parameters.AddWithValue("@selectedMemberID", memberId);

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
                    feedback.FeedbackID = !reader.IsDBNull(0) ?
                    reader.GetInt32(0) : 0;
                    feedback.MemberID = memberId;
                    //feedback.DateTimePosted = (DateTime)reader.IsDBNull(1) ?
                    //reader.GetDateTime(1) : (DateTime?)null;
                    feedback.Title = !reader.IsDBNull(2) ?
                    reader.GetString(2) : null;
                    feedback.Text = !reader.IsDBNull(3) ?
                    reader.GetString(3) : null;
                    feedback.ImageFileName = !reader.IsDBNull(4) ?
                    reader.GetString(4) : null;

                }
            }
            //Close data reader
            reader.Close();

            //Close database connection
            conn.Close();

            return feedback;
        }


    }
}
