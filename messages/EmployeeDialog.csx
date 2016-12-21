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

[LuisModel("3ca36565-7dab-4db1-960d-c4fbdb13cb01", "04d1b2968c6a44e79dd9a3ec2cb6e313 ", LuisApiVersion.V2)]
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