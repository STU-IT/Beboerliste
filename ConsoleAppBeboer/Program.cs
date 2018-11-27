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

            string sql = "CREATE TABLE workers (name VARCHAR(20), is_banned BOOLEAN, join_date DATETIME, banned_date DATETIME)";//making the sql query

            SQLiteCommand command = new SQLiteCommand(sql, Workers_dbConnection);//make's the command

            command.ExecuteNonQuery();//Execute the query

            DateTime now = DateTime.Now;
            sql = "insert into workers (name, is_banned, join_date, banned_date) values ('ImNumber1', true, DATETIME('1919-09-19'), DATETIME('now'))";//insert data into database
            command = new SQLiteCommand(sql, Workers_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into workers (name, is_banned, join_date, banned_date) values ('ImNumber2', false, DATETIME('1929-09-19'), null)";
            command = new SQLiteCommand(sql, Workers_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into workers (name, is_banned, join_date, banned_date) values ('ImNumber3', false, DATETIME('1939-09-19'), null)";
            command = new SQLiteCommand(sql, Workers_dbConnection);
            command.ExecuteNonQuery();

            sql = "select * from workers";
            command = new SQLiteCommand(sql, Workers_dbConnection);

            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine("Name: " + reader["name"] + "\t\t" + "is_banned?: " + reader["is_banned"] + "\t" + "Join Date: " + reader["join_date"] + "\t" + "Banned Date: " + reader["banned_date"]);
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
