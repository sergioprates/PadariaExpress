using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PadariaExpress.Aplicacao.Interfaces;
using PadariaExpress.Dominio.Interface.Servico;

namespace PadariaExpress.Aplicacao
{
    public class AppServicoBase<TEntity> : IAppServicoBase<TEntity>
        where TEntity : class
    {
        private readonly IServicoBase<TEntity> _servico;

        public AppServicoBase(IServicoBase<TEntity> servico)
        {
            _servico = servico;
        }

        public void Inserir(TEntity obj)
        {
            _servico.Inserir(obj);
        }

        public TEntity BuscarPorId(int id)
        {
            return _servico.BuscarPorId(id);
        }

        public IEnumerable<TEntity> Listar()
        {
            return _servico.Listar();
        }

        public void Alterar(TEntity obj)
        {
            _servico.Alterar(obj);
        }

        public void Deletar(TEntity obj)
        {
            _servico.Deletar(obj);
        }
    }
}
