using System;
using System.Collections.Generic;
using System.Text;

namespace SajidIrfan.Code.Helper
{
    public class ConsoleInputsHelper : IConsoleInputsHelper
    {

        /// <summary>
        /// Get input from console and validates the input 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>data entered by user</returns>
        public string GetDetails<T>()
        {
            string result = "";
            try
            {
                var input = System.Console.ReadLine();
                var type = typeof(T);
                if (type.Name.ToLower() == "string")
                {
                    result = input;
                }
                else if (type.Name.ToLower() == "int32" && Convert.ToInt32(input) > 0) //validation
                {
                    result = input;
                }
                else
                {
                    System.Console.WriteLine($"Invalid Type. Doesn't support this type.");
                }
            }
            catch (System.Exception)
            {
                System.Console.WriteLine($"Invalid input. Please retry.");
            }

            return result;
        }


        /// <summary>
        /// Holds the screen
        /// </summary>
        public void ClosingHolder()
        {
            System.Console.ReadKey();
        }
    }
}
