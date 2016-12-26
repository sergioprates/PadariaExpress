using PadariaExpress.Dominio.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PadariaExpress.Dominio.Interface.Repositorio
{
    public interface ISexoRepositorio : IRepositorioBase<Sexo>
    {
        IEnumerable<Sexo> ListarAtivos();
    }
}
