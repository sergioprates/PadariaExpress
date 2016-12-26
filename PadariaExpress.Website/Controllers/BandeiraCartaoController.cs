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
    public class BandeiraCartaoController : ApiController
    {
        private readonly IBandeiraCartaoAppServico _servico;

        public BandeiraCartaoController(IBandeiraCartaoAppServico servico)
        {
            _servico = servico;
        }

        [HttpGet]
        [Route("api/bandeiras")]
        public Task<HttpResponseMessage> ListarAtivos()
        {
            HttpResponseMessage response;

            try
            {
                var bandeiras = _servico.ListarAtivos();

                var bandeirasViewModel = Mapper.Map<IEnumerable<BandeiraCartao>, IEnumerable<BandeiraCartaoViewModel>>(bandeiras);
                response = Request.CreateResponse(HttpStatusCode.OK, new
                {
                    d = bandeirasViewModel
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
