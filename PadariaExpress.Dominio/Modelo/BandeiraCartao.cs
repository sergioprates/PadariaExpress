using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PadariaExpress.Dominio.Modelo
{
    public class BandeiraCartao
    {
        public int BandeiraCartaoId { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataAlteracao { get; set; }
        public bool Ativo { get; set; }
        public string Nome { get; set; }

        public ICollection<FormaDePagamento> FormasDePagamentos { get; set; }
    }
}
