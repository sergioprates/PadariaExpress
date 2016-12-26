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
    public class ConviteFuncionarioAppServico : AppServicoBase<ConviteFuncionario>, IConviteFuncionarioAppServico
    {
        private readonly IConviteFuncionarioServico _servico;

        public ConviteFuncionarioAppServico(IConviteFuncionarioServico servico)
            : base(servico)
        {
            _servico = servico;
        }


        public ConviteFuncionario BuscarPorHash(string hash)
        {
            return _servico.BuscarPorHash(hash);
        }
    }
}
