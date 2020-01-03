using System;
using System.Collections.Generic;

namespace ToDoList
{
    public class Tasklist : List<string>
    {
        public void Reprioritize(string prioritySetting, int index)
        {   
            string taskItem = this[index];
            

            //FIXME: currently if you move the last item in the list lower if drops the item from the list
            if(prioritySetting == "highest")
            {
                this.RemoveAt(index);
                this.Insert(0, taskItem);
            }            
            else if(prioritySetting == "lowest")
            {
                this.RemoveAt(index);
                this.Insert(this.Count - 1, taskItem); 
            }
            else if(prioritySetting == "higher" && this.IndexOf(taskItem) != 0)
            {
                this.RemoveAt(index);
                this.Insert(index - 1 , taskItem);
            }
            else if(prioritySetting == "lower" && this.IndexOf(taskItem) != (this.Count - 1))
            {
                this.RemoveAt(index);
                this.Insert(index + 1, taskItem);
            }
            else
            //FIXME: the rejected scenarios from the higher and lower conditions where the index is out of range 
            //are falling into here.
            //they 
            {   
                Console.WriteLine();
                Console.WriteLine("Not a valid re-prioritization option");
            }
        }
    }
}