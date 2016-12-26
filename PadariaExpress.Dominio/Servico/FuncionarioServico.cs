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
    public class FuncionarioServico : ServicoBase<Funcionario>, IFuncionarioServico
    {
        private readonly IFuncionarioRepositorio _funcionarioRepositorio;

        public FuncionarioServico(IFuncionarioRepositorio funcionarioRepositorio)
            : base(funcionarioRepositorio)
        {
            _funcionarioRepositorio = funcionarioRepositorio;
        }
    }
}
