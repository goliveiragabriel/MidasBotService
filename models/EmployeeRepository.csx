#load "EmployeeCollection.csx"

using System;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

public class EmployeeRepository 
{
    public EmployeeCollection GetEmployees() 
    {
        EmployeeCollection collection = new EmployeeCollection();
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(EmployeeCollection));
        using(FileStream fs = new FileStream(@"..\employes.xml"))
        {
            xmlSerializer.Serialize(fs, collection);
        }
        return collection;
    }
}
