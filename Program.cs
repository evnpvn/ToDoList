using System;
using System.Text.RegularExpressions;
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
                //prompt the user for what they want to do
                WriteLine();
                WriteLine("Main Menu");
                WriteLine("To select any of the functions below enter the related number and hit return");
                WriteLine("1 - Create a new task");
                WriteLine("2 - Show all tasks");
                WriteLine("9 - Exit program");
            
                mainmenuUserinput = ReadLine();

                //prompt the user for input from the console to create a task
                if(mainmenuUserinput == "1")
                {
                    WriteLine("Enter a task and hit return or 0 to exit");
                    string firsttaskUserinput = ReadLine();
                    if(firsttaskUserinput != "0")
                    {
                        Tasks.Add(firsttaskUserinput);
                        string twoOrMoreTasksUserInput = "";
                        do
                        {
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
                    if(Tasks.Count == 0)
                    {
                        WriteLine("No tasks found.");
                    }
                    else
                    {
                        string showAllTasksInput = "";
                        do
                        {
                            WriteLine();
                            WriteLine("Current Tasks");
                            for(int priority = 1; priority <= Tasks.Count; priority++)
                            {
                                WriteLine(priority + ": " + Tasks[priority - 1]);
                            }
                            WriteLine();
                            WriteLine("To reprioritize tasks enter the task number + any of the following options"); 
                            WriteLine(" \"highest\" - make task the highest priority  ");
                            WriteLine(" \"lowest\" - make task the lowest priority  ");
                            WriteLine(" \"higher\" - move task up 1 spot in the current order");
                            WriteLine(" \"lower\" - move task down 1 spot in the current order");
                            WriteLine("Example: \"3 highest\"");
                            WriteLine("Or to return to the main menu enter 0");

                            showAllTasksInput = ReadLine();

                            //strip out the digit character from the user input
                            string inputStripped = Regex.Replace(showAllTasksInput, "[^0-9]", "");
                            //parse the digit character in the string into an integer
                            bool success = Int32.TryParse(inputStripped, out int convertedNumber);

                            //FIXME: Handle out of range execeptions 
                            //if the index specified by the user is larger than the currently size of the Task list

                            if(success)
                            {
                                string prioritySetting = PrioritySetting(showAllTasksInput);
                                Tasks.Reprioritize(prioritySetting, convertedNumber);
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