using System.Collections.Generic;
using WebMotorsRestAPI.Model;

namespace WebMotorsRestAPI.Services
{
    public interface IAnuncioService
    {
        Anuncio Create(Anuncio pessoa);
        Anuncio FindByID(long Id);
        List<Anuncio> FindAll();
        Anuncio Update(Anuncio pessoa);
        void Delete(long Id);
    }
}
