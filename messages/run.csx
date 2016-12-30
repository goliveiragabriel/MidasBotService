#r "Newtonsoft.Json"
#load "EchoDialog.csx"
#load "EmployeeDialog.csx"

using System;
using System.Net;
using System.Threading;
using Newtonsoft.Json;
using Microsoft.Bot.Builder.Azure;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System.Web;

public static async Task<object> Run(HttpRequestMessage req, TraceWriter log)
{
    log.Info($"Webhook was triggered!");

    // Initialize the azure bot
    using (BotService.Initialize())
    {
        // Deserialize the incoming activity
        string jsonContent = await req.Content.ReadAsStringAsync();
        var activity = JsonConvert.DeserializeObject<Activity>(jsonContent);
        
        // authenticate incoming request and add activity.ServiceUrl to MicrosoftAppCredentials.TrustedHostNames
        // if request is authenticated
        if (!await BotService.Authenticator.TryAuthenticateAsync(req, new [] {activity}, CancellationToken.None))
        {
            return BotAuthenticator.GenerateUnauthorizedResponse(req);
        }
        
        if (activity != null)
        {
            // one of these will have an interface and process it
            switch (activity.GetActivityType())
            {
                case ActivityTypes.Message:
                    if(activity.Text.TrimStart(' ').ToLower() == "login")
                    {
                        var connectorClient = new ConnectorClient(new Uri(activity.ServiceUrl));
                        Activity replyToConversation = activity.CreateReply();
                        replyToConversation.Recipient = activity.From;
                        replyToConversation.Type = "message";
                        replyToConversation.Attachments = new List<Attachment>();
                        List<CardAction> cardButtons = new List<CardAction>();
                        CardAction plButton = new CardAction()
                        {
                            Value = $"http://midasbotapi20161226074422.azurewebsites.net/Home/Login?userid=" + System.Uri.EscapeDataString(activity.From.Id),
                            Type = "signin",
                            Title = "Acesse aqui"
                        };
                        cardButtons.Add(plButton);
                        SigninCard plCard = new SigninCard("Por favor, acesse o Office 365", new List<CardAction>() { plButton });
                        Attachment plAttachment = plCard.ToAttachment();
                        replyToConversation.Attachments.Add(plAttachment);
                        var reply = await connectorClient.Conversations.SendToConversationAsync(replyToConversation);
                    }
                    else
                    {
                        await Conversation.SendAsync(activity, () => new EmployeeDialog());
                    }
                    break;
                case ActivityTypes.ConversationUpdate:
                    var client = new ConnectorClient(new Uri(activity.ServiceUrl));
                    IConversationUpdateActivity update = activity;
                    if (update.MembersAdded.Any())
                    {
                        var reply = activity.CreateReply();
                        var newMembers = update.MembersAdded?.Where(t => t.Id != activity.Recipient.Id);
                        foreach (var newMember in newMembers)
                        {
                            reply.Text = "Olá";
                            if (!string.IsNullOrEmpty(newMember.Name))
                            {
                                reply.Text += $" {newMember.Name}";
                            }
                            reply.Text += "! Eu sou o robot Midas, estou aqui para ajudá-lo sobre as novidades da empresa, ramais, agendamentos, etc. Espero ser útil e que você possa me ajudar a ficar mais inteligente no futuro.";
                            await client.Conversations.ReplyToActivityAsync(reply);
                        }
                    }
                    break;
                case ActivityTypes.ContactRelationUpdate:
                case ActivityTypes.Typing:
                case ActivityTypes.DeleteUserData:
                case ActivityTypes.Ping:
                default:
                    log.Error($"Unknown activity type ignored: {activity.GetActivityType()}");
                    break;
            }
        }
        return req.CreateResponse(HttpStatusCode.Accepted);
    }    
}
