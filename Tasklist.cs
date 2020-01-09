using System;
using System.Collections.Generic;
using static System.Console;

namespace ToDoList
{
    public class Tasklist : List<string>
    {
        public bool TasksIsNull()
        {          
            if(this.Count == 0)
            {
                WriteLine();
                WriteLine("No tasks found.");
                return true;
            }
            else
            {
                return false;
            }            
        }

        public bool ValidateIndex(int index)
        {
            if(index < (this.Count) && index > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void PrintAllTasks()
        {
            WriteLine();
            WriteLine("Current Tasks");
            for(int priority = 1; priority <= this.Count; priority++)
            {
                WriteLine(priority + ": " + this[priority - 1]);
            }
        }

        public void Reprioritize(string prioritySetting, int index)
        {   
            string taskItem = this[index];
            
            if(prioritySetting == "highest")
            {
                this.RemoveAt(index);
                this.Insert(0, taskItem);
            }            
            else if(prioritySetting == "lowest")
            {
                this.RemoveAt(index);
                this.Insert(this.Count, taskItem); 
            }
            else if(prioritySetting == "higher")
            {
                if(this.IndexOf(taskItem) != 0)
                {
                    this.RemoveAt(index);
                    this.Insert(index - 1 , taskItem);
                }
                else
                {
                    WriteLine("This task is your number 1 priority. It can't move any higher");
                }
            }
            else if(prioritySetting == "lower")
            {
                if(this.IndexOf(taskItem) != (this.Count - 1))
                {
                    this.RemoveAt(index);
                    this.Insert(index + 1, taskItem);
                }
                else
                {
                    WriteLine("This task is your lowest priority. It can't move any lower");
                }
            }
            else
            {   
                WriteLine();
                WriteLine("Not a valid re-prioritization option");
            }
        }
    }
}