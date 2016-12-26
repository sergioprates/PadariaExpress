using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util.Email.Modelo
{
    public class SmtpEmail
    {
        public SmtpEmail()
        {}

        public SmtpEmail(string host)
        {
            Host = host;
        }

        public SmtpEmail(string host, int porta)
            :this(host)
        {
            Porta = porta;
        }

        public SmtpEmail(string host, int porta, string usuario, string senha)
            :this(host, porta)
        {
            Usuario = usuario;
            Senha = senha;
            Autenticar = true;
        }

        public string Host { get; set; }
        public int Porta { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public bool Autenticar { get; set; }
    }
}
