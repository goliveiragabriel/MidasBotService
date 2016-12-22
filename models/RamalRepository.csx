#load "RamalInfo.csx"

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Newtonsoft.Json;

public class RamalRepository 
{
    public List<RamalInfo> GetRamals() 
    {
        List<RamalInfo> collection = new List<RamalInfo>();
        using(StreamReader file = File.OpenText(@"D:\home\site\wwwroot\models\ramals.json"))
        {
            JsonSerializer serializer = new JsonSerializer();
            collection = serializer.Deserialize(file, typeof(List<RamalInfo>)) as List<RamalInfo>;
        }
        return collection;
    }
}
