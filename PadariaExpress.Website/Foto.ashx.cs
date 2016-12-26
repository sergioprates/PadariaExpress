using PadariaExpress.Aplicacao.Interfaces;
using PadariaExpress.Dominio.Modelo;
using PadariaExpress.IOC;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PadariaExpress.Website
{
    /// <summary>
    /// Summary description for Foto
    /// </summary>
    public class Foto : IHttpHandler
    {
        private readonly IProdutoAppServico _servicoProduto;
        private readonly IPadariaAppServico _servicoPadaria;

        public Foto()
        {
            Container container = RegistradorDeDependencias.GetContainer();
            _servicoProduto = container.GetInstance<IProdutoAppServico>();
            _servicoPadaria = container.GetInstance<IPadariaAppServico>();
        }

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                context.Response.ContentType = "image/png";
                byte[] foto = null;

                if (string.IsNullOrWhiteSpace(context.Request.QueryString["ProdutoId"]) == false)
                {
                    Produto p = _servicoProduto.BuscarPorId(Convert.ToInt32(context.Request.QueryString["ProdutoId"]));
                    if (p.Foto != null)
                    {
                        foto = p.Foto;
                    }
                }
                else if (string.IsNullOrWhiteSpace(context.Request.QueryString["PadariaId"]) == false)
                {
                    Padaria p = _servicoPadaria.BuscarPorId(Convert.ToInt32(context.Request.QueryString["PadariaId"]));
                    
                    if (p.FotoPrincipal != null)
                    {
                        foto = p.FotoPrincipal;
                    }
                }

                if (foto != null)
                {
                    RenderizaFoto(context, foto);
                }
            }
            catch(Exception e)
            {

            }
        }

        private void RenderizaFoto(HttpContext context, byte[] foto)
        {
            context.Response.BinaryWrite(foto);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}