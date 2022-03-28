using AutoMapper;
using WebApiContribuyente_Segundo.DTOs;
using WebApiContribuyente_Segundo.Entidades;

namespace WebApiContribuyente_Segundo.Utilidades
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<ContribuyenteDTO, Contribuyente>();
            CreateMap<Contribuyente, GetContribuyenteDTO>();
        }
    }
}