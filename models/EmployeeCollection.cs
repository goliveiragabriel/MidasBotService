using System;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace MidasBotService.Models
{
    [Serializable]
    public class EmployeeCollection 
    {
        [XmlArray("EmployeeCollection"), XmlArrayItem(typeof(Employee), ElementName = "Employee")]
        List<Employee> Employees { get; set; }
    }
}