using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PadariaExpress.Dominio.Interface.Repositorio;
using PadariaExpress.Dominio.Modelo;

namespace PadariaExpress.Infra.Dados.Repositorio
{
    public class SexoRepositorio : RepositorioBase<Sexo>, ISexoRepositorio
    {
        public IEnumerable<Sexo> ListarAtivos()
        {
            return Db.Sexos.Where(x => x.Ativo == true).ToList();
        }
    }
}
