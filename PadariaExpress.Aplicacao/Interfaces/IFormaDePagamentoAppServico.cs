using PadariaExpress.Dominio.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PadariaExpress.Aplicacao.Interfaces
{
    public interface IFormaDePagamentoAppServico : IAppServicoBase<FormaDePagamento>
    {
        IEnumerable<FormaDePagamento> ListarAtivos(Padaria padaria);
        IEnumerable<FormaDePagamento> Listar(Padaria padaria);
        void Registrar(FormaDePagamento formaDePagamento);
    }
}
