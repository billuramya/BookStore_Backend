using CommanLayer.Model;
using Microsoft.Data.SqlClient;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Service
{
    public class ReviewRepo : IReviewRepo
    {
        private readonly Context _context;
        public ReviewRepo(Context _context)
        {
            this._context = _context;
        }
        public ReviewModel AddReview(ReviewModel model)
        {
            using (var conn=_context.CreateConnection())
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("spAddReview", (SqlConnection)conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", model.UserId);
                    cmd.Parameters.AddWithValue("@Review", model.Review);
                    cmd.Parameters.AddWithValue("@Stars", model.Star);
                    cmd.Parameters.AddWithValue("@BookId", model.BookId);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return model;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
                return null;
            }
        }

        public List<ReviewList> GetReviews(int bookid)
        {

            using (var conn = _context.CreateConnection())
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("GetReviewsForBook", (SqlConnection)conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookId", bookid);

                    conn.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    List<ReviewList> reviews = new List<ReviewList>();

                    while (dataReader.Read())
                    {
                        ReviewList review = new ReviewList();
                        review.FullName = dataReader["FullName"].ToString();
                        review.Review = dataReader["Review"].ToString();
                        review.Stars = Convert.ToInt32(dataReader["Stars"]);
                       

                        reviews.Add(review);

                    }
                    return reviews;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
                return null;
            }
        }





    }
}