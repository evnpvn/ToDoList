using System;
using System.Collections.Generic;

namespace ToDoList
{
    public class Tasklist : List<string>
    {
        public void Reprioritize(string prioritySetting, int index)
        {   
            string taskItem = this[index];
            this.RemoveAt(index);

            if(prioritySetting == "highest")
            {
                this.Insert(0, taskItem);
            }            
            else if(prioritySetting == "lowest")
            {
                this.Insert(this.Count - 1, taskItem); 
            }
            else if(prioritySetting == "higher")
            {
                this.Insert(index - 1 , taskItem);
            }
            else if(prioritySetting == "lower")
            {
                this.Insert(index + 1, taskItem);
            }
        }
    }
}