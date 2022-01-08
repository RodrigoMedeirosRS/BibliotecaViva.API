using System;
using Newtonsoft.Json;
using BibliotecaViva.BLL.Interfaces;

namespace BibliotecaViva.BLL
{
    public abstract class BaseBLL : IBaseBLL
    {
        
        public virtual string SerializarRetorno(object dado)
        {
            return dado != null ? JsonConvert.SerializeObject(dado) : throw new Exception("Pessoa Não Encontrada");
        }
    }
}