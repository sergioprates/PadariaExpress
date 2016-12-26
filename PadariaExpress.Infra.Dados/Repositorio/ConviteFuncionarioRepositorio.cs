using PadariaExpress.Dominio.Interface.Repositorio;
using PadariaExpress.Dominio.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PadariaExpress.Infra.Dados.Repositorio
{
    public class ConviteFuncionarioRepositorio : RepositorioBase<ConviteFuncionario>, IConviteFuncionarioRepositorio
    {
        public ConviteFuncionario BuscarPorHash(string hash)
        {
            return Db.ConvitesFuncionario.FirstOrDefault(x => x.Hash == hash);
        }
    }
}
