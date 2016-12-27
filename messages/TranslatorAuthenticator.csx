using System;
using System.Diagnostics;
using System.Net;
using System.IO;
using System.Text;

[Serializable]
public class TranslatorAuthenticator 
{
    public readonly string token = string.Empty;

    public TranslatorAuthenticator()
    {
        string subcriptionKey = @"5125ba4dfbde4c99852cc503efc66e2a";
        string strTranslatorAccessURI = @"https://api.cognitive.microsoft.com/sts/v1.0/issueToken?Subscription-Key=" + subcriptionKey;
        WebRequest webRequest = WebRequest.Create(strTranslatorAccessURI);
        webRequest.ContentType = @"application/x-www-form-urlencoded";
        webRequest.Method = "POST";
        byte[] bytes = new byte[1024];
        webRequest.ContentLength = bytes.Length;

        using (Stream outputStream = webRequest.GetRequestStream())
        {
            outputStream.Write(bytes, 0, bytes.Length);
        }
        WebResponse webResponse = webRequest.GetResponse();
        //Get deserialized object from Stream
        using (Stream stream = webResponse.GetResponseStream())
        {
            StreamReader reader = new StreamReader(stream, Encoding.UTF8);
            String responseString = reader.ReadToEnd();
            token = "Bearer " + responseString; //create the string for the http header
        }
    }

    public string Translate(string text, string languageCode = "en")
    {
        string translatedString = text;
        string uri = string.Format(@"http://api.microsofttranslator.com/v2/Http.svc/Translate?text={0}&to={1}", translatedString, languageCode);
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
