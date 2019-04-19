using System;
using System.Windows;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace eSawmill_WPF
{
    /**
     * klasa reprezentująca jeden rekord w bazie
     * odzwierciedla rekordy w tabeli "Firmy"
     */
    class Company
    {
        public int ID { get; set; }
        public string CompanyName { get; set; }
        public string OwnerName { get; set; }
        public string OwnerSurname { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string PhoneNumber { get; set; }
        public string HomeNumber { get; set; }
        public string Website { get; set; }
        public string NIP { get; set; }
        public string Regon { get; set; }
        public string Mail { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }

        /// <summary>
        /// Wyszukuje konkretną firmę po jej ID w bazie danych
        /// </summary>
        /// <param name="id">id firmy do wyszukania</param>
        /// <returns></returns>
        public static Company find(int id)
        {
            string commandText = "SELECT * FROM Firmy " +
                "WHERE id = @id";
            SqlCommand command = new SqlCommand(commandText, DB.GetInstance());
            command.Parameters.Add("@id", SqlDbType.BigInt);

            // nadaje wartość określonemu parametrowi w zapytaniu SQL
            command.Parameters["@id"].Value = id;

            try
            {
                SqlDataReader reader = command.ExecuteReader();
                int counter = 0;
                Company company = new Company();

                while(reader.Read())
                {
                    counter++;
                    company.ID              = Convert.ToInt32(reader.GetValue(0));
                    company.CompanyName     = reader.GetValue(1).ToString();
                    company.OwnerName       = reader.GetValue(2).ToString();
                    company.OwnerSurname    = reader.GetValue(3).ToString();
                    company.City            = reader.GetValue(4).ToString();
                    company.ZipCode         = reader.GetValue(5).ToString();
                    company.PhoneNumber     = reader.GetValue(6).ToString();
                    company.HomeNumber      = reader.GetValue(7).ToString();
                    company.Website         = reader.GetValue(8).ToString();
                    company.NIP             = reader.GetValue(9).ToString();
                    company.Regon           = reader.GetValue(10).ToString();
                    company.Mail            = reader.GetValue(11).ToString();
                    company.UserName        = reader.GetValue(12).ToString();
                    company.UserPassword    = reader.GetValue(13).ToString();
                }
                reader.Close();

                return counter == 0 ? null : company;
            }
            catch(SqlException exception)
            {
                MessageBox.Show(exception.Message, "Sql Query Exception",
                    MessageBoxButton.OK, MessageBoxImage.Error);

                return null;
            }
        }

        /// <summary>
        /// dodaje nową firmę do bazy
        /// dane pobiera z obiektu firmowego przekazanego w argumentach
        /// </summary>
        /// <param name="company">obiekt reprezentujący firmę</param>
        /// <returns></returns>
        public static bool create(Company company)
        {
            string cmdText = "INSERT INTO Firmy " +
                "(nazwa_firmy, imie, nazwisko, miejscowosc, kod_pocztowy, nr_telefonu, nr_lokalu, www, nip, regon, email, nazwa_uzytkownika, haslo) " +
                "VALUES (@companyName, @ownerName, @ownerSurname, @city, @zipCode, @phoneNumber, @houseNumber, @website, @nip, @regon, @mail, @userName, @password)";

            SqlCommand command = new SqlCommand(cmdText, DB.GetInstance());
            command.Parameters.Add("@companyName", SqlDbType.VarChar);
            command.Parameters.Add("@ownerName", SqlDbType.VarChar);
            command.Parameters.Add("@ownerSurname", SqlDbType.VarChar);
            command.Parameters.Add("@city", SqlDbType.VarChar);
            command.Parameters.Add("@zipCode", SqlDbType.VarChar);
            command.Parameters.Add("@phoneNumber", SqlDbType.VarChar);
            command.Parameters.Add("@houseNumber", SqlDbType.VarChar);
            command.Parameters.Add("@website", SqlDbType.VarChar);
            command.Parameters.Add("@nip", SqlDbType.VarChar);
            command.Parameters.Add("@regon", SqlDbType.VarChar);
            command.Parameters.Add("@mail", SqlDbType.VarChar);
            command.Parameters.Add("@userName", SqlDbType.VarChar);
            command.Parameters.Add("@password", SqlDbType.VarChar);

            // przypisanie wartości do parametrów zapytania SQL
            command.Parameters["@companyName"].Value = company.CompanyName;
            command.Parameters["@ownerName"].Value = company.OwnerName;
            command.Parameters["@ownerSurname"].Value = company.OwnerSurname;
            command.Parameters["@city"].Value = company.City;
            command.Parameters["@zipCode"].Value = company.ZipCode;
            command.Parameters["@phoneNumber"].Value = company.PhoneNumber;
            command.Parameters["@houseNumber"].Value = company.HomeNumber;
            command.Parameters["@website"].Value = company.Website;
            command.Parameters["@nip"].Value = company.NIP;
            command.Parameters["@regon"].Value = company.Regon;
            command.Parameters["@mail"].Value = company.Mail;
            command.Parameters["@userName"].Value = company.UserName;
            command.Parameters["@password"].Value = company.UserPassword;

            try
            {
                command.ExecuteNonQuery();
                return true;
            }
            catch(SqlException exception)
            {
                MessageBox.Show(exception.Message, "Sql Query Exception",
                    MessageBoxButton.OK, MessageBoxImage.Error);

                return false;
            }
            catch(InvalidOperationException exception)
            {
                MessageBox.Show(exception.Message, "Sql Query Exception",
                    MessageBoxButton.OK, MessageBoxImage.Error);

                return false;
            }
        }
    }
}
