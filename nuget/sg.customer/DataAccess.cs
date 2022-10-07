using Dapper;
using MongoDB.Bson;
using MongoDB.Driver;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace sg.customer
{
    public class DataAccess
    {
        private const string MongoKey = "lastidkey";
        private const string MongoCollection = "IdCollection";
        private const string MongoDatabase = "IdStore";

        private async Task<IdCollection> GetLastIdFromCosmosdb(string? connectionString = null)
        {
            try
            {
                var collection = GetIdCollection(GetCosmosDbConnectionString(connectionString));
                var result = await collection.Find(x => x.Key == MongoKey)
                    .FirstOrDefaultAsync();
                if (result != null) return result;
                return await AddLastId(0, connectionString);
                
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private async Task<IdCollection> AddLastId(int lastId, string? connectionString = null)
        {
            var collection = GetIdCollection(GetCosmosDbConnectionString(connectionString));
            var idCollection = new IdCollection { Key = MongoKey, LastId = lastId };
            await collection.InsertOneAsync(idCollection);
            return idCollection;
        }
        private async Task<IdCollection> UpdateLastId(IdCollection idCollection,  string? connectionString = null)
        {
            var collection = GetIdCollection(GetCosmosDbConnectionString(connectionString));
            await collection.InsertOneAsync(idCollection);
            return idCollection;
        }
        private string GetCosmosDbConnectionString(string? connectionString = null)
        {
            connectionString = connectionString ?? Environment.GetEnvironmentVariable("COSMOS_CONNECTION_STR", EnvironmentVariableTarget.Process);
            if(connectionString is null)
                throw new ArgumentNullException(nameof(connectionString),"cosmosdb connection string is null");

            return connectionString;
        }
        private  IMongoCollection<IdCollection> GetIdCollection(string connectionString)
        {
            var settings = MongoClientSettings.FromUrl(new MongoUrl(connectionString));
            settings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
            var mongoClient = new MongoClient(settings);
            var db = mongoClient.GetDatabase(MongoDatabase);
            return db.GetCollection<IdCollection>(MongoCollection);
        }
        
        
        public IEnumerable<Customer> GetCustomer(int pageSize, string? mySqlCon = null, string? cosmosCon = null )
        {
            mySqlCon = mySqlCon ?? Environment.GetEnvironmentVariable("MYSQL_CONNECTION_STR", EnvironmentVariableTarget.Process);


            using var connection = new MySqlConnection(mySqlCon);
            var idCollection = GetLastIdFromCosmosdb(cosmosCon).Result;
                
            var parameters = new DynamicParameters();
            parameters.Add("@lastId", idCollection.LastId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@pageSize", pageSize, DbType.Int32, ParameterDirection.Input);

            var ret = connection.Query<Customer>("GetCustomer", parameters, commandType: CommandType.StoredProcedure)
                .AsList();

            if (ret.Count <= 0) return ret;
            idCollection.LastId = ret.Last().Id;
            var collection = UpdateLastId(idCollection, cosmosCon).Result;

            return ret;
        }

        
    }
}
