using System;
using Newtonsoft.Json;
using System.Text;
using Newtonsoft.Json;

namespace MidasBotService.Models
{
    [Serializable]
    public class Employee 
    {
        public string Name {get;set;}

        public DateTime AdmissionDate {get;set;}
    }
}