using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PadariaExpress.Dominio.Modelo;

namespace PadariaExpress.Dominio.Interface.Repositorio
{
    public interface IPedidoRepositorio : IRepositorioBase<Pedido>
    {
        IEnumerable<Pedido> ListarNaoCanceladoPorPadaria(Padaria p);

        void AtualizarStatus(Pedido p, StatusPedido Status);
        IEnumerable<Pedido> ListarPorCliente(Usuario u);
    }
}
