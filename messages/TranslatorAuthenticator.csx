using System;
using System.Diagnostics;
using System.Net;
using System.Web;
using System.IO;
using System.Media;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable]
public class TranslatorAuthenticator 
{
    public readonly string token = string.Empty;

    public TranslatorAuthenticator() 
    {
        string clientID = "midasbottranslator_2016";
        string clientSecret = "Wti/hf/WYYHlOeqlCW377tQyF97KE/qFBURCRQQWqSg=";
        string strTranslatorAccessURI = "https://datamarket.accesscontrol.windows.net/v2/OAuth2-13";
        string strRequestDetails = string.Format("grant_type=client_credentials&client_id={0}&client_secret={1}&scope=http://api.microsofttranslator.com", System.Uri.EscapeDataString(clientID), System.Uri.EscapeDataString(clientSecret));
        WebRequest webRequest = WebRequest.Create(strTranslatorAccessURI);
        webRequest.ContentType = "application/x-www-form-urlencoded";
        webRequest.Method = "POST";
        byte[] bytes = Encoding.ASCII.GetBytes(strRequestDetails);
        webRequest.ContentLength = bytes.Length;

        using (Stream outputStream = webRequest.GetRequestStream())
        {
            outputStream.Write(bytes, 0, bytes.Length);
        }
        WebResponse webResponse = webRequest.GetResponse();
        System.Runtime.Serialization.Json.DataContractJsonSerializer serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(AdmAccessToken));
        //Get deserialized object from Stream
        AdmAccessToken admAccessToken = (AdmAccessToken)serializer.ReadObject(webResponse.GetResponseStream());
        token = "Bearer " + admAccessToken.access_token; //create the string for the http header
    }

    public string Translate(string text, string languageCode = "en") 
    {
        string translatedString = text;           
        string uri = string.Format("http://api.microsofttranslator.com/v2/Http.svc/Translate?text={0}&language={1}", translatedString, languageCode);
        string result = string.Empty;
        WebRequest webRequest = WebRequest.Create(uri);
        webRequest.Headers.Add("Authorization", token);
        WebResponse response = null;
        try
        {
            response = webRequest.GetResponse();

            using (Stream stream = response.GetResponseStream())
            {
                System.Runtime.Serialization.DataContractSerializer dcs = new System.Runtime.Serialization.DataContractSerializer(Type.GetType("System.String"));
                result = (string)dcs.ReadObject(stream);
            }
        }
        catch
        {

            throw;
        }
        finally
        {
            if (response != null)
            {
                response.Close();
                response = null;
            }
        }
        return result;
    }
}

public class AdmAccessToken
{
    public string access_token { get; set; }
    public string token_type { get; set; }
    public string expires_in { get; set; }
    public string scope { get; set; }
}