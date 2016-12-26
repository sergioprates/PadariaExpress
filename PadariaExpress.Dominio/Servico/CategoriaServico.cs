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
    public class CategoriaServico : ServicoBase<Categoria>, ICategoriaServico
    {
        private readonly ICategoriaRepositorio _categoriaRepositorio;

        public CategoriaServico(ICategoriaRepositorio categoriaRepositorio)
            : base(categoriaRepositorio)
        {
            _categoriaRepositorio = categoriaRepositorio;
        }

        public IEnumerable<Categoria> ListarAtivos(Padaria padaria)
        {
            return _categoriaRepositorio.ListarAtivos(padaria);
        }

        public IEnumerable<Categoria> Listar(Padaria padaria)
        {
            return _categoriaRepositorio.Listar(padaria);
        }

        public void Registrar(Categoria categoria)
        {
            using (_categoriaRepositorio)
            {
                _categoriaRepositorio.Inserir(categoria);
            }
        }
    }
}
