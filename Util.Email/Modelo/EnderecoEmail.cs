using PadariaExpress.Validacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util.Email.Modelo
{
    public class EnderecoEmail
    {
        public EnderecoEmail(string endereco)
        {
            EmailAssertionConcern.AssertIsValid(endereco);
            Endereco = endereco;
        }

        public EnderecoEmail(string endereco, string nome)
            : this(endereco)
        {
            Nome = nome;
        }

        public string Nome { get; set; }

        public string Endereco { get; set; }
    }
}
