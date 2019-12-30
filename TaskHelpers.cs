using System;
using System.Collections.Generic;

namespace ToDoList
{
    public static class TaskHelpers
    {
        public static string PrioritySetting(string showAllTasksInput)
        {
            if(showAllTasksInput.Contains("highest"))
            {
                return "highest";
            }
            else if(showAllTasksInput.Contains("lowest"))
            {
                return "lowest";
            }
            else if(showAllTasksInput.Contains("higher"))
            {
                return "higher";
            }
            else if(showAllTasksInput.Contains("lower"))
            {
                return "lower";
            }
            else
            {
                Console.WriteLine("Invalid input value for setting task priority");
                return ""; 
            }
        }
    }
}