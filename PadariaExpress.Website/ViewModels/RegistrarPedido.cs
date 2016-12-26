using PadariaExpress.Dominio.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PadariaExpress.Website.ViewModels
{
    public class RegistrarPedido
    {
        public int PedidoId { get; set; }
        public DateTime DataPedido { get; set; }
        public string Observacao { get; set; }
        public ICollection<RegistrarItemPedido> Itens { get; set; }
        public PadariaViewModel Padaria { get; set; }
        public RegistrarCliente Cliente { get; set; }
        public FormaDePagamentoViewModel FormaDePagamento { get; set; }
        public double ValorPago { get; set; }
        public double? Troco { get; set; }
        public double? ValorTotal { get; set; }
        public EnderecoEntrega Endereco { get; set; }
        public StatusPedido Status { get; set; }
    }
}