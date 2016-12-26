using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PadariaExpress.Website.ViewModels
{
    public class EnderecoUsuarioViewModel
    {
        public int EnderecoUsuarioId { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataAlteracao { get; set; }
        public bool Ativo { get; set; }
        public string CEP { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string Estado { get; set; }
    }
}