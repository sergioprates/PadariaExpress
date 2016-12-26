using PadariaExpress.Dominio.Interface.Repositorio;
using PadariaExpress.Dominio.Interface.Servico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PadariaExpress.Dominio.Servico
{
    public class ServicoBase<TEntity> : IServicoBase<TEntity> where TEntity : class
    {
        private readonly IRepositorioBase<TEntity> _repositorio;

        public ServicoBase(IRepositorioBase<TEntity> repositorio)
        {
            _repositorio = repositorio;
        }

        public virtual void Inserir(TEntity obj)
        {
            _repositorio.Inserir(obj);
        }

        public TEntity BuscarPorId(int id)
        {
            return _repositorio.BuscarPorId(id);
        }

        public IEnumerable<TEntity> Listar()
        {
            return _repositorio.Listar();
        }

        

        public virtual void Alterar(TEntity obj)
        {
            _repositorio.Alterar(obj);
        }

        public void Deletar(TEntity obj)
        {
            _repositorio.Deletar(obj);
        }
    }
}
