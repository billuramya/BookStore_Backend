using CommanLayer.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryLayer.Entity;
using static System.Reflection.Metadata.BlobBuilder;


namespace RepositoryLayer.Service
{
    public class BookRepo : IBookRepo
    {
        private readonly Context context;

        public BookRepo(Context context)
        {
            this.context = context;

        }

        public BookModel BookRegistration(BookModel model)

        {
            using (var connection = context.CreateConnection())
            {

                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("InsertBook", (SqlConnection)connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Title", model.Title);
                    cmd.Parameters.AddWithValue("@Author", model.Author);
                    cmd.Parameters.AddWithValue("@Description", model.Description);
                    cmd.Parameters.AddWithValue("@OriginalPrice", model.OriginalPrice);
                    cmd.Parameters.AddWithValue("@DiscountPrice", model.DiscountPrice);
                    cmd.Parameters.AddWithValue("@Ratting", model.Ratting);
                    cmd.Parameters.AddWithValue("@RatedPersons", model.RatedPersons);
                    cmd.Parameters.AddWithValue("@Quantity", model.Quantity);
                    cmd.Parameters.AddWithValue("@Image", model.Image);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.ToString());

                }
            }
            return model;
        }

        public object GetAllBooks()
        {

            List<BookEntity> books = new List<BookEntity>();
            using (var conn = context.CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("GetAllBooks", (SqlConnection)conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
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
                        books.Add(book);

                    }
                    return books;
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


        public bool UpdateBook(BookModel model, int BookId)
        {
            using (var conn = context.CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("UpdateBook", (SqlConnection)conn);
                    cmd.CommandType = CommandType.StoredProcedure;


                    cmd.Parameters.AddWithValue("@BookId", BookId);
                    cmd.Parameters.AddWithValue("@Title", model.Title);
                    cmd.Parameters.AddWithValue("@Author", model.Author);
                    cmd.Parameters.AddWithValue("@Description", model.Description);
                    cmd.Parameters.AddWithValue("@OriginalPrice", model.OriginalPrice);
                    cmd.Parameters.AddWithValue("@DiscountPrice", model.DiscountPrice);
                    cmd.Parameters.AddWithValue("@Ratting", model.Ratting);
                    cmd.Parameters.AddWithValue("@RatedPersons", model.RatedPersons);
                    cmd.Parameters.AddWithValue("@Quantity", model.Quantity);
                    cmd.Parameters.AddWithValue("@Image", model.Image);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public bool DeleteBook(int BookId)
        {
            using (var conn = context.CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("DeleteById", (SqlConnection)conn);
                    cmd.CommandType = CommandType.StoredProcedure;


                    cmd.Parameters.AddWithValue("@BookId", BookId);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public BookEntity GetBookById(int BookId)
        {
            using (var conn = context.CreateConnection())
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("GetBookById", (SqlConnection)conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookId", BookId);
                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
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
                        return book;


                    }
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

        // 1) search a book by book name

        public BookEntity GetBookByName(string Title)
        {
            using (var conn = context.CreateConnection())
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("GetBookByTitle", (SqlConnection)conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Title", Title);
                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
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
                        return book;


                    }
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

        // 3) total count of books in table, cart and order
        //public List<BookCountEntity> GetTotalBookCount(int UserId)
        //{
        //    List<BookCountEntity> books = new List<BookCountEntity>();

        //    using (var conn = context.CreateConnection())
        //    {
        //        try
        //        {
        //            SqlCommand cmd = new SqlCommand("countBooks", (SqlConnection)conn);
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@UserId", UserId);
        //            conn.Open();
        //            SqlDataReader dr = cmd.ExecuteReader();
        //            while (dr.Read())
        //            {
        //                BookCountEntity book = new BookCountEntity();
        //                book.BookId = Convert.ToInt32(dr["BookId"]);
        //                book.Title = dr["Title"].ToString();
        //                book.Author = dr["Author"].ToString();
        //                book.OriginalPrice = Convert.ToInt64(dr["OriginalPrice"]);
        //                book.DiscountPrice = Convert.ToInt64(dr["DiscountPrice"]);
        //                book.Description = dr["Description"].ToString();
        //                book.count = Convert.ToInt32(dr["BookCount"]);
        //                book.Quantity = Convert.ToInt32(dr["Quantity"]);
        //                book.Ratting = Convert.ToDecimal(dr["Ratting"]);
        //                book.RatedPersons = Convert.ToInt32(dr["RatedPersons"]);
        //                book.Image = dr["Image"].ToString();

        //                books.Add(book);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine(ex.Message);
        //        }
        //        finally
        //        {
        //            conn.Close();
        //        }
        //    }

        //    return books;
        //}


        public List<BookCountEntity> GetTotalBookCount(int userId)
        {
            List<BookCountEntity> books = new List<BookCountEntity>();

            using (var conn = context.CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("countBooks", (SqlConnection)conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        BookCountEntity book = new BookCountEntity();
                        book.BookId = Convert.ToInt32(dr["BookId"]);
                        book.Title = dr["Title"].ToString();
                        book.Author = dr["Author"].ToString();
                        book.OriginalPrice = Convert.ToInt64(dr["OriginalPrice"]);
                        book.DiscountPrice = Convert.ToInt64(dr["DiscountPrice"]);
                        book.Description = dr["Description"].ToString();
                        book.Ratting = Convert.ToDecimal(dr["Ratting"]);
                        book.RatedPersons = Convert.ToInt32(dr["RatedPersons"]);
                        book.Quantity = Convert.ToInt32(dr["Quantity"]);
                        book.Image = dr["Image"].ToString();
                        book.count = Convert.ToInt32(dr["BookCount"]);

                        books.Add(book);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                   
                }
                finally
                {
                    conn.Close();
                }
            }

            return books;
        }


    }
}
