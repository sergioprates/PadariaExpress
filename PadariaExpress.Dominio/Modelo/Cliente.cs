using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PadariaExpress.Dominio.Modelo
{
    public class Cliente : Usuario
    {
        protected Cliente()
        {
            TipoUsuario = Modelo.TipoUsuario.Cliente;
        }

        public Cliente(string email, string nome)
            : base(email, nome)
        {
            TipoUsuario = Modelo.TipoUsuario.Cliente;
        }

        public ICollection<Pedido> Pedidos { get; set; }

        public string RegistroAndroid { get; set; }
    }
}
