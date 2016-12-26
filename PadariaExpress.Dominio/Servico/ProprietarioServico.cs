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
    public class ProprietarioServico : ServicoBase<Proprietario>, IProprietarioServico
    {
        private readonly IProprietarioRepositorio _proprietarioRepositorio;

        public ProprietarioServico(IProprietarioRepositorio proprietarioRepositorio)
            : base(proprietarioRepositorio)
        {
            _proprietarioRepositorio = proprietarioRepositorio;
        }
    }
}
