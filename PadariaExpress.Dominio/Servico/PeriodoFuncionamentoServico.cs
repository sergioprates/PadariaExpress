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
    public class PeriodoFuncionamentoServico : ServicoBase<PeriodoFuncionamento>, IPeriodoFuncionamentoServico
    {
        private readonly IPeriodoFuncionamentoRepositorio _periodoFuncionamentoRepositorio;

        public PeriodoFuncionamentoServico(IPeriodoFuncionamentoRepositorio periodoFuncionamentoRepositorio)
            : base(periodoFuncionamentoRepositorio)
        {
            _periodoFuncionamentoRepositorio = periodoFuncionamentoRepositorio;
        }
    }
}
