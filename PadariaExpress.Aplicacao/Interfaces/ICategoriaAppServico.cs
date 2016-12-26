using PadariaExpress.Dominio.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PadariaExpress.Aplicacao.Interfaces
{
    public interface ICategoriaAppServico : IAppServicoBase<Categoria>
    {
        IEnumerable<Categoria> ListarAtivos(Padaria padaria);
        IEnumerable<Categoria> Listar(Padaria padaria);
        void Registrar(Categoria categoria);
    }
}
