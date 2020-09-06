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

    public class XMLServiceExtended : IXMLService
    {
        private static string xmlPath = "";
        private readonly ILogger<TestService> _logger;
        private readonly AppSettings _config;

        public XMLServiceExtended(ILogger<TestService> logger,
            IOptions<AppSettings> config)
        {
            _logger = logger;
            _config = config.Value;
            xmlPath = config.Value.XMLFilePath;
        }

        /// <summary>
        /// Prints all employees to screen
        /// </summary>
        /// <returns>Read Status</returns>
        public bool ReadAllEmployees()
        {
            List<EmployeeExtended> employees = new List<EmployeeExtended>();

            _logger.LogTrace("ReadAll starts.");
            try
            {
                var doc = XDocument.Load(Directory.GetCurrentDirectory() + xmlPath);
                foreach (var node in doc.Descendants("employee"))
                {
                    employees.Add(new EmployeeExtended
                    {
                        ID = Int32.Parse(node.Descendants("id")?.FirstOrDefault()?.Value ?? "0"),
                        Name = node.Descendants("name").FirstOrDefault().Value,
                        Age = Int32.Parse(node.Descendants("age").FirstOrDefault().Value),
                        Designation = node.Descendants("designation").FirstOrDefault().Value,
                        Address = node.Descendants("address")?.FirstOrDefault() == null ? new Address()
                        : new Address()
                        {
                            DoorNo = node.Descendants("doorNo").FirstOrDefault().Value,
                            State = node.Descendants("street").FirstOrDefault().Value,
                            street = node.Descendants("town").FirstOrDefault().Value,
                            Town = node.Descendants("state").FirstOrDefault().Value,
                        }
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
        public EmployeeExtended GetEmployeeByName(string name)
        {
            var employee = new EmployeeExtended();

            _logger.LogTrace("GetEmployeeByName starts. Params name: {0}", name);
            try
            {
                var doc = XDocument.Load(Directory.GetCurrentDirectory() + xmlPath);
                foreach (var node in doc.Descendants("employee"))
                {
                    if (node.Descendants("name").FirstOrDefault().Value.ToLower() == name.ToLower())
                    {
                        employee = new EmployeeExtended
                        {
                            ID = Int32.Parse(node.Descendants("id")?.FirstOrDefault()?.Value ?? "0"),
                            Name = node.Descendants("name").FirstOrDefault().Value,
                            Age = Int32.Parse(node.Descendants("age").FirstOrDefault().Value),
                            Designation = node.Descendants("designation").FirstOrDefault().Value,
                            Address = node.Descendants("address")?.FirstOrDefault() == null ? new Address()
                            : new Address()
                            {
                                DoorNo = node.Descendants("doorNo").FirstOrDefault().Value,
                                State = node.Descendants("street").FirstOrDefault().Value,
                                street = node.Descendants("town").FirstOrDefault().Value,
                                Town = node.Descendants("state").FirstOrDefault().Value,
                            }
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
            var employees = new List<EmployeeExtended>();

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
            EmployeeExtended employee = new EmployeeExtended();

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
                System.Console.WriteLine("If you don't want to Enter Address of {0} then press Enter else Please enter DoorNo", employee.Name);
                string doorNoValue = GetInputsFromConsole.GetDetails<string>();
                if (doorNoValue == "")
                {
                    employee.Address = new Address();
                }
                else
                {
                    System.Console.WriteLine("Please enter street");
                    string streetValue = GetInputsFromConsole.GetDetails<string>();
                    System.Console.WriteLine("Please enter town");
                    string townValue = GetInputsFromConsole.GetDetails<string>();
                    System.Console.WriteLine("Please enter state");
                    string stateValue = GetInputsFromConsole.GetDetails<string>();
                    employee.Address = new Address()
                    {
                        DoorNo = doorNoValue,
                        State = streetValue,
                        street = townValue,
                        Town = stateValue,
                    };
                }
                     



                XElement newEmployee = new XElement("employee");
                XElement newAddress = new XElement("address");
                XElement id = new XElement("id"); id.Value = employee.ID.ToString();
                XElement name = new XElement("name"); name.Value = employee.Name;
                XElement age = new XElement("age"); age.Value = employee.Age.ToString();
                XElement designation = new XElement("designation"); designation.Value = employee.Designation;
                XElement doorNo = new XElement("doorNo"); doorNo.Value = employee.Address?.DoorNo ?? "";
                XElement street = new XElement("street"); street.Value = employee.Address?.street ?? "";
                XElement town = new XElement("town"); town.Value = employee.Address?.Town ?? "";
                XElement state = new XElement("state"); state.Value = employee.Address?.State ?? "";
                newAddress.Add(doorNo, street, town, state);
                newEmployee.Add(id, name, age, designation, newAddress);
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
            EmployeeExtended employee = new EmployeeExtended();

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

                System.Console.WriteLine("If you want to change the Address of Employee. Enter any letter else Press # and Enter to skip address");
                string updatedAddress = GetInputsFromConsole.GetDetails<string>();

                

                XElement updatedEmployee = doc.Descendants("employee").Where(_ => _.Descendants("name").FirstOrDefault().Value == emp.Name).FirstOrDefault();
                updatedEmployee.Descendants("name").FirstOrDefault().Value = employee.Name;
                updatedEmployee.Descendants("age").FirstOrDefault().Value = employee.Age.ToString();
                updatedEmployee.Descendants("designation").FirstOrDefault().Value = employee.Designation;


                if (updatedAddress == "#")
                {
                    employee.Address = emp.Address;
                }
                else
                {
                    employee.Address = emp.Address;
                    System.Console.WriteLine("If you want to change the Address of Employee. Enter new Door Number else Press # to skip");
                    string doorNoValue = GetInputsFromConsole.GetDetails<string>();
                    employee.Address.DoorNo = doorNoValue == "#" ? emp.Address.DoorNo : doorNoValue;

                    System.Console.WriteLine("If you want to change the street of Employee. Enter new street else Press # to skip");
                    string streetValue = GetInputsFromConsole.GetDetails<string>();
                    employee.Address.street = streetValue == "#" ? emp.Address.street : streetValue;

                    System.Console.WriteLine("If you want to change the Town of Employee. Enter new Town else Press # to skip");
                    string townValue = GetInputsFromConsole.GetDetails<string>();
                    employee.Address.Town = townValue == "#" ? emp.Address.Town : townValue;

                    System.Console.WriteLine("If you want to change the state of Employee. Enter new state else Press # to skip");
                    string stateValue = GetInputsFromConsole.GetDetails<string>();
                    employee.Address.State = stateValue == "#" ? emp.Address.Town : stateValue;


                    updatedEmployee.Descendants("doorNo").FirstOrDefault().Value = employee.Address.DoorNo;
                    updatedEmployee.Descendants("street").FirstOrDefault().Value = employee.Address.street;
                    updatedEmployee.Descendants("town").FirstOrDefault().Value = employee.Address.Town;
                    updatedEmployee.Descendants("state").FirstOrDefault().Value = employee.Address.State;
                }


                SaveXML(doc);
                System.Console.WriteLine("Successfully Updated new Employee");
                System.Console.WriteLine(JsonConvert.SerializeObject(employee));
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception occured: " + e.Message);
                return false;
            }
            _logger.LogTrace("UpdateEmployee Ends.");
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
        public List<EmployeeExtended> GetAllEmployees()
        {
            var employees = new List<EmployeeExtended>();

            _logger.LogTrace("ReadAll starts.");
            //_logger.LogTrace("ReadAll starts. Parameters lstClaimData={lstClaimData}", JsonConvert.SerializeObject(lstClaimUdfs));
            try
            {
                var doc = XDocument.Load(Directory.GetCurrentDirectory() + xmlPath);
                foreach (var node in doc.Descendants("employee"))
                {
                    employees.Add(new EmployeeExtended
                    {
                        ID = Int32.Parse(node.Descendants("id")?.FirstOrDefault()?.Value ?? "0"),
                        Name = node.Descendants("name").FirstOrDefault().Value,
                        Age = Int32.Parse(node.Descendants("age").FirstOrDefault().Value),
                        Designation = node.Descendants("designation").FirstOrDefault().Value,
                        Address = node.Descendants("address")?.FirstOrDefault() == null ? new Address()
                            : new Address()
                            {
                                DoorNo = node.Descendants("doorNo").FirstOrDefault().Value,
                                State = node.Descendants("street").FirstOrDefault().Value,
                                street = node.Descendants("town").FirstOrDefault().Value,
                                Town = node.Descendants("state").FirstOrDefault().Value,
                            }
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
