#load "EmployeeInfo.csx"

using System;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

public class EmployeeCollection 
{
    public List<EmployeeInfo> Employees { get; set; }
}
