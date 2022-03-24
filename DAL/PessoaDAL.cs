using System;
using MoreLinq;
using System.Linq;
using System.Globalization;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using BibliotecaViva.DAO;
using BibliotecaViva.DTO;
using BibliotecaViva.DAL.Utils;
using BibliotecaViva.DAL.Interfaces;

namespace BibliotecaViva.DAL
{
    public class PessoaDAL : BaseDAL, IPessoaDAL
    {
        private IApelidoDAL ApelidoDAL { get; set; }
        private IPessoaRegistroDAL PessoaRegistroDAL { get; set; }
        public PessoaDAL(bibliotecavivaContext dataContext, IApelidoDAL apelidoDAL, IPessoaRegistroDAL pessoaRegistroDAL) : base(dataContext)
        {
            ApelidoDAL = apelidoDAL;
            PessoaRegistroDAL = pessoaRegistroDAL;
        }
        public int Cadastrar(PessoaDTO pessoaDTO)
        {
            var pessoa = DataContext.Pessoas.AsNoTracking().FirstOrDefault(pessoa => pessoa.Codigo == pessoaDTO.Codigo);
            if(pessoa != null)
            {
                pessoa.Nome = pessoaDTO.Nome;
                pessoa.Sobrenome = pessoaDTO.Sobrenome;
                pessoa.Genero = pessoaDTO.Genero;
                DataContext.Update(pessoa);
                DataContext.SaveChanges();
            }
            else
            {
                pessoa = Conversor.Mapear(pessoaDTO);
                DataContext.Add(pessoa);
                DataContext.SaveChanges();
                pessoaDTO.Codigo = pessoa.Codigo;
            }
            CadastrarDadosOpcionais(pessoaDTO);
            return pessoa.Codigo;
        }
        public List<PessoaDTO> Consultar(PessoaDTO pessoaDTO)
        {
            var pessoas = (from pessoa in DataContext.Pessoas
                join
                    pessoaApelido in DataContext.Pessoaapelidos
                    on pessoa.Codigo equals pessoaApelido.Pessoa into pessoaApelidoLeftJoin from pessoaApelidoLeft in pessoaApelidoLeftJoin.DefaultIfEmpty()
                join
                   apelido in DataContext.Apelidos
                   on new Pessoaapelido(){ 
                       Apelido = pessoaApelidoLeft != null ? pessoaApelidoLeft.Apelido : 0
                    }.Apelido equals apelido.Codigo into apelidoLeftJoin from apelidoLeft in apelidoLeftJoin.DefaultIfEmpty()

                where 
                    (!string.IsNullOrEmpty(pessoaDTO.Nome) && pessoa.Nome.ToLower().Contains(pessoaDTO.Nome.ToLower())) 
                    || (!string.IsNullOrEmpty(pessoaDTO.Sobrenome) && pessoa.Sobrenome.ToLower().Contains(pessoaDTO.Sobrenome.ToLower()))
                    || (!string.IsNullOrEmpty(pessoaDTO.Apelido) && (apelidoLeft != null) && apelidoLeft.Nome.ToLower().Contains(pessoaDTO.Apelido.ToLower()))
                
                select new PessoaDTO()
                {
                    Codigo = pessoa.Codigo,
                    Nome = pessoa.Nome,
                    Sobrenome = pessoa.Sobrenome,
                    Genero = pessoa.Genero,
                    Apelido = apelidoLeft != null ? apelidoLeft.Nome : string.Empty
                }).AsNoTracking().DistinctBy(pessoaDB => pessoaDB.Codigo).ToList();
                
            foreach(var pessoa in pessoas)
                pessoa.Relacoes = PessoaRegistroDAL.ObterRelacao((int)pessoa.Codigo);
            
            return pessoas;
        }
        public PessoaDTO Consultar(int codigoPessoa)
        {
            var pessoaDB = (from pessoa in DataContext.Pessoas
                join
                    pessoaApelido in DataContext.Pessoaapelidos
                    on pessoa.Codigo equals pessoaApelido.Pessoa into pessoaApelidoLeftJoin from pessoaApelidoLeft in pessoaApelidoLeftJoin.DefaultIfEmpty()
                join
                   apelido in DataContext.Apelidos
                   on new Pessoaapelido(){ 
                       Apelido = pessoaApelidoLeft != null ? pessoaApelidoLeft.Apelido : 0
                    }.Apelido equals apelido.Codigo into apelidoLeftJoin from apelidoLeft in apelidoLeftJoin.DefaultIfEmpty()
                
                where 
                    pessoa.Codigo == codigoPessoa
                
                select new PessoaDTO()
                {
                    Codigo = pessoa.Codigo,
                    Nome = pessoa.Nome,
                    Sobrenome = pessoa.Sobrenome,
                    Genero = pessoa.Genero,
                    Apelido = apelidoLeft != null ? apelidoLeft.Nome : string.Empty,
                }).AsNoTracking().FirstOrDefault();
                
            pessoaDB.Relacoes = PessoaRegistroDAL.ObterRelacao((int)pessoaDB.Codigo);
            
            return pessoaDB;
        }
        private void CadastrarDadosOpcionais(PessoaDTO pessoaDTO)
        {
            CadastrarApelido(pessoaDTO);
            PessoaRegistroDAL.VincularReferencia(pessoaDTO);
        }
        private void CadastrarApelido(PessoaDTO pessoaDTO)
        {
            if (string.IsNullOrEmpty(pessoaDTO.Apelido))
                ApelidoDAL.RemoverVinculo(pessoaDTO.Codigo);
            else
            {
                var apelidoDTO = new ApelidoDTO()
                { 
                    Nome = pessoaDTO.Apelido 
                };
                
                ApelidoDAL.Cadastrar(apelidoDTO);
                apelidoDTO.Codigo = DataContext.Apelidos.FirstOrDefault(apelido => apelido.Nome.ToLower() == pessoaDTO.Apelido.ToLower()).Codigo;
                ApelidoDAL.VincularPessoa(apelidoDTO, pessoaDTO);
            }     
        }
    }
}