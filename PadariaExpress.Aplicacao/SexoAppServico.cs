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
    public class SexoAppServico : AppServicoBase<Sexo>, ISexoAppServico
    {
        private readonly ISexoServico _servico;

        public SexoAppServico(ISexoServico servico)
            : base(servico)
        {
            _servico = servico;
        }

        public IEnumerable<Sexo> ListarAtivos()
        {
            return _servico.ListarAtivos();
        }
    }
}
