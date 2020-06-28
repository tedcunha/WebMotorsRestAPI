using Microsoft.Rest;
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
    public class AnuncioBusinessImpl : IAnuncioBusiness
    {
        private readonly MySqlContext _mySqlContext;

        public AnuncioBusinessImpl(MySqlContext mySqlContext)
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
            return _mySqlContext.tb_anunciowebmotors.ToList();
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
