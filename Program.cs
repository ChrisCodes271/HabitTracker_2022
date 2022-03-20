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
                    Console.WriteLine("------------------------------------Welcome to Chris's Water Consuption Tracker!------------------------------------");                                        
                    Console.WriteLine("[1] Insert a new entry into your table");
                    Console.WriteLine("[2] View your table");
                    Console.WriteLine("[3] Delete an entry from your table");
                    Console.WriteLine("[4] Change an entry from your table");              //Options for user to choose from
                    Console.WriteLine("[0] Exit");
                    Console.WriteLine("Please select an option:");

                    try  //Input validation for user options. 
                    {
                        userInput = Convert.ToInt32(Console.ReadLine());

                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Please ensure you enter a number for your option.");
                    }

                    bool reRun = true;

                    switch (userInput)
                    {
                        case 1: //Case for adding another entry
                            while (reRun)
                            {

                                Console.WriteLine("What date do you wish to input data for?"); //These 2 blocks take a date string, and bottlesdrank int for later
                                string userDateInsert = Console.ReadLine();
                                Convert.ToString(userDateInsert);

                                Console.WriteLine("How many bottles of water did you drink?");
                                string userWaterInsert = Console.ReadLine();
                                
                                bool entryCheck = true;

                                int number = 99999999; //Establishing value for number to be modified later. 

                                while (entryCheck)
                                {

                                    Console.Write("How many bottles of water did you drink?");  //Integer established for entry deleted.
                                    userWaterInsert = Console.ReadLine();

                                    bool validIdEntry = int.TryParse(userWaterInsert, out number); //Attempt to parse users input to an integer. Return to assigned bool

                                    if (validIdEntry) //If parse was sucessufl. Exit while loop
                                    {
                                        entryCheck = false;
                                    }
                                }

                                string newEntry = $"INSERT INTO WaterConsumed (date, bottlesdrank) values ('{userDateInsert}' , {number})"; //insert string value from first statement and int value from second as Date, and Bottlesdrank on table. 

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

                        case 2: //Case for viewing table
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

                        case 3: //Case for deleting an entry

                            while (reRun)
                            {

                                string idToDelete;                                
                                bool entryCheck = true;
                                int number = 99999999; //Establishing value for number to be modified later. 

                                while (entryCheck)                                {
                                    

                                    Console.Write("Please enter the Entry number you wish to delete: ");  //Integer established for entry deleted.
                                    idToDelete = Console.ReadLine();                                    

                                    bool validIdEntry = int.TryParse(idToDelete, out number); //Attempt to parse users input to an integer. Return to assigned bool

                                    if (validIdEntry) //If parse was sucessufl. Exit while loop
                                    {
                                        entryCheck = false;
                                    }
                                }                               


                                string deleteFromTable = $"DELETE FROM WaterConsumed WHERE id={number} "; //SQL Code that will delete id entry. 

                                SQLiteCommand deleteCommand = new SQLiteCommand(deleteFromTable, m_dbConnection);

                                deleteCommand.ExecuteNonQuery();
                                
                                Console.WriteLine("Would you like to delete more information? (y/n)");

                                string repeatDelete = Console.ReadLine();

                                repeatDelete = repeatDelete.ToUpper();

                                if (repeatDelete == "Y") //Modified the input validation for repeating deletion confirmation.
                                                         //By default it will now exit instead of repeating the deletion process if a miskey is input. 
                                {
                                    reRun = true;
                                }
                                else
                                {
                                    reRun = false;
                                }
                            }
                            break;

                        case 4: //Case for modifying a current entry

                            while (reRun)
                            {                                
                                string idToModify;
                                bool entryCheck = true;
                                int number = 99999999; //Establishing value for number to be modified later. 

                                while (entryCheck)
                                {
                                    Console.Write("Please enter the Entry number you wish to modify: ");  //Integer established for entry deleted.
                                    idToModify = Console.ReadLine();

                                    bool validIdEntry = int.TryParse(idToModify, out number); //Attempt to parse users input to an integer. Return to assigned bool

                                    if (validIdEntry) //If parse was sucessufl. Exit while loop
                                    {
                                        entryCheck = false;
                                    }
                                }

                                Console.WriteLine("What is the correct date for this entry? : "); //These 2 blocks take a date string, and bottlesdrank int for later
                                string userDateInsert = Console.ReadLine();
                                Convert.ToString(userDateInsert);

                                string bottlesMod;
                                bool entryCheck2 = true;
                                int number2 = 999999;

                                while (entryCheck2)
                                {
                                    Console.Write("What is the correct number of bottles drank for this entry? : ");  //Integer established for entry deleted.
                                    bottlesMod = Console.ReadLine();

                                    bool validIdEntry2 = int.TryParse(bottlesMod, out number2); //Attempt to parse users input to an integer. Return to assigned bool

                                    if (validIdEntry2) //If parse was sucessufl. Exit while loop
                                    {
                                        entryCheck2 = false;
                                    }
                                }

                                string newEntry = $"INSERT or REPLACE INTO WaterConsumed (id, date, bottlesdrank) values ( {number} , '{userDateInsert}' , {number2})"; 

                                SQLiteCommand sqlNewEntry = new SQLiteCommand(newEntry, m_dbConnection);

                                sqlNewEntry.ExecuteNonQuery();

                                Console.WriteLine("Would you like to modify another entry? (y/n)");

                                string repeatInput = Console.ReadLine();

                                repeatInput = repeatInput.ToUpper();

                                if (repeatInput == "N")
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

            }
            Console.WriteLine("Good job tracking your habits! Keep it up!");

            Console.ReadKey();
        }
    }
}
