using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;


namespace FlashCards.Data
{

    public static class MockDatabase
    {

        static void ConnectToDatabase()
        {
            string connectionString = "Server=localhost,1433;Database=FlashCards;User Id=SA;Password=Allahis#1;TrustServerCertificate=True;";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText =
                @"IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='FlashCard' AND xtype='U')
                CREATE TABLE FlashCard (
                    Id INT IDENTITY(1,1) PRIMARY KEY,
                    StackId INT NOT NULL,
                    Question NVARCHAR(MAX) NOT NULL,
                    Answer NVARCHAR(MAX) NOT NULL
                );";

                tableCmd.ExecuteNonQuery();

                connection.Close();
            }


        }

        public static string GetConnectionString()
        {
            return  "Server=localhost,1433;Database=FlashCards;User Id=SA;Password=Allahis#1;TrustServerCertificate=True;";
        }
    }
}
