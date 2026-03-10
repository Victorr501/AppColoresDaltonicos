using System;
using System.Collections.Generic;
using System.Text;
using APIColoresDaltonicos.Models.ConfiguracionDaltonismos;
using APIColoresDaltonicos.Repositories.Repositories.ConfiguracionDaltonismos;
using APIColoresDaltonicos.Services.Services.Generic;
using Microsoft.Extensions.Logging;

namespace APIColoresDaltonicos.Services.Services.ConfiguracionDaltonismos
{
    public class ConfiguracionDaltonismoService : GenericService<ConfiguracionDaltonismo>, IConfiguracionDaltonismoService
    {
        private readonly IConfiguracionDaltonismoRepository _configuracionDaltonismoRepository;

        public  ConfiguracionDaltonismoService(IConfiguracionDaltonismoRepository configuracionDaltonismoRepository, ILogger<ConfiguracionDaltonismoService> logger) : base(configuracionDaltonismoRepository, logger)
        {
             _configuracionDaltonismoRepository = configuracionDaltonismoRepository;
        }


    }

}
