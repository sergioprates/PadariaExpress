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
    public class SexoServico : ServicoBase<Sexo>, ISexoServico
    {
        private readonly ISexoRepositorio _sexoRepositorio;

        public SexoServico(ISexoRepositorio sexoRepositorio)
            : base(sexoRepositorio)
        {
            _sexoRepositorio = sexoRepositorio;
        }

        public IEnumerable<Sexo> ListarAtivos()
        {
            using (_sexoRepositorio)
            {
                return _sexoRepositorio.ListarAtivos();
            }
        }
    }
}
