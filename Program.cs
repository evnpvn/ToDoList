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
                    if(Tasks.TasksIsNull() == false)
                    {
                        string showAllTasksInput = "";
                        do
                        {
                            Tasks.PrintAllTasks();
                            PrintPrioritizeMenu();
                            showAllTasksInput = ReadLine().ToUpper();

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

                            //parse response to see if they prompted editing.
                            //strip out the digit character from the user input
                            string inputStripped = Regex.Replace(editTasksInput, "[^0-9]", "");

                            //parse the digit character in the string into an integer
                            bool strippingSuccess = Int32.TryParse(inputStripped, out int convertedNumber);
                            int index = convertedNumber - 1;

                            if(strippingSuccess)
                            {
                                if(editTasksInput.Contains("EDIT"))
                                {
                                    PrintEditSubmenu();
                                    string editResponse = ReadLine();
                                    Tasks[index] = editResponse;
                                    //FIXME: currently cancelling also edits the task to "0"
                                    //add an if statement for this separate condition

                                    //FIXME: handle outofrange
                                    //this should be handled right before the submenu is presented.
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