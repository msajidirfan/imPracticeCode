namespace SajidIrfan.Code.Models
{
    public class AppSettings
    {
        /// <summary>
        /// Sets header title in console window
        /// </summary>
        public string ConsoleTitle { get; set; }

        /// <summary>
        /// Sets the path of XML file
        /// </summary>
        public string XMLFilePath { get; set; }

        /// <summary>
        /// Sets the limit for valid operation count
        /// </summary>
        public int ValidOperationCount { get; set; }

        /// <summary>
        /// Sets the limit for Maximun Repeation Allowed
        /// </summary>
        public int MaxRepeatAllowed { get; set; }
    }
}
