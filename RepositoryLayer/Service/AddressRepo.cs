using CommanLayer.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Service
{
    public class OrderRepo : IAddressRepo
    {
        private readonly Context context;
        private readonly IConfiguration configuration;
        public OrderRepo(Context context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }
        public AddressModel AddAddress(AddressModel model)
        {

            using (var conn = context.CreateConnection())
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("spAddAddress", (SqlConnection)conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", model.UserId);
                    cmd.Parameters.AddWithValue("@FullAddress", model.FullAddress);
                    cmd.Parameters.AddWithValue("@City", model.City);
                    cmd.Parameters.AddWithValue("@State", model.State);
                    cmd.Parameters.AddWithValue("@Type", model.Type);
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
        public List<AddressModel> GetAddresses(int userid)
        {
            using (var conn = context.CreateConnection())
            {
                try
                {
                    List<AddressModel> addresses = new List<AddressModel>();
                    SqlCommand cmd = new SqlCommand("spGetAddress", (SqlConnection)conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", userid);
                    conn.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        AddressModel model = new AddressModel();
                        model.UserId = Convert.ToInt32(dataReader["UserId"]);
                        model.FullAddress = dataReader["FullAddress"].ToString();
                        model.City = dataReader["City"].ToString();
                        model.State = dataReader["State"].ToString();
                        model.Type = dataReader["Type"].ToString();
                        addresses.Add(model);

                    }
                    return addresses;
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

        public UpdateAddressModel UpdateAddress(UpdateAddressModel model)
        {

             using (var conn = context.CreateConnection())
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("spUpdateAddress", (SqlConnection)conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", model.UserId);
                    cmd.Parameters.AddWithValue("@AdId", model.AdID);
                    cmd.Parameters.AddWithValue("@FullAddress", model.FullAddress);
                    cmd.Parameters.AddWithValue("@City", model.City);
                    cmd.Parameters.AddWithValue("@State", model.State);
                    cmd.Parameters.AddWithValue("@Type", model.Type);
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

    }
}