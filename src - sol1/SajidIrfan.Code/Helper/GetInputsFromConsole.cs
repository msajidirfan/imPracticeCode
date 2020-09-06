using System;
using System.Collections.Generic;
using System.Text;

namespace SajidIrfan.Code.Helper
{
    public static class GetInputsFromConsole
    {

        /// <summary>
        /// Get input from console and validates the input 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>data entered by user</returns>
        public static string GetDetails<T>()
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
                else if (type.Name.ToLower() == "int32" && Convert.ToInt32(input) > 0)
                {
                    result = input;
                }
                else
                {
                    System.Console.WriteLine($"Invalid input. Please retry.");
                }
            }
            catch (System.Exception)
            {
                System.Console.WriteLine($"Invalid input. Please retry.");
            }

            return result;
        }
    }
}
