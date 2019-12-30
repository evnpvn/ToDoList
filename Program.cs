using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ToDoList
{
    public class Program
    {
        public static void Main(string[] args)
        {
            List<string> Tasks = new List<string>{};

            string mainmenuUserinput = "";

            do
            {
                //prompt the user for what they want to do
                Console.WriteLine();
                Console.WriteLine("Main Menu");
                Console.WriteLine("To select any of the functions below enter the related number and hit return");
                Console.WriteLine("1 - Create a new task");
                Console.WriteLine("2 - Show all tasks");
                Console.WriteLine("9 - Exit program");
            
                mainmenuUserinput = Console.ReadLine();

                //prompt the user for input from the console to create a task
                if(mainmenuUserinput == "1")
                {
                    Console.WriteLine("Enter a task and hit return or 0 to exit");
                    string firsttaskUserinput = Console.ReadLine();
                    if(firsttaskUserinput != "0")
                    {
                        Tasks.Add(firsttaskUserinput);
                        string twoOrMoreTasksUserInput = "";
                        do
                        {
                            Console.WriteLine("Enter another task and hit return or 0 to exit");
                            twoOrMoreTasksUserInput = Console.ReadLine();

                            if(twoOrMoreTasksUserInput == "0")
                            {
                               //do nothing;
                            }
                            else                         
                            {
                                Tasks.Add(twoOrMoreTasksUserInput);
                            }
                        }
                        while(twoOrMoreTasksUserInput != "0");
                    }
                }
                
                if(mainmenuUserinput == "2")
                {      
                    if(Tasks.Count == 0)
                    {
                        Console.WriteLine("No tasks found.");
                    }
                    else
                    {
                        string showAllTasksInput = "";
                        do
                        {
                            Console.WriteLine();
                            Console.WriteLine("Current Tasks");
                            for(int priority = 1; priority <= Tasks.Count; priority++)
                            {
                                Console.WriteLine(priority + ": " + Tasks[priority - 1]);
                            }
                            Console.WriteLine();
                            Console.WriteLine("To reprioritize tasks enter the task number + any of the following options"); 
                            Console.WriteLine(" \"highest\" - make task the highest priority  ");
                            Console.WriteLine(" \"lowest\" - make task the lowest priority  ");
                            Console.WriteLine(" \"higher\" - move task up 1 spot in the current order");
                            Console.WriteLine(" \"lower\" - move task down 1 spot in the current order");
                            Console.WriteLine("Example: \"3 highest\"");
                            Console.WriteLine("Or to return to the main menu enter 0");

                            showAllTasksInput = Console.ReadLine();

                            //strip out the digit character from the user input
                            string inputStripped = Regex.Replace(showAllTasksInput, "[^0-9]", "");
                            //parse the digit character in the string into an integer
                            bool success = Int32.TryParse(inputStripped, out int convertedNumber);

                            //TODO: Handle out of range execeptions 
                            //if the index specified by the user is larger than the currently size of the Task list

                            if(success)
                            {
                                if(showAllTasksInput.Contains("highest"))
                                {
                                    string taskItem = Tasks[convertedNumber - 1];
                                    Tasks.RemoveAt(convertedNumber - 1);
                                    Tasks.Insert(0, taskItem);
                                }                             
                                else if(showAllTasksInput.Contains("lowest"))
                                {
                                    string taskItem = Tasks[convertedNumber - 1];
                                    int taskCount = Tasks.Count;
                                    Tasks.RemoveAt(convertedNumber - 1);
                                    Tasks.Insert(taskCount - 1, taskItem);                               
                                }
                                else if(showAllTasksInput.Contains("higher"))
                                {
                                    string taskItem = Tasks[convertedNumber - 1];
                                    Tasks.RemoveAt(convertedNumber - 1); 
                                    Tasks.Insert(convertedNumber - 2 , taskItem);
                                }
                                else if(showAllTasksInput.Contains("lower"))
                                {
                                    string taskItem = Tasks[convertedNumber - 1];
                                    Tasks.RemoveAt(convertedNumber - 1); 
                                    Tasks.Insert(convertedNumber, taskItem);
                                }
                            }
                        }
                        while(showAllTasksInput != "0");                                           
                    }  
                }
            }
            while(mainmenuUserinput != "9");
        }
    }
}