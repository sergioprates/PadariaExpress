using PadariaExpress.Dominio.Interface.Repositorio;
using PadariaExpress.Dominio.Interface.Servico;
using PadariaExpress.Dominio.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PadariaExpress.Dominio.Servico
{
    public class BandeiraCartaoServico : ServicoBase<BandeiraCartao>, IBandeiraCartaoServico
    {
        private readonly IBandeiraCartaoRepositorio _bandeiraCartaoRepositorio;

        public BandeiraCartaoServico(IBandeiraCartaoRepositorio bandeiraCartaoRepositorio)
            : base(bandeiraCartaoRepositorio)
        {
            _bandeiraCartaoRepositorio = bandeiraCartaoRepositorio;
        }

        public IEnumerable<BandeiraCartao> ListarAtivos()
        {
            using (_bandeiraCartaoRepositorio)
            {
                return _bandeiraCartaoRepositorio.ListarAtivos();
            }
        }
    }
}
