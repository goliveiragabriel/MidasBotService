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
using Microsoft.Bot.Builder.FormFlow;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

[LuisModel("b5f63b2d-fc34-41b0-8a07-f1fa28b55395", "43f83a53d85144409c50edeedc8b9a9b", LuisApiVersion.V2)]
[Serializable]
public class EmployeeDialog : LuisDialog<object> 
{
    public EmployeeDialog () 
    {
      
    }

    [LuisIntent("DaysInCompany")]
    public async Task GetDays(IDialogContext context, LuisResult result) 
    {
        if(result.Entities != null && result.Entities.Count > 0) 
        {
            string results = result.Entities[0].Entity;
            //context.ConversationData.SetValue<string>("");
            await context.PostAsync(await Employee.GetDays(results));
        }
        context.Wait(MessageReceived);
    }
}