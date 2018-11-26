using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

// for vi har let adgang til namespacet
// kræver en reference til dll-filen, men det er klare af NuGet
// Se packages.config,
// man vælger bare System.Data.SQLite NuGet manager
using System.Data.SQLite;

namespace ConsoleAppBeboer
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!File.Exists("WorkersDatabase.sqlite"))
            {
                SQLiteConnection.CreateFile("WorkersDatabase.sqlite");
                Console.WriteLine("The file is now createt.");
            }

            SQLiteConnection Workers_dbConnection = new SQLiteConnection("Data Source=WorkersDatabase.sqlite3;Version=3;");

            Workers_dbConnection.Open();
            /*
            // Se mere om connectionsstrings på https://www.connectionstrings.com/sqlite/
            SQLiteConnection beboer_dbConnection = new SQLiteConnection("Data Source=beboerliste.sqlite3;Version=3;");
            // ----------------------------------------------------------------------^ filnavn

            beboer_dbConnection.Open();

            SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM beboere", beboer_dbConnection);

            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine("{0} {1}", reader["id"], reader["navn"]);
            }

            beboer_dbConnection.Close();

            */
            Workers_dbConnection.Close();
        }
    }
}
