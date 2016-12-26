using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PadariaExpress.Website.ViewModels
{
    public class RegistrarProduto
    {
        public int ProdutoId { get; set; }
        public bool Ativo { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public double Preco { get; set; }
        public byte[] Foto { get; set; }
        public string Caminho { get; set; }
        public RegistrarCategoria Categoria { get; set; }
    }
}