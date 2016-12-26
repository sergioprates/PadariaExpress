using System;
using System.Collections.Generic;
using System.Web;

namespace PadariaExpress.Website.ViewModels
{
    public class RegistrarProprietario
    {

        private string _cpf;
        private DateTime _dataNascimento;

        public int UsuarioId { get; set; }
        public bool Ativo { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        public string ConfirmaSenha { get; set; }
        public string Email { get; set; }
        public SexoViewModel Sexo { get; set; }
        public DateTime DataNascimento {
            get { return _dataNascimento; }
            set 
            { 
                _dataNascimento = value;
                _dataNascimento = Convert.ToDateTime(_dataNascimento.ToString("dd/MM/yyyy"));
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
    }
}