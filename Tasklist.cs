using System;
using System.Collections.Generic;

namespace ToDoList
{
    public class Tasklist : List<string>
    {
        public void Reprioritize(string prioritySetting, int indexPlusOne)
        {
            string taskItem = this[indexPlusOne - 1];
            this.RemoveAt(indexPlusOne - 1);

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
                this.Insert(indexPlusOne - 2 , taskItem);
            }
            else if(prioritySetting == "lower")
            {
                this.Insert(indexPlusOne, taskItem);
            }
        }
    }
}