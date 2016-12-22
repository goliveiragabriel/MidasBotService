#load "EmployeeCollection.csx"

using System;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using System.IO;

public class EmployeeRepository 
{
    public EmployeeCollection GetEmployees() 
    {
        EmployeeCollection collection = new EmployeeCollection();
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(EmployeeCollection));
        byte[] file = File.ReadAllBytes(@"..\employes.xml");
        using (MemoryStream ms = new MemoryStream(file))
        {
            collection = xmlSerializer.Deserialize(ms) as EmployeeCollection;
        }
        return collection;
    }
}
