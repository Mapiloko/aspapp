using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspApp.Data;
using AspApp.DTO.Actor;
using AspApp.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActorController: ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        public ActorController(DatabaseContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;

        }

        [HttpGet]
        public async Task<ActionResult<List<ActorDto>>> Get()
        {
            var actor =  await _context.Actors.ToListAsync();

            return _mapper.Map<List<ActorDto>>(actor);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ActorDto>> Get(int id)
        {
            var actor = await _context.Actors.FirstOrDefaultAsync(x => x.Id == id );
            if(actor == null)
            {
                return NotFound();
            }
            return _mapper.Map<ActorDto>(actor);
        }

        [HttpPost]
         public async Task<ActionResult> Post([FromBody] ActorCreationDto actorCreationDto)
         {
            var actor = _mapper.Map<Actor>(actorCreationDto);

           _context.Add(actor);
           await _context.SaveChangesAsync();

           return NoContent();
         }

         [HttpPut("{id:int}")]
         public async Task<ActionResult> Put(int id, [FromBody] ActorCreationDto actorCreationDto)
         {
            var actor = _mapper.Map<Actor>(actorCreationDto);
            actor.Id = id;

            _context.Entry(actor).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return NoContent();
         }

         [HttpDelete("{id:int}")]
         public async Task<ActionResult> Delete(int id)
         {
            var actor = await _context.Actors.FirstOrDefaultAsync(x => x.Id == id);

            if(actor == null)
            {
                return NotFound();
            }
            _context.Remove(actor);
            await _context.SaveChangesAsync();
            return NoContent();
         }

        
    }
}