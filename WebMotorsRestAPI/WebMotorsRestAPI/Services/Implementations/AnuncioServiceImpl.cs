using System.Collections.Generic;
using System.Threading;
using WebMotorsRestAPI.Model;

namespace WebMotorsRestAPI.Services.Implementations
{
    public class AnuncioServiceImpl : IAnuncioService
    {
        private volatile int count;

        public Anuncio Create(Anuncio anuncio)
        {
            return anuncio;
        }

        public void Delete(long Id)
        {
        }

        public List<Anuncio> FindAll()
        {
            List<Anuncio> anuncios = new List<Anuncio>();
            for (int i = 0; i < 8; i++)
            {
                Anuncio anuncio = MockAnuncio(i);
                anuncios.Add(anuncio);
            }
            return anuncios;
        }

        public Anuncio FindByID(long Id)
        {
            return new Anuncio { Id = 1,
                                 Marca = "Fiat",
                                 Modelo = "Idea",
                                 Versao = "Adventure",
                                 Ano = 2008,
                                 Quilometragem = 10850,
                                 Observacao = "Teste de Observação"
            };
        }

        public Anuncio Update(Anuncio anuncio)
        {
            return anuncio;
        }

        private Anuncio MockAnuncio(int i)
        {
            return new Anuncio
            {
                Id = IncrementAndGet(),
                Marca = "Fiat",
                Modelo = "Idea",
                Versao = "Adventure",
                Ano = 2008,
                Quilometragem = 10850,
                Observacao = "Teste de Observação"
            };
        }

        private int IncrementAndGet()
        {
            return Interlocked.Increment(ref count);
        }
    }
}
