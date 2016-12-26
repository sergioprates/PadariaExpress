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
    public class ConviteFuncionarioServico : ServicoBase<ConviteFuncionario>, IConviteFuncionarioServico
    {
        private readonly IConviteFuncionarioRepositorio _conviteRepositorio;

        public ConviteFuncionarioServico(IConviteFuncionarioRepositorio conviteRepositorio)
            : base(conviteRepositorio)
        {
            _conviteRepositorio = conviteRepositorio;
        }

        public ConviteFuncionario BuscarPorHash(string hash)
        {
            return _conviteRepositorio.BuscarPorHash(hash);
        }
    }
}
