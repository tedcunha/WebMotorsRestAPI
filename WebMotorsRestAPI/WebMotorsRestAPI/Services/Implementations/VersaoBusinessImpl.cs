using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WebMotorsRestAPI.Model;

namespace WebMotorsRestAPI.Services.Implementations
{
    public class VersaoBusinessImpl : IVersaoBusiness
    {
        public List<Versao> PesquisaTodoasVersoes(int IdModelo)
        {
            return RetornaVersoes(IdModelo).Result.ToList();
        }

        public Versao RetornaVersao(int IdModelo, string strVersao)
        {
            List<Versao> versoes = RetornaVersoes(IdModelo).Result.ToList();
            if (versoes == null || versoes.Count == 0)
            {
                return null;
            }

            Versao versao = versoes.FirstOrDefault<Versao>(m => m.ModelID == IdModelo && m.Name == strVersao);
            if (versao == null)
            {
                return null;
            }

            return versao;
        }

        private async Task<List<Versao>> RetornaVersoes(int IdModelo)
        {
            string retorno = string.Empty;
            List<Versao> lstversoes = new List<Versao>();
            var client = new HttpClient();

            client.BaseAddress = new Uri("http://desafioonline.webmotors.com.br/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage versoes = await client.GetAsync($"api/OnlineChallenge/Version?ModelID={IdModelo}");

            if (versoes.IsSuccessStatusCode)
            {
                retorno = await versoes.Content.ReadAsStringAsync();
                lstversoes = JsonConvert.DeserializeObject<List<Versao>>(retorno);
            }

            return lstversoes;
        }
    }
}
