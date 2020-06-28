using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMotorsRestAPI.Model;

namespace WebMotorsRestAPI.Services
{
    public interface IModeloBusiness
    {
        List<Modelo> PesquisaTodosModelos(int IdMarca);
        Modelo RetornaModelo(int IdMarca, string strmodelo);
    }
}
