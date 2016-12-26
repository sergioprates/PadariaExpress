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
    public class BandeiraCartaoAppServico : AppServicoBase<BandeiraCartao>, IBandeiraCartaoAppServico
    {

        private readonly IBandeiraCartaoServico _servico;

        public BandeiraCartaoAppServico(IBandeiraCartaoServico servico)
            : base(servico)
        {
            _servico = servico;
        }

        public IEnumerable<BandeiraCartao> ListarAtivos()
        {
            return _servico.ListarAtivos();
        }
    }
}
