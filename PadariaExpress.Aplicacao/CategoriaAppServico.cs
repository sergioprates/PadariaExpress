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
    public class CategoriaAppServico : AppServicoBase<Categoria>, ICategoriaAppServico
    {
        private readonly ICategoriaServico _servico;

        public CategoriaAppServico(ICategoriaServico servico)
            : base(servico)
        {
            _servico = servico;
        }

        public IEnumerable<Categoria> ListarAtivos(Padaria padaria)
        {
            return _servico.ListarAtivos(padaria);
        }

        public IEnumerable<Categoria> Listar(Padaria padaria)
        {
            return _servico.Listar(padaria);
        }

        public void Registrar(Categoria categoria)
        {
            _servico.Registrar(categoria);
        }
    }
}
