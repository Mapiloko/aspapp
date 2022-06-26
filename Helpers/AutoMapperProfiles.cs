using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspApp.Controllers.Models;
using AspApp.DTO.Genre;
using AspApp.DTO.Actor;
using AspApp.Models;
using AutoMapper;

namespace AspApp.Helpers
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<GenreDto,Genre>().ReverseMap();
            CreateMap<GenreCreationDto,Genre>();
            CreateMap<ActorDto,Actor>().ReverseMap();
            CreateMap<ActorCreationDto,Actor>();
        }        
    }
}