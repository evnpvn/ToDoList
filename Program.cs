using System;
using static ToDoList.TaskHelpers;
using static System.Console;
using System.Collections.Generic;

namespace ToDoList
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //this is now a List of Lists of strings which is null
            Tasklist tasks = new Tasklist{};
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
                        List<string> firstSubList = new List<string>{};
                        firstSubList.Add(firsttaskUserinput);
                        tasks.Add(firstSubList);

                        string twoOrMoreTasksUserInput = "";
                        do
                        {
                            WriteLine();
                            WriteLine("Enter another task and hit return or 0 to exit");
                            twoOrMoreTasksUserInput = ReadLine();

                            if(twoOrMoreTasksUserInput != "0")                      
                            {
                                List<string> nextSubList = new List<string>{};
                                nextSubList.Add(twoOrMoreTasksUserInput);
                                tasks.Add(nextSubList);
                            }
                        }
                        while(twoOrMoreTasksUserInput != "0");
                    }
                }
                if(mainmenuUserinput == "2")
                {      
                    if(tasks.TasksIsNull() == false)
                    {
                        string showAllTasksInput = "";
                        do
                        {
                            tasks.PrintAllTasks();
                            PrintPrioritizeMenu();
                            showAllTasksInput = ReadLine().ToUpper();

                            MathMethods PriorityDigitparsing = new MathMethods();
                            int index = PriorityDigitparsing.ParseDigitToIndex(showAllTasksInput);

                            if(PriorityDigitparsing.strippingSuccess == true)
                            {
                                string prioritySetting = PrioritySetting(showAllTasksInput);
                                try
                                {
                                    tasks.Reprioritize(prioritySetting, index); 
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
                    if(tasks.TasksIsNull() == false)
                    {
                        string editTasksInput = "";
                        do
                        {
                            tasks.PrintAllTasks();
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
                                        if(tasks.ValidateIndex(index) == true)
                                        {
                                            PrintEditSubmenu();
                                            string editResponse = ReadLine();
                                            if(editResponse != "0")
                                            {
                                                try
                                                {
                                                    tasks[index][0] = editResponse;
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
                    if(tasks.TasksIsNull() == false)
                    {
                        string subTasksInput = "";
                        do
                        {
                            //FIXME: some off by 1 error present (no task "1" exists
                            
                            tasks.PrintAllTasks();
                            PrintSubTasksMenu();
                            subTasksInput = ReadLine().ToUpper();

                            MathMethods SubTaskDigitparsing = new MathMethods();
                            int index = SubTaskDigitparsing.ParseDigitToIndex(subTasksInput);
                            
                            if(SubTaskDigitparsing.strippingSuccess == true && subTasksInput != "0")
                            {
                                if(subTasksInput.Contains("SUB"))
                                {
                                    if(tasks.ValidateIndex(index) == true)
                                    {
                                        PrintSubTasksSubMenu();
                                        string subTaskResponse = ReadLine();
                                        if(subTaskResponse != "0")
                                        {
                                            try
                                            {                                               
                                                List<string> subtasklist = tasks[index];
                                                subtasklist.Add(subTaskResponse);                              
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
}