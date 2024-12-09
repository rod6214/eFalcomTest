using AutoMapper;
using Domain;
using Domain.Dtos;

namespace Application
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile() 
        {
            CreateMap<InsertPallet, Pallet>();
            CreateMap<RemovePallet, Pallet>();
            CreateMap<Ubicacion, UbicacionDto>();
            CreateMap<Pallet, PalletDto>();
        }
    }
}
