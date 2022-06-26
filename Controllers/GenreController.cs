using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspApp.Controllers.Models;
using AspApp.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AspApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenreController : ControllerBase
    {
        private readonly IRepository _repo;
        public GenreController(IRepository repo)
        {
            _repo = repo;

        }

        [HttpGet]
        public async Task<ActionResult<List<Genre>>> Get()
        {
            return await _repo.GetGenres();
        }
        [HttpGet("{id:int}")]
        public  ActionResult<Genre> Get(int id)
        {
            var genre = _repo.GetGenreById(id);
            if(genre == null)
            {
                return NotFound();
            }
            return genre;
        }

        [HttpPost]
         public ActionResult Post([FromBody] Genre genre)
         {
            _repo.AddGenre(genre);
            
            return NoContent();
         }

         [HttpPut]
         public ActionResult Put([FromBody] Genre genre)
         {
            return NoContent();
         }

         [HttpDelete]
         public ActionResult Delete()
         {
            return NoContent();
         }

    }
}