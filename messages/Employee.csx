using System;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using MidasBotService;

class Employee {
    
    public static async Task<string> GetDays(string name)
    {
        EmployeeRepository repository = new EmployeeRepository();
        EmployeeCollection collection = repository.GetEmployees();
        EmployeeInfo employee = collection.Employee.Find(p => p.Name == name);
        if ( employee == null ) 
        {
            return string.Format("Poxa! Não consegui encontrar o colaborador {0}", name);
        }
        /*EmployeeInfo employee = new EmployeeInfo();
        employee.AdmissionDate = new DateTime(2005, 12, 13);*/
        TimeSpan diff = DateTime.Now.Subtract(employee.AdmissionDate);
        return string.Format("Fazem {0} dias {1} horas {2} minutos {3} segundos, que o {4} trabalha na Midas", diff.Days, diff.Hours, diff.Minutes, diff.Seconds, name);
    }
}