using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PadariaExpress.Dominio.Modelo
{
    public enum StatusPedido
    {
        Cancelado=0,
        Pendente=1,
        Aceito=2,
        Entregando=3,
        Entregue=4,
        Rejeitado=5
    }
}
