using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PadariaExpress.Dominio.Interface.Repositorio;
using PadariaExpress.Dominio.Modelo;
using System.Device.Location;
using System.Data.Entity.Spatial;

namespace PadariaExpress.Infra.Dados.Repositorio
{
    public class PadariaRepositorio : RepositorioBase<Padaria>, IPadariaRepositorio
    {
        public bool ExistePorCNPJ(string cnpj)
        {
            return Db.Padarias.Any(x => x.CNPJ == cnpj);
        }

        public Padaria BuscarPorCNPJ(string cnpj)
        {
            return Db.Padarias.FirstOrDefault(x => x.CNPJ == cnpj);
        }

        public IEnumerable<Padaria> ListarPorProprietario(Proprietario proprietario)
        {
            return Db.Padarias
                .Where(p =>
                    p.Proprietarios.Any(x => x.UsuarioId == proprietario.UsuarioId))
                    .OrderBy(x => x.NomeFantasia).ToList();
        }

        public override void Inserir(Padaria obj)
        {
            //Validação realizada para garantir que o usuário não seja cadastrado novamente.
            for (int i = 0; i < obj.Proprietarios.Count; i++)
            {
                var proprietario = Db.Proprietarios.Find(obj.Proprietarios[i].UsuarioId);

                if (proprietario == null)
                {
                    throw new Exception("Usuário não encontrado.");
                }

                obj.Proprietarios[i] = proprietario;
            }

            base.Inserir(obj);
        }

        public override void Alterar(Padaria obj)
        {
            var periodosAux = new List<PeriodoFuncionamento>(obj.PeriodosDeFuncionamento);
            obj.PeriodosDeFuncionamento = null;
            base.Alterar(obj);
            var periodosAntigos = Db.PeriodosFuncionamento.Where(x => x.Padaria.PadariaId == obj.PadariaId).ToList();
            Db.PeriodosFuncionamento.RemoveRange(periodosAntigos);
            Db.SaveChanges();
            Padaria padariaAux = Db.Padarias.Find(obj.PadariaId);

            for (int i = 0; i < periodosAux.Count; i++)
            {
                periodosAux[i].Padaria = padariaAux;
            }

            Db.PeriodosFuncionamento.AddRange(periodosAux);
            Db.SaveChanges();
        }


        public IEnumerable<Padaria> ListarPorProximidade(double latitude, double longitude, int top)
        {
            DbGeography searchLocation = DbGeography.FromText(String.Format("POINT({0} {1})", longitude.ToString().Replace(",", "."), latitude.ToString().Replace(",", ".")));
            var padariasFiltro =
                (from p in Db.Padarias
                 //where  // (Additional filtering criteria here...)
                 select new
                 {
                     PadariaId = p.PadariaId,
                     Distancia = searchLocation.Distance(DbGeography.FromText("POINT(" + p.Longitude + " " + p.Latitude + ")")),
                     DistanciaEntrega = p.DistanciaEntrega
                 })
                 .Where(x => x.Distancia <= x.DistanciaEntrega)
                .OrderBy(x => x.Distancia)
                .Take(top)
                .ToList();

            List<int> ids = padariasFiltro.Select<dynamic, int>(x => x.PadariaId).ToList();

            return Db.Padarias.Where(x => ids.Contains(x.PadariaId) && x.Ativo == true).ToList();


            //List<Padaria> padarias = new List<Padaria>();
            //for (int i = 0; i < padariasFiltro.Count; i++)
            //{
            //    Padaria padaria = new Padaria();
            //    padaria.PadariaId = padariasFiltro[i].PadariaId;
            //    padaria.DataCadastro = padariasFiltro[i].DataCadastro;
            //    padaria.Ativo = padariasFiltro[i].Ativo;
            //    padaria.RazaoSocial = padariasFiltro[i].RazaoSocial;
            //    padaria.NomeFantasia = padariasFiltro[i].NomeFantasia;
            //    padaria.CNPJ = padariasFiltro[i].CNPJ;
            //    padaria.FotoPrincipal = padariasFiltro[i].FotoPrincipal;
            //    padaria.Descricao = padariasFiltro[i].Descricao;
            //    padaria.CEP = padariasFiltro[i].CEP;
            //    padaria.Logradouro = padariasFiltro[i].Logradouro;
            //    padaria.Numero = padariasFiltro[i].Numero;
            //    padaria.Complemento = padariasFiltro[i].Complemento;
            //    padaria.Cidade = padariasFiltro[i].Cidade;
            //    padaria.Bairro = padariasFiltro[i].Bairro;
            //    padaria.Estado = padariasFiltro[i].Estado;
            //    padaria.Latitude = padariasFiltro[i].Latitude;
            //    padaria.Longitude = padariasFiltro[i].Longitude;
            //    padaria.DistanciaEntrega = padariasFiltro[i].DistanciaEntrega;
            //    padaria.Email = padariasFiltro[i].Email;
            //    padaria.Telefone = padariasFiltro[i].Telefone;
            //    padaria.Distancia = padariasFiltro[i].Distancia;

            //    padarias.Add(padaria);
            //}

        }

        public IEnumerable<Padaria> ListarPorNome(string nome)
        {
            return Db.Padarias
                .Where(x => x.NomeFantasia.ToLower().Contains(nome.ToLower()) && x.Ativo == true)
                    .ToList();
        }
    }
}
