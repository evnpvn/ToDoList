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
                /*if(mainmenuUserinput == "2")
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
                                                    tasks[index] = editResponse;
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
                            tasks.PrintAllTasks();
                            PrintSubTasksMenu();
                            subTasksInput = ReadLine().ToUpper();

                            MathMethods SubTaskDigitparsing = new MathMethods();
                            int index = SubTaskDigitparsing.ParseDigitToIndex(subTasksInput);

                            if(SubTaskDigitparsing.strippingSuccess == true)
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
                                                //create a list within the list
                                                //this actually may eff up everything else because now the object
                                                //is no longer a list it's a list of lists
                                                //let's play it out and see
                                                //otherwise we will need to change the class
                                                //and initialize it as a list of lists.
                                                
                                                //IF no subtasks exists
                                                //create a list and initialize it with the task as index 0
                                                //and the sub-task as 1
                                                //insert list into the index that the current member is at

                                                //the list will contain the task as 

                                                //IF subtasks exist then add the new subtask as the last item

                                                //Pretty sure this is going to eff everything up.

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
                }*/
            }
            while(mainmenuUserinput != "9");
        }
    }
}