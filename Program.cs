using System;
using static ToDoList.TaskHelpers;
using static System.Console;
using System.Data.SqlClient;
using System.Text;


namespace ToDoList
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Tasklist Tasks = new Tasklist{};
            string mainmenuUserinput = "";

            //Connect to SQL Database
            try
            {
                string connectString = "Server=localhost\\SQLEXPRESS;Database=master;Trusted_Connection=True;";
                using (SqlConnection connection = new SqlConnection(connectString))
                {
                    connection.Open();
                    //Create database if it doesn't already exist
                    
                    StringBuilder strBuildy = new StringBuilder();
                    strBuildy.Append("IF (db_id(N'TodoDB') IS NULL) ");
                    strBuildy.Append("BEGIN ");
                    strBuildy.Append("CREATE DATABASE [TodoDB] ");
                    strBuildy.Append("END;");
                    string sql = strBuildy.ToString();
                    using(SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    //Create the ToDoList Table if it doesn't already exist
                    StringBuilder strBuilder = new StringBuilder();
                    strBuilder.Append("USE TodoDB; ");
                    strBuilder.Append("IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'TodoList')");
                    strBuilder.Append("BEGIN ");
                    strBuilder.Append("CREATE TABLE TodoList ( ");
                    strBuilder.Append("Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY, ");
                    strBuilder.Append("ParentTask TEXT, ");
                    strBuilder.Append("SubTask1 TEXT, ");
                    strBuilder.Append("SubTask2 TEXT, ");
                    strBuilder.Append("SubTask3 TEXT, ");
                    strBuilder.Append("SubTask4 TEXT, ");
                    strBuilder.Append("SubTask5 TEXT ");
                    strBuilder.Append("); ");
                    strBuilder.Append("END;");
                    sql = strBuilder.ToString();

                    using(SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    do
                    {
                        PrintMainMenu();         
                        mainmenuUserinput = ReadLine();
                        if(mainmenuUserinput == "1")
                        {
                            WriteLine();
                            WriteLine("Enter a task and hit return or 0 to exit");
                            string firsttaskUserinput = ReadLine();
                            if(firsttaskUserinput != "0")
                            {
                                Tasks.Add(firsttaskUserinput);
                                string twoOrMoreTasksUserInput = "";
                                do
                                {
                                    WriteLine();
                                    WriteLine("Enter another task and hit return or 0 to exit");
                                    twoOrMoreTasksUserInput = ReadLine();

                                    if(twoOrMoreTasksUserInput != "0")                      
                                    {
                                        Tasks.Add(twoOrMoreTasksUserInput);
                                    }
                                }
                                while(twoOrMoreTasksUserInput != "0");
                            }
                        }
                        if(mainmenuUserinput == "2")
                        {      
                            if(Tasks.TasksIsNull() == false)
                            {
                                string showAllTasksInput = "";
                                do
                                {
                                    Tasks.PrintAllTasks();
                                    PrintPrioritizeMenu();
                                    showAllTasksInput = ReadLine().ToUpper();

                                    MathMethods PriorityDigitparsing = new MathMethods();
                                    int index = PriorityDigitparsing.ParseDigitToIndex(showAllTasksInput);

                                    if(PriorityDigitparsing.strippingSuccess == true)
                                    {
                                        string prioritySetting = PrioritySetting(showAllTasksInput);
                                        try
                                        {
                                            Tasks.Reprioritize(prioritySetting, index); 
                                        }
                                        catch(System.ArgumentOutOfRangeException)
                                        {
                                            NoTaskExists(index);
                                        }
                                    }
                                }
                                while(showAllTasksInput != "0");                                           
                            }  
                        }
                        if(mainmenuUserinput == "3")
                        {      
                            if(Tasks.TasksIsNull() == false)
                            {
                                string editTasksInput = "";
                                do
                                {
                                    Tasks.PrintAllTasks();
                                    PrintEditMenu();
                                    editTasksInput = ReadLine().ToUpper();
                                    
                                    if(editTasksInput != "0")
                                    {
                                        MathMethods EditingDigitparsing = new MathMethods();
                                        int index = EditingDigitparsing.ParseDigitToIndex(editTasksInput);

                                        if(EditingDigitparsing.strippingSuccess == true)
                                        {
                                            if(editTasksInput.Contains("EDIT"))
                                            {
                                                if(Tasks.ValidateIndex(index) == true)
                                                {
                                                    PrintEditSubmenu();
                                                    string editResponse = ReadLine();
                                                    if(editResponse != "0")
                                                    {
                                                        try
                                                        {
                                                            Tasks[index] = editResponse;
                                                        }
                                                        catch(System.ArgumentOutOfRangeException)
                                                        {
                                                            NoTaskExists(index);
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    NoTaskExists(index);
                                                }
                                            }
                                            else
                                            {
                                                WriteLine();
                                                WriteLine("Not a valid option entered");
                                            }
                                        }
                                    }
                                }
                                while(editTasksInput != "0");
                            } 
                        }
                        if(mainmenuUserinput == "4")
                        {
                            if(Tasks.TasksIsNull() == false)
                            {
                                string subTasksInput = "";
                                do
                                {
                                    Tasks.PrintAllTasks();
                                    PrintSubTasksMenu();
                                    subTasksInput = ReadLine().ToUpper();

                                    MathMethods SubTaskDigitparsing = new MathMethods();
                                    int index = SubTaskDigitparsing.ParseDigitToIndex(subTasksInput);

                                    if(SubTaskDigitparsing.strippingSuccess == true)
                                    {
                                        if(subTasksInput.Contains("SUB"))
                                        {
                                            if(Tasks.ValidateIndex(index) == true)
                                            {
                                                PrintSubTasksSubMenu();
                                                string subTaskResponse = ReadLine();
                                                if(subTaskResponse != "0")
                                                {
                                                    try
                                                    {
 
                                                    }
                                                    catch(System.ArgumentOutOfRangeException)
                                                    {
                                                        NoTaskExists(index);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                NoTaskExists(index);
                                            }
                                        }
                                        else
                                        {
                                            WriteLine();
                                            WriteLine("Not a valid option entered");
                                        }
                                    }
                                }
                                while(subTasksInput != "0");                                           
                            }  
                        }
                        if(mainmenuUserinput == "5")
                        {
                            WriteLine("Enter \"Reset\" to confirm deletion of all current tasks in task list");
                            WriteLine("Or press any key to return to main menu");

                            if(ReadLine().ToUpper() == "RESET")
                            {
                                string sqlString = "DELETE FROM Todolist;";
                                using(SqlCommand command = new SqlCommand(sqlString, connection))
                                {
                                    int rowsAffected = command.ExecuteNonQuery();
                                    WriteLine(rowsAffected + " Tasks deleted");
                                }
                            }
                        }
                    }
                    while(mainmenuUserinput != "9");
                }
            }
            catch(SqlException exception)
            {
                WriteLine(exception.ToString());
            }
        }
    }
}