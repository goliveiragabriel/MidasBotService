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

//[LuisModel("3ca36565-7dab-4db1-960d-c4fbdb13cb01", "43f83a53d85144409c50edeedc8b9a9b", LuisApiVersion.V2)]
[Serializable]
public class EmployeeDialog : LuisDialog<object> 
{
    public EmployeeDialog () : base(new LuisServiceHost(new LuisService(new LuisModelAttribute("3ca36565-7dab-4db1-960d-c4fbdb13cb01","43f83a53d85144409c50edeedc8b9a9b"))))
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
        context.Wait(MessageReceived);
    }
}

[Serializable]
public sealed class LuisServiceHost : ILuisService 
{
    private readonly ILuisService service;
    public LuisServiceHost (ILuisService service)
    {
      this.service = service;
    }

    async Task<LuisResult> ILuisService.QueryAsync(Uri uri, CancellationToken token) 
    {
        var builder = new UriBuilder(uri);
        builder.Host = "api.projectoxfor.ai";
        return await this.service.QueryAsync(builder.Uri, token);
    } 

    Uri ILuisService.BuildUri(LuisResult text) 
    {
        return this.service.BuildUri(text);
    } 	
}