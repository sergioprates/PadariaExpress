using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace PadariaExpress.Website
{
    /// <summary>
    /// Summary description for Upload
    /// </summary>
    public class Upload : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            try
            {
                if (context.Request.QueryString["FotoProduto"] == "1")
                {
                    uploadFoto(context, "FotosProdutos", "FotoProduto");
                }
                else if (context.Request.QueryString["FotoPadaria"] == "1")
                {
                    uploadFoto(context, "FotosPadarias", "FotoPadaria");
                }
                else if (context.Request.QueryString["DeletarFoto"] == "1")
                {
                    deletarFoto(context.Request.QueryString["Caminho"]);
                }
            }
            catch (Exception ac)
            {

            }

        }

        private void deletarFoto(string arquivo)
        {
            string dirFullPath = HttpContext.Current.Server.MapPath("~/" + arquivo);

            if(File.Exists(dirFullPath))
            {
                File.Delete(dirFullPath);
            }
        }

        private void uploadFoto(HttpContext context, string pasta, string prefixo)
        {
            string dirFullPath = HttpContext.Current.Server.MapPath("~/" + pasta + "/");
            string[] files;
            int numFiles;
            files = System.IO.Directory.GetFiles(dirFullPath);
            numFiles = files.Length;
            numFiles = numFiles + 1;
            string str_image = "";

            foreach (string s in context.Request.Files)
            {
                HttpPostedFile file = context.Request.Files[s];
                string fileName = file.FileName;
                string fileExtension = file.ContentType;

                if (!string.IsNullOrEmpty(fileName))
                {
                    fileExtension = Path.GetExtension(fileName);
                    str_image = prefixo + "_" + Guid.NewGuid().ToString().Substring(0, 8) + fileExtension;
                    string pathToSave_100 = HttpContext.Current.Server.MapPath("~/" + pasta + "/") + str_image;
                    file.SaveAs(pathToSave_100);
                }
            }
            //  database record update logic here  ()

            context.Response.Write(str_image);
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