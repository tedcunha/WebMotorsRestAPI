using Microsoft.Data.OData.Query.SemanticAst;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WebMotorsRestAPI.Model;

namespace WebMotorsRestAPI.Repository.Implementations
{
    public class VeiculoRepositoryImpl : IVeiculoRepository
    {
        public List<Veiculos> RetornaVeiculos(string marca, 
                                              string modelo, 
                                              string versao,
                                              int kilometragem,
                                              string preco,
                                              int anoModelo,
                                              int anoFabricacao,
                                              string cor)
        {
            List<Veiculos> resultado = new List<Veiculos>();    

            for (int i = 1; i < 100; i++)
            {
                List<Veiculos> lveiculos = RetornaVeiculos(i).Result.ToList();
                if (lveiculos == null || lveiculos.Count == 0)
                {
                    break;
                }

                List<Veiculos> lstveiculos = lveiculos.Where(ma => ma.Make.StartsWith(marca))
                                                      .Where(mo => mo.Model.StartsWith(modelo))
                                                      .Where(v => v.Version.StartsWith(versao))
                                                      .Where(k => k.KM >= kilometragem)
                                                      .Where(p => p.Price.StartsWith(preco))
                                                      .Where(am => am.YearModel >= anoModelo)
                                                      .Where(af => af.YearFab >= anoFabricacao)
                                                      .Where(c => c.Color.StartsWith(cor))
                                                      .ToList();
                                                        
                if (lstveiculos != null || lstveiculos.Count > 0)
                {
                    resultado.AddRange(lstveiculos);
                }
            }
            return resultado;
        }


        private async Task<List<Veiculos>> RetornaVeiculos(int Pagina)
        {
            string retorno = string.Empty;
            List<Veiculos> lstVeiculos = new List<Veiculos>();
            var client = new HttpClient();

            client.BaseAddress = new Uri("http://desafioonline.webmotors.com.br/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage versoes = await client.GetAsync($"api/OnlineChallenge/Vehicles?Page={Pagina}");

            if (versoes.IsSuccessStatusCode)
            {
                retorno = await versoes.Content.ReadAsStringAsync();
                lstVeiculos = JsonConvert.DeserializeObject<List<Veiculos>>(retorno);
            }

            return lstVeiculos;
        }
    }
}
