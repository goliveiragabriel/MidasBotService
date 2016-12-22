using System;
using System.Xml;
using System.Text;
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace MidasBotService {
    [XmlRoot]
    public class EmployeeInfo
    {
        [XmlElement]
        public string Name { get; set; }

        [XmlElement]
        public DateTime AdmissionDate { get; set; }
    }
}
