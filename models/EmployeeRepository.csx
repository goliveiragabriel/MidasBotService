#load "EmployeeCollection.csx"

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Newtonsoft.Json;

public class EmployeeRepository 
{
    public EmployeeCollection GetEmployees() 
    {
        EmployeeCollection collection = new EmployeeCollection();
        collection = JsonConvert.DeserializeObject<EmployeeCollection>(File.ReadAllText(@"..\employees.json"));
        return collection;
    }
}
