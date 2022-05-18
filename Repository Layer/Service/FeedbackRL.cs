using Common_Layer.Models;
using Microsoft.Extensions.Configuration;
using Repository_Layer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Repository_Layer.Service
{
    public class FeedbackRL: IFeedbackRL
    {
        private SqlConnection sqlConnection;
        private IConfiguration Configuration { get; }
        public FeedbackRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public FeedbackModel AddFeedback(FeedbackModel feedback, int userId)
        {
            sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BookStoreDB"]);
            try
            {
                using (sqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("AddFeedback", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Comment", feedback.Comment);
                    cmd.Parameters.AddWithValue("@Rating", feedback.Rating);
                    cmd.Parameters.AddWithValue("@BookId", feedback.BookId);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    sqlConnection.Open();
                    int result = Convert.ToInt32(cmd.ExecuteScalar());
                    sqlConnection.Close();
                    if (result == 1)
                    {
                        return feedback;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<FeedbackResponse> GetRecordsByBookId(int bookId)
        {
            using (SqlConnection sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BookStoreDB"]))
            {
                try
                {
                    List<FeedbackResponse> feedbackRes = new List<FeedbackResponse>();
                    SqlCommand cmd = new SqlCommand("GetAllFeedback", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@BookId", bookId);

                    sqlConnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            FeedbackResponse feedback = new FeedbackResponse();
                            FeedbackResponse res;
                            res = ScanData(feedback, reader);
                            feedbackRes.Add(res);
                        }
                        sqlConnection.Close();
                        return feedbackRes;
                    }
                    else
                    {
                        sqlConnection.Close();
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        public FeedbackResponse ScanData(FeedbackResponse feedback, SqlDataReader reader)
        {
            feedback.FeedbackId = Convert.ToInt32(reader["FeedbackId"] == DBNull.Value ? default : reader["FeedbackId"]);
            feedback.BookId = Convert.ToInt32(reader["BookId"] == DBNull.Value ? default : reader["BookId"]);
            feedback.UserId = Convert.ToInt32(reader["UserId"] == DBNull.Value ? default : reader["UserId"]);
            feedback.Comment = Convert.ToString(reader["Comment"] == DBNull.Value ? default : reader["Comment"]);
            feedback.Rating = Convert.ToInt32(reader["Rating"] == DBNull.Value ? default : reader["Rating"]);
            feedback.FullName = Convert.ToString(reader["FullName"] == DBNull.Value ? default : reader["FullName"]);

            return feedback;
        }
    }
}
