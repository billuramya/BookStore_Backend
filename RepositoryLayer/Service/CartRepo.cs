using CommanLayer.Model;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace RepositoryLayer.Service
{
    public class CartRepo : ICartRepo
    {
        private readonly IConfiguration _configuration;
        private readonly Context context;
        public CartRepo(Context context, IConfiguration configuration)
        {
            this.context = context;
            this._configuration = configuration;
        }
        public List<BookEntity> AddToCart(CartModel model, int userid)
        {
            using (var conn = context.CreateConnection())
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("spAddToCart", (SqlConnection)conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", userid);
                    cmd.Parameters.AddWithValue("@Quantity", model.Quantity);
                    cmd.Parameters.AddWithValue("@BookId", model.BookId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return GetCartBooks(userid);
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

        public List<BookEntity> GetCartBooks(int userid)
        {

            using (var conn = context.CreateConnection())
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("spGet_cart", (SqlConnection)conn);
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
                        book.Quantity = Convert.ToInt32(dr["Quantity"]);
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

        public bool DeleteCart(DeleteCart model)
        {
            using (var conn = context.CreateConnection())
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("spDelete_cart", (SqlConnection)conn);
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

        public CartModel UpdateQuantity(int userid, CartModel model)
        {
            using (var conn = context.CreateConnection())
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("spUpdateQuantity", (SqlConnection)conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", userid);
                    cmd.Parameters.AddWithValue("@Quantity", model.Quantity);
                    cmd.Parameters.AddWithValue("@BookId", model.BookId);
                    conn.Open();
                    int rowseefected = cmd.ExecuteNonQuery();
                    if (rowseefected > 0)
                    {
                        return model;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally { conn.Close(); }
                return null;

            }
        }
    }
}
