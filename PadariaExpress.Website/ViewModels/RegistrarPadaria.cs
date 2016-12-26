using AutoMapper;
using PadariaExpress.Dominio.Modelo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PadariaExpress.Website.ViewModels
{
    public class RegistrarPadaria
    {
        private string _cnpj;
        private string _telefone;
        private string _cep;

        public int PadariaId { get; set; }
        public bool Ativo { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string Email { get; set; }
        public double ValorFrete { get; set; }
        public string CNPJ
        {
            get
            {
                return _cnpj;
            }
            set
            {
                _cnpj = value;
                if (string.IsNullOrWhiteSpace(_cnpj) == false)
                {
                    _cnpj = _cnpj.Replace(".", "");
                    _cnpj = _cnpj.Replace("-", "");
                    _cnpj = _cnpj.Replace(" ", "");
                    _cnpj = _cnpj.Replace("/", "");
                }
            }
        }
        public byte[] FotoPrincipal { get; set; }
        public string Descricao { get; set; }
        public string CEP
        {
            get
            {
                return _cep;
            }
            set
            {
                _cep = value;
                if (string.IsNullOrWhiteSpace(_cep) == false)
                {
                    _cep = _cep.Replace("-", "");
                    _cep = _cep.Trim();
                }
            }
        }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string Estado { get; set; }
        public double DistanciaEntrega { get; set; }
        public double Distancia { get; set; }
        public string Caminho { get; set; }
        public string Telefone
        {
            get
            {
                return _telefone;
            }
            set
            {
                _telefone = value;
                if (string.IsNullOrWhiteSpace(_telefone) == false)
                {
                    _telefone = _telefone.Replace("(", "");
                    _telefone = _telefone.Replace(")", "");
                    _telefone = _telefone.Replace(" ", "");
                    _telefone = _telefone.Replace("-", "");
                    _telefone = _telefone.Trim();
                }
            }
        }
        public List<PeriodoDeFuncionamentoViewModel> PeriodosDeFuncionamento { get; set; }

        public Proprietario Proprietario { get; set; }

        public Padaria ToPadaria()
        {
            Padaria p = new Padaria();
            p.PadariaId = PadariaId;
            p.Ativo = Ativo;
            p.RazaoSocial = RazaoSocial;
            p.NomeFantasia = NomeFantasia;
            p.CNPJ = CNPJ;
            p.FotoPrincipal = FotoPrincipal;
            p.Descricao = Descricao;
            p.CEP = CEP;
            p.Logradouro = Logradouro;
            p.Numero = Numero;
            p.Complemento = Complemento;
            p.Cidade = Cidade;
            p.Bairro = Bairro;
            p.Estado = Estado;
            p.DistanciaEntrega = DistanciaEntrega;
            p.Email = Email;
            p.Telefone = Telefone;
            p.ValorFrete = ValorFrete;

            //for (int i = 0; i < PeriodosDeFuncionamento.Count; i++)
            //{
            //    PeriodosDeFuncionamento[i].HoraAbertura = Convert.ToDateTime(PeriodosDeFuncionamento[i].HoraAbertura.ToString("HH:mm"));
            //    PeriodosDeFuncionamento[i].HoraFechamento = Convert.ToDateTime(PeriodosDeFuncionamento[i].HoraFechamento.ToString("HH:mm"));
            //}

            p.PeriodosDeFuncionamento = Mapper.Map<IEnumerable<PeriodoDeFuncionamentoViewModel>, IList<PeriodoFuncionamento>>(PeriodosDeFuncionamento);

            return p;
        }
    }
}