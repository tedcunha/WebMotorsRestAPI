using System.Collections.Generic;
using WebMotorsRestAPI.Model;

namespace WebMotorsRestAPI.Repository
{
    public interface IAnuncioRepository
    {
        Anuncio Create(Anuncio pessoa);
        Anuncio FindByID(long Id);
        List<Anuncio> FindAll();
        Anuncio Update(Anuncio pessoa);
        void Delete(long Id);
    }
}
