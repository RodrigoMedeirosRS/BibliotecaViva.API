using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;

using BibliotecaViva.Models.DAL;
using BibliotecaViva.Models.DTO;
using BibliotecaViva.Models.BLL.Utils;
using BibliotecaViva.Models.DTO.Dominio;

namespace BibliotecaViva.Models.BLL
{
    public class PessoaBLL
    {
        private biblioteca_vivaContext DataContext;

        public PessoaBLL(biblioteca_vivaContext dataContext)
        {
            DataContext = dataContext;
        }

        public string Cadastrar(PessoaDTO pessoa)
        {
            return new PessoaDAL(DataContext).Cadastrar(Mapear(pessoa));
        }

        public string Editar(PessoaDTO pessoa)
        {
            return new PessoaDAL(DataContext).Editar(Mapear(pessoa));
        }

        public string Consultar(PessoaDTO pessoa)
        {
            var retorno = new PessoaDAL(DataContext).Consultar(Mapear(pessoa));
            return JsonConvert.SerializeObject(Mapear(retorno));
        }

        private Pessoa Mapear(PessoaDTO pessoaDTO)
        {
            return new AutoMapperGenerico().Mapear<PessoaDTO,Pessoa>(pessoaDTO);
        }
        private PessoaDTO Mapear(Pessoa pessoa)
        {
            return new AutoMapperGenerico().Mapear<Pessoa,PessoaDTO>(pessoa);
        }
    }
}