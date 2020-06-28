using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMotorsRestAPI.Model;

namespace WebMotorsRestAPI.Repository
{
    public interface IMarcaRepository
    {
        List<Marca> PesquisaTodasMarcas();
        Marca RetornaMarca(string marca);
        int RetornaIdMarca(string marca);
    }
}
