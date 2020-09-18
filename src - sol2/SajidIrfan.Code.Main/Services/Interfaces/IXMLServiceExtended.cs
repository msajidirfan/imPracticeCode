using SajidIrfan.Code.Models;
using System.Collections.Generic;

namespace SajidIrfan.Code.Services
{
    /// <summary>
    /// XML Service that performs CRUD operations using EmployeeExtended object in XML file
    /// </summary>
    public interface IXMLServiceExtended
    {
        /// <summary>
        /// Prints all employees to screen
        /// </summary>
        /// <returns>Read Status</returns>
        bool ReadAllEmployees();

        /// <summary>
        /// Get Employee By Name
        /// </summary>
        /// <param name="name">Name of Employee</param>
        /// <returns>Employee Details, if Name match</returns>
        EmployeeExtended GetEmployeeByName(string name);

        /// <summary>
        /// Gets all employees count from XML. 
        /// </summary>
        /// <returns>all employees count</returns>
        int GetAllEmployeesCount();

        /// <summary>
        /// Adds new Employee to XML
        /// </summary>
        /// <returns>Add Status</returns>
        bool AddEmployee();

        /// <summary>
        /// Updates existing Employee to XML
        /// </summary>
        /// <param name="empName">Name of Employee</param>
        /// <returns>Update Status</returns>
        bool UpdateEmployee(string empName);

        /// <summary>
        /// Deletes existing Employee from XML
        /// </summary>
        /// <param name="empName">Name of Employee</param>
        /// <returns>Delete Status</returns>
        bool DeleteEmployee(string empName);

        /// <summary>
        /// Gets all employees from XML. 
        /// </summary>
        /// <returns>all employees</returns>
        List<EmployeeExtended> GetAllEmployees();
    }
}
