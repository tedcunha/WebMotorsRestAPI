using System.Collections.Generic;
using WebMotorsRestAPI.Model;
using WebMotorsRestAPI.Repository;

namespace WebMotorsRestAPI.Business.Implementations
{
    public class MarcaBusinessImpl : IMarcaBusiness
    {
        private readonly IMarcaRepository _marcaRepository;

        public MarcaBusinessImpl(IMarcaRepository marcaRepository)
        {
            _marcaRepository = marcaRepository;
        }

        public List<Marca> PesquisaTodasMarcas()
        {
            return _marcaRepository.PesquisaTodasMarcas();
        }

        public Marca RetornaMarca(string marca)
        {
            return _marcaRepository.RetornaMarca(marca);
        }

        public int RetornaIdMarca(string marca)
        {
            return _marcaRepository.RetornaIdMarca(marca);
        }
    }
}
