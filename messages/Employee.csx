using System;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Collections.Generic;
using System.Linq;

class Employee {
    
    public static async Task<string> GetDays(string name)
    {
        // 13 de Dezembro de 2005
        MidasBotService.Repository.EmployeeRepository repository = new MidasBotService.Repository.EmployeeRepository();
        MidasBotService.Models.EmployeeCollection collection = repository.getEmployees();
        MidasBotService.Models.Employee employee = collection.Find(p => p.Name == name);
        if ( employee == null ) 
        {
            return string.Format("Poxa! Não consegui encontrar o colaborador {0}", name);
        }
        TimeSpan diff = DateTime.Now.Subtract(employee.AdmissionDate);
        return string.Format("Fazem {0} dias {1} horas {2} minutos {3} segundos, que o {4} trabalha na Midas", diff.Days, diff.Hours, diff.Minutes, diff.Seconds, name);
    }
}