﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMotorsRestAPI.Model;

namespace WebMotorsRestAPI.Business
{
    public interface IVeiculoBusiness
    {
        List<Veiculos> RetornaVeiculos(string marca,
                                       string modelo,
                                       string versao,
                                       int kilometragem,
                                       string preco,
                                       int anoModelo,
                                       int anoFabricacao,
                                       string cor);
    }
}
