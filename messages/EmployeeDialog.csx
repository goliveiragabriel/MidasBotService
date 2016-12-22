#load "Employee.csx"

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
    private readonly ILuisModel model;
    public LuisService(ILuisModel model)
    {
        this.model = model;
    }

    public static readonly Uri UriBase = new Uri("https://api.projectoxford.ai/luis/v2/application");
 
    Uri ILuisService.BuildUri(string text)
    {
        var id = HttpUtility.UrlEncode(this.model.ModelID);
        var sk = HttpUtility.UrlEncode(this.model.SubscriptionKey);
        var q = HttpUtility.UrlEncode(text);

        var builder = new UriBuilder(UriBase);
        builder.Query = $"id={id}&subscription-key={sk}&q={q}";
        return builder.Uri;
    }

    async Task<LuisResult> ILuisService.QueryAsync(Uri uri, CancellationToken token)
    {
        string json;
        using (var client = new HttpClient())
        using (var response = await client.GetAsync(uri, HttpCompletionOption.ResponseContentRead, token))
        {
            json = await response.Content.ReadAsStringAsync();
        }

        try
        {
            var result = JsonConvert.DeserializeObject<LuisResult>(json);
            return result;
        }
        catch (JsonException ex)
        {
            throw new ArgumentException("Unable to deserialize the LUIS response.", ex);
        }
    }
}