using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer
{
    public class Context
    {
        private readonly IConfiguration _configuration;
        private readonly string _ConnectionString;
        public Context(IConfiguration configuration)
        {
            _configuration = configuration;
            _ConnectionString = _configuration.GetConnectionString("DBConnection");


        }
        public IDbConnection CreateConnection() => new SqlConnection(_ConnectionString);


    }
}
