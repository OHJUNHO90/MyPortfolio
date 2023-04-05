using Dapper;
using System.Data;
using MySqlConnector;

namespace WebApplication3.Services
{
    public class DbService : IDbService
    {
        private readonly IDbConnection _db;

        public DbService(IConfiguration configuration)
        {
            _db = new MySqlConnection(configuration.GetConnectionString("Employeedb"));
        }

        public async Task<List<T>> GetAll<T>(string command)
        {
            return (await _db.QueryAsync<T>(command)).ToList();
        }

        public async Task<T?> GetAsync<T>(string command, object parms)
        {
            return (await _db.QueryAsync<T>(command, parms).ConfigureAwait(false)).FirstOrDefault();
        }

        public async Task<int> EditData(string command, object? parms = null)
        {
            return await _db.ExecuteAsync(command, parms);
        }
    }
}
