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
using System.IO;
using System.Web;

namespace PadariaExpress.Website.Controllers
{
    public class ProdutoController : ApiController
    {
        private readonly IProdutoAppServico _servico;

        public ProdutoController(IProdutoAppServico servico)
        {
            _servico = servico;
        }

        [HttpPost]
        //[Authorize]
        [Route("api/produtos")]
        public Task<HttpResponseMessage> Registrar(RegistrarProduto model)
        {
            HttpResponseMessage response;

            try
            {
                if (string.IsNullOrWhiteSpace(model.Caminho) == false)
                {
                    model.Foto = File.ReadAllBytes(HttpContext.Current.Server.MapPath("~/" + model.Caminho));
                }


                var produtoCore = Mapper.Map<RegistrarProduto, Produto>(model);

                _servico.Registrar(produtoCore);
                response = Request.CreateResponse(HttpStatusCode.OK, new
                {
                    msg = "Produto cadastrado com sucesso!",
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
        [Route("api/produtos/atualizar")]
        public Task<HttpResponseMessage> Atualizar(RegistrarProduto model)
        {
            HttpResponseMessage response;

            try
            {
                Produto produtoCore = null;
                if (string.IsNullOrWhiteSpace(model.Caminho) == false)
                {
                    if (model.Caminho.IndexOf("FotosProdutos/", StringComparison.InvariantCultureIgnoreCase) > -1)
                    {
                        model.Foto = File.ReadAllBytes(HttpContext.Current.Server.MapPath("~/" + model.Caminho));
                    }
                    else
                    {
                        WebRequest request = WebRequest.Create("http://" + System.Web.HttpContext.Current.Request.Url.Authority + "/" + model.Caminho);
                        using (MemoryStream ms = new MemoryStream())
                        {
                            request.GetResponse().GetResponseStream().CopyTo(ms);
                            model.Foto = ms.ToArray();
                        }
                    }
                }
                produtoCore = Mapper.Map<RegistrarProduto, Produto>(model);


                _servico.Alterar(produtoCore);

                DeletaArquivo(model.Caminho);

                response = Request.CreateResponse(HttpStatusCode.OK, new
                {
                    msg = "Produto alterado com sucesso!",
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
                    if (caminho.IndexOf("FotosProdutos/", StringComparison.InvariantCultureIgnoreCase) > -1)
                    {
                        if (File.Exists(HttpContext.Current.Server.MapPath("~/" +caminho)))
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
        [Route("api/produtos/buscar/{ProdutoId}")]
        public Task<HttpResponseMessage> Buscar(int ProdutoId)
        {
            HttpResponseMessage response;

            try
            {

                Produto produto = _servico.BuscarPorId(ProdutoId);

                var produtoViewModel = Mapper.Map<Produto, RegistrarProduto>(produto);
                response = Request.CreateResponse(HttpStatusCode.OK, new
                {
                    msg = "OK",
                    d = produtoViewModel
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
        [Route("api/produtos/{PadariaId}")]
        public Task<HttpResponseMessage> Listar(int PadariaId)
        {
            HttpResponseMessage response;

            try
            {
                Padaria p = new Padaria();
                p.PadariaId = PadariaId;
                IEnumerable<Produto> produtos = _servico.Listar(p);

                var produtosViewModel = Mapper.Map<IEnumerable<Produto>, IEnumerable<RegistrarProduto>>(produtos);
                response = Request.CreateResponse(HttpStatusCode.OK, new
                {
                    d = produtosViewModel
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

        [HttpGet]
        [Route("api/produtos/ativos/{PadariaId}")]
        public Task<HttpResponseMessage> ListarAtivos(int PadariaId)
        {
            HttpResponseMessage response;

            try
            {
                Padaria p = new Padaria();
                p.PadariaId = PadariaId;
                IEnumerable<Produto> produtos = _servico.ListarAtivos(p);

                var produtosViewModel = Mapper.Map<IEnumerable<Produto>, IEnumerable<RegistrarProduto>>(produtos);
                response = Request.CreateResponse(HttpStatusCode.OK, new
                {
                    d = produtosViewModel
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
