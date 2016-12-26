using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PadariaExpress.Dominio.Modelo;

namespace PadariaExpress.Relatorio.Modelo
{
    public class RelatorioFaturamento
    {
        public DateTime Data { get; set; }
        public StatusPedido Status { get; set; }
        public double ValorTotal { get; set; }
    }
}
