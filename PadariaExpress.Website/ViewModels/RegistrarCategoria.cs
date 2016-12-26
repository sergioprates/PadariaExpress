using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PadariaExpress.Website.ViewModels
{
    public class RegistrarCategoria
    {
        public int CategoriaId { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataAlteracao { get; set; }
        public bool Ativo { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public PadariaViewModel Padaria { get; set; }
    }
}