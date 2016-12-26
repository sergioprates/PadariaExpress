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
    public class ProdutoServico : ServicoBase<Produto>, IProdutoServico
    {
        private readonly IProdutoRepositorio _produtoRepositorio;

        public ProdutoServico(IProdutoRepositorio ProdutoRepositorio)
            : base(ProdutoRepositorio)
        {
            _produtoRepositorio = ProdutoRepositorio;
        }

        public IEnumerable<Produto> ListarAtivos(Padaria padaria)
        {
            return _produtoRepositorio.ListarAtivos(padaria);
        }

        public IEnumerable<Produto> Listar(Padaria padaria)
        {
            return _produtoRepositorio.Listar(padaria);
        }


        public void Registrar(Produto produto)
        {
            using (_produtoRepositorio)
            {
                _produtoRepositorio.Inserir(produto);
            }
        }
    }
}
