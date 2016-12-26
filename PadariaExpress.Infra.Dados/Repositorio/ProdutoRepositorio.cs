using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PadariaExpress.Dominio.Interface.Repositorio;
using PadariaExpress.Dominio.Modelo;

namespace PadariaExpress.Infra.Dados.Repositorio
{
    public class ProdutoRepositorio : RepositorioBase<Produto>, IProdutoRepositorio
    {
        public IEnumerable<Produto> ListarAtivos(Padaria padaria)
        {
            return Db.Produtos
                .Where(x => x.Categoria.Padaria.PadariaId == padaria.PadariaId && x.Ativo == true)
                    .OrderBy(x => x.Nome).ToArray();
        }

        public IEnumerable<Produto> Listar(Padaria padaria)
        {
            return Db.Produtos
                .Where(x => x.Categoria.Padaria.PadariaId == padaria.PadariaId)
                    .OrderBy(x => x.Nome).ToArray();
        }

        public override void Inserir(Produto obj)
        {
            obj.Categoria = Db.Categorias.Find(obj.Categoria.CategoriaId);
            base.Inserir(obj);
        }

        public override void Alterar(Produto obj)
        {
            obj.Categoria = Db.Categorias.Find(obj.Categoria.CategoriaId);
            base.Alterar(obj);
        }
    }
}
