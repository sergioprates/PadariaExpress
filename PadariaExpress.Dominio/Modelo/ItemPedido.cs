using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PadariaExpress.Dominio.Modelo
{
    public class ItemPedido
    {
        public int ItemPedidoId { get; set; }
        public int Quantidade { get; set; }
        public double? PrecoTotal
        {
            get
            {
                return PrecoUnitario * Quantidade;
            }
            private set
            { }
        }
        public double PrecoUnitario { get; set; }
        public virtual Produto Produto { get; set; }
        public Pedido Pedido { get; set; }
    }
}
