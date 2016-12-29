#load "NotificationInfo.csx"

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net;
using Newtonsoft.Json;

public class NotificationRepository 
{
    private readonly string url = @"http://midasbotapi20161226074422.azurewebsites.net/api/Notifications";

    public async Task<List<NotificationInfo>> GetNotificationsAsync() 
    {
        List<NotificationInfo> lst = new List<NotificationInfo>();
        string sJson = string.Empty;
        using (WebClient client = new WebClient()) 
        {
            client.Encoding = System.Text.Encoding.UTF8;
            sJson = await client.DownloadStringTaskAsync(url).ConfigureAwait(false);
        }
        try
        {
            lst = JsonConvert.DeserializeObject<List<NotificationInfo>>(sJson);
        }
        catch 
        {

        }
        return lst;
    }
}
