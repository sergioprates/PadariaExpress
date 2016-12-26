using PadariaExpress.Dominio.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PadariaExpress.Dominio.Factory
{
    public static class UsuarioFactory
    {
        public static Usuario Instanciar(TipoUsuario tipo, string email, string nome)
        {
            switch (tipo)
            {
                case TipoUsuario.Proprietario:
                    return new Proprietario(email, nome);
                case TipoUsuario.Cliente:
                    return new Cliente(email, nome);
                case TipoUsuario.Funcionario:
                    return new Funcionario(email, nome);
            }

            throw new Exception("Tipo de usuário desconhecido.");
        }
    }
}
