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
            if (File.Exists("WorkersDatabase.sqlite"))
            {
                //SQLiteConnection.CreateFile("WorkersDatabase.sqlite");
                File.Delete("WorkersDatabase.sqlite");
                Console.WriteLine("Deleting the file.");

            }

            SQLiteConnection Workers_dbConnection = new SQLiteConnection("Data Source=WorkersDatabase.sqlite;Version=3;");//den her laver også en database file hvis den ikke er der

            Workers_dbConnection.Open();//open the data steam / database

            string sql = "CREATE TABLE workers (name VARCHAR(20), is_banned BOOLEAN)";//making the sql query

            SQLiteCommand command = new SQLiteCommand(sql, Workers_dbConnection);//make's the command

            command.ExecuteNonQuery();//Execute the query

            sql = "insert into workers (name, is_banned) values ('ImNumber1', 0)";//insert data into database
            command = new SQLiteCommand(sql, Workers_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into workers (name, is_banned) values ('ImNumber2', 1)";
            command = new SQLiteCommand(sql, Workers_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into workers (name, is_banned) values ('ImNumber3', 0)";
            command = new SQLiteCommand(sql, Workers_dbConnection);
            command.ExecuteNonQuery();

            sql = "select * from workers";
            command = new SQLiteCommand(sql, Workers_dbConnection);

            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine("Name: " + reader["name"] + "\tis_banned?: " + reader["is_banned"]);
            }


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
