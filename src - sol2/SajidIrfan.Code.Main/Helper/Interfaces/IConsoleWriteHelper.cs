namespace SajidIrfan.Code.Helper
{
    public interface IConsoleWriteHelper
    {
        /// <summary>
        /// Writes Initial Message to console.
        /// </summary>
        void ConsoleInitialMessage();

        /// <summary>
        /// Print Delete Message
        /// </summary>
        void DeleteMessage();

        /// <summary>
        /// Print Add Message
        /// </summary>
        void AddMessage();

        /// <summary>
        /// Print Update Message
        /// </summary>
        void UpdateMessage();

        /// <summary>
        /// Print Read All Message
        /// </summary>
        void ReadAllMessage();

        /// <summary>
        /// Print Next Operation Message
        /// </summary>
        void NextOperationMessage();

        /// <summary>
        /// Print Invalid Operation Message
        /// </summary>
        void InvalidOperationMessage();

        /// <summary>
        /// Print Closing Thanks Message
        /// </summary>
        void ClosingThanksMessage();
    }
}