#load "Employee.csx"
#load "Ramal.csx"
#load "Notification.csx"
#load "TranslatorAuthenticator.csx"

using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using Newtonsoft.Json;
using Autofac;
using Microsoft.Bot.Builder.Azure;
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

    public const string Entity_Date = "Time";   
    public const string Entity_Name = "Employee";   
    private TranslatorAuthenticator translator;

    public EmployeeDialog ()
    {
        this.translator = new TranslatorAuthenticator();
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
            context.ConversationData.SetValue<string>("LastRamal", results);
            await context.PostAsync(await Ramal.GetByName(results));
        }
        else 
        {
            await context.PostAsync("Infelizmente, ainda não consigo entender o que você disse. :(");
        }        
        context.Wait(MessageReceived);
    }

    [LuisIntent("RepeatLastRamal")]
    public async Task RepeatLastRamal(IDialogContext context, LuisResult result) 
    {
        string strRet = string.Empty;
        string strName = string.Empty;
        if(result.Entities != null && result.Entities.Count > 0) 
        {
            string results = result.Entities[0].Entity;
            context.ConversationData.SetValue<string>("LastRamal", results);
            await context.PostAsync(await Ramal.GetByName(results));
        }
        else 
        {
            await context.PostAsync("Infelizmente, ainda não consigo entender o que você disse. :(");
        }        
        context.Wait(MessageReceived);
    }

    [LuisIntent("WhatCanYouDo")]
    public async Task WhatCanYouDo(IDialogContext context, LuisResult result) 
    {
        if(result.Entities != null && result.Entities.Count > 0) 
        {
            await context.PostAsync("Olá, por enquanto eu posso: listar os ramais, e outras coisinhas mais. Sobre o que posso te ajudar?");
        }
        else 
        {
            await context.PostAsync("Infelizmente, ainda não consigo entender o que você disse. :(");
        }        
        context.Wait(MessageReceived);
    }

    [LuisIntent("GetNotification")]
    public async Task GetNotification(IDialogContext context, LuisResult result) 
    {
        if(result.Entities != null && result.Entities.Count > 0) 
        {
            EntityRecommendation date;   
            EntityRecommendation nameEntity;         
            result.TryFindEntity(Entity_Name, out nameEntity);
            if (!result.TryFindEntity(Entity_Date, out date))
            {
                date = new EntityRecommendation(type: Entity_Date) { Entity = DateTime.Now.Date.ToString("dd/MM/yyyy") };
            }
            var parser = new Chronic.Parser();
            var span = parser.Parse(this.translator.Translate(date.Entity));
            var when = span.Start ?? span.End;
            await context.PostAsync(await Notification.GetByEmployeeAndDate(nameEntity.Entity, when.Value));
        }
        else 
        {
            await context.PostAsync("Infelizmente, ainda não consigo entender o que você disse. :(");
        }        
        context.Wait(MessageReceived);
    }

    [LuisIntent("AllNotifications")]
    public async Task AllNotifications(IDialogContext context, LuisResult result) 
    {
        if(result.Entities != null && result.Entities.Count > 0) 
        {
            EntityRecommendation date;   
            if (!result.TryFindEntity(Entity_Date, out date))
            {
                date = new EntityRecommendation(type: Entity_Date) { Entity = DateTime.Now.Date.ToString("dd/MM/yyyy") };
            }
            var parser = new Chronic.Parser();
            var span = parser.Parse(this.translator.Translate(date.Entity));
            var when = span.Start ?? span.End;
            await context.PostAsync(await Notification.GetByDate(when.Value));
        }
        else 
        {
            await context.PostAsync("Infelizmente, ainda não consigo entender o que você disse. :(");
        }        
        context.Wait(MessageReceived);
    }

    [LuisIntent("Greetings")]
    public async Task Greetings(IDialogContext context, LuisResult result) 
    {
        await context.PostAsync("Olá, como eu posso te ajudar?");
        context.Wait(MessageReceived);
    } 

    [LuisIntent("FarewellGreetings")]
    public async Task FarewellGreetings(IDialogContext context, LuisResult result) 
    {
        string word = string.Empty;
        if(DateTime.Now.Hour >= 6 && DateTime.Now.Hour < 12 ) 
        {
            word = "bom dia";
        }
        else if ( DateTime.Now.Hour < 18 ) 
        {
            word = "boa tarde";
        }
        else 
        {
            word = "boa noite";
        }
        await context.PostAsync("Até logo, " + word);
        context.Wait(MessageReceived);
    } 

    [LuisIntent("None")]
    public async Task None(IDialogContext context, LuisResult result) 
    {
        await context.PostAsync("Infelizmente, ainda não consigo entender o que você disse. :(");
        context.Wait(MessageReceived);
    } 
    
}