using System;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Xml.Serialization;

#load "EmployeeInfo.csx"

[XmlRoot]
public class EmployeeCollection
{
    [XmlElement]
    public List<EmployeeInfo> Employee { get; set; }
}