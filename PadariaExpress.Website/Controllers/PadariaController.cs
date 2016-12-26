using AutoMapper;
using PadariaExpress.Aplicacao.Interfaces;
using PadariaExpress.Dominio.Modelo;
using PadariaExpress.Website.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace PadariaExpress.Website.Controllers
{
    public class PadariaController : ApiController
    {
        private readonly IPadariaAppServico _servico;

        public PadariaController(IPadariaAppServico servico)
        {
            _servico = servico;
        }

        [HttpPost]
        //[Authorize]
        [Route("api/padaria")]
        public Task<HttpResponseMessage> Registrar(RegistrarPadaria model)
        {
            HttpResponseMessage response;

            try
            {
                var padaria = _servico.Registrar(model.ToPadaria(), model.Proprietario);
                response = Request.CreateResponse(HttpStatusCode.OK, new
                {
                    msg = "Padaria cadastrada com sucesso!",
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
        [Route("api/padaria/{UsuarioId}")]
        public Task<HttpResponseMessage> ListarPadariasPorProprietario(int UsuarioId)
        {
            HttpResponseMessage response;

            try
            {
                Proprietario usuario = new Proprietario();
                usuario.UsuarioId = UsuarioId;
                IEnumerable<Padaria> padarias = _servico.ListarPorProprietario(usuario);
                var padariasViewModel = Mapper.Map<IEnumerable<Padaria>, IEnumerable<PadariaViewModel>>(padarias);
                response = Request.CreateResponse(HttpStatusCode.OK, new
                {
                    msg = "Padaria cadastrada com sucesso!",
                    d = padariasViewModel
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
        [Route("api/padaria/atualizar")]
        public Task<HttpResponseMessage> Atualizar(RegistrarPadaria model)
        {
            HttpResponseMessage response;

            try
            {
                Padaria padariaCore = null;
                if (string.IsNullOrWhiteSpace(model.Caminho) == false)
                {
                    if (model.Caminho.IndexOf("FotosPadarias/", StringComparison.InvariantCultureIgnoreCase) > -1)
                    {
                        model.FotoPrincipal = File.ReadAllBytes(HttpContext.Current.Server.MapPath("~/" + model.Caminho));
                    }
                    else
                    {
                        WebRequest request = WebRequest.Create("http://" + System.Web.HttpContext.Current.Request.Url.Authority + "/" + model.Caminho);
                        using (MemoryStream ms = new MemoryStream())
                        {
                            request.GetResponse().GetResponseStream().CopyTo(ms);
                            model.FotoPrincipal = ms.ToArray();
                        }
                    }
                }
                padariaCore = Mapper.Map<RegistrarPadaria, Padaria>(model);


                _servico.Alterar(padariaCore);

                DeletaArquivo(model.Caminho);

                response = Request.CreateResponse(HttpStatusCode.OK, new
                {
                    msg = "Padaria alterada com sucesso!",
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

        private void DeletaArquivo(string caminho)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(caminho) == false)
                {
                    if (caminho.IndexOf("FotosPadarias/", StringComparison.InvariantCultureIgnoreCase) > -1)
                    {
                        if (File.Exists(HttpContext.Current.Server.MapPath("~/" + caminho)))
                        {
                            File.Delete(HttpContext.Current.Server.MapPath("~/" + caminho));
                        }
                    }
                }

            }
            catch (Exception erro)
            {

            }
        }

        [HttpGet]
        //[Authorize]
        [Route("api/padaria/buscar/{PadariaId}")]
        public Task<HttpResponseMessage> Buscar(int PadariaId)
        {
            HttpResponseMessage response;

            try
            {

                Padaria p = _servico.BuscarPorId(PadariaId);

                var padariaViewModel = Mapper.Map<Padaria, PadariaViewModel>(p);
                response = Request.CreateResponse(HttpStatusCode.OK, new
                {
                    msg = "OK",
                    d = padariaViewModel
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
        [Route("api/padaria/listar/nome")]
        public Task<HttpResponseMessage> ListarPorNome([FromBody]string nome)
        {
            HttpResponseMessage response;

            try
            {
                var padarias = _servico.ListarPorNome(nome);
                var padariasViewModel = Mapper.Map<IEnumerable<Padaria>, IEnumerable<PadariaViewModel>>(padarias);
                response = Request.CreateResponse(HttpStatusCode.OK, new
                {
                    msg = "OK",
                    d = padariasViewModel
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
        [Route("api/padaria/listar/proximidade")]
        public Task<HttpResponseMessage> ListarPorProximidade([FromUri]double lat, double lng)
        {
            HttpResponseMessage response;

            try
            {
                var padarias = _servico.ListarPorProximidade(lat, lng, Convert.ToInt32(ConfigurationManager.AppSettings["PadariasTop"]));
                var padariasViewModel = Mapper.Map<IEnumerable<Padaria>, IEnumerable<PadariaViewModel>>(padarias);
                response = Request.CreateResponse(HttpStatusCode.OK, new
                {
                    msg = "OK",
                    d = padariasViewModel
                });
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, new {
                    Message = ex.Message,
                    InnerException = ex.InnerException
                });
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }


    }
}
