using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PadariaExpress.Dominio.Modelo
{
    public class Produto
    {
        public int ProdutoId { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataAlteracao { get; set; }
        public bool Ativo { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public double Preco { get; set; }
        public byte[] Foto { get; set; }
        public virtual Categoria Categoria { get; set; }

        public ICollection<ItemPedido> ItensPedido { get; set; }
    }
}
