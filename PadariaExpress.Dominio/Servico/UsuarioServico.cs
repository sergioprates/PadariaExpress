using PadariaExpress.Dominio.Interface.Repositorio;
using PadariaExpress.Dominio.Interface.Servico;
using PadariaExpress.Dominio.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PadariaExpress.Validacao;
using PadariaExpress.Dominio.Factory;
using Util.Localizacao;
using Util.Localizacao.Servicos;
namespace PadariaExpress.Dominio.Servico
{
    public class UsuarioServico : ServicoBase<Usuario>, IUsuarioServico
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISexoRepositorio _sexoRepositorio;

        public UsuarioServico(IUsuarioRepositorio usuarioRepositorio, ISexoRepositorio sexoRepositorio)
            : base(usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _sexoRepositorio = sexoRepositorio;
        }

        public Usuario Registrar(Usuario usuario, string confirmacaoSenha)
        {
            if (_usuarioRepositorio.ExistePorEmail(usuario.Email) == true)
            {
                throw new Exception("E-mail já cadastrado!");
            }

            if (_usuarioRepositorio.ExistePorCpf(usuario.Cpf) == true)
            {
                throw new Exception("CPF já cadastrado!");
            }

            var u = UsuarioFactory.Instanciar(usuario.TipoUsuario, usuario.Email, usuario.Nome);
            u.DataNascimento = usuario.DataNascimento;
            u.Ativo = usuario.Ativo;
            u.Telefone = usuario.Telefone;

            using (_sexoRepositorio)
            {
                u.Sexo = _sexoRepositorio.BuscarPorId(usuario.Sexo.SexoId);
            }
            u.Cpf = usuario.Cpf;
            u.AlterarSenha(usuario.Senha, confirmacaoSenha);
            u.AlterarEmail(usuario.Email);
            u.EnderecosUsuario = usuario.EnderecosUsuario;

            if (u is Cliente)
            {
                ((Cliente)u).RegistroAndroid = ((Cliente)usuario).RegistroAndroid;
            }

            if (u is Funcionario)
            {
                ((Funcionario)u).Padarias = ((Funcionario)usuario).Padarias;
            }

            _usuarioRepositorio.Inserir(u);
            return u;
        }


        public void AtualizarUsuario(Usuario obj, string confirmacaoSenha)
        {
            var u = UsuarioFactory.Instanciar(obj.TipoUsuario, obj.Email, obj.Nome);
            u.UsuarioId = obj.UsuarioId;
            u.DataNascimento = obj.DataNascimento;
            u.Ativo = obj.Ativo;

            using (_sexoRepositorio)
            {
                u.Sexo = _sexoRepositorio.BuscarPorId(obj.Sexo.SexoId);
            }

            u.Cpf = obj.Cpf;
            u.Telefone = obj.Telefone;
            u.AlterarSenha(obj.Senha, confirmacaoSenha);
            u.AlterarEmail(obj.Email);
            u.EnderecosUsuario = obj.EnderecosUsuario;

            if (u is Cliente)
            {
                ((Cliente)u).RegistroAndroid = ((Cliente)obj).RegistroAndroid;
            }

            if (u is Funcionario)
            {
                ((Funcionario)u).Padarias = ((Funcionario)obj).Padarias;
            }

            base.Alterar(u);
        }

        public Usuario Autenticar(string email, string senha)
        {
            EmailAssertionConcern.AssertIsValid(email);
            PasswordAssertionConcern.AssertIsValid(senha);

            Usuario u = _usuarioRepositorio.BuscarPorEmail(email);

            AssertionConcern.AssertArgumentNotNull(u, "Usuário inexistente.");
            string senhaEncriptada = PasswordAssertionConcern.Encrypt(senha);

            if (u.Ativo == false)
            {
                throw new Exception("Este usuário foi inativado do sistema.");
            }

            if (u.Senha == senhaEncriptada)
            {
                return u;
            }
            else
            {
                throw new Exception("Senha incorreta.");
            }

        }

        public Usuario BuscarPorEmail(string email)
        {
            EmailAssertionConcern.AssertIsValid(email);
            return _usuarioRepositorio.BuscarPorEmail(email);
        }

        public bool ExistePorEmail(string email)
        {
            EmailAssertionConcern.AssertIsValid(email);
            return _usuarioRepositorio.ExistePorEmail(email);
        }


        public IEnumerable<Usuario> ListarFuncionarioPorPadaria(Padaria p)
        {
            return _usuarioRepositorio.ListarFuncionarioPorPadaria(p);
        }
    }
}
