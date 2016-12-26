using PadariaExpress.Dominio.Interface.Repositorio;
using PadariaExpress.Dominio.Interface.Servico;
using PadariaExpress.Dominio.Modelo;
using Util.Localizacao;
using PadariaExpress.Validacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util.Localizacao.Servicos;

namespace PadariaExpress.Dominio.Servico
{
    public class PadariaServico : ServicoBase<Padaria>, IPadariaServico
    {
        private readonly IPadariaRepositorio _padariaRepositorio;

        public PadariaServico(IPadariaRepositorio padariaRepositorio)
            : base(padariaRepositorio)
        {
            _padariaRepositorio = padariaRepositorio;
        }

        public Padaria Registrar(Padaria padaria, Proprietario proprietario)
        {
            padaria = CarregaGeolozacalizacao(padaria);

            using (_padariaRepositorio)
            {
                if (_padariaRepositorio.ExistePorCNPJ(padaria.CNPJ) == true)
                {
                    throw new Exception("CNPJ já cadastrado!");
                }

                if (padaria.Proprietarios == null)
                {
                    padaria.Proprietarios = new List<Proprietario>();
                }

                padaria.Proprietarios.Add(proprietario);
                _padariaRepositorio.Inserir(padaria);

                padaria = _padariaRepositorio.BuscarPorCNPJ(padaria.CNPJ);
            }

            return padaria;
        }
        public override void Alterar(Padaria obj)
        {
            obj = CarregaGeolozacalizacao(obj);
            base.Alterar(obj);
        }

        public IEnumerable<Padaria> ListarPorProprietario(Proprietario proprietario)
        {
            return _padariaRepositorio.ListarPorProprietario(proprietario);
        }

        private Padaria CarregaGeolozacalizacao(Padaria padaria)
        {
            StringBuilder url = new StringBuilder();

            url.Append(padaria.Logradouro);
            url.Append(", ");
            url.Append(padaria.Numero);
            url.Append(" - ");
            url.Append(padaria.Bairro);
            url.Append(", ");
            url.Append(padaria.Cidade);
            url.Append(" - ");
            url.Append(padaria.Estado);
            url.Append(", ");
            url.Append(padaria.CEP);

            Geolocalizacao geo = GeolocalizacaoGoogleMapsServico.Geolocalizar(url.ToString());
            padaria.Latitude = geo.Latitude;
            padaria.Longitude = geo.Longitude;

            return padaria;
        }


        public IEnumerable<Padaria> ListarPorProximidade(double latitude, double longitude, int top)
        {
            return _padariaRepositorio.ListarPorProximidade(latitude, longitude, top);
        }

        public IEnumerable<Padaria> ListarPorNome(string nome)
        {
            return _padariaRepositorio.ListarPorNome(nome);
        }
    }
}
