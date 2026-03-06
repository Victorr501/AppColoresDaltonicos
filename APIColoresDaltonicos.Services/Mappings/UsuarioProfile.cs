using APIColoresDaltonicos.Models.Usuarios;
using APIColoresDaltonicos.Models.Usuarios.DTOs;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace APIColoresDaltonicos.Services.Mappings
{
    public class UsuarioProfile: Profile
    {
        public UsuarioProfile() 
        {
            CreateMap<Usuario, UsuarioResponseDto>();
            CreateMap<RegistroUsuarioDto, Usuario>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
