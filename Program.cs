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
                    //Create database
                    //Remove the Drop database command once up and running so data is not deleted each run of program
                    //TODO: make drop database an action in the TODO list menu
                    string sql = "DROP DATABASE IF EXISTS [TodoDB]; CREATE DATABASE [TodoDB]";
                    using(SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    //Create the ToDoList Table
                    StringBuilder strBuilder = new StringBuilder();
                    strBuilder.Append("USE TodoDB; ");
                    strBuilder.Append("CREATE TABLE TodoList ( ");
                    strBuilder.Append("Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY, ");
                    strBuilder.Append("ParentTask TEXT, ");
                    strBuilder.Append("SubTask1 TEXT, ");
                    strBuilder.Append("SubTask2 TEXT, ");
                    strBuilder.Append("SubTask3 TEXT, ");
                    strBuilder.Append("SubTask4 TEXT, ");
                    strBuilder.Append("SubTask5 TEXT ");
                    strBuilder.Append("); ");
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