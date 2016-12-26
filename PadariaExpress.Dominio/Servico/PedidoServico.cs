using PadariaExpress.Dominio.Interface.Repositorio;
using PadariaExpress.Dominio.Interface.Servico;
using PadariaExpress.Dominio.Modelo;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util.Localizacao;
using Util.Localizacao.Servicos;

namespace PadariaExpress.Dominio.Servico
{
    public class PedidoServico : ServicoBase<Pedido>, IPedidoServico
    {
        private readonly IPedidoRepositorio _pedidoRepositorio;
        private readonly IUsuarioServico _usuarioServico;
        private readonly IPadariaRepositorio _padariaRepositorio;

        public PedidoServico(IPedidoRepositorio pedidoRepositorio,
            IUsuarioServico usuarioServico,
            IPadariaRepositorio padariaRepositorio)
            : base(pedidoRepositorio)
        {
            _pedidoRepositorio = pedidoRepositorio;
            _usuarioServico = usuarioServico;
            _padariaRepositorio = padariaRepositorio;
        }

        public Pedido Registrar(Pedido pedido)
        {
            _usuarioServico.Autenticar(pedido.Cliente.Email, pedido.Cliente.Senha);
            pedido.DataPedido = DateTime.UtcNow;
            pedido.Status = StatusPedido.Pendente;
            pedido = CarregaGeolozacalizacao(pedido);

            var padaria = _padariaRepositorio.BuscarPorId(pedido.Padaria.PadariaId);

            GeoCoordinate geo = new GeoCoordinate(padaria.Latitude, padaria.Longitude);

            double distancia = geo.GetDistanceTo(new GeoCoordinate(pedido.Endereco.Latitude, pedido.Endereco.Longitude));

            if (distancia > padaria.DistanciaEntrega)
            {
                throw new Exception("Esta padaria não atende neste endereço. Por favor procure uma padaria mais próxima.");
            }

            _pedidoRepositorio.Inserir(pedido);
            return pedido;
        }

        private Pedido CarregaGeolozacalizacao(Pedido pedido)
        {
            StringBuilder url = new StringBuilder();

            url.Append(pedido.Endereco.Logradouro);
            url.Append(", ");
            url.Append(pedido.Endereco.Numero);
            url.Append(" - ");
            url.Append(pedido.Endereco.Bairro);
            url.Append(", ");
            url.Append(pedido.Endereco.Cidade);
            url.Append(" - ");
            url.Append(pedido.Endereco.Estado);
            url.Append(", ");
            url.Append(pedido.Endereco.CEP);

            Geolocalizacao geo = GeolocalizacaoGoogleMapsServico.Geolocalizar(url.ToString());
            pedido.Endereco.Latitude = geo.Latitude;
            pedido.Endereco.Longitude = geo.Longitude;

            return pedido;
        }

        public IEnumerable<Pedido> ListarNaoCanceladoPorPadaria(Padaria p)
        {
            return _pedidoRepositorio.ListarNaoCanceladoPorPadaria(p);
        }


        public void AtualizarStatus(Pedido p, StatusPedido Status)
        {
            p = _pedidoRepositorio.BuscarPorId(p.PedidoId);

            if (Status == StatusPedido.Cancelado)
            {
                if (p.Status == StatusPedido.Pendente)
                {
                    p.Status = Status;
                    _pedidoRepositorio.AtualizarStatus(p, Status);
                }
                else
                {
                    throw new Exception("Não é possível atualizar este pedido com este status.");
                }
            }
            else
            {

                if (((int)p.Status) >= (int)Status)
                {
                    throw new Exception("Não é possível atualizar este pedido com este status.");
                }
                else
                {
                    p.Status = Status;
                    _pedidoRepositorio.AtualizarStatus(p, Status);
                }
            }           
        }


        public IEnumerable<Pedido> ListarPorCliente(Usuario u)
        {
            return _pedidoRepositorio.ListarPorCliente(u);
        }
    }
}
