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

namespace RepositoryLayer.Service
{
    public class AdminRepo : IAdminRepo
    {
        private readonly Context context;
        private readonly IConfiguration configuration;
        public AdminRepo(Context context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        public AdminModel AddAdmin(AdminModel model)

        {
            using (var connection = context.CreateConnection())
            {

                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("InsertAdmin", (SqlConnection)connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@AdminName", model.AdminName);
                    cmd.Parameters.AddWithValue("@Email", model.Email);
                    cmd.Parameters.AddWithValue("@Password", model.Password);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.ToString());

                }
                finally
                {
                    connection.Close();
                }
            }
            return model;
        }

        private string GenerateToken(int AdminId, string AdminEmail)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.Role,"Admin"),
                new Claim("Email",AdminEmail),
                new Claim("AdminId", AdminId.ToString())
            };
            var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(1440),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public AdminTokenModel LoginAdmin(AdminLoginModel model)
        {
            AdminEntity user = new AdminEntity();
            using (var connection = context.CreateConnection())
            {
                try
                {


                    SqlCommand cmd = new SqlCommand("AdminLogin", (SqlConnection)connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Email", model.Email);
                    cmd.Parameters.AddWithValue("@Password", model.Password);
                    connection.Open();

                    SqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        user.AdminId = Convert.ToInt32(dataReader["AdminId"]);
                        user.AdminName = dataReader["AdminName"].ToString();
                        user.Email = dataReader["Email"].ToString();
                        user.Password = dataReader["Password"].ToString();
                        

                    }
                    if (user.Email == model.Email && user.Password == model.Password)
                    {
                        AdminTokenModel login = new AdminTokenModel();
                        var token = GenerateToken(user.AdminId, user.Email);
                        login.AdminId = user.AdminId;
                        login.AdminName = user.AdminName;
                        login.Email = user.Email;
                        login.Token = token;
                        login.Password = user.Password;
                        


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


    }
}
