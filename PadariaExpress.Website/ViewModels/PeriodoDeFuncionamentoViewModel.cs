using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PadariaExpress.Website.ViewModels
{
    public class PeriodoDeFuncionamentoViewModel
    {
        private TimeSpan _horaFechamento;
        private TimeSpan _horaAbertura;
        public int PeriodoFuncionamentoId { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataAlteracao { get; set; }
        public TimeSpan HoraAbertura
        {
            get
            {
                if (string.IsNullOrWhiteSpace(StrHoraAbertura) == false)
                {
                    _horaAbertura = new TimeSpan(Convert.ToInt32(StrHoraAbertura.Split(':')[0]), Convert.ToInt32(StrHoraAbertura.Split(':')[1]), 0);
                }
                {
                    return _horaAbertura;
                }
            }
            set
            {
                _horaAbertura = value;
            }
        }
        public string StrHoraAbertura { get; set; }
        public TimeSpan HoraFechamento
        {
            get
            {
                if (string.IsNullOrWhiteSpace(StrHoraFechamento) == false)
                {
                    _horaFechamento = new TimeSpan(Convert.ToInt32(StrHoraFechamento.Split(':')[0]), Convert.ToInt32(StrHoraFechamento.Split(':')[1]), 0);
                }
                {
                    return _horaFechamento;
                }
            }
            set
            {
                _horaFechamento = value;
            }
        }
        public string StrHoraFechamento { get; set; }
        public DayOfWeek DiaDaSemana { get; set; }
    }
}