using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PadariaExpress.Dominio.Interface.Repositorio;
using PadariaExpress.Dominio.Modelo;

namespace PadariaExpress.Infra.Dados.Repositorio
{
    public class BandeiraCartaoRepositorio : RepositorioBase<BandeiraCartao>, IBandeiraCartaoRepositorio
    {
        public IEnumerable<BandeiraCartao> ListarAtivos()
        {
            return Db.BandeirasCartao.Where(x => x.Ativo == true).OrderBy(x=> x.Nome).ToArray();
        }
    }
}
