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
    public class EnderecoUsuarioServico : ServicoBase<EnderecoUsuario>, IEnderecoUsuarioServico
    {
        private readonly IEnderecoUsuarioRepositorio _enderecoUsuarioRepositorio;

        public EnderecoUsuarioServico(IEnderecoUsuarioRepositorio enderecoUsuarioRepositorio)
            : base(enderecoUsuarioRepositorio)
        {
            _enderecoUsuarioRepositorio = enderecoUsuarioRepositorio;
        }
    }
}
