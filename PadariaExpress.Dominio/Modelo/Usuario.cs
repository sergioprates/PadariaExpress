using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PadariaExpress.Validacao;

namespace PadariaExpress.Dominio.Modelo
{
    public class Usuario
    {
        protected Usuario()
        {
            
        }

        public Usuario(string email, string nome)
        {
            AssertionConcern.AssertArgumentNotEmpty(nome, "Nome inválido.");
            EmailAssertionConcern.AssertIsValid(email);
            Nome = nome;
            Email = email;
        }

        private string _cpf;
        private string _telefone;

        public int UsuarioId { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataAlteracao { get; set; }
        public bool Ativo { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }
        public string Telefone
        {
            get
            {
                return _telefone;
            }
            set
            {
                _telefone = value;
                if (string.IsNullOrWhiteSpace(_telefone) == false)
                {
                    _telefone = _telefone.Replace("-", "");
                    _telefone = _telefone.Replace("(", "");
                    _telefone = _telefone.Replace(" ", "");
                    _telefone = _telefone.Trim();
                }   
            }
        }
        public DateTime DataNascimento { get; set; }
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
        public virtual Sexo Sexo { get; set; }
        public virtual ICollection<EnderecoUsuario> EnderecosUsuario { get; set; }

        public TipoUsuario TipoUsuario { get; set; }

        public void AlterarEmail(string email)
        {
            EmailAssertionConcern.AssertIsValid(email);
            Email = email;
        }

        public void AlterarCpf(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf) == false)
            {
                if (IsCpf(cpf) == false)
                {
                    throw new Exception("CPF inválido.");   
                }
            }
        }

        public void AlterarSenha(string senha, string confirmacaoSenha)
        {
            AssertionConcern.AssertArgumentNotEmpty(senha, "Senha inválida.");
            AssertionConcern.AssertArgumentEquals(senha, confirmacaoSenha, "Senha de confirmação não confere com a senha.");

            Senha = PasswordAssertionConcern.Encrypt(senha);
        }

        private static bool IsCpf(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }
    }
}
