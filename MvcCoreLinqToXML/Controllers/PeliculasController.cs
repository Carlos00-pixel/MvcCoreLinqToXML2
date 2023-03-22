using Microsoft.AspNetCore.Mvc;
using MvcCoreLinqToXML.Models;
using MvcCoreLinqToXML.Repositories;

namespace MvcCoreLinqToXML.Controllers
{
    public class PeliculasController : Controller
    {
        private RepositoryPeliculas repo;

        public PeliculasController(RepositoryPeliculas repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            List<Pelicula> peliculas = this.repo.GetPeliculas();
            return View(peliculas);
        }
    }
}
