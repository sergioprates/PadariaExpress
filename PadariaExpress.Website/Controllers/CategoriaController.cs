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
    public class CategoriaController : ApiController
    {
         private readonly ICategoriaAppServico _servico;

         public CategoriaController(ICategoriaAppServico servico)
        {
            _servico = servico;
        }

        [HttpPost]
        //[Authorize]
        [Route("api/categorias")]
        public Task<HttpResponseMessage> Registrar(RegistrarCategoria model)
        {
            HttpResponseMessage response;

            try
            {
                var categoriaCore = Mapper.Map<RegistrarCategoria, Categoria>(model);

                _servico.Registrar(categoriaCore);
                response = Request.CreateResponse(HttpStatusCode.OK, new
                {
                    msg = "Categoria cadastrada com sucesso!",
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
        [Route("api/categorias/atualizar")]
        public Task<HttpResponseMessage> Atualizar(RegistrarCategoria model)
        {
            HttpResponseMessage response;

            try
            {
                var categoriaCore = Mapper.Map<RegistrarCategoria, Categoria>(model);

                _servico.Alterar(categoriaCore);
                response = Request.CreateResponse(HttpStatusCode.OK, new
                {
                    msg = "Categoria alterada com sucesso!",
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
        [Route("api/categorias/buscar/{CategoriaId}")]
        public Task<HttpResponseMessage> Buscar(int CategoriaId)
        {
            HttpResponseMessage response;

            try
            {

                Categoria formaDePagamento = _servico.BuscarPorId(CategoriaId);

                var categoriasViewModel = Mapper.Map<Categoria, RegistrarCategoria>(formaDePagamento);
                response = Request.CreateResponse(HttpStatusCode.OK, new
                {
                    msg = "OK",
                    d = categoriasViewModel
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
        [Route("api/categorias/{PadariaId}")]
        public Task<HttpResponseMessage> Listar(int PadariaId)
        {
            HttpResponseMessage response;

            try
            {
                Padaria p = new Padaria();
                p.PadariaId = PadariaId;
                IEnumerable<Categoria> categorias = _servico.Listar(p);

                var categoriasViewModel = Mapper.Map<IEnumerable<Categoria>, IEnumerable<RegistrarCategoria>>(categorias);
                response = Request.CreateResponse(HttpStatusCode.OK, new
                {
                    d = categoriasViewModel
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
