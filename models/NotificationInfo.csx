using System;
using System.Xml;
using System.Text;
using Newtonsoft.Json;
using System.Xml.Serialization;

public class NotificationInfo
{
    public string Author { get; set; }

    public DateTime CreatedDate { get; set; }

    public string Text { get; set; }
}
