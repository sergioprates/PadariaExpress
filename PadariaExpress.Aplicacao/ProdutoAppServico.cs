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
    public class ProdutoAppServico : AppServicoBase<Produto>, IProdutoAppServico
    {
        private readonly IProdutoServico _servico;

        public ProdutoAppServico(IProdutoServico servico)
            : base(servico)
        {
            _servico = servico;
        }

        public IEnumerable<Produto> ListarAtivos(Padaria padaria)
        {
            return _servico.ListarAtivos(padaria);
        }

        public IEnumerable<Produto> Listar(Padaria padaria)
        {
            return _servico.Listar(padaria);
        }

        public void Registrar(Produto produto)
        {
            _servico.Registrar(produto);
        }
    
    }
}
