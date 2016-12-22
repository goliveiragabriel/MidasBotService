#load "EmployeeInfo.csx"

using System;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

[XmlType("EmployeeCollection")]
public class EmployeeCollection 
{
    [XmlArray("EmployeeCollection")]
    [XmlArrayItem("Employee")]
    public List<EmployeeInfo> Employees { get; set; }
}
