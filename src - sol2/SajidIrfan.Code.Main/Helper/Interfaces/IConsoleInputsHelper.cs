namespace SajidIrfan.Code.Helper
{
    public interface IConsoleInputsHelper
    {


        /// <summary>
        /// Holds the screen
        /// </summary>
        void ClosingHolder();

        /// <summary>
        /// Get input from console and validates the input 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>data entered by user</returns>
        string GetDetails<T>();

    }
}