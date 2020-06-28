using System.Collections.Generic;
using WebMotorsRestAPI.Model;

namespace WebMotorsRestAPI.Services
{
    public interface IAnuncioBusiness
    {
        Anuncio Create(Anuncio pessoa);
        Anuncio FindByID(long Id);
        List<Anuncio> FindAll();
        Anuncio Update(Anuncio pessoa);
        void Delete(long Id);
    }
}
