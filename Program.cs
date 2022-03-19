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

            //Check if database exists, if not create one!
            if (File.Exists("HabitDatabase.sqlite"))
            {
                Console.WriteLine("Database detected");
            }
            else
            {
                //Create new sqlite database file.
                SQLiteConnection.CreateFile("HabitDatabase.sqlite"); 
            }            

            SQLiteConnection m_dbConnection; //create a connection object
            m_dbConnection = new SQLiteConnection("Data Source=HabitDatabase.sqlite;"); //assign that connection object to our new database
            m_dbConnection.Open(); //Open the connection            

            try
            {
                string sqlCreateTable = "CREATE TABLE WaterConsumed (id INTEGER PRIMARY KEY AUTOINCREMENT, date TEXT, bottlesdrank INT)"; //String created to store sql commands that creates a table. 

                SQLiteCommand createTableCommand = new SQLiteCommand(sqlCreateTable, m_dbConnection); //Command created using our sql command string and connection object. 

                createTableCommand.ExecuteNonQuery(); //Execute the createTableCommand
            }
            catch (SQLiteException) //This exception will occur if table already exists. Error message will display as "Table Detected"
            {
                Console.WriteLine("Table Detected");
            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong..."); //No other errors expected. If they do happen it will notify the user for admin intervention. 
            }
            finally
            {
                Console.WriteLine("Initializing...");                
            }

            bool runApp = true;

            while (runApp)
            {
                int userInput = 999; // Establish user input variable

                while (userInput != 1 && userInput != 2 && userInput != 3 && userInput != 4 && userInput != 0)
                {
                    userInput = 999;
                    Console.WriteLine("----------------------------------------------Welcome to water tracker!----------------------------------------------");
                    Console.WriteLine("Please select an option:");
                    Console.WriteLine("[1] Insert a new entry into your table");
                    Console.WriteLine("[2] View your table");
                    Console.WriteLine("[3] Delete an entry from your table");
                    Console.WriteLine("[4] Change an entry from your table");               //Options for user to choose from
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("[0] Exit");
                    userInput = Convert.ToInt32(Console.ReadLine());
                }
                
                bool reRun = true;            

                switch (userInput) 
                {
                    case 1:
                        while (reRun)
                        {                           

                            Console.WriteLine("What date do you wish to input data for?"); //These 2 blocks take a date string, and bottlesdrank int for later
                            string userDateInsert = Console.ReadLine();                         
                            Convert.ToString(userDateInsert);

                            Console.WriteLine("How many bottles of water did you drink?");
                            string userWaterInsert = Console.ReadLine();
                            Convert.ToInt32(userWaterInsert);

                            string newEntry = $"INSERT INTO WaterConsumed (date, bottlesdrank) values ('{userDateInsert}' , {userWaterInsert})"; //insert string value from first statement and int value from second as Date, and Bottlesdrank on table. 

                            SQLiteCommand sqlNewEntry = new SQLiteCommand(newEntry, m_dbConnection);

                            sqlNewEntry.ExecuteNonQuery();

                            Console.WriteLine("Would you like to enter more information? (y/n)");

                            string repeatInput = Console.ReadLine();

                            repeatInput = repeatInput.ToUpper();

                            if (repeatInput == "N")
                            {
                                reRun = false;
                            }
                        }
                        
                        break;

                    case 2:
                        bool runInventory = true;

                        while (runInventory)
                        {
                            string pullFromTable = "SELECT * FROM WaterConsumed";

                            SQLiteCommand pullCommand = new SQLiteCommand(pullFromTable, m_dbConnection); //Read values. 

                            SQLiteDataReader reader = pullCommand.ExecuteReader();

                            Console.WriteLine("-----------------------------Your water drinking habits sorted by entry number.-----------------------------");

                            while (reader.Read())
                            {

                                Console.WriteLine($" ENTRY Number: {reader.GetInt32(0)} | On {reader.GetString(1)} You drank {reader.GetInt32(2)} glasses of water");

                            }
                            Console.WriteLine("Enter [E] to exit");

                            string closeTable = Console.ReadLine();

                            closeTable = closeTable.ToUpper();

                            if (closeTable == "E")
                            {
                                runInventory = false;
                            }

                        } 
                        break;

                    case 3:

                        while (reRun)
                        {
                            Console.WriteLine("This feature has not yet been implemented.");
                            Console.WriteLine("Would you like to delete more information? (y/n)");

                            string repeatDelete = Console.ReadLine();

                            repeatDelete = repeatDelete.ToUpper();

                            if (repeatDelete == "N")
                            {
                                reRun = false;
                            }
                        }
                        
                        break;

                    case 4:

                        while (reRun)
                        {
                            Console.WriteLine("This feature has not yet been implemented.");

                            Console.WriteLine("Would you like to modify another entry? (y/n)");

                            string repeatModify = Console.ReadLine();

                            repeatModify = repeatModify.ToUpper();

                            if (repeatModify == "N")
                            {
                                reRun = false;
                            }
                        }
                        
                        break;

                    case 0:

                        runApp = false;

                        break;                    

                }          
                         
                            
            }

            Console.WriteLine("Good job tracking your habits! Keep it up!");

            Console.ReadKey();

        }
    }
}
