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
    public class SexoController : ApiController
    {

        private readonly ISexoAppServico _servico;

        public SexoController(ISexoAppServico servico)
        {
            _servico = servico;
        }

        [HttpGet]
        [Route("api/sexos")]
        public Task<HttpResponseMessage> ListarAtivos()
        {
            HttpResponseMessage response;

            try
            {
                var sexos = _servico.ListarAtivos();

                var sexosViewModel = Mapper.Map<IEnumerable<Sexo>, IEnumerable<SexoViewModel>>(sexos);
                response = Request.CreateResponse(HttpStatusCode.OK, new
                {
                    d = sexosViewModel
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

        // GET: api/Sexo/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Sexo
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Sexo/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Sexo/5
        public void Delete(int id)
        {
        }
    }
}
