using System;
using static System.Console;

namespace ToDoList
{
    public static class TaskHelpers
    {
        public static void PrintMainMenu()
        {
            WriteLine();
            WriteLine("Main Menu");
            WriteLine("To select any of the functions below enter the related number and hit return");
            WriteLine("1 - Create a new task");
            WriteLine("2 - Show all tasks");
            WriteLine("9 - Exit program");
        }

        public static void PrintPrioritizeMenu()
        {
            WriteLine();
            WriteLine("To reprioritize tasks enter the task number + any of the following options"); 
            WriteLine(" highest - make task the highest priority  ");
            WriteLine(" lowest - make task the lowest priority  ");
            WriteLine(" higher - move task up 1 spot in the current order");
            WriteLine(" lower - move task down 1 spot in the current order");
            WriteLine("Example: 3 highest");
            WriteLine("Or to return to the main menu enter 0");
        }

        public static string PrioritySetting(string showAllTasksInput)
        {
            if(showAllTasksInput.Contains("HIGHEST"))
            {
                return "highest";
            }
            else if(showAllTasksInput.Contains("LOWEST"))
            {
                return "lowest";
            }
            else if(showAllTasksInput.Contains("HIGHER"))
            {
                return "higher";
            }
            else if(showAllTasksInput.Contains("LOWER"))
            {
                return "lower";
            }
            else
            {
                return ""; 
            }
        }
    }
}