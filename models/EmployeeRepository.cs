using System;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using MidasBotService.Models;

namespace MidasBotService.Repository 
{
    public class EmployeeRepository 
    {
        public EmployeeCollection GetEmployees() 
        {
            EmployeeCollection collection = new EmployeCollection();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(EmployeeCollection));
            using(FileStream fs = new FileStream("..\employes.xml"))
            {
                xmlSerializer.Serialize(fs, collection)
            }
            return collection;
        }
    }
}