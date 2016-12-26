using AutoMapper;
using PadariaExpress.Aplicacao.Interfaces;
using PadariaExpress.Dominio.Modelo;
using PadariaExpress.Website.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace PadariaExpress.Website.Controllers
{
    public class PedidoController : ApiController
    {
        private readonly IPedidoAppServico _servico;

        public PedidoController(IPedidoAppServico servico)
        {
            _servico = servico;
        }

        [HttpPost]
        //[Authorize]
        [Route("api/pedido")]
        public Task<HttpResponseMessage> Registrar(RegistrarPedido model)
        {
            HttpResponseMessage response;

            try
            {
                var pedidoDominio = Mapper.Map<RegistrarPedido, Pedido>(model);
                var pedido = _servico.Registrar(pedidoDominio);
                model.PedidoId = pedido.PedidoId;
                model.DataPedido = pedido.DataPedido;
                response = Request.CreateResponse(HttpStatusCode.OK, new
                {
                    msg = "Pedido cadastrado com sucesso!",
                    d = ""
                });
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = ex.Message, InnerException = ex.InnerException });
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }

        [HttpGet]
        //[Authorize]
        [Route("api/pedidos/{PadariaId}")]
        public Task<HttpResponseMessage> ListarNaoCanceladosPorPadaria(int PadariaId)
        {
            HttpResponseMessage response;

            try
            {
                Padaria p = new Padaria();
                p.PadariaId = PadariaId;
                var pedidos = _servico.ListarNaoCanceladoPorPadaria(p);
                var pedidosViewModel = Mapper.Map<IEnumerable<Pedido>, IEnumerable<RegistrarPedido>>(pedidos);
                response = Request.CreateResponse(HttpStatusCode.OK, new
                {
                    msg = "OK",
                    d = pedidosViewModel
                });
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = ex.Message, InnerException = ex.InnerException });
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }

        [HttpGet]
        //[Authorize]
        [Route("api/pedidos/buscar/{PedidoId}")]
        public Task<HttpResponseMessage> BuscarPorId(int PedidoId)
        {
            HttpResponseMessage response;

            try
            {
                var pedido = _servico.BuscarPorId(PedidoId);
                var pedidosViewModel = Mapper.Map<Pedido, RegistrarPedido>(pedido);
                response = Request.CreateResponse(HttpStatusCode.OK, new
                {
                    msg = "OK",
                    d = pedidosViewModel
                });
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = ex.Message, InnerException = ex.InnerException });
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }

        [HttpGet]
        //[Authorize]
        [Route("api/pedidos/{PedidoId}/{Status}")]
        public Task<HttpResponseMessage> AtualizarStatusPedido(int PedidoId, StatusPedido Status)
        {
            HttpResponseMessage response;

            try
            {
                Pedido p = new Pedido();
                p.PedidoId = PedidoId;
                _servico.AtualizarStatus(p, Status);
                response = Request.CreateResponse(HttpStatusCode.OK, new
                {
                    msg = "Status alterado com sucesso."
                });
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = ex.Message, InnerException = ex.InnerException });
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }


        [HttpGet]
        //[Authorize]
        [Route("api/pedidoscliente/{UsuarioId}")]
        public Task<HttpResponseMessage> ListarPedidosDoCliente(int UsuarioId)
        {
            HttpResponseMessage response;

            try
            {
                var pedidosDominio = _servico.ListarPorCliente(new Usuario("consulta@consulta.com","consulta") { UsuarioId = UsuarioId });
                var pedidosViewModel = Mapper.Map<IEnumerable<Pedido>, IEnumerable<RegistrarPedido>>(pedidosDominio);
                response = Request.CreateResponse(HttpStatusCode.OK, new
                {
                    msg = "Status alterado com sucesso.",
                    d = pedidosViewModel
                });
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = ex.Message, InnerException = ex.InnerException });
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }
    }
}
