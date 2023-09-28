using AutoMapper;
using GalponIndustrial_API.Models;
using GalponIndustrial_API.Models.Dtos;

namespace GalponIndustrial_API
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Galpon, GalponDto>().ReverseMap();
            CreateMap<Galpon, GalponCreateDto>().ReverseMap();
            CreateMap<Galpon,GalponUpdateDto>().ReverseMap();
            
            CreateMap<NumeroGalpon, NumeroGalponDto>().ReverseMap();
            CreateMap<NumeroGalpon, NumeroGalponCreateDto>().ReverseMap();
            CreateMap<NumeroGalpon, NumeroGalponUpdateDto>().ReverseMap();
        }
        
    }
}
