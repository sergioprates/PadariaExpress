using PadariaExpress.Dominio.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PadariaExpress.Dominio.Interface.Servico
{
    public interface IPadariaServico : IServicoBase<Padaria>
    {
        Padaria Registrar(Padaria padaria, Proprietario proprietario);

        IEnumerable<Padaria> ListarPorProprietario(Proprietario proprietario);
        IEnumerable<Padaria> ListarPorProximidade(double latitude, double longitude, int top);
        IEnumerable<Padaria> ListarPorNome(string nome);
    }
}
