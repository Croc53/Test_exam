using System.Data.SqlClient;

namespace probkic
{
    public static class Database
    {
        private static readonly string connectionString =
           "Server=ADCLG1;Database=Obuv_Egorov;Trusted_Connection=True;TrustServerCertificate=True;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}