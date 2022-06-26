using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspApp.Controllers.Models;
using AspApp.Interfaces;

namespace AspApp.Services
{
    public class InMemoryRepository: IRepository
    {
        private List<Genre> _genres;

        public InMemoryRepository()
        {
            _genres = new List<Genre>()
            {
                new Genre(){Id = 1, Name = "Action"},
                new Genre(){Id = 2, Name = "Comedy"}
            };
        }
        public async Task<List<Genre>> GetGenres()
        {
            await Task.Delay(5000);
            return _genres;
        }

        public Genre GetGenreById(int id)
        {
            return _genres.First(x => x.Id == id);
        }

        public void AddGenre(Genre genre)
        {
            genre.Id = _genres.Max(x => x.Id) + 1;
            _genres.Add(genre);
        }
    }
}