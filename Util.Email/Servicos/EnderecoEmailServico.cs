using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Util.Email.Modelo;

namespace Util.Email.Servicos
{
    public class EnderecoEmailServico
    {

        public static void EnviarEmail
            (
            EnderecoEmail de,
            List<EnderecoEmail> para,
            string html,
            string assunto,
            SmtpEmail smtp,
            string personalizacoes,
            string personalizado,
            char separador
            )
        {
            MailMessage email = new MailMessage();
            email.IsBodyHtml = true;
            email.Body = html;
            email.Subject = assunto;

            if (string.IsNullOrWhiteSpace(de.Nome) == false)
            {
                email.From = new MailAddress(de.Endereco, de.Nome);
            }
            else
            {
                email.From = new MailAddress(de.Endereco);
            }


            for (int i = 0; i < para.Count; i++)
            {
                if (string.IsNullOrWhiteSpace(para[i].Nome) == false)
                {
                    email.From = new MailAddress(para[i].Endereco, para[i].Nome);
                }
                else
                {
                    email.To.Add(new MailAddress(para[i].Endereco));
                }
            }

            List<string> personalizacoesArr = personalizacoes.Split(separador).ToList();
            List<string> personalizadoArr = personalizado.Split(separador).ToList();


            for (int i = 0; i < personalizacoesArr.Count; i++)
            {
                email.Body = email.Body.Replace(personalizacoesArr[i], personalizadoArr[i]);
                email.Subject = email.Subject.Replace(personalizacoesArr[i], personalizadoArr[i]);
            }

            using (SmtpClient smtpClient = new SmtpClient(smtp.Host, smtp.Porta))
            {
                if (smtp.Autenticar == true)
                {
                    smtpClient.Credentials = new NetworkCredential(smtp.Usuario, smtp.Senha);
                }

                smtpClient.Send(email);
            }
        }
    }
}
