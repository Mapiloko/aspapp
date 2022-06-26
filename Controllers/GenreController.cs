using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspApp.Controllers.Models;
using AspApp.Data;
using AspApp.DTO.Genre;
using AspApp.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenreController : ControllerBase
    {
        private readonly IRepository _repo;
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        public GenreController(IRepository repo, DatabaseContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
            _repo = repo;

        }

        [HttpGet]
        public async Task<ActionResult<List<GenreDto>>> Get()
        {
            var genres =  await _context.Genres.ToListAsync();

            return _mapper.Map<List<GenreDto>>(genres);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<GenreDto>> Get(int id)
        {
            var genre = await _context.Genres.FirstOrDefaultAsync(x => x.Id == id );
            if(genre == null)
            {
                return NotFound();
            }
            return _mapper.Map<GenreDto>(genre);
        }

        [HttpPost]
         public async Task<ActionResult> Post([FromBody] GenreCreationDto genreCreationDto)
         {
            var genre = _mapper.Map<Genre>(genreCreationDto);

           _context.Add(genre);
           await _context.SaveChangesAsync();

           return NoContent();
         }

         [HttpPut("{id:int}")]
         public async Task<ActionResult> Put(int id, [FromBody] GenreCreationDto genreCreationDto)
         {
            var genre = _mapper.Map<Genre>(genreCreationDto);
            genre.Id = id;

            _context.Entry(genre).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return NoContent();
         }

         [HttpDelete("{id:int}")]
         public async Task<ActionResult> Delete(int id)
         {
            var genre = await _context.Genres.FirstOrDefaultAsync(x => x.Id == id);

            if(genre == null)
            {
                return NotFound();
            }
            _context.Remove(genre);
            await _context.SaveChangesAsync();
            return NoContent();
         }

    }
}