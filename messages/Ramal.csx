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
        RamalInfo ramalInfo = repository.GetRamals().Find(p => p.Name.ToLower() == name.ToLower());
        if ( employee == null ) 
        {
            return string.Format("Poxa! Não consegui encontrar o ramal do colaborador {0}. Preciso atualizar minhas definições de ramais.", name);
        } 
        return ramalInfo.Number;
    }
}