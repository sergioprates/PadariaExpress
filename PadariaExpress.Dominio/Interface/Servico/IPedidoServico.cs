using PadariaExpress.Dominio.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PadariaExpress.Dominio.Interface.Servico
{
    public interface IPedidoServico : IServicoBase<Pedido>
    {
        Pedido Registrar(Pedido pedido);
        IEnumerable<Pedido> ListarNaoCanceladoPorPadaria(Padaria p);
        void AtualizarStatus(Pedido p, StatusPedido Status);
        IEnumerable<Pedido> ListarPorCliente(Usuario u);
    }
}
