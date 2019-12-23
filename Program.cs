using System;
using System.Collections.Generic;

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
                Console.WriteLine("3 - Exit program");
            
                mainmenuUserinput = Console.ReadLine();

                //prompt the user for input from the console to create a task
                if(mainmenuUserinput == "1")
                {
                    Console.WriteLine("Enter a task and hit return or 0 to exit");
                    string firsttaskUserinput = Console.ReadLine();
                    if(firsttaskUserinput == "0")
                    {
                        //break;
                    }
                    else 
                    {
                        Tasks.Add(firsttaskUserinput);
                        string twoOrMoreTasksUserInput = "";
                        do
                        {
                            Console.WriteLine("Enter another task and hit return or 0 to exit");
                            twoOrMoreTasksUserInput = Console.ReadLine();

                            if(twoOrMoreTasksUserInput == "0")
                            {
                               //break;
                            }
                            else                         
                            {
                                Tasks.Add(twoOrMoreTasksUserInput); //embed the line above once the new list functionality works
                            }

                        }
                        while(twoOrMoreTasksUserInput != "0");
                    }
                }
                
                if(mainmenuUserinput == "2")
                {
                    foreach(string task in Tasks)
                    {
                        Console.WriteLine(task);
                    }
                }

            } while(mainmenuUserinput != "3");
        }
    }
}