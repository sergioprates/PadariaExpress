using PadariaExpress.Dominio.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PadariaExpress.Aplicacao.Interfaces
{
    public interface IConviteFuncionarioAppServico : IAppServicoBase<ConviteFuncionario>
    {
        ConviteFuncionario BuscarPorHash(string hash);
    }
}
