using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PadariaExpress.Dominio.Modelo
{
    public class Pedido
    {
        private double? _valorTotal;
        private double? _troco;
        public int PedidoId { get; set; }
        public DateTime DataPedido { get; set; }
        public string Observacao { get; set; }
        public double? ValorTotal
        {
            get
            {
                _valorTotal = 0;
                if (Itens != null)
                {
                    _valorTotal = Itens.Sum(x => x.PrecoTotal);
                }

                if (Padaria != null)
                {
                    _valorTotal += Padaria.ValorFrete;
                }

                return _valorTotal;
            }
            private set
            {

            }
        }
        public virtual ICollection<ItemPedido> Itens { get; set; }
        public virtual Padaria Padaria { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual FormaDePagamento FormaDePagamento { get; set; }
        public double ValorPago { get; set; }
        public double? Troco
        {
            get
            {
                if (FormaDePagamento != null)
                {
                    if (FormaDePagamento.Tipo == TipoFormaDePagamento.Dinheiro)
                    {
                        return ValorPago - ValorTotal;
                    } 
                }
                

                return null;
            }
            private set
            { }
        }
        public StatusPedido Status { get; set; }
        public EnderecoEntrega Endereco { get; set; }
    }
}
