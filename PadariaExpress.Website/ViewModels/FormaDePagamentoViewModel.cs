using PadariaExpress.Dominio.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PadariaExpress.Website.ViewModels
{
    public class FormaDePagamentoViewModel
    {
        public int FormaDePagamentoId { get; set; }
        public bool Ativo { get; set; }
        public string Nome { get; set; }
        public TipoFormaDePagamento Tipo { get; set; }
        public BandeiraCartaoViewModel BandeiraCartao { get; set; }
        public int PadariaId { get; set; }
    }
}