using PadariaExpress.Dominio.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PadariaExpress.Dominio.Interface.Servico
{
    public interface IProdutoServico : IServicoBase<Produto>
    {
        IEnumerable<Produto> ListarAtivos(Padaria padaria);
        IEnumerable<Produto> Listar(Padaria padaria);
        void Registrar(Produto produto);
    }
}
