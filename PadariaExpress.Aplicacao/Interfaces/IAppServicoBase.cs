using System.Collections.Generic;

namespace PadariaExpress.Aplicacao.Interfaces
{
    public interface IAppServicoBase<TEntity> where TEntity : class
    {
        void Inserir(TEntity obj);
        TEntity BuscarPorId(int id);
        IEnumerable<TEntity> Listar();
        
        void Alterar(TEntity obj);
        void Deletar(TEntity obj);
    }
}
