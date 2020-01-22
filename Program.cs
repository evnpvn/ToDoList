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

            //Connect to SQL server and initiate database and table 
            DatabaseHelpers dbHelpers = new DatabaseHelpers();
            dbHelpers.dbServerConnect();
            dbHelpers.dbCreate();
            dbHelpers.dbTableCreate();
            //dbHelpers.dbInsertTestRecs();

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
                        dbHelpers.dbTableClear();
                    }
                }
            }
            while(mainmenuUserinput != "9");
        }
    }
}