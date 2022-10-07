using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Text;

namespace sg.customer
{
    public class DataAceess
    {
        public IEnumerable<Customer> GetCustomer( int lastId, int pageSize, string? connectionString = null) 
        {

            connectionString = connectionString ?? Environment.GetEnvironmentVariable("ConnectionString", EnvironmentVariableTarget.Process); ;
            
            using (var connection = new MySqlConnection(connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@lastId", lastId, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@pageSize", pageSize, DbType.Int32, ParameterDirection.Input);

                return connection.Query<Customer>("GetCustomer", parameters,commandType: CommandType.StoredProcedure)
                    .AsList();
            }
        }
    }
}
