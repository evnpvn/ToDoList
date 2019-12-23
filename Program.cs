using System;

namespace ToDoList
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //prompt the user for what they want to do
            Console.WriteLine("To select any of the functions below enter the related number and hit return");
            Console.WriteLine("1 - Create a new task");
            Console.WriteLine("2 - Show all tasks");
        
            string firstUserInput = Console.ReadLine();

            //prompt the user for input from the console to create a task
            if(firstUserInput == "1")
            {
                Console.WriteLine("Enter a task and hit return or 0 to exit");
                string SecondUserInput = Console.ReadLine();
                if(SecondUserInput == "0")
                {
                    
                }


                string task1 = Console.ReadLine();

                Console.WriteLine("Enter another task and hit return");
                string task2 = Console.ReadLine();
                //make it take multiple tasks
            }
            

        }
    }
}