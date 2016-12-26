using PadariaExpress.Dominio.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PadariaExpress.Aplicacao.Interfaces
{
    public interface IProdutoAppServico : IAppServicoBase<Produto>
    {
        IEnumerable<Produto> ListarAtivos(Padaria padaria);
        IEnumerable<Produto> Listar(Padaria padaria);
        void Registrar(Produto categoria);
    }
}
