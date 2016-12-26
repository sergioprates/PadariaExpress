using PadariaExpress.Dominio.Modelo;
using PadariaExpress.Relatorio.Interface;
using PadariaExpress.Relatorio.Modelo;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace PadariaExpress.Infra.Dados.Repositorio
{
    public class RelatorioFaturamentoRepositorio : IRelatorioFaturamentoRepositorio
    {
        public IEnumerable<RelatorioFaturamento> ListarRelatorioFaturamento(Padaria p, DateTime DataInicial, DateTime DataFinal)
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PadariaExpress"].ToString());
            List<RelatorioFaturamento> relatorios = new List<RelatorioFaturamento>();
            using(con)
            {
                con.Open();
               relatorios = con.Query<RelatorioFaturamento>(@"
                SELECT CAST([DataPedido] as date) as Data
                      ,[Status]
                      ,SUM([ValorTotal]) as ValorTotal
                  FROM [Pedido]
                  WHERE
                  PadariaId = @PadariaId
                  AND
                  CAST([DataPedido] as date) between
                  CAST(@DataInicial as date) AND CAST(@DataFinal as date) 
                  GROUP BY
                  [Pedido].Status,
                  CAST([DataPedido] as date)
                  ORDER BY CAST([DataPedido] as date)", new
                 {
                     PadariaId = p.PadariaId,
                     DataInicial = DataInicial,
                     DataFinal = DataFinal
                 }).ToList();
            }


            while (DataInicial.Date <= DataFinal.Date)
            {
                var itens= relatorios.Where(x => x.Data.Date == DataInicial.Date).ToList();

                if (itens.Count == 6)
                {
                    DataInicial = DataInicial.AddDays(1);
                    continue;
                }
                else
                {
                    for (int i = 0; i < 6; i++)
                    {
                        var item = itens.FirstOrDefault(x => x.Status == (StatusPedido)i);

                        if (item == null)
                        {
                            relatorios.Add(new RelatorioFaturamento()
                                {
                                    Data = DataInicial.Date,
                                    Status = (StatusPedido)i,
                                    ValorTotal=0
                                });
                        }
                    }

                    DataInicial = DataInicial.AddDays(1);
                }                
            }

            return relatorios.OrderBy(x=> x.Data).ThenBy(x=> x.Status).ToList();

        }
    }
}
