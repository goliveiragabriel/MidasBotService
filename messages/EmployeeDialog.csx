#load "Employee.csx"
#load "Ramal.csx"

using System;
using System.Net;
using System.Net.Http;
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
using System.Web;

[LuisModel("3ca36565-7dab-4db1-960d-c4fbdb13cb01", "43f83a53d85144409c50edeedc8b9a9b")]
[Serializable]
public class EmployeeDialog : LuisDialog<object> 
{
    public EmployeeDialog () //: base(new LuisServiceHost((new LuisModel("3ca36565-7dab-4db1-960d-c4fbdb13cb01","43f83a53d85144409c50edeedc8b9a9b"))))
    {
      
    }

    [LuisIntent("DaysInCompany")]
    public async Task DaysInCompany(IDialogContext context, LuisResult result) 
    {
        if(result.Entities != null && result.Entities.Count > 0) 
        {
            string results = result.Entities[0].Entity;
            //context.ConversationData.SetValue<string>("");
            await context.PostAsync(await Employee.GetDays(results));
        }
        else {
            await context.PostAsync("Infelizmente, ainda não consigo entender o que você disse!");
        }
        context.Wait(MessageReceived);
    }

    [LuisIntent("GetRamal")]
    public async Task GetRamal(IDialogContext context, LuisResult result) 
    {
        if(result.Entities != null && result.Entities.Count > 0) 
        {
            string results = result.Entities[0].Entity;
            await context.PostAsync(await Ramal.GetByName(results));
        }
        else 
        {
            await context.PostAsync("Infelizmente, ainda não consigo entender o que você disse!");
        }        
    }
}