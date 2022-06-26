using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspApp.Controllers.Models;

namespace AspApp.Interfaces
{
    public interface IRepository
    {
        Task<List<Genre>> GetGenres();
        Genre GetGenreById(int id);
        void AddGenre(Genre genre);
    }
}