using CommanLayer.Model;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Service
{
    public class WishRepo : IWishRepo
    {
        private readonly Context _context;
        public WishRepo(Context _context)
        {
            this._context = _context;
        }
        public List<BookEntity> AddToWishList(WishList model)
        {
            using (var conn = _context.CreateConnection())
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("spAddtowhishlist", (SqlConnection)conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", model.UserId);
                    cmd.Parameters.AddWithValue("@BookId", model.BookId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return GetWhishListBooks(model.UserId);
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
        public List<BookEntity> GetWhishListBooks(int userid)
        {

            using (var conn = _context.CreateConnection())
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("spGetWhishlist", (SqlConnection)conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", userid);

                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    List<BookEntity> list = new List<BookEntity>();

                    while (dr.Read())
                    {
                        BookEntity book = new BookEntity();
                        book.BookId = Convert.ToInt32(dr["BookId"]);
                        book.Title = dr["Title"].ToString();
                        book.Author = dr["Author"].ToString();
                        book.OriginalPrice = Convert.ToInt64(dr["OriginalPrice"]);
                        book.DiscountPrice = Convert.ToInt64(dr["DiscountPrice"]);
                        book.Description = dr["Description"].ToString();
                        book.Ratting = Convert.ToDecimal(dr["Ratting"]);
                        book.RatedPersons = Convert.ToInt32(dr["RatedPersons"]);
                        book.Image = dr["Image"].ToString();
                        list.Add(book);
                    }
                    return list;
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

        public bool DeleteWhishlist(WishList model)
        {
            using (var conn =_context.CreateConnection())
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("spDeleteWhishlist", (SqlConnection)conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", model.UserId);
                    cmd.Parameters.AddWithValue("@BookId", model.BookId);


                    conn.Open();
                    int rowseefected = cmd.ExecuteNonQuery();
                    if (rowseefected > 0)
                    {
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally { conn.Close(); }
                return false;

            }
        }
    }
}
   