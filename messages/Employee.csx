#load "../models/EmployeeRepository.csx" 
#load "../models/EmployeeCollection.csx"
#load "../models/EmployeeInfo.csx"

using System;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Collections.Generic;
using System.Linq;


class Employee {
    
    public static async Task<string> GetDays(string name)
    {
        EmployeeRepository repository = new EmployeeRepository();
        EmployeeCollection collection = repository.GetEmployees();
        EmployeeInfo employee = collection.Employee.Find(p => p.Name.ToLower() == name.ToLower());
        if ( employee == null ) 
        {
            return string.Format("Poxa! Não consegui encontrar o colaborador {0}.  Pergunte-me sobre outros colaboradores", name);
        }
        TimeSpan diff = DateTime.Now.Subtract(employee.AdmissionDate);
        return string.Format("Fazem {0} dias {1} horas {2} minutos {3} segundos, que o {4} trabalha na Midas", diff.Days, diff.Hours, diff.Minutes, diff.Seconds, name);
    }
}