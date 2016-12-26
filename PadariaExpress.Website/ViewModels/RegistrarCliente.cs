using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace PadariaExpress.Website.ViewModels
{
    public class RegistrarCliente
    {
        private string _cpf;
        private DateTime _dataNascimento;

        public int UsuarioId { get; set; }
        public bool Ativo { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        public string ConfirmaSenha { get; set; }
        public string Email { get; set; }
        public IEnumerable<EnderecoUsuarioViewModel> EnderecosUsuario { get; set; }
        public string StrDataNascimento { get; set; }
        public string Telefone { get; set; }
        public string RegistroAndroid { get; set; }
        public DateTime DataNascimento
        {
            get 
            {
                if (string.IsNullOrWhiteSpace(StrDataNascimento) == false)
                {
                    StrDataNascimento = StrDataNascimento.Replace("/","");
                    _dataNascimento = DateTime.ParseExact(StrDataNascimento.Substring(0, 2) + "/" + StrDataNascimento.Substring(2, 2) + "/" + StrDataNascimento.Substring(4, 4), "dd/MM/yyyy", new CultureInfo("pt-BR")); 
                
                }
                StrDataNascimento = _dataNascimento.ToString("dd/MM/yyyy");
                return _dataNascimento;
                
            }
            set
            {
                _dataNascimento = value;
                _dataNascimento = _dataNascimento.Date;
                if (string.IsNullOrWhiteSpace(StrDataNascimento))
                {
                    StrDataNascimento = _dataNascimento.ToString("dd/MM/yyyy"); 
                }
               
            }
        }

      
        public string Cpf
        {
            get
            {
                return _cpf;
            }
            set
            {
                _cpf = value;
                if (string.IsNullOrWhiteSpace(_cpf) == false)
                {
                    _cpf = _cpf.Replace(".", "");
                    _cpf = _cpf.Replace("-", "");
                    _cpf = _cpf.Replace(" ", "");
                }
            }
        }
        public int SexoId { get; set; }

        public string GetHash()
        {
            return "3b9774e4593aae6ada3c";
        }
    }
}