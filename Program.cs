using System;
using static ToDoList.TaskHelpers;
using static System.Console;

namespace ToDoList
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Tasklist Tasks = new Tasklist{};
            string mainmenuUserinput = "";

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
                                    WriteLine("No task number \"" + (index + 1) + "\" exists");
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
                                                    WriteLine("No task number \"" + (index + 1) + "\" exists");
                                                }
                                            }
                                        }
                                        else
                                        {
                                            WriteLine("No task number \"" + (index + 1) + "\" exists");
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
            }
            while(mainmenuUserinput != "9");
        }
    }
}