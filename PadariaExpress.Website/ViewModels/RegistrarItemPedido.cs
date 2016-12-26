using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PadariaExpress.Website.ViewModels
{
    public class RegistrarItemPedido
    {
        public int ItemPedidoId { get; set; }
        public int Quantidade { get; set; }
        public double PrecoTotal { get; set; }
        public double PrecoUnitario { get; set; }
        public RegistrarProduto Produto { get; set; }
    }
}