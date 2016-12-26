using PadariaExpress.Dominio.Interface.Repositorio;
using PadariaExpress.Dominio.Interface.Servico;
using PadariaExpress.Dominio.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PadariaExpress.Dominio.Servico
{
    public class ItemPedidoServico : ServicoBase<ItemPedido>, IItemPedidoServico
    {
        private readonly IItemPedidoRepositorio _itemPedidoRepositorio;

        public ItemPedidoServico(IItemPedidoRepositorio itemPedidoRepositorio)
            : base(itemPedidoRepositorio)
        {
            _itemPedidoRepositorio = itemPedidoRepositorio;
        }
    }
}
