using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PadariaExpress.Dominio.Interface.Repositorio;
using PadariaExpress.Dominio.Modelo;

namespace PadariaExpress.Infra.Dados.Repositorio
{
    public class FormaDePagamentoRepositorio : RepositorioBase<FormaDePagamento>, IFormaDePagamentoRepositorio
    {
        public IEnumerable<FormaDePagamento> ListarAtivos(Padaria padaria)
        {
            return Db.FormasDePagamento.Where(x => x.Padaria.PadariaId == padaria.PadariaId && x.Ativo == true).OrderBy(x=> x.Nome).ToArray();
        }

        public IEnumerable<FormaDePagamento> Listar(Padaria padaria)
        {
            return Db.FormasDePagamento.Where(x => x.Padaria.PadariaId == padaria.PadariaId).OrderBy(x => x.Nome).ToArray();
        }

        public override void Inserir(FormaDePagamento obj)
        {
            obj.Padaria = Db.Padarias.Find(obj.Padaria.PadariaId);

            if (obj.Tipo == TipoFormaDePagamento.Cartao)
            {
                obj.BandeiraCartao = Db.BandeirasCartao.Find(obj.BandeiraCartao.BandeiraCartaoId);
            }
           
            base.Inserir(obj);
        }

        public override void Alterar(FormaDePagamento obj)
        {
            obj.Padaria = Db.Padarias.Find(obj.Padaria.PadariaId);
            obj.BandeiraCartao = Db.BandeirasCartao.Find(obj.BandeiraCartao.BandeiraCartaoId);
            base.Alterar(obj);
        }
    }
}
