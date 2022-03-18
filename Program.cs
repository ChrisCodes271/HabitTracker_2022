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
                string sqlCreateTable = "CREATE TABLE WaterConsumed (id INT, bottlesdrank INT)"; //String created to store sql commands that creates a table. 

                SQLiteCommand createTableCommand = new SQLiteCommand(sqlCreateTable, m_dbConnection); //Command created using our sql command string and connection object. 

                createTableCommand.ExecuteNonQuery(); //Execute the createTableCommand
            }
            catch (SQLiteException)
            {
                Console.WriteLine("Table Detected");
            }





            Console.WriteLine("What day is it?");
            string userDateInsert = Console.ReadLine(); //Store integer value from user to use later. 
            Convert.ToInt32(userDateInsert);


            Console.WriteLine("How many bottles of water did you drink?");
            string userWaterInsert = Console.ReadLine();
            Convert.ToInt32(userWaterInsert);

            string newEntry = $"INSERT INTO WaterConsumed (id, bottlesdrank) values ({userDateInsert} , {userWaterInsert})"; //insert integer values from user requested information into SQL command to create new column with id and watervalue

            SQLiteCommand sqlNewEntry = new SQLiteCommand(newEntry, m_dbConnection);

            sqlNewEntry.ExecuteNonQuery();




            //string sqlFirstInsert = "UPDATE WaterConsumed SET bottlesdrank = 3 WHERE  id = 1";  

            //SQLiteCommand creatIdOneCommand = new SQLiteCommand(sqlFirstInsert, m_dbConnection);

            //string sqlSecondInsert = "INSERT INTO WaterConsumed (id, bottlesdrank) values (2, 4)"; //This is an example of an insert command. 

            //SQLiteCommand creatIdTwoCommand = new SQLiteCommand(sqlSecondInsert, m_dbConnection);

            //string sqlThirdInsert = "INSERT INTO WaterConsumed (id, bottlesdrank) values (3, 7)";

            //SQLiteCommand creatIdThreeCommand = new SQLiteCommand(sqlThirdInsert, m_dbConnection);

            //creatIdOneCommand.ExecuteNonQuery(); //These execute commands will not write over other ID's. Instead will generate new ones. (something to keep in mind) 
            //creatIdTwoCommand.ExecuteNonQuery();
            //creatIdThreeCommand.ExecuteNonQuery();



            string pullFromTable = "SELECT * FROM WaterConsumed";

            SQLiteCommand pullCommand = new SQLiteCommand(pullFromTable, m_dbConnection); //Read values. 

            SQLiteDataReader reader = pullCommand.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine($"{reader.GetInt32(0)} {reader.GetInt32(1)}");
            }






            //Information added to test branching









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
