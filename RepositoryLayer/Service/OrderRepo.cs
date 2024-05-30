using CommanLayer.Model;
using CommonLayer.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;


namespace RepositoryLayer.Service
{
    public class OrdersRepo : IOrderRepo
    {
        private readonly Context _context;

        public OrdersRepo( Context _context)
        {
            this._context = _context;

        }

        public List<BookEntity> AddToOrder(OrderModel model, int userid)
        {
            using (var conn = _context.CreateConnection())
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("spAddOrder", (SqlConnection)conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", userid);
                    cmd.Parameters.AddWithValue("@Quantity", model.Quantity);
                    cmd.Parameters.AddWithValue("@BookId", model.BookId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return GetOrders(userid);
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

        public List<BookEntity> GetOrders(int userid)
        {
            using (var conn = _context.CreateConnection())
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("spGetOrders", (SqlConnection)conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", userid);

                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    List<BookEntity> orders = new List<BookEntity>();

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
                       orders.Add(book);

                    }
                    return orders;
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

        public double GetPriceInOrder(int userid)
        {
            List<BookEntity> bookList = GetOrders(userid);
            double totalPrice = 0;
            foreach (var book in bookList)
            {
                totalPrice += (book.Quantity * book.DiscountPrice);
            }
            return totalPrice;
        }
    }

}

