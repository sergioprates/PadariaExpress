using AutoMapper;
using PadariaExpress.Dominio.Modelo;
using PadariaExpress.Website.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PadariaExpress.Website.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DomainToViewModelMappings"; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<Proprietario, ProprietarioViewModel>();
            Mapper.CreateMap<Proprietario, RegistrarProprietario>();
            Mapper.CreateMap<Funcionario, RegistrarFuncionario>();
            Mapper.CreateMap<Funcionario, FuncionarioViewModel>(); 
            Mapper.CreateMap<Sexo, SexoViewModel>();
            Mapper.CreateMap<EnderecoUsuario, EnderecoUsuarioViewModel>();
            Mapper.CreateMap<Padaria, PadariaViewModel>();
            Mapper.CreateMap<Padaria, RegistrarPadaria>();
            Mapper.CreateMap<BandeiraCartao, BandeiraCartaoViewModel>();
            Mapper.CreateMap<FormaDePagamento, FormaDePagamentoViewModel>();
            Mapper.CreateMap<FormaDePagamento, RegistrarFormaDePagamento>();
            Mapper.CreateMap<Categoria, RegistrarCategoria>();
            Mapper.CreateMap<Produto, RegistrarProduto>();
            Mapper.CreateMap<PeriodoFuncionamento, PeriodoDeFuncionamentoViewModel>();
            Mapper.CreateMap<Cliente, RegistrarCliente>();
            Mapper.CreateMap<ItemPedido, RegistrarItemPedido>();
            Mapper.CreateMap<Pedido, RegistrarPedido>();
        }
    }
}