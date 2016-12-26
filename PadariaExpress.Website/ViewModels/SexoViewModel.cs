using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PadariaExpress.Website.ViewModels
{
    public class SexoViewModel
    {
        public int SexoId { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataAlteracao { get; set; }
        public bool Ativo { get; set; }
        public string Nome { get; set; }
    }
}