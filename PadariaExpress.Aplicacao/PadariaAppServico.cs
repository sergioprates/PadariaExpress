using PadariaExpress.Aplicacao.Interfaces;
using PadariaExpress.Dominio.Interface.Servico;
using PadariaExpress.Dominio.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PadariaExpress.Aplicacao
{
    public class PadariaAppServico : AppServicoBase<Padaria>, IPadariaAppServico
    {
        private readonly IPadariaServico _servico;

        public PadariaAppServico(IPadariaServico servico)
            : base(servico)
        {
            _servico = servico;
        }

        public Padaria Registrar(Padaria padaria, Proprietario proprietario)
        {
            return _servico.Registrar(padaria, proprietario);
        }

        public IEnumerable<Padaria> ListarPorProprietario(Proprietario proprietario)
        {
            return _servico.ListarPorProprietario(proprietario);
        }


        public IEnumerable<Padaria> ListarPorProximidade(double latitude, double longitude, int top)
        {
            return _servico.ListarPorProximidade(latitude, longitude, top);
        }

        public IEnumerable<Padaria> ListarPorNome(string nome)
        {
            return _servico.ListarPorNome(nome);
        }
    }
}
