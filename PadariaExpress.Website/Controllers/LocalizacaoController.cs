using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Util.Localizacao.Modelo;
using Util.Localizacao.Servicos;

namespace PadariaExpress.Website.Controllers
{
    public class LocalizacaoController : ApiController
    {
        // GET: api/Localizacao
        [HttpGet]
        [Route("api/localizacao/{cep}")]
        public Task<HttpResponseMessage> Get(string cep)
        {
            HttpResponseMessage response;

            try
            {
                var endereco = new EnderecoAvisoBrasilServico().BuscarCep(cep);
                response = Request.CreateResponse(HttpStatusCode.OK, new
                {
                    d = endereco
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

        // GET: api/Localizacao/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Localizacao
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Localizacao/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Localizacao/5
        public void Delete(int id)
        {
        }
    }
}
