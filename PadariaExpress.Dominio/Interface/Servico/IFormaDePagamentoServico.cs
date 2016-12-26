using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PadariaExpress.Dominio.Modelo;

namespace PadariaExpress.Dominio.Interface.Servico
{
    public interface IFormaDePagamentoServico : IServicoBase<FormaDePagamento>
    {
        IEnumerable<FormaDePagamento> ListarAtivos(Padaria padaria);
        IEnumerable<FormaDePagamento> Listar(Padaria padaria);

        void Registrar(FormaDePagamento formaDePagamento);
    }
}
