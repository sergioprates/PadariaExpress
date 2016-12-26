using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PadariaExpress.Dominio.Modelo
{
    public class Funcionario : Usuario
    {
        protected Funcionario()
        {
            TipoUsuario = Modelo.TipoUsuario.Funcionario;
        }

        public Funcionario(string email, string nome)
            : base(email, nome)
        {
            TipoUsuario = Modelo.TipoUsuario.Funcionario;
        }
        public virtual IList<Padaria> Padarias { get; set; }
    }
}
