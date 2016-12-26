using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PadariaExpress.Dominio.Modelo
{
    public class ConviteFuncionario
    {
        public int ConviteFuncionarioId { get; set; }
        public string Hash { get; set; }
        public string  Email { get; set; }
        public bool Utilizado { get; set; }
        public int PadariaId { get; set; }
    }
}
