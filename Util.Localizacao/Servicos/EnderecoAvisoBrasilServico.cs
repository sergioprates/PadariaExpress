using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util.Localizacao.Modelo;

namespace Util.Localizacao.Servicos
{
    public class EnderecoAvisoBrasilServico
    {
        private readonly string _url = "http://cep.correiocontrol.com.br/";

        public EnderecoAvisoBrasil BuscarCep(string cep)
        {
            if (string.IsNullOrWhiteSpace(cep))
            {
                throw new Exception("CEP não preenchido.");
            }
            cep = cep.Replace("-", "");
            cep = cep.Trim();
            string json = Web.GetRequest(_url + cep + ".json");

            EnderecoAvisoBrasil obj = JsonConvert.DeserializeObject<EnderecoAvisoBrasil>(json);

            return obj;
        }
    }
}
