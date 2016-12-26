using PadariaExpress.Aplicacao.Interfaces;
using PadariaExpress.Dominio.Interface.Servico;
using PadariaExpress.Dominio.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PadariaExpress.Aplicacao
{
    public class FormaDePagamentoAppServico : AppServicoBase<FormaDePagamento>, IFormaDePagamentoAppServico
    {
        private readonly IFormaDePagamentoServico _servico;

        public FormaDePagamentoAppServico(IFormaDePagamentoServico servico)
            : base(servico)
        {
            _servico = servico;
        }

        public IEnumerable<FormaDePagamento> ListarAtivos(Padaria padaria)
        {
            return _servico.ListarAtivos(padaria);
        }

        public IEnumerable<FormaDePagamento> Listar(Padaria padaria)
        {
            return _servico.Listar(padaria);
        }


        public void Registrar(FormaDePagamento formaDePagamento)
        {
            _servico.Registrar(formaDePagamento);
        }
    }
}
