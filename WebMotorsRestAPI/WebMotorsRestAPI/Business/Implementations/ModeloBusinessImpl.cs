using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WebMotorsRestAPI.Model;
using WebMotorsRestAPI.Repository;

namespace WebMotorsRestAPI.Business.Implementations
{
    public class ModeloBusinessImpl : IModeloBusiness
    {
        private readonly IModeloRepository _modeloRepository;
        public ModeloBusinessImpl(IModeloRepository modeloRepository)
        {
            _modeloRepository = modeloRepository;
        }

        public List<Modelo> PesquisaTodosModelos(int IdMarca)
        {
            return _modeloRepository.PesquisaTodosModelos(IdMarca);
        }

        public Modelo RetornaModelo(int IdMarca, string strmodelo)
        {
            return _modeloRepository.RetornaModelo(IdMarca,strmodelo);
        }
    }
}
