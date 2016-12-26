using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PadariaExpress.Dominio.Modelo
{
    public class PeriodoFuncionamento
    {
        public int PeriodoFuncionamentoId { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataAlteracao { get; set; }
        public TimeSpan HoraAbertura { get; set; }
        public TimeSpan HoraFechamento { get; set; }
        public DayOfWeek DiaDaSemana { get; set; }

        public Padaria Padaria { get; set; }
    }
}
