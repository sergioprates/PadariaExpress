using PadariaExpress.Dominio.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PadariaExpress.Aplicacao.Interfaces
{
    public interface IBandeiraCartaoAppServico : IAppServicoBase<BandeiraCartao>
    {
        IEnumerable<BandeiraCartao> ListarAtivos();
    }
}
