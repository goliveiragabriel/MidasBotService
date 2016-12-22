#load "EmployeeInfo.csx"

using System;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

[Serializable]
public class EmployeeCollection 
{
    [XmlArray("EmployeeCollection"), XmlArrayItem(typeof(Employee), ElementName = "Employee")]
    List<EmployeeInfo> Employees { get; set; }
}
