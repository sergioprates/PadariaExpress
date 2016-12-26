using PadariaExpress.Dominio.Servico;
using PadariaExpress.Infra.Dados.Repositorio;
using PadariaExpress.IOC;
using PadariaExpress.Validacao;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace PadariaExpress.Teste
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
               // string s = PasswordAssertionConcern.Encrypt("123456");
                //Dominio.Modelo.Padaria p =new Dominio.Modelo.Padaria();
                //p.PadariaId = 7;

                //DateTime dataInicial = DateTime.Now.Date.AddDays(-2);
                //DateTime dataFinal = DateTime.Now.Date.AddHours(23).AddMinutes(59);
                //var relatorio = new RelatorioFaturamentoRepositorio().ListarRelatorioFaturamento(p, dataInicial, dataFinal);


                MailMessage email = new MailMessage();
                using (StreamReader sr = new StreamReader("proprietariocadastrado.html"))
                {
                    email.IsBodyHtml = true;
                    email.Body = sr.ReadToEnd();
                }


                email.From = new MailAddress("padariaexpress@padariaexpress.com", "Padaria Express");
                email.To.Add(new MailAddress("sergioprates.student@gmail.com", "Sérgio Prates"));

                email.Body = email.Body.Replace("<#NOME>", "Sérgio");
                email.Subject = "Padaria Express - Sérgio, Obrigado por se cadastrar!";

                using(SmtpClient smtp = new SmtpClient("smtp.padariaexpress.com", 25))
                {
                    smtp.Credentials = new NetworkCredential("", "");
                    smtp.Send(email);
                }
            }
            catch(Exception erro)
            {
                Console.WriteLine(erro.Message);
            }
        }
    }
}
