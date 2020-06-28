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
    public class MarcaBusinessImpl : IMarcaBusiness
    {
        public List<Marca> PesquisaTodasMarcas()
        {
            return RetornaMarcas().Result.ToList();
        }

        public Marca RetornaMarca(string marca)
        {
            List<Marca> lstmarcas = RetornaMarcas().Result.ToList();
            if (lstmarcas == null || lstmarcas.Count == 0)
            {
                return null;
            }

            Marca retorno = lstmarcas.FirstOrDefault<Marca>(m => m.Name == marca);
            if (retorno == null)
            {
                return null;
            }

            return retorno;
        }

        public int RetornaIdMarca(string marca)
        {
            List<Marca> lstmarcas = RetornaMarcas().Result.ToList();
            if (lstmarcas == null || lstmarcas.Count == 0)
            {
                return 0;
            }

            Marca retorno = lstmarcas.FirstOrDefault<Marca>(m => m.Name == marca);
            if (retorno == null)
            {
                return 0;
            }

            return retorno.ID;
        }

        private async Task<List<Marca>> RetornaMarcas()
        {
            string retorno = string.Empty;
            List<Marca> lstmarcas = new List<Marca>();
            var client = new HttpClient();

            client.BaseAddress = new Uri("http://desafioonline.webmotors.com.br/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage marcas = await client.GetAsync("api/OnlineChallenge/Make");

            if (marcas.IsSuccessStatusCode)
            {
                retorno = await marcas.Content.ReadAsStringAsync();
                lstmarcas = JsonConvert.DeserializeObject<List<Marca>>(retorno);
            }

            return lstmarcas;
        }
    }
}
