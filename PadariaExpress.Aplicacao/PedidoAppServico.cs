using Newtonsoft.Json;
using PadariaExpress.Aplicacao.Interfaces;
using PadariaExpress.Dominio.Interface.Servico;
using PadariaExpress.Dominio.Modelo;
using PushNotification.Dominio.Entities;
using PushNotification.Dominio.Facade;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PadariaExpress.Aplicacao
{
    public class PedidoAppServico : AppServicoBase<Pedido>, IPedidoAppServico
    {
        private readonly IPedidoServico _servico;

        public PedidoAppServico(IPedidoServico servico)
            : base(servico)
        {
            _servico = servico;
        }        

        public Pedido Registrar(Pedido pedido)
        {
            return _servico.Registrar(pedido);
        }


        public IEnumerable<Pedido> ListarNaoCanceladoPorPadaria(Padaria p)
        {
            return _servico.ListarNaoCanceladoPorPadaria(p);
        }


        public void AtualizarStatus(Pedido p, StatusPedido Status)
        {
            _servico.AtualizarStatus(p, Status);
            p = _servico.BuscarPorId(p.PedidoId);

            if (string.IsNullOrWhiteSpace(p.Cliente.RegistroAndroid) == false)
            {
                PushAndroid push = new PushAndroid();
                push.RegistrationIDS.Add(p.Cliente.RegistroAndroid);
                push.SenderID = ConfigurationManager.AppSettings["SenderIDAndroid"];
                push.ApplicationID = ConfigurationManager.AppSettings["ApplicationIDAndroid"];
                push.Title = ConfigurationManager.AppSettings["TituloPushAndroid"];
                push.Message = ConfigurationManager.AppSettings["MensagemPadraoPushAndroid"];
                push.Message = push.Message.Replace("#STATUS", Status.ToString());
                push.Message = push.Message.Replace("#PEDIDO", p.PedidoId.ToString());
                //push.Data = JsonConvert.SerializeObject(push);
                new PushFacade(PushNotification.Dominio.Enum.Platform.Android).Send(push);
            }
            
        }


        public IEnumerable<Pedido> ListarPorCliente(Usuario u)
        {
            return _servico.ListarPorCliente(u);
        }
    }
}
