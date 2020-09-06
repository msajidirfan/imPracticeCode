using System;
using System.Collections.Generic;
using System.Text;

namespace SajidIrfan.Code.Models
{
    /// <summary>
    /// Employee Entity
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// EmployeeID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// EmployeeName
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Employee Age
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Employee Designation
        /// </summary>
        public string Designation { get; set; }
    }
}
