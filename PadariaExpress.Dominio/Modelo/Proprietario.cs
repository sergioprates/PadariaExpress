using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PadariaExpress.Dominio.Modelo
{
    public class Proprietario : Usuario
    {
        public Proprietario()
        {
            TipoUsuario = Modelo.TipoUsuario.Proprietario;
        }

        public Proprietario(string email, string nome)
            : base(email, nome)
        {
            TipoUsuario = Modelo.TipoUsuario.Proprietario;
        }
        public virtual ICollection<Padaria> Padarias { get; set; }
    }
}
