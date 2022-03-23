using MoreLinq;
using System;
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
    public class RegistroDAL : BaseDAL, IRegistroDAL
    {
        private ITipoDAL TipoDAL { get ; set; }
        private IIdiomaDAL IdiomaDAL { get ; set; }
        private IApelidoDAL ApelidoDAL { get ; set; }
        private IReferenciaDAL ReferenciaDAL { get; set; }
        private IDescricaoDAL DescricaoDAL { get ; set; }
        private ILocalizacaoGeograficaDAL LocalizacaoGeograficaDAL { get; set; }
        public RegistroDAL(bibliotecavivaContext dataContext, IDescricaoDAL descricaoDAL, IIdiomaDAL idiomaDAL, IApelidoDAL apelidoDAL,ITipoDAL tipoDAL, ILocalizacaoGeograficaDAL localizacaoGeograficaDAL, IReferenciaDAL referenciaDAL) : base(dataContext)
        {
            TipoDAL = tipoDAL;
            IdiomaDAL = idiomaDAL;
            ApelidoDAL = apelidoDAL;
            DescricaoDAL = descricaoDAL;
            ReferenciaDAL = referenciaDAL;
            LocalizacaoGeograficaDAL = localizacaoGeograficaDAL;
        }
        public RegistroDTO Consultar(int codRegistro)
        {
            var resultado = (from registro in DataContext.Registros.AsNoTracking()
                join
                    idioma in DataContext.Idiomas.AsNoTracking()
                    on registro.Idioma equals idioma.Codigo
                join
                    tipo in DataContext.Tipos.AsNoTracking()
                    on registro.Tipo equals tipo.Codigo
                join
                    descricao in DataContext.Descricaos.AsNoTracking()
                    on registro.Codigo equals descricao.Registro into descricaoLeftJoin from descricaoLeft in descricaoLeftJoin.DefaultIfEmpty()
                join
                    registroApelido in DataContext.Registroapelidos.AsNoTracking()
                    on registro.Codigo equals registroApelido.Registro into registroApelidoLeftJoin from registroApelidoLeft in registroApelidoLeftJoin.DefaultIfEmpty()
                join
                   apelido in DataContext.Apelidos.AsNoTracking()
                   on new Registroapelido(){ 
                       Apelido = registroApelidoLeft != null ? registroApelidoLeft.Apelido : 0
                    }.Apelido equals apelido.Codigo into apelidoLeftJoin from apelidoLeft in apelidoLeftJoin.DefaultIfEmpty()
                join
                    registroLocalizacao in DataContext.Registrolocalizacaos.AsNoTracking()
                    on registro.Codigo equals registroLocalizacao.Registro into registroLocalizacaoLeftJoin from registroLocalizacaoLeft in registroLocalizacaoLeftJoin.DefaultIfEmpty()
                join
                   localizacaoGeografica in DataContext.Localizacaogeograficas.AsNoTracking()
                   on new Registrolocalizacao(){ 
                       Localizacaogeografica = registroLocalizacaoLeft != null ? registroLocalizacaoLeft.Localizacaogeografica : 0
                    }.Localizacaogeografica equals localizacaoGeografica.Codigo into localizacaoGeograficaLeftJoin from localizacaoGeograficaLeft in localizacaoGeograficaLeftJoin.DefaultIfEmpty()

                where 
                    registro.Codigo == codRegistro

                select new RegistroDTO()
                {
                    Codigo = registro.Codigo,
                    Nome = registro.Nome,
                    Apelido = apelidoLeft != null ? apelidoLeft.Nome : string.Empty,
                    Idioma = idioma.Nome,
                    Tipo = tipo.Nome,
                    Conteudo = registro.Conteudo,
                    Descricao = descricaoLeft != null ? descricaoLeft.Conteudo : string.Empty,
                    DataInsercao = registro.Datainsercao,
                    Latitude = ObterLocalizacaoGeografica(localizacaoGeograficaLeft, true).ToString(),
                    Longitude = ObterLocalizacaoGeografica(localizacaoGeograficaLeft, false).ToString(),
                }).AsNoTracking().FirstOrDefault();
            
            resultado.Referencias = ReferenciaDAL.ObterReferencia(resultado.Codigo);
            
            return resultado;
        }

        public List<RegistroDTO> Consultar(RegistroDTO registroDTO)
        {
            var registros = (from registro in DataContext.Registros
                join
                    idioma in DataContext.Idiomas
                    on registro.Idioma equals idioma.Codigo
                join
                    tipo in DataContext.Tipos
                    on registro.Tipo equals tipo.Codigo
                join
                    descricao in DataContext.Descricaos
                    on registro.Codigo equals descricao.Registro into descricaoLeftJoin from descricaoLeft in descricaoLeftJoin.DefaultIfEmpty()
                join
                    registroApelido in DataContext.Registroapelidos
                    on registro.Codigo equals registroApelido.Registro into registroApelidoLeftJoin from registroApelidoLeft in registroApelidoLeftJoin.DefaultIfEmpty()
                join
                   apelido in DataContext.Apelidos
                   on new Registroapelido(){ 
                       Apelido = registroApelidoLeft != null ? registroApelidoLeft.Apelido : 0
                    }.Apelido equals apelido.Codigo into apelidoLeftJoin from apelidoLeft in apelidoLeftJoin.DefaultIfEmpty()
                join
                    registroLocalizacao in DataContext.Registrolocalizacaos
                    on registro.Codigo equals registroLocalizacao.Registro into registroLocalizacaoLeftJoin from registroLocalizacaoLeft in registroLocalizacaoLeftJoin.DefaultIfEmpty()
                join
                   localizacaoGeografica in DataContext.Localizacaogeograficas
                   on new Registrolocalizacao(){ 
                       Localizacaogeografica = registroLocalizacaoLeft != null ? registroLocalizacaoLeft.Localizacaogeografica : 0
                    }.Localizacaogeografica equals localizacaoGeografica.Codigo into localizacaoGeograficaLeftJoin from localizacaoGeograficaLeft in localizacaoGeograficaLeftJoin.DefaultIfEmpty()

                where
                    registroDTO.Idioma.ToLower() == idioma.Nome.ToLower() &&
                    ((!string.IsNullOrEmpty(registroDTO.Nome) && registro.Nome.ToLower().Contains(registroDTO.Nome.ToLower())) 
                    || (!string.IsNullOrEmpty(registroDTO.Apelido) && (apelidoLeft != null) && apelidoLeft.Nome.ToLower().Contains(registroDTO.Apelido.ToLower())))                
                
                select new RegistroDTO()
                {
                    Codigo = registro.Codigo,
                    Nome = registro.Nome,
                    Apelido = apelidoLeft != null ? apelidoLeft.Nome : string.Empty,
                    Idioma = idioma.Nome,
                    Tipo = tipo.Nome,
                    Conteudo = registro.Conteudo,
                    Descricao = descricaoLeft != null ? descricaoLeft.Conteudo : string.Empty,
                    DataInsercao = registro.Datainsercao,
                    Latitude = ObterLocalizacaoGeografica(localizacaoGeograficaLeft, true).ToString().Replace(",", "."),
                    Longitude = ObterLocalizacaoGeografica(localizacaoGeograficaLeft, false).ToString().Replace(",", ".")
                }).AsNoTracking().DistinctBy(registroDB => registroDB.Codigo).ToList(); 

            foreach(var registro in registros)
                registro.Referencias = ReferenciaDAL.ObterReferencia(registro.Codigo);
            
            return registros;
        }

        private static double? ObterLocalizacaoGeografica(Localizacaogeografica localizacaoGeograficaLeft, bool latitude)
        {
            if (localizacaoGeograficaLeft != null)
                return latitude ? localizacaoGeograficaLeft.Latitude : localizacaoGeograficaLeft.Longitude;
            return null;
        }
        public int Cadastrar(RegistroDTO registroDTO)
        {
            var registro = DataContext.Registros.AsNoTracking().FirstOrDefault(registro => registro.Nome.ToLower() == registroDTO.Nome.ToLower());
            if(registro != null)
            {
                registroDTO.Codigo = registro.Codigo;
                DataContext.Update(MapearRegistro(registroDTO));
                DataContext.SaveChanges();
            }
            else
            {
                registro = MapearRegistro(registroDTO);
                DataContext.Add(registro);
                DataContext.SaveChanges();
                registroDTO.Codigo = registro.Codigo;
            }
            CadastrarDadosOpcionais(registroDTO);
            return registro.Codigo;
        }
        private Registro MapearRegistro(RegistroDTO registroDTO)
        {
            var idioma = IdiomaDAL.Consultar(new IdiomaDTO(){ Nome = registroDTO.Idioma });
            var tipo = TipoDAL.Consultar(new TipoDTO(){ Nome = registroDTO.Tipo });

            return new Registro()
            {
                Codigo = (int)registroDTO.Codigo,
                Idioma = (int)idioma.Codigo,
                Tipo = (int)tipo.Codigo,
                Nome = registroDTO.Nome,
                Conteudo = registroDTO.Conteudo,
                Datainsercao = DateTime.Now
            };
        }
        private void CadastrarDadosOpcionais(RegistroDTO registroDTO)
        {
            CadastrarDescricao(registroDTO);
            CadastrarApelido(registroDTO);
            CadastrarLocalizacaoGeografica(registroDTO);
            CadastrarReferencias(registroDTO);
        }
        private void CadastrarDescricao(RegistroDTO registroDTO)
        {
            if (string.IsNullOrEmpty(registroDTO.Descricao))
                try
                {
                    DescricaoDAL.Remover(registroDTO.Codigo);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            else
                DescricaoDAL.Cadastrar(new DescricaoDTO()
                {
                    Registro = registroDTO.Codigo,
                    Conteudo = registroDTO.Descricao
                });
        }
        private void CadastrarApelido(RegistroDTO registroDTO)
        {
            if (string.IsNullOrEmpty(registroDTO.Apelido))
                try
                {
                    ApelidoDAL.RemoverVinculo(registroDTO.Codigo);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            else
            {
                var apelidoDTO = new ApelidoDTO()
                { 
                    Nome = registroDTO.Apelido 
                };
                
                ApelidoDAL.Cadastrar(apelidoDTO);
                apelidoDTO.Codigo = DataContext.Apelidos.FirstOrDefault(apelido => apelido.Nome.ToLower() == registroDTO.Apelido.ToLower()).Codigo;
                ApelidoDAL.VincularRegistro(apelidoDTO, registroDTO);
            }     
        }
        private void CadastrarLocalizacaoGeografica(RegistroDTO registroDTO)
        {
            try
            {
                LocalizacaoGeograficaDAL.RemoverVinculoRegistro(registroDTO.Codigo);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            if (registroDTO.Latitude != null && registroDTO.Longitude != null)
            {
                var localizacaoGeograficaDTO = new LocalizacaoGeograficaDTO()
                { 
                    Latitude = double.Parse(registroDTO.Latitude, System.Globalization.NumberStyles.Any, CultureInfo.GetCultureInfo("en-US")),
                    Longitude = double.Parse(registroDTO.Longitude, System.Globalization.NumberStyles.Any, CultureInfo.GetCultureInfo("en-US")),
                };
                
                LocalizacaoGeograficaDAL.Cadastrar(localizacaoGeograficaDTO);
                localizacaoGeograficaDTO.Codigo = DataContext.Localizacaogeograficas.FirstOrDefault
                    (localizao => localizao.Latitude == localizacaoGeograficaDTO.Latitude && 
                        localizao.Longitude == localizacaoGeograficaDTO.Longitude).Codigo;
                LocalizacaoGeograficaDAL.Vincular(localizacaoGeograficaDTO, registroDTO);
            }     
        }
        private void CadastrarReferencias(RegistroDTO registroDTO)
        {           
            ReferenciaDAL.VincularReferencia(registroDTO);
        }
    }
}