using PadariaExpress.Dominio.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PadariaExpress.Dominio.Interface.Repositorio
{
    public interface IPadariaRepositorio : IRepositorioBase<Padaria>
    {
        bool ExistePorCNPJ(string cnpj);

        Padaria BuscarPorCNPJ(string cnpj);

        IEnumerable<Padaria> ListarPorProprietario(Proprietario proprietario);
        IEnumerable<Padaria> ListarPorProximidade(double latitude, double longitude, int top);
        IEnumerable<Padaria> ListarPorNome(string nome);
    }
}
