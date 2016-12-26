using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PadariaExpress.Dominio.Interface.Repositorio;
using PadariaExpress.Dominio.Modelo;

namespace PadariaExpress.Infra.Dados.Repositorio
{
    public class CategoriaRepositorio : RepositorioBase<Categoria>, ICategoriaRepositorio
    {
        public IEnumerable<Categoria> ListarAtivos(Padaria padaria)
        {
            return Db.Categorias.Where(x => x.Padaria.PadariaId == padaria.PadariaId && x.Ativo == true).OrderBy(x => x.Nome).ToArray();
        }

        public IEnumerable<Categoria> Listar(Padaria padaria)
        {
            return Db.Categorias.Where(x => x.Padaria.PadariaId == padaria.PadariaId).OrderBy(x => x.Nome).ToArray();
        }

        public override void Inserir(Categoria obj)
        {
            obj.Padaria = Db.Padarias.Find(obj.Padaria.PadariaId);
            base.Inserir(obj);
        }

        public override void Alterar(Categoria obj)
        {
            obj.Padaria = Db.Padarias.Find(obj.Padaria.PadariaId);
            base.Alterar(obj);
        }
    }
}
