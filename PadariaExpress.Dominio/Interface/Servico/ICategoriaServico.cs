using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PadariaExpress.Dominio.Modelo;

namespace PadariaExpress.Dominio.Interface.Servico
{
    public interface ICategoriaServico : IServicoBase<Categoria>
    {
        IEnumerable<Categoria> ListarAtivos(Padaria padaria);
        IEnumerable<Categoria> Listar(Padaria padaria);
        void Registrar(Categoria categoria);
    }
}
