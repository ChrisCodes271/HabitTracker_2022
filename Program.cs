using SQLitePCL;
using System;
using System.Data.SQLite;
using System.IO;

namespace HabitTracker
{
    class Program
    {
        static void Main(string[] args)
        {

            if (File.Exists("HabitDatabase.sqlite"))
            {
                Console.WriteLine("Database detected");
            }

            else
            {
                SQLiteConnection.CreateFile("HabitDatabase.sqlite"); //Create new sqlite database file.
            }            

            SQLiteConnection m_dbConnection; //create a connection object

            m_dbConnection = new SQLiteConnection("Data Source=HabitDatabase.sqlite;"); //assign that connection object to our new database

            m_dbConnection.Open(); //Open the connection

            try
            {
                String sqlCreateTable = "CREATE TABLE WaterConsumed (id INT, bottlesdrank INT)"; //String created to store sql commands that creates a table. 

                SQLiteCommand createTableCommand = new SQLiteCommand(sqlCreateTable, m_dbConnection); //Command created using our sql command string and connection object. 

                createTableCommand.ExecuteNonQuery(); //Execute the createTableCommand
            }
            catch (SQLiteException)
            {
                Console.WriteLine("Table Detected");
            }
           


            String sqlFirstInsert = "INSERT INTO WaterConsumed (id, bottlesdrank) values (1, 6)"; //These three sections all perform the same function. Creating an sql command to insert data. Then running them below. 

            SQLiteCommand creatIdOneCommand = new SQLiteCommand(sqlFirstInsert, m_dbConnection);

            String sqlSecondInsert = "INSERT INTO WaterConsumed (id, bottlesdrank) values (2, 4)";

            SQLiteCommand creatIdTwoCommand = new SQLiteCommand(sqlSecondInsert, m_dbConnection);

            String sqlThirdInsert = "INSERT INTO WaterConsumed (id, bottlesdrank) values (3, 7)";

            SQLiteCommand creatIdThreeCommand = new SQLiteCommand(sqlThirdInsert, m_dbConnection);

            creatIdOneCommand.ExecuteNonQuery(); //These execute commands will not write over other ID's. Instead will generate new ones. (something to keep in mind) 
            creatIdTwoCommand.ExecuteNonQuery();
            creatIdThreeCommand.ExecuteNonQuery();

            String pullFromTable = "SELECT * FROM WaterConsumed";

            SQLiteCommand pullCommand = new SQLiteCommand(pullFromTable, m_dbConnection);

            SQLiteDataReader reader = pullCommand.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine($"{reader.GetInt32(0)} {reader.GetInt32(1)}");
            }



































            /*string cs = "URI=file:D:\\CSharpStuff\\Projects\\HabitTracker\\habittracker.db";

                using var con = new SQLiteConnection(cs);

                con.Open();

                string stm = "SELECT * FROM SleepTracker";

                using var cmd = new SQLiteCommand(stm, con);

                using SQLiteDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Console.WriteLine($"{rdr.GetInt32(0)} {rdr.GetInt32(1)} {rdr.GetInt32(2)}");
                }
            
            */



            Console.ReadKey();
        }
    }
}
