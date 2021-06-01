using AutoMapper;
using Dota.Models.Heroes;
using Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dota.AutoMapper
{
    public class HeroProfile : Profile
    {
        public HeroProfile()
        {
            CreateMap<Hero, HeroesVM>().ReverseMap();
        }
    }
}
