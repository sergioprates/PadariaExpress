using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PadariaExpress.Dominio.Interface.Repositorio;
using PadariaExpress.Dominio.Modelo;

namespace PadariaExpress.Infra.Dados.Repositorio
{
    public class PedidoRepositorio : RepositorioBase<Pedido>, IPedidoRepositorio
    {
        public override void Inserir(Pedido obj)
        {
            obj.Cliente = (Cliente)Db.Usuarios.Find(obj.Cliente.UsuarioId);            
            obj.Padaria = Db.Padarias.Find(obj.Padaria.PadariaId);
            obj.FormaDePagamento = Db.FormasDePagamento.Find(obj.FormaDePagamento.FormaDePagamentoId);

            List<ItemPedido> items = new List<ItemPedido>(obj.Itens);
            obj.Itens = new List<ItemPedido>();

            for (int i = 0; i < items.Count; i++)
            {
                items[i].Produto = Db.Produtos.Find(items[i].Produto.ProdutoId);
                items[i].PrecoUnitario = items[i].Produto.Preco;
                obj.Itens.Add(items[i]);
            }

            base.Inserir(obj);
        }

        public override void Alterar(Pedido obj)
        {
            obj.Cliente = (Cliente)Db.Usuarios.Find(obj.Cliente.UsuarioId);
            obj.Padaria = Db.Padarias.Find(obj.Padaria.PadariaId);
            obj.FormaDePagamento = Db.FormasDePagamento.Find(obj.FormaDePagamento.FormaDePagamentoId);


            base.Alterar(obj);
        }


        public IEnumerable<Pedido> ListarNaoCanceladoPorPadaria(Padaria p)
        {
            return Db.Pedidos
                .Where(x => x.Padaria.PadariaId == p.PadariaId 
                    && x.Status != StatusPedido.Cancelado 
                    && x.Status != StatusPedido.Entregue)
                    .OrderBy(x=> x.DataPedido).ToList();
        }


        public void AtualizarStatus(Pedido p, StatusPedido Status)
        {
            p = Db.Pedidos.Find(p.PedidoId);
            p.Status = Status;
            base.Alterar(p);
        }


        public IEnumerable<Pedido> ListarPorCliente(Usuario u)
        {
            return Db.Pedidos
                .Where(x => x.Cliente.UsuarioId == u.UsuarioId)
                .OrderByDescending(x => x.DataPedido).ToList();
        }
    }
}
