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
        List<NotificationInfo> lst = repository.GetNotifications();
        NotificationInfo notificationInfo = lst.Where(p => p.ToLower().Contains(name.ToLower() && (date == DateTime.MinValue || p.CreatedDate == date ) ).Last();
        if ( notificationInfo == null ) 
        {
            return string.Format("Infelizmente não temos nenhuma novidade do colaborador {0}", name); 
        }
        return string.Format("{0}", notificationInfo.Text);
    }
}