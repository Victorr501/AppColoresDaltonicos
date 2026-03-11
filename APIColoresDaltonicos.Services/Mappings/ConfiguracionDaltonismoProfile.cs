using APIColoresDaltonicos.Models.ConfiguracionDaltonismos;
using APIColoresDaltonicos.Models.ConfiguracionDaltonismos.DTOs;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace APIColoresDaltonicos.Services.Mappings
{
    public class ConfiguracionDaltonismoProfile : Profile
    {
        public ConfiguracionDaltonismoProfile()
        {
            CreateMap<ConfiguracionDaltonismoDto, ConfiguracionDaltonismo>();
        }
    }
}
