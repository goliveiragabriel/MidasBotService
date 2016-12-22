using System;
using System.Xml;
using Newtonsoft.Json;
using System.Text;
using Newtonsoft.Json;

[XmlType("Employee")]
public class EmployeeInfo
{
    [XmlElement("Name")]    
    public string Name {get;set;}

    [XmlElement("AdmissionDate")]    
    public DateTime AdmissionDate {get;set;}
}
