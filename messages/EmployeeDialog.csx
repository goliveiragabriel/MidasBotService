#load "Employee.csx"

using System;
using System.Net;
using System.Threading;
using Newtonsoft.Json;
using Autofac;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Internals;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
public class EmployeeDialog : LuisDialog<object> 
{
    public EmployeeDialog () : base(new LuisServiceHost(new LuisService(new LuisModelAttribute("3ca36565-7dab-4db1-960d-c4fbdb13cb01","d9714e73ce3e47258dd8417176140a84"))))
    {
      
    }

    [LuisIntent("DaysInCompany")]
    public async Task GetDays(IDialogContext context, LuisResult result) 
    {
        if(result.Entities != null && result.Entities.Count > 0) 
        {
            string result = result.Entities[0].Entity;
            //context.ConversationData.SetValue<string>("");
            await context.PostAsync(await Employee.GetDays(result));
        }
        context.Wait(MessageReceived);
    }
}