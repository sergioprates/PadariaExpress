using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PadariaExpress.Dominio.Interface.Repositorio
{
    public interface IRepositorioBase<TEntity> : IDisposable
        where TEntity : class
    {
        void Inserir(TEntity obj);
        TEntity BuscarPorId(int id);
        IEnumerable<TEntity> Listar();
        void Alterar(TEntity obj);
        void Deletar(TEntity obj);
    }
}
