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
    public class VersaoBusinessImpl : IVersaoBusiness
    {
        private readonly IVersaoRepository _versaoRepository;

        public VersaoBusinessImpl(IVersaoRepository versaoRepository)
        {
            _versaoRepository = versaoRepository;
        }

        public List<Versao> PesquisaTodoasVersoes(int IdModelo)
        {
            return _versaoRepository.PesquisaTodoasVersoes(IdModelo);
        }

        public Versao RetornaVersao(int IdModelo, string strVersao)
        {
            return _versaoRepository.RetornaVersao(IdModelo,strVersao);
        }
    }
}
