using Microsoft.Data.OData.Query.SemanticAst;
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
    public class VeiculoBusinessImpl : IVeiculoBusiness
    {
        private readonly IVeiculoRepository _veiculoRepository;

        public VeiculoBusinessImpl(IVeiculoRepository veiculoRepository)
        {
            _veiculoRepository = veiculoRepository;
        }

        public List<Veiculos> RetornaVeiculos(string marca, 
                                              string modelo, 
                                              string versao,
                                              int kilometragem,
                                              string preco,
                                              int anoModelo,
                                              int anoFabricacao,
                                              string cor)
        {
            return _veiculoRepository.RetornaVeiculos(marca,   
                                                      modelo,
                                                      versao,
                                                      kilometragem,
                                                      preco,
                                                      anoModelo,
                                                      anoFabricacao,
                                                      cor);
        }
    }
}
