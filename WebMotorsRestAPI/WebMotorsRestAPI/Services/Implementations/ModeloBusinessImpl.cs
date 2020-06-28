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
    public class ModeloBusinessImpl : IModeloBusiness
    {
        public List<Modelo> PesquisaTodosModelos(int IdMarca)
        {
            return RetornaModelos(IdMarca).Result.ToList();
        }

        public Modelo RetornaModelo(int IdMarca, string strmodelo)
        {
            List<Modelo> modelos = RetornaModelos(IdMarca).Result.ToList();
            if (modelos == null || modelos.Count == 0)
            {
                return null;
            }

            Modelo modelo = modelos.FirstOrDefault<Modelo>(m => m.MakeID == IdMarca && m.Name == strmodelo);
            if (modelo == null)
            {
                return null;
            }

            return modelo;
        }


        private async Task<List<Modelo>> RetornaModelos(int IdMarca)
        {
            string retorno = string.Empty;
            List<Modelo> lstmodelos = new List<Modelo>();
            var client = new HttpClient();

            client.BaseAddress = new Uri("http://desafioonline.webmotors.com.br/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage marcas = await client.GetAsync($"api/OnlineChallenge/Model?MakeID={IdMarca}");

            if (marcas.IsSuccessStatusCode)
            {
                retorno = await marcas.Content.ReadAsStringAsync();
                lstmodelos = JsonConvert.DeserializeObject<List<Modelo>>(retorno);
            }

            return lstmodelos;
        }
    }
}
