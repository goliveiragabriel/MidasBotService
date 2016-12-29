#load "../models/RamalRepository.csx" 
#load "../models/RamalInfo.csx" 

using System;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Collections.Generic;
using System.Linq;

class Ramal 
{
    public static async Task<string> GetByName(string name) 
    {
        RamalRepository repository = new RamalRepository();
        RamalInfo ramalInfo = repository.GetRamals().Find(p => System.Text.Encoding.UTF8.GetString(System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(p.Name.ToLower())) == System.Text.Encoding.UTF8.GetString(System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(name.ToLower())));
        if ( ramalInfo == null ) 
        {
            return string.Format("Poxa! Não consegui encontrar o ramal do colaborador {0}. Preciso atualizar minhas definições de ramais.", name);
        } 
        return ramalInfo.Number;
    }
}