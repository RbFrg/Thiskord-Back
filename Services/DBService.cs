using Microsoft.Data.SqlClient;
using System.Runtime.ConstrainedExecution;
namespace Thiskord_Back.Services
{

    public interface IDbConnectionService
    {
        SqlConnection CreateConnection();
        void Test();
    }
    public class DBService : IDbConnectionService
    {
        //private const string = "Data Source=(local);Initial Catalog=Thiskord;"
         //       + "Integrated Security=SSPI";

        private readonly IConfiguration _config;
        private readonly IDbConnectionService _db;

        public DBService(IConfiguration config)
        {
            _config = config;
        }

        public SqlConnection CreateConnection()
        {
            return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
        }

        public void Test()
        {
            using var connection = CreateConnection();
            connection.Open();
            string query = "INSERT INTO table_test (mess) VALUES ('test')";

        }
    }
}
