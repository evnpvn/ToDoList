using System;
using System.Text.RegularExpressions;

namespace ToDoList
{
    public class MathMethods
    {
        private bool _strippingSuccess;
        public bool strippingSuccess { get => _strippingSuccess; set => _strippingSuccess = true; }

        public MathMethods()
        { }

        public int ParseDigitToIndex(string userInput)
        {
            //strip out the digit character from the user input
            string inputStripped = Regex.Replace(userInput, "[^0-9]", "");

            //parse the digit character in the string into an integer
            strippingSuccess = Int32.TryParse(inputStripped, out int convertedNumber);

            //FIXME: need to return the index + the stripping success bool
            return (convertedNumber - 1);
        }
    }
}