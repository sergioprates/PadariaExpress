using PadariaExpress.Dominio.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PadariaExpress.Website.ViewModels
{
    public class FuncionarioViewModel
    {
        public int UsuarioId { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataAlteracao { get; set; }
        public bool Ativo { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Cpf { get; set; }
        public virtual IEnumerable<EnderecoUsuarioViewModel> EnderecosUsuario { get; set; }
        public SexoViewModel Sexo { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
        public IList<PadariaViewModel>Padarias { get; set; }
    }
}