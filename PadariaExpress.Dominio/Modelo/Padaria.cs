using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PadariaExpress.Dominio.Modelo
{
    public class Padaria
    {
        public int PadariaId { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataAlteracao { get; set; }
        public bool Ativo { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string CNPJ { get; set; }
        public byte[] FotoPrincipal { get; set; }
        public string Descricao { get; set; }
        public string CEP { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string Estado { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double DistanciaEntrega { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public double? Distancia { get; set; }
        public double ValorFrete { get; set; }

        public virtual ICollection<Funcionario> Funcionarios { get; set; }
        public virtual List<Proprietario> Proprietarios { get; set; }

        public virtual IList<PeriodoFuncionamento> PeriodosDeFuncionamento { get; set; }
        public virtual ICollection<FormaDePagamento> FormasDePagamento { get; set; }
        public ICollection<Categoria> Categorias { get; set; }
        public ICollection<Pedido> Pedidos { get; set; }
    }
}
