using PadariaExpress.Aplicacao;
using PadariaExpress.Aplicacao.Interfaces;
using PadariaExpress.Dominio.Interface.Repositorio;
using PadariaExpress.Dominio.Interface.Servico;
using PadariaExpress.Dominio.Servico;
using PadariaExpress.Infra.Dados.Repositorio;
using PadariaExpress.Relatorio.Interface;
using SimpleInjector;
using SimpleInjector.Integration.Web;

namespace PadariaExpress.IOC
{
    public static class RegistradorDeDependencias
    {
        public static Container GetContainer()
        {
            var container = new Container();


            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();
            //Repositorios
            container.Register<IBandeiraCartaoRepositorio, BandeiraCartaoRepositorio>(Lifestyle.Scoped);
            container.Register<ICategoriaRepositorio, CategoriaRepositorio>(Lifestyle.Scoped);
            container.Register<IEnderecoUsuarioRepositorio, EnderecoUsuarioRepositorio>(Lifestyle.Scoped);
            container.Register<IFormaDePagamentoRepositorio, FormaDePagamentoRepositorio>(Lifestyle.Scoped);
            container.Register<IFuncionarioRepositorio, FuncionarioRepositorio>(Lifestyle.Scoped);
            container.Register<IItemPedidoRepositorio, ItemPedidoRepositorio>(Lifestyle.Scoped);
            container.Register<IPadariaRepositorio, PadariaRepositorio>(Lifestyle.Scoped);
            container.Register<IPedidoRepositorio, PedidoRepositorio>(Lifestyle.Scoped);
            container.Register<IPeriodoFuncionamentoRepositorio, PeriodoFuncionamentoRepositorio>(Lifestyle.Scoped);
            container.Register<IProdutoRepositorio, ProdutoRepositorio>(Lifestyle.Scoped);
            container.Register<IProprietarioRepositorio, ProprietarioRepositorio>(Lifestyle.Scoped);
            container.Register<ISexoRepositorio, SexoRepositorio>(Lifestyle.Scoped);
            container.Register<IUsuarioRepositorio, UsuarioRepositorio>(Lifestyle.Scoped);
            container.Register<IConviteFuncionarioRepositorio, ConviteFuncionarioRepositorio>(Lifestyle.Scoped);


            container.Register<IRelatorioFaturamentoRepositorio, RelatorioFaturamentoRepositorio>();
            

            //Serviços
            container.Register<IBandeiraCartaoServico, BandeiraCartaoServico>();
            container.Register<ICategoriaServico, CategoriaServico>();
            container.Register<IEnderecoUsuarioServico, EnderecoUsuarioServico>();
            container.Register<IFormaDePagamentoServico, FormaDePagamentoServico>();
            container.Register<IFuncionarioServico, FuncionarioServico>();
            container.Register<IItemPedidoServico, ItemPedidoServico>();
            container.Register<IPadariaServico, PadariaServico>();
            container.Register<IPedidoServico, PedidoServico>();
            container.Register<IPeriodoFuncionamentoServico, PeriodoFuncionamentoServico>();
            container.Register<IProdutoServico, ProdutoServico>();
            container.Register<IProprietarioServico, ProprietarioServico>();
            container.Register<ISexoServico, SexoServico>();
            container.Register<IUsuarioServico, UsuarioServico>();
            container.Register<IConviteFuncionarioServico, ConviteFuncionarioServico>();


            //Aplicação
            container.Register<IUsuarioAppServico, UsuarioAppServico>();
            container.Register<ISexoAppServico, SexoAppServico>();
            container.Register<IPadariaAppServico, PadariaAppServico>();
            container.Register<IFormaDePagamentoAppServico, FormaDePagamentoAppServico>();
            container.Register<IBandeiraCartaoAppServico, BandeiraCartaoAppServico>();
            container.Register<ICategoriaAppServico, CategoriaAppServico>();
            container.Register<IProdutoAppServico, ProdutoAppServico>();
            container.Register<IPedidoAppServico, PedidoAppServico>();
            container.Register<IConviteFuncionarioAppServico, ConviteFuncionarioAppServico>();
            

            return container;
        }
    }
}