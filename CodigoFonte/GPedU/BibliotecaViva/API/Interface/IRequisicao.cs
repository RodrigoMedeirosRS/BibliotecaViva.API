using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace API.Interface
{
    public interface IRequisicao
    {
        Task<S> ExecutarRequisicao<T, S>(T entrada, Func<T, Task<S>> metodo);
        Task<T> ExecutarRequisicao<T>(Func<Task<T>> metodo);
    }
}
