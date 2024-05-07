using System.Data;
using Dapper;
using System.Data.SqlClient;


namespace TPHDotNetCore.Shared
{
    public class DapperService
    {

        private readonly string _connectionString;

        public DapperService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<T> Query<T>(string query, object? param = null)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            var lst = db.Query<T>(query, param).ToList(); // if param => null, auto skip
            return lst;


        }

        public M QueryFirstOrDefault<M>(string query, object? param = null)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            var item = db.Query<M>(query, param).FirstOrDefault(); // if param => null, auto skip
            return item!;


        }

        public int Execute(string query, object? paran = null)
        {
            using IDbConnection db = new SqlConnection(_connectionString);

            var result =  db.Execute(query, paran);
            return result;
        }

        
    }
}
