using PadariaExpress.Dominio.Modelo;
using PadariaExpress.Relatorio.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PadariaExpress.Relatorio.Interface
{
    public interface IRelatorioFaturamentoRepositorio
    {
        IEnumerable<RelatorioFaturamento> ListarRelatorioFaturamento(Padaria p, DateTime DataInicial, DateTime DataFinal);
    }
}
