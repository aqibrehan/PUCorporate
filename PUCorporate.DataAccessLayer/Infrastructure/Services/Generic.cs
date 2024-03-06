using Dapper;
using Microsoft.Extensions.Configuration;
using PUCorporate.DataAccessLayer.Services.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace PUCorporateAPI.Services.Services
{
    public class Generic : IGeneric
    {
        private readonly IConfiguration _config;

        public Generic(IConfiguration config)
        {
            _config = config;
        }

        public async Task<IEnumerable<T>> LoadData<T, U>(
            string SP,
            U parameters)
        {
            using (IDbConnection con = new SqlConnection(_config.GetConnectionString("PUCRMConnection")))
            {
                if (con.State != ConnectionState.Open) con.Open();

                return await con.QueryAsync<T>(
                    SP, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task SaveData<T>( string SP,T parameters)
        {
            using (IDbConnection con = new SqlConnection(_config.GetConnectionString("PUCRMConnection")))
            {
                if (con.State != ConnectionState.Open) con.Open();

                await con.ExecuteAsync(
                    SP, parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
