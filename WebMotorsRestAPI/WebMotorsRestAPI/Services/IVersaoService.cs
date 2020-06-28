﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMotorsRestAPI.Model;

namespace WebMotorsRestAPI.Services
{
    public interface IVersaoService
    {
        List<Versao> PesquisaTodoasVersoes(int IdModelo);
        Versao RetornaVersao(int IdModelo, string strVersao);
    }
}
