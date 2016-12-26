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
    public class FormaDePagamentoServico : ServicoBase<FormaDePagamento>, IFormaDePagamentoServico
    {
        private readonly IFormaDePagamentoRepositorio _formaDePagamentoRepositorio;

        public FormaDePagamentoServico(IFormaDePagamentoRepositorio formaDePagamentoRepositorio)
            : base(formaDePagamentoRepositorio)
        {
            _formaDePagamentoRepositorio = formaDePagamentoRepositorio;
        }

        public IEnumerable<FormaDePagamento> ListarAtivos(Padaria padaria)
        {
            using (_formaDePagamentoRepositorio)
            {
                return _formaDePagamentoRepositorio.ListarAtivos(padaria);
            }
        }

        public IEnumerable<FormaDePagamento> Listar(Padaria padaria)
        {
            return _formaDePagamentoRepositorio.Listar(padaria);
        }

        public void Registrar(FormaDePagamento formaDePagamento)
        {
            using (_formaDePagamentoRepositorio)
            {
                _formaDePagamentoRepositorio.Inserir(formaDePagamento);
            }
        }
    }
}
