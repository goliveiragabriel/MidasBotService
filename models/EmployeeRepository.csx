#load "EmployeeCollection.csx"

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

public class EmployeeRepository 
{
    public EmployeeCollection GetEmployees() 
    {
        EmployeeCollection collection = new EmployeeCollection();
        collection = JsonConvert.DeserializeObject<EmployeeCollection>(@"..\employees.json");
        return collection;
    }
}
