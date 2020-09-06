using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SajidIrfan.Code.Helper;
using SajidIrfan.Code.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace SajidIrfan.Code.Services
{

    public class XMLService : IXMLService
    {
        private static string xmlPath = "";
        private readonly ILogger<TestService> _logger;
        private readonly AppSettings _config;
        //private int iNumberOfEntries = 1;
        //private XDocument doc;

        public XMLService(ILogger<TestService> logger,
            IOptions<AppSettings> config)
        {
            _logger = logger;
            _config = config.Value;
            xmlPath = config.Value.XMLFilePath;
        }

        //public void Run()
        //{
        //    _logger.LogWarning($"Wow! We are now in the test service of: {_config.ConsoleTitle}");
        //}


        /// <summary>
        /// Prints all employees to screen
        /// </summary>
        /// <returns>Read Status</returns>
        public bool ReadAllEmployees()
        {
            List<Employee> employees = new List<Employee>();

            _logger.LogTrace("ReadAll starts.");
            try
            {
                var doc = XDocument.Load(Directory.GetCurrentDirectory() + xmlPath);
                foreach (var node in doc.Descendants("employee"))
                {
                    employees.Add(new Employee
                    {
                        ID = Int32.Parse(node.Descendants("id")?.FirstOrDefault()?.Value ?? "0"),
                        Name = node.Descendants("name").FirstOrDefault().Value,
                        Age = Int32.Parse(node.Descendants("age").FirstOrDefault().Value),
                        Designation = node.Descendants("designation").FirstOrDefault().Value,
                    });
                }
                System.Console.WriteLine("Employee Details");
                foreach (var employee in employees)
                {
                    System.Console.WriteLine(JsonConvert.SerializeObject(employee));
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception occured: " + e.Message);
                return false;
            }
            _logger.LogTrace("ReadAll Ends.");
            return true;
        }


        /// <summary>
        /// Get Employee By Name
        /// </summary>
        /// <param name="name">Name of Employee</param>
        /// <returns>Employee Details, if Name match</returns>  
        public Employee GetEmployeeByName(string name)
        {
            Employee employee = new Employee();

            _logger.LogTrace("GetEmployeeByName starts. Params name: {0}", name);
            try
            {
                var doc = XDocument.Load(Directory.GetCurrentDirectory() + xmlPath);
                foreach (var node in doc.Descendants("employee"))
                {
                    if (node.Descendants("name").FirstOrDefault().Value.ToLower() == name.ToLower())
                    {
                        employee = new Employee
                        {
                            ID = Int32.Parse(node.Descendants("id")?.FirstOrDefault()?.Value ?? "0"),
                            Name = node.Descendants("name").FirstOrDefault().Value,
                            Age = Int32.Parse(node.Descendants("age").FirstOrDefault().Value),
                            Designation = node.Descendants("designation").FirstOrDefault().Value,
                        };
                    }
                }
                System.Console.WriteLine("Following Employee Details Fetched: \n");
                System.Console.WriteLine(JsonConvert.SerializeObject(employee));
                _logger.LogTrace("GetEmployeeByName Ends.");
                return employee;

            }
            catch (Exception e)
            {
                _logger.LogError($"Exception occured: " + e.Message);
                return employee;
                //throw;
            }

        }

        /// <summary>
        /// Gets all employees count from XML. 
        /// </summary>
        /// <returns>all employees count</returns>
        public int GetAllEmployeesCount()
        {
            List<Employee> employees = new List<Employee>();

            _logger.LogTrace("GetAllEmployeesCount starts.");
            try
            {
                int count = 0;
                var doc = XDocument.Load(Directory.GetCurrentDirectory() + xmlPath);

                count = doc.Descendants("employee").Count();
                System.Console.WriteLine("Total Employee count is {0} \n", count);
                _logger.LogTrace("GetAllEmployeesCount Ends.");
                return count;
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception occured: " + e.Message);
                return 0;
                //throw;
            }
        }

        /// <summary>
        /// Adds new Employee to XML
        /// </summary>
        /// <returns>Add Status</returns>
        public bool AddEmployee()
        {
            Employee employee = new Employee();

            _logger.LogTrace("AddEmployee starts.");
            try
            {

                var doc = XDocument.Load(Directory.GetCurrentDirectory() + xmlPath);
                employee.ID = GetAllEmployeesCount() + 1;
                System.Console.WriteLine("Please Enter Name of Employee and press Enter");
                employee.Name = GetInputsFromConsole.GetDetails<string>();
                System.Console.WriteLine("Please Enter Age of {0} and press Enter", employee.Name);
                employee.Age = Int32.Parse(GetInputsFromConsole.GetDetails<int>());
                System.Console.WriteLine("Please Enter Designation of {0} and press Enter", employee.Name);
                employee.Designation = GetInputsFromConsole.GetDetails<string>();



                XElement newEmployee = new XElement("employee");
                XElement id = new XElement("id"); id.Value = employee.ID.ToString();
                XElement name = new XElement("name"); name.Value = employee.Name;
                XElement age = new XElement("age"); age.Value = employee.Age.ToString();
                XElement designation = new XElement("designation"); designation.Value = employee.Designation;
                newEmployee.Add(id, name, age, designation);
                doc.Root.Add(newEmployee);
                SaveXML(doc);
                System.Console.WriteLine("Successfully Added new Employee");
                System.Console.WriteLine(JsonConvert.SerializeObject(newEmployee));
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception occured: " + e.Message);
                return false;
            }
            _logger.LogTrace("AddEmployee Ends.");
            return true;
        }

        /// <summary>
        /// Updates existing Employee to XML
        /// </summary>
        /// <param name="empName">Name of Employee</param>
        /// <returns>Update Status</returns>
        public bool UpdateEmployee(string empName)
        {
            _logger.LogTrace("UpdateEmployee starts. Params name: {0}", empName);
            if (empName == null)
            {
                System.Console.WriteLine("Name of Employee cannot be null");
                return false;
            }
            Employee employee = new Employee();

            try
            {
                var emp = GetEmployeeByName(empName);

                if (emp.Name == null)
                {
                    System.Console.WriteLine("Name of Employee provided did not matched with existing records. Please retry");
                    return false;
                }
                var doc = XDocument.Load(Directory.GetCurrentDirectory() + xmlPath);
                employee.ID = emp.ID;
                System.Console.WriteLine("Existing value is {0}. If you want to change the Name of Employee. Enter new Name else Press # to skip", emp.Name);
                string updateName = GetInputsFromConsole.GetDetails<string>();
                employee.Name = updateName == "#" ? emp.Name : updateName;

                System.Console.WriteLine("Existing value is {0}. If you want to change the Age of Employee. Enter new Age else Press 0 to skip", emp.Age);
                string updatedAge = GetInputsFromConsole.GetDetails<string>();
                employee.Age = updatedAge == "0" ? emp.Age : int.Parse(updatedAge);

                System.Console.WriteLine("Existing value is {0}. If you want to change the Designation of Employee. Enter new Designation else Press # to skip", emp.Designation);
                string updatedDesig = GetInputsFromConsole.GetDetails<string>();
                employee.Designation = updatedDesig == "#" ? emp.Designation : updatedDesig;

                XElement updatedEmployee = doc.Descendants("employee").Where(_ => _.Descendants("name").FirstOrDefault().Value == emp.Name).FirstOrDefault();
                updatedEmployee.Descendants("name").FirstOrDefault().Value = employee.Name;
                updatedEmployee.Descendants("age").FirstOrDefault().Value = employee.Age.ToString();
                updatedEmployee.Descendants("designation").FirstOrDefault().Value = employee.Designation;

                SaveXML(doc);
                System.Console.WriteLine("Successfully Updated new Employee");
                System.Console.WriteLine(JsonConvert.SerializeObject(employee));
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception occured: " + e.Message);
                return false;
            }
            _logger.LogTrace("AddEmployee Ends.");
            return true;
        }


        /// <summary>
        /// Deletes existing Employee from XML
        /// </summary>
        /// <param name="empName">Name of Employee</param>
        /// <returns>Delete Status</returns>
        public bool DeleteEmployee(string empName)
        {
            _logger.LogTrace("DeleteEmployee starts. Params name: {0}", empName);
            if (empName == null)
            {
                System.Console.WriteLine("Name of Employee cannot be null. Please retry");
                return false;
            }
            Employee employee = new Employee();

            try
            {
                var emp = GetEmployeeByName(empName);
                if (emp.Name == null)
                {
                    System.Console.WriteLine("Name of Employee provided did not matched with existing records. Please retry");
                    return false;
                }
                else
                {
                    System.Console.WriteLine("Data found with Name: {0}. Are you sure to delete the Employee then Press Y", emp.Name);


                    if (GetInputsFromConsole.GetDetails<string>().ToLower() == "y")
                    {
                        var doc = XDocument.Load(Directory.GetCurrentDirectory() + xmlPath);
                        doc.Root.Descendants("employee").Where(n => n.Descendants("name").FirstOrDefault().Value == emp.Name).Remove();
                        SaveXML(doc);
                        System.Console.WriteLine("Successfully deleted an Employeewith Name: {0}", emp.Name);
                    }
                    else
                    {
                        System.Console.WriteLine("Delete operation cancelled");
                        return true;
                    }

                    // return true;
                }

            }
            catch (Exception e)
            {
                _logger.LogError($"Exception occured: " + e.Message);
                return false;
            }
            _logger.LogTrace("DeleteEmployee Ends.");
            return true;
        }

        /// <summary>
        /// Saves XML to drive
        /// </summary>
        /// <param name="doc"></param>
        private void SaveXML(XDocument doc)
        {
            try
            {
                doc.Save(Directory.GetCurrentDirectory() + xmlPath);
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception occured while saving: " + e.Message);
            }
        }


        /// <summary>
        /// Gets all employees from XML. 
        /// </summary>
        /// <returns>all employees</returns>
        public List<Employee> GetAllEmployees()
        {
            List<Employee> employees = new List<Employee>();

            _logger.LogTrace("ReadAll starts.");
            //_logger.LogTrace("ReadAll starts. Parameters lstClaimData={lstClaimData}", JsonConvert.SerializeObject(lstClaimUdfs));
            try
            {
                var doc = XDocument.Load(Directory.GetCurrentDirectory() + xmlPath);
                foreach (var node in doc.Descendants("employee"))
                {
                    employees.Add(new Employee
                    {
                        ID = Int32.Parse(node.Descendants("id")?.FirstOrDefault()?.Value ?? "0"),
                        Name = node.Descendants("name").FirstOrDefault().Value,
                        Age = Int32.Parse(node.Descendants("age").FirstOrDefault().Value),
                        Designation = node.Descendants("designation").FirstOrDefault().Value,
                    });
                }
                System.Console.WriteLine("Following Employee Details Fetched: \n");
                foreach (var employee in employees)
                {
                    System.Console.WriteLine(JsonConvert.SerializeObject(employee));
                }
                _logger.LogTrace("ReadAll Ends.");
                return employees;

            }
            catch (Exception e)
            {
                _logger.LogError($"Exception occured: " + e.Message);
                return employees;
                //throw;
            }
        }
    }
}
