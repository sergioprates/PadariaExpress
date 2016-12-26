using PadariaExpress.Dominio.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PadariaExpress.Dominio.Interface.Repositorio
{
    public interface IUsuarioRepositorio : IRepositorioBase<Usuario>
    {
        Usuario BuscarPorEmail(string email);

        bool ExistePorEmail(string email);

        bool ExistePorCpf(string Cpf);

        IEnumerable<Usuario> ListarFuncionarioPorPadaria(Padaria p);
    }
}
