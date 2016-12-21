using System;
using System.Text;
using System.Threading.Tasks;

class Employee {
    
    public static async Task<string> GetDays(string name)
    {
        // 13 de Dezembro de 2005
        DateTime date = new DateTime(2005, 12, 13);
        TimeSpan diff = DateTime.Now.Subtract(date);
        return string.Format("Fazem {0} dias {1} horas {2} minutos {3} segundos, que o {4} trabalha na Midas", diff.Days, diff.Hours, diff.Minutes, diff.Seconds, name);
    }

}