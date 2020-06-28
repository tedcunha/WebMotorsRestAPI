using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMotorsRestAPI.Model;

namespace WebMotorsRestAPI.Repository
{
    public interface IModeloRepository
    {
        List<Modelo> PesquisaTodosModelos(int IdMarca);
        Modelo RetornaModelo(int IdMarca, string strmodelo);
    }
}
