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
        using(StreamReader file = File.OpenText(@"employees.json"))
        {
            JsonSerializer serializer = new JsonSerializer();
            collection = serializer.Deserialize(file, typeof(EmployeeCollection)) as EmployeeCollection;
        }
        return collection;
    }
}
