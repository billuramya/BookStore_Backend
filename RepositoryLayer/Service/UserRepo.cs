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
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Service
{
    public class UserRepo : IUserRepo
    {
        private readonly Context context;
        private readonly IConfiguration configuration;
        public UserRepo(Context context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        //string ConnectionString = @"Data Source=CHANDUU\SQLEXPRESS;Initial Catalog=BookStore;Integrated Security=True;";

        public UserRegisterModel UserRegistration(UserRegisterModel model)

        {
            using (var connection =context.CreateConnection())
            {
                
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("InsertUser", (SqlConnection)connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@FullName", model.FullName);
                    cmd.Parameters.AddWithValue("@Email", model.Email);
                    cmd.Parameters.AddWithValue("@Password", model.Password);
                    cmd.Parameters.AddWithValue("@MobileNumber", model.MobileNumber);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                   
                    Console.WriteLine(ex.ToString());
                   
                }
            }
            return model;
        }

        public object GetData()
        {
            List<UserEntity> users = new List<UserEntity>();
            using (var connection = context.CreateConnection())
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("GetAllUser",(SqlConnection)connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        UserEntity user = new UserEntity();
                        user.UserId = Convert.ToInt32(dataReader["UserId"]);
                        user.FullName = dataReader["FullName"].ToString();
                        user.Email = dataReader["Email"].ToString();
                        user.Password = dataReader["Password"].ToString();
                        user.MobileNumber = Convert.ToInt64(dataReader["MobileNumber"]);
                        users.Add(user);
                    }
                }
                catch (Exception ex)
                {
                   
                    Console.WriteLine(ex.Message);
                    
                }
            }
            return users;
        }
        private string GenerateToken(int UserId, string userEmail)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.Role,"User"),
                new Claim("Email",userEmail),
                new Claim("UserId", UserId.ToString())
            };
            var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(1440),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public LoginTokenModel LoginUser(Login model)
        {
            UserEntity user = new UserEntity();
            using (var connection = context.CreateConnection())
            {
                try
                {


                    SqlCommand cmd = new SqlCommand("LoginUser", (SqlConnection)connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Email", model.Email);
                    cmd.Parameters.AddWithValue("@Password", model.Password);
                    connection.Open();

                    SqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        user.UserId = Convert.ToInt32(dataReader["UserId"]);
                        user.FullName = dataReader["FullName"].ToString();
                        user.Email = dataReader["Email"].ToString();
                        user.Password = dataReader["Password"].ToString();
                        user.MobileNumber = Convert.ToInt64(dataReader["MobileNumber"]);

                    }
                    if (user.Email == model.Email && user.Password == model.Password)
                    {
                        LoginTokenModel login = new LoginTokenModel();
                        var token = GenerateToken(user.UserId, user.Email);
                        login.UserId = user.UserId;
                        login.FullName = user.FullName;
                        login.Email = user.Email;
                        login.Token = token;
                        login.Password = user.Password;
                        login.MobileNumber = user.MobileNumber;


                        return login;
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
                return null;
            }

        }

        public bool ResetPassword(string email, string password)
        {
            using (var connection = context.CreateConnection())
            {
                try
                {
                    connection.Open(); // Open the connection

                    SqlCommand cmd = new SqlCommand("ResetPassword", (SqlConnection) connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmailId", email);
                    cmd.Parameters.AddWithValue("@Password", password);


                    int rowsAffected = cmd.ExecuteNonQuery();


                    if (rowsAffected > 0)
                    {

                        return true;
                    }
                    else
                    {

                        return false;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                    return false;
                }
                finally
                {

                    connection.Close();
                }
            }

        }

        public ForgotPasswordModel ForgotPassword(string email)
        {
            UserEntity user = new UserEntity();
            using (var conn = context.CreateConnection())
            {
                try
                {


                    SqlCommand cmd = new SqlCommand("ForgotPassword", (SqlConnection)conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Email", email);
                    
                    conn.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        user.UserId = Convert.ToInt32(dataReader["UserId"]);
                        user.FullName = dataReader["FullName"].ToString();
                        user.Email = dataReader["Email"].ToString();
                        user.Password = dataReader["Password"].ToString();
                        user.MobileNumber = Convert.ToInt64(dataReader["MobileNumber"]);

                    }
                    if (email == user.Email)
                    {
                        ForgotPasswordModel model = new ForgotPasswordModel();

                        model.Email = user.Email;
                        model.UserId = user.UserId;
                        model.token = GenerateToken(user.UserId, user.Email);
                        return model;
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

        public object UpdateUser(int id, UpdateUser model)
        {
            using (var conn = context.CreateConnection())
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("spUpdateUser",( SqlConnection)conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@FullName", model.FullName);
                    cmd.Parameters.AddWithValue("@Email", model.Email);
                    cmd.Parameters.AddWithValue("@MobileNumber", model.MobileNumber);
                    conn.Open();
                    int result = cmd.ExecuteNonQuery();
                    if (result == 0)
                    {
                        return null;
                    }
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



    }

}