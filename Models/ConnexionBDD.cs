using Npgsql;

namespace Commercial.Models
{
    public class ConnexionBDD
    {
        private static readonly string HOST = "localhost";
        private static readonly string PORT = "5432";
        private static readonly string DATABASE_NAME = "societe";
        private static readonly string USERNAME = "postgres";
        private static readonly string PASSWORD = "elyse";


        public NpgsqlConnection Connect()
        {
            string connectionString = $"Host={HOST};Port={PORT};Database={DATABASE_NAME};Username={USERNAME};Password={PASSWORD}";
            NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();
            return connection;
        }
    }
}
