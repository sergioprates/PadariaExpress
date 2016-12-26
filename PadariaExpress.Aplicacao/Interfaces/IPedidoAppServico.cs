using PadariaExpress.Dominio.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PadariaExpress.Aplicacao.Interfaces
{
    public interface IPedidoAppServico : IAppServicoBase<Pedido>
    {
        Pedido Registrar(Pedido pedido);
        IEnumerable<Pedido> ListarNaoCanceladoPorPadaria(Padaria p);
        void AtualizarStatus(Pedido p, StatusPedido Status);

        IEnumerable<Pedido> ListarPorCliente(Usuario u);
    }
}
