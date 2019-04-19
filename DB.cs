using System;
using System.Windows;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace eSawmill_WPF
{
    /**
     * Klasa pośrednicząca pomiędzy bazą danych
     * MSSQL Server a aplikacją
     */
    class DB
    {
        static private SqlConnection sqlConnection; // uchwyt na połączenie z bazą danych

        /// <summary>
        /// Nawiązanie połączenia z serwerem MSSQL
        /// oraz obsługa błędów podczas połączenia
        /// poprzez wyjątki
        /// </summary>
        /// <param name="instance">uchyt do połączenia z bazą danych</param>
        /// <param name="dbName">nazwa bazy danych</param>
        static public void Open(string instance, string dbName)
        {
            string connectionString = $"Data Source={instance};" +
                "Trusted_Connection=Yes;" +
                $"database={dbName};" +
                "connection timeout=3";

            try
            {
                sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();
            }
            catch(SqlException exception)
            {
                MessageBox.Show(exception.Message, "Sql Connection Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Zwraca uchyt na połączenie z aktualną bazą danych
        /// </summary>
        /// <returns>SqlConnection</returns>
        static public SqlConnection GetInstance()
        {
            return sqlConnection;
        }

        /// <summary>
        /// Wyszukuje oraz loguje użytkownika w systemie bazodanowym
        /// </summary>
        /// <param name="userName">Nazwa użytkownika</param>
        /// <param name="password">Hasło</param>
        /// <returns></returns>
        static public bool AuthUser(string userName,string password)
        {
            string commandText = "SELECT * FROM Firmy " +
                "WHERE nazwa_uzytkownika = @userName " +
                "AND haslo = @password";

            SqlCommand command = new SqlCommand(commandText, sqlConnection);
            command.Parameters.Add("@userName", SqlDbType.VarChar);
            command.Parameters.Add("@password", SqlDbType.VarChar);

            // wpisuje wartości do pytania SQL
            command.Parameters["@userName"].Value = userName;
            command.Parameters["@password"].Value = password;

            try
            {
                SqlDataReader reader = command.ExecuteReader();
                int counter = 0;
                while(reader.Read())
                {
                    counter++;
                    MessageBox.Show(reader.GetValue(1).ToString(),
                        reader.GetValue(2).ToString());
                    break;
                }

                return counter == 0 ? false : true;
            }
            catch(SqlException exception)
            {
                MessageBox.Show(exception.Message, "SqlQuery",
                    MessageBoxButton.OK, MessageBoxImage.Error);

                return false;
            }
        }
    }
}
