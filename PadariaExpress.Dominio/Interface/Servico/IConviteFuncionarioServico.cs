using PadariaExpress.Dominio.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PadariaExpress.Dominio.Interface.Servico
{
    public interface IConviteFuncionarioServico : IServicoBase<ConviteFuncionario>
    {
        ConviteFuncionario BuscarPorHash(string hash);
    }
}
