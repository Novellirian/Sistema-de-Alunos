using MySql.Data.MySqlClient;

namespace Products_DRUD
{
    public sealed class DatabaseConnection
    {
        private static DatabaseConnection _instance; // Instância única
        private static readonly object _lock = new object(); // Para evitar condições de corrida
        private MySqlConnection _connection;

        // String de conexão (configure conforme necessário)
        private readonly string _connectionString = "server=localhost;user=root;password=123456;database=sistema_alunos;sslmode=none;";

        // Construtor privado para impedir a criação direta
        private DatabaseConnection()
        {
            _connection = new MySqlConnection(_connectionString);
        }

        // Propriedade para obter a única instância da classe
        public static DatabaseConnection Instance
        {
            get
            {
                lock (_lock) // Garantir thread safety
                {
                    if (_instance == null)
                    {
                        _instance = new DatabaseConnection();
                    }
                    return _instance;
                }
            }
        }

        // Método para obter a conexão MySql
        public MySqlConnection GetConnection()
        {
            return _connection;
        }
    }
}
