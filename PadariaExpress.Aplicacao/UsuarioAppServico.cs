using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PadariaExpress.Aplicacao.Interfaces;
using PadariaExpress.Dominio.Interface.Servico;
using PadariaExpress.Dominio.Modelo;
using Util.Email.Modelo;
using Util.Email.Servicos;
using System.Configuration;
using System.IO;

namespace PadariaExpress.Aplicacao
{
    public class UsuarioAppServico : AppServicoBase<Usuario>, IUsuarioAppServico
    {
        private readonly IUsuarioServico _servico;
        private readonly IPadariaServico _padariaServico;
        private readonly IConviteFuncionarioServico _conviteFuncionarioServico;

        public UsuarioAppServico(IUsuarioServico servico, IPadariaServico padariaServico, IConviteFuncionarioServico conviteFuncionarioServico)
            : base(servico)
        {
            _servico = servico;
            _padariaServico = padariaServico;
            _conviteFuncionarioServico = conviteFuncionarioServico;
        }

        public Usuario Registrar(Usuario usuario, string confirmacaoSenha, string html)
        {
            Usuario u = _servico.Registrar(usuario, confirmacaoSenha);
            string assunto = String.Empty;
            SmtpEmail smtp = new SmtpEmail(ConfigurationManager.AppSettings["EnderecoSmtp"], Convert.ToInt32(ConfigurationManager.AppSettings["PortaSmtp"]));
            smtp.Usuario = ConfigurationManager.AppSettings["UsuarioSmtp"];
            smtp.Senha = ConfigurationManager.AppSettings["SenhaSmtp"];
            smtp.Autenticar = true;

            EnderecoEmail remetente = new EnderecoEmail(ConfigurationManager.AppSettings["EmailPadariaExpress"], ConfigurationManager.AppSettings["NomeEmailPadariaExpress"]);

            List<EnderecoEmail> lista = new List<EnderecoEmail>();
            lista.Add(new EnderecoEmail(u.Email));

            assunto = ConfigurationManager.AppSettings["AssuntoCadastro"];

            EnderecoEmailServico.EnviarEmail(
                    remetente,
                    lista,
                    html,
                    assunto,
                    smtp,
                    "<#NOME>",
                    u.Nome,
                    ';');

            return u;
        }

        public Usuario Autenticar(string email, string senha)
        {
            return _servico.Autenticar(email, senha);
        }


        public void AtualizarUsuario(Usuario obj, string confirmacaoSenha)
        {
            _servico.AtualizarUsuario(obj, confirmacaoSenha);
        }


        public IEnumerable<Usuario> ListarFuncionarioPorPadaria(Padaria p)
        {
            return _servico.ListarFuncionarioPorPadaria(p);
        }


        public void ConvidarFuncionario(Usuario u, Padaria p, string html)
        {
            List<EnderecoEmail> lista = new List<EnderecoEmail>();
            lista.Add(new EnderecoEmail(u.Email));
            p = _padariaServico.BuscarPorId(p.PadariaId);

            ConviteFuncionario convite = new ConviteFuncionario();
            convite.PadariaId = p.PadariaId;
            convite.Hash = Guid.NewGuid().ToString().Substring(0, 8);
            convite.Email = u.Email;
            convite.Utilizado = false;
            _conviteFuncionarioServico.Inserir(convite);
            EnderecoEmail remetente = new EnderecoEmail(ConfigurationManager.AppSettings["EmailPadariaExpress"], ConfigurationManager.AppSettings["NomeEmailPadariaExpress"]);

            SmtpEmail smtp = new SmtpEmail(ConfigurationManager.AppSettings["EnderecoSmtp"], Convert.ToInt32(ConfigurationManager.AppSettings["PortaSmtp"]));
            smtp.Usuario = ConfigurationManager.AppSettings["UsuarioSmtp"];
            smtp.Senha = ConfigurationManager.AppSettings["SenhaSmtp"];
            smtp.Autenticar = true;

            EnderecoEmailServico.EnviarEmail(
                remetente,
                lista,
                html,
                ConfigurationManager.AppSettings["AssuntoConviteFuncionario"],
                smtp,
                "<#HASH>;<#PADARIAID>;<#NOMEPADARIA>",
                convite.Hash + ";" + p.PadariaId.ToString() + ";" + p.NomeFantasia,
                ';');
        }
    }
}
