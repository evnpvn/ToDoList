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
                    if(Tasks.Count == 0)
                    {
                        WriteLine();
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

                            PrintPrioritizeMenu();
                            showAllTasksInput = ReadLine();

                            //strip out the digit character from the user input
                            string inputStripped = Regex.Replace(showAllTasksInput, "[^0-9]", "");

                            //parse the digit character in the string into an integer
                            bool strippingSuccess = Int32.TryParse(inputStripped, out int convertedNumber);
                            int index = convertedNumber - 1;

                            if(strippingSuccess)
                            {
                                string prioritySetting = PrioritySetting(showAllTasksInput);

                                try
                                {           
                                    //FIXME: Need to handle this exception. Printing to the console won't happen
                                    //because the exeption is being thrown first
                                    Tasks.Reprioritize(prioritySetting, index); 
                                }
                                catch(System.ArgumentOutOfRangeException)
                                {
                                    WriteLine("No task number \"" + convertedNumber + "\" exists");
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