﻿using PadariaExpress.Dominio.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PadariaExpress.Dominio.Interface.Repositorio
{
    public interface ICategoriaRepositorio : IRepositorioBase<Categoria>
    {
        IEnumerable<Categoria> ListarAtivos(Padaria padaria);
        IEnumerable<Categoria> Listar(Padaria padaria);
    }
}
