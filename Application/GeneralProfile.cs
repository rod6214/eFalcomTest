using AutoMapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile() 
        {
            CreateMap<InsertPallet, Pallet>();
            CreateMap<RemovePallet, Pallet>();
        }
    }
}
