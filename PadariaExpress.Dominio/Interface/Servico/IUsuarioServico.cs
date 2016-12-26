using PadariaExpress.Dominio.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PadariaExpress.Dominio.Interface.Servico
{
    public interface IUsuarioServico : IServicoBase<Usuario>
    {
        Usuario Registrar(Usuario usuario, string confirmacaoSenha);
        void AtualizarUsuario(Usuario obj, string confirmacaoSenha);
        bool ExistePorEmail(string email);

        Usuario BuscarPorEmail(string email);

        Usuario Autenticar(string email, string senha);

        IEnumerable<Usuario> ListarFuncionarioPorPadaria(Padaria p);
    }
}
