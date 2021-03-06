﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortening.Model;

namespace UrlShortening.Contract
{
    public interface IClientRepository
    {
        Task<UrlModel> Save(UrlModel model);

        Task<UrlModel> Get(string shortUrl);
    }
}
