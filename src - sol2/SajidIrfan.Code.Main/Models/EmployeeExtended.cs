namespace SajidIrfan.Code.Models
{
    /// <summary>
    /// Extending Employee by adding new properties
    /// </summary>
    public class EmployeeExtended : Employee
    {

        /// <summary>
        /// Employee Designation
        /// </summary>
        public Address Address { get; set; }
        public bool IsSuccess { get; set; }

    }
}
