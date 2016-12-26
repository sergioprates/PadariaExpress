using AutoMapper;
using PadariaExpress.Dominio.Modelo;
using PadariaExpress.Website.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PadariaExpress.Website.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "ViewModelToDomainMappings"; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<ProprietarioViewModel, Proprietario>().ForMember(c => c.Padarias, o => o.UseDestinationValue());

            Mapper.CreateMap<RegistrarProprietario, Proprietario>();
            Mapper.CreateMap<RegistrarFuncionario, Funcionario>();
            Mapper.CreateMap<FuncionarioViewModel, Funcionario>(); 
            Mapper.CreateMap<EnderecoUsuarioViewModel, EnderecoUsuario>();
            Mapper.CreateMap<SexoViewModel, Sexo>();
            Mapper.CreateMap<PadariaViewModel, Padaria>();
            Mapper.CreateMap<RegistrarPadaria, Padaria>();
            Mapper.CreateMap<BandeiraCartaoViewModel, BandeiraCartao>();
            Mapper.CreateMap<FormaDePagamentoViewModel, FormaDePagamento>().ForMember(c => c.BandeiraCartao, o => o.UseDestinationValue());
            Mapper.CreateMap<RegistrarFormaDePagamento, FormaDePagamento>().ForMember(c => c.BandeiraCartao, o => o.UseDestinationValue());
            Mapper.CreateMap<RegistrarCategoria, Categoria>();
            Mapper.CreateMap<RegistrarProduto, Produto>();
            Mapper.CreateMap<PeriodoDeFuncionamentoViewModel, PeriodoFuncionamento>();
            Mapper.CreateMap<RegistrarCliente, Cliente>();
            
            Mapper.CreateMap<RegistrarItemPedido, ItemPedido>();
            Mapper.CreateMap<RegistrarPedido, Pedido>();
        }
    }
}