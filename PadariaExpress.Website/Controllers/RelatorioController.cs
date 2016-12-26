using PadariaExpress.Dominio.Modelo;
using PadariaExpress.Relatorio.Interface;
using PadariaExpress.Relatorio.Modelo;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace PadariaExpress.Website.Controllers
{
    public class RelatorioController : ApiController
    {

        private readonly IRelatorioFaturamentoRepositorio _repositorio;

        public RelatorioController(IRelatorioFaturamentoRepositorio servico)
        {
            _repositorio = servico;
        }
        [HttpGet]
        [Route("api/relatoriofaturamento/googlecharts/linha/{PadariaId}/{DataInicial}/{DataFinal}")]
        public Task<HttpResponseMessage> BuscarRelatorioFaturamentoGoogleCharts(int PadariaId, int DataInicial, int DataFinal)
        {
            HttpResponseMessage response;
            try
            {
                DateTime dataInicial = DateTime.ParseExact(DataInicial.ToString(), "yyyyMMdd", new CultureInfo("pt-BR"));
                DateTime dataFinal = DateTime.ParseExact(DataFinal.ToString(), "yyyyMMdd", new CultureInfo("pt-BR")).AddHours(23).AddMinutes(59);

                dataInicial = dataInicial.ToUniversalTime();
                dataFinal = dataFinal.ToUniversalTime();
                Padaria p = new Padaria()
                {
                    PadariaId = PadariaId
                };

                List<object> data = new List<object>();
                data.Add(new[] { 
                "Data do Pedido", 
                "Cancelado", 
                "Pendente", 
                "Aceito", 
                "Saiu Entrega", 
                "Entregue",
                "Rejeitado"
            });


                List<RelatorioFaturamento> relatorios = new List<RelatorioFaturamento>(_repositorio.ListarRelatorioFaturamento(p, dataInicial, dataFinal));
                int qtd = 0;

                while (qtd < relatorios.Count)
                {
                    List<double> arr = new List<double>();
                    arr.Add(Convert.ToInt32(relatorios[qtd].Data.ToString("yyyyMMdd")));                    
                    arr.Add(relatorios[qtd].ValorTotal);
                    qtd++;
                    arr.Add(relatorios[qtd].ValorTotal);
                    qtd++;
                    arr.Add(relatorios[qtd].ValorTotal);
                    qtd++;
                    arr.Add(relatorios[qtd].ValorTotal);
                    qtd++;
                    arr.Add(relatorios[qtd].ValorTotal);
                    qtd++;
                    arr.Add(relatorios[qtd].ValorTotal);
                    qtd++;
                    data.Add(arr);
                }

                response = Request.CreateResponse(HttpStatusCode.OK, new
                {
                    msg = "OK",
                    d = data
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

        // GET: api/Relatorio/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Relatorio
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Relatorio/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Relatorio/5
        public void Delete(int id)
        {
        }
    }
}
