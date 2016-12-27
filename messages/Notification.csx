#load "../models/NotificationRepository.csx" 
#load "../models/NotificationInfo.csx"

using System;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Collections.Generic;
using System.Linq;

class Notification 
{
    public static async Task<string> GetByEmployeeAndDate(string name, DateTime date) 
    {
        NotificationRepository repository = new NotificationRepository();
        List<NotificationInfo> lst = await repository.GetNotificationsAsync();
        var results = lst.Where(p => p.Text.Split(' ').ToList().Any( x => System.Text.Encoding.UTF8.GetString(System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(x.ToLower())) == System.Text.Encoding.UTF8.GetString(System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(name.ToLower()))) && (date == DateTime.MinValue || p.CreatedDate.Date == date.Date ) );
        if (results == null || !results.Any() ) 
        {
            return string.Format("Infelizmente não temos nenhuma novidade do colaborador {0} para o dia {1}", name, date.ToString("dd/MM/yyyy")); 
        }
        NotificationInfo notificationInfo = results.Last();
        return string.Format("{0} - {1}", notificationInfo.Text, date.ToString("dd/MM/yyyy"));
    }

    public static async Task<string> GetByDate(DateTime date) 
    {
        NotificationRepository repository = new NotificationRepository();
        List<NotificationInfo> lst = await repository.GetNotificationsAsync();
        var results = lst.Where(p => date == DateTime.MinValue || p.CreatedDate.Date == date.Date );
        if (results == null || !results.Any() ) 
        {
            return string.Format("Infelizmente não temos nenhuma novidade para o dia {0}", date.ToString("dd/MM/yyyy")); 
        }
        string[] notifications = results.Select( p => string.Format("{0} - {1}", p.Text, p.CreatedDate.ToString("dd/MM/yyyy"))).ToArray();
        return string.Join(System.Environment.NewLine, notifications);
    }
}