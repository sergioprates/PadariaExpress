using AutoMapper;
using PadariaExpress.Aplicacao.Interfaces;
using PadariaExpress.Dominio.Modelo;
using PadariaExpress.Validacao;
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
    public class FormaDePagamentoController : ApiController
    {
        private readonly IFormaDePagamentoAppServico _servico;

        public FormaDePagamentoController(IFormaDePagamentoAppServico servico)
        {
            _servico = servico;
        }

        [HttpPost]
        //[Authorize]
        [Route("api/formasdepagamento")]
        public Task<HttpResponseMessage> Registrar(RegistrarFormaDePagamento model)
        {
            HttpResponseMessage response;

            try
            {
                var formaDePagamentoCore = Mapper.Map<RegistrarFormaDePagamento, FormaDePagamento>(model);

                _servico.Registrar(formaDePagamentoCore);
                response = Request.CreateResponse(HttpStatusCode.OK, new
                {
                    msg = "Forma de Pagamento cadastrada com sucesso!",
                    d = ""
                });
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, new {Message = ex.Message,InnerException = ex.InnerException});
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }

        [HttpPost]
        //[Authorize]
        [Route("api/formasdepagamento/atualizar")]
        public Task<HttpResponseMessage> Atualizar(RegistrarFormaDePagamento model)
        {
            HttpResponseMessage response;

            try
            {
                var formaDePagamentoCore = Mapper.Map<RegistrarFormaDePagamento, FormaDePagamento>(model);

                _servico.Alterar(formaDePagamentoCore);
                response = Request.CreateResponse(HttpStatusCode.OK, new
                {
                    msg = "Forma de Pagamento alterada com sucesso!",
                    d = ""
                });
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, new {Message = ex.Message,InnerException = ex.InnerException});
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }

        [HttpGet]
        //[Authorize]
        [Route("api/formasdepagamento/buscar/{FormaDePagamentoId}")]
        public Task<HttpResponseMessage> Buscar(int FormaDePagamentoId)
        {
            HttpResponseMessage response;

            try
            {

                FormaDePagamento formaDePagamento =  _servico.BuscarPorId(FormaDePagamentoId);

                var formasDePagamentoViewModel = Mapper.Map<FormaDePagamento, FormaDePagamentoViewModel>(formaDePagamento);
                response = Request.CreateResponse(HttpStatusCode.OK, new
                {
                    msg = "OK",
                    d = formasDePagamentoViewModel
                });
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, new {Message = ex.Message,InnerException = ex.InnerException});
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }

        [HttpGet]
        [Route("api/formasdepagamento/{PadariaId}")]
        public Task<HttpResponseMessage> Listar(int PadariaId)
        {
            HttpResponseMessage response;

            try
            {
                Padaria p = new Padaria();
                p.PadariaId = PadariaId;
                IEnumerable<FormaDePagamento> formasDePagamento = _servico.Listar(p);

                var formasDePagamentoViewModel = Mapper.Map<IEnumerable<FormaDePagamento>, IEnumerable<FormaDePagamentoViewModel>>(formasDePagamento);
                response = Request.CreateResponse(HttpStatusCode.OK, new
                {
                    d = formasDePagamentoViewModel
                });
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }
    }
}
