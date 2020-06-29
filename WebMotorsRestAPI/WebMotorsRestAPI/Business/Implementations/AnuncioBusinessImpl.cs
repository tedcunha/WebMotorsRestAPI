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
using WebMotorsRestAPI.Repository;

namespace WebMotorsRestAPI.Business.Implementations
{
    public class AnuncioBusinessImpl : IAnuncioBusiness
    {
        private readonly IAnuncioRepository _anuncioRepository;

        public AnuncioBusinessImpl(IAnuncioRepository anuncioRepository)
        {
            _anuncioRepository = anuncioRepository;
        }

        public Anuncio Create(Anuncio anuncio)
        {
            return _anuncioRepository.Create(anuncio);
        }

        public void Delete(long Id)
        {
            _anuncioRepository.Delete(Id);
        }

        public List<Anuncio> FindAll()
        {
            return _anuncioRepository.FindAll();
        }

        public Anuncio FindByID(long Id)
        {
            return _anuncioRepository.FindByID(Id);
        }

        public Anuncio Update(Anuncio anuncio)
        {
            return _anuncioRepository.Update(anuncio);
        }
    }
}
