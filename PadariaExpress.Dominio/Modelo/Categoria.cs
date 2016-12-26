using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PadariaExpress.Dominio.Modelo
{
    public class Categoria
    {
        public int CategoriaId { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataAlteracao { get; set; }
        public bool Ativo { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public Padaria Padaria { get; set; }

        public ICollection<Produto> Produtos { get; set; }
    }
}
