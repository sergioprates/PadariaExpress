using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PadariaExpress.Dominio.Interface.Repositorio;
using PadariaExpress.Dominio.Modelo;

namespace PadariaExpress.Infra.Dados.Repositorio
{
    public class UsuarioRepositorio : RepositorioBase<Usuario>, IUsuarioRepositorio
    {

        public override void Inserir(Usuario obj)
        {
            Db.Entry(obj.Sexo).State = System.Data.Entity.EntityState.Unchanged;

            if (obj is Funcionario)
            {
                for (int i = 0; i < ((Funcionario)obj).Padarias.Count; i++)
                {
                    ((Funcionario)obj).Padarias[i] = Db.Padarias.Find(((Funcionario)obj).Padarias[i].PadariaId);
                }

            }

            Db.Set<Usuario>().Add(obj);
            Db.SaveChanges();
        }

        public override void Alterar(Usuario obj)
        {
            base.Alterar(obj);
        }

        public Usuario BuscarPorEmail(string email)
        {
            return Db.Usuarios.FirstOrDefault(x => x.Email == email);
        }

        public bool ExistePorEmail(string email)
        {
            return Db.Usuarios.Any(x => x.Email == email);
        }

        public bool ExistePorCpf(string cpf)
        {
            return Db.Usuarios.Any(x => x.Cpf == cpf);
        }


        public IEnumerable<Usuario> ListarFuncionarioPorPadaria(Padaria p)
        {
            return Db.Usuarios.OfType<Funcionario>().Where(x => x.Padarias.Any(b => b.PadariaId == p.PadariaId)).ToList();

        }
    }
}
