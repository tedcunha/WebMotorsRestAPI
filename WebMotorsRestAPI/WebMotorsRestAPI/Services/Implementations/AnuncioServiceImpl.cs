using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using WebMotorsRestAPI.Model;
using WebMotorsRestAPI.Model.Context;

namespace WebMotorsRestAPI.Services.Implementations
{
    public class AnuncioServiceImpl : IAnuncioService
    {
        private readonly MySqlContext _mySqlContext;

        public AnuncioServiceImpl(MySqlContext mySqlContext)
        {
            _mySqlContext = mySqlContext;
        }


        public Anuncio Create(Anuncio anuncio)
        {
            try
            {
                _mySqlContext.Add(anuncio);
                _mySqlContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return anuncio;
        }

        public void Delete(long Id)
        {
            var retorno = _mySqlContext.tb_anunciowebmotors.SingleOrDefault(p => p.Id == Id);
            try
            {
                if (retorno != null)
                {
                    _mySqlContext.tb_anunciowebmotors.Remove(retorno);
                    _mySqlContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Anuncio> FindAll()
        {
            RunAsync();
            return _mySqlContext.tb_anunciowebmotors.ToList();
        }

        static async Task RunAsync()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://desafioonline.webmotors.com.br/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage responseA = await client.GetAsync("api/OnlineChallenge/Make");
            HttpResponseMessage modelos = await client.GetAsync("api/OnlineChallenge/Model?MakeID=3");
            if (responseA.IsSuccessStatusCode)
            {
                string teste = await modelos.Content.ReadAsStringAsync();
                List<Modelo> lstmodelos = JsonConvert.DeserializeObject<List<Modelo>>(teste);
            }
        }


        public Anuncio FindByID(long Id)
        {
            return _mySqlContext.tb_anunciowebmotors.SingleOrDefault(p => p.Id == Id);
        }

        public Anuncio Update(Anuncio anuncio)
        {
            if (!Exist(anuncio.Id))
            {
                return new Anuncio();
            }

            var retorno = _mySqlContext.tb_anunciowebmotors.SingleOrDefault(p => p.Id == anuncio.Id);

            try
            {
                _mySqlContext.Entry(retorno).CurrentValues.SetValues(anuncio);
                _mySqlContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return anuncio;
        }

        private bool Exist(int? id)
        {
            return _mySqlContext.tb_anunciowebmotors.Any(p => p.Id == id);
        }
    }
}
