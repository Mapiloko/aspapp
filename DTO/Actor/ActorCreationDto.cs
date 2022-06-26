using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspApp.DTO.Actor
{
    public class ActorCreationDto
    {
        public string Name {get; set;}

        public DateTime DateOfBirth {get; set;}
        public string Biography {get; set;} 
        public string Picture {get; set;}
    }
}