using System;
using System.Threading.Tasks;

using API.Interface;

namespace API
{
    public class Requisicao : IRequisicao
    {
        public Task<S> ExecutarRequisicao<T, S>(T entrada, Func<T, Task<S>> metodo)
        {
            try
            {
                return Task<S>.Run(async () =>
                {
                    return await metodo(entrada);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public Task<T> ExecutarRequisicao<T>(Func<Task<T>> metodo)
        {
            try
            {
                return Task<T>.Run(async () =>
                {
                    return await metodo();
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}