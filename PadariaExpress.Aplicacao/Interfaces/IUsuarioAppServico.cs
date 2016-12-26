using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PadariaExpress.Dominio.Modelo;

namespace PadariaExpress.Aplicacao.Interfaces
{
    public interface IUsuarioAppServico : IAppServicoBase<Usuario>
    {
        Usuario Registrar(Usuario usuario, string confirmacaoSenha, string html);
        void AtualizarUsuario(Usuario obj, string confirmacaoSenha);
        Usuario Autenticar(string email, string senha);
        IEnumerable<Usuario> ListarFuncionarioPorPadaria(Padaria p);
        void ConvidarFuncionario(Usuario u, Padaria p, string html);
    }
}
