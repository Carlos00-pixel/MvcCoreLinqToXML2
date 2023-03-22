using MvcCoreLinqToXML.Helpers;
using MvcCoreLinqToXML.Models;
using System.Xml.Linq;

namespace MvcCoreLinqToXML.Repositories
{
    public class RepositoryPeliculas
    {
        private HelperPathProvider helper;
        private XDocument documentPeliculas;
        private string pathPeliculas;

        public RepositoryPeliculas(HelperPathProvider helper)
        {
            this.helper = helper;
        }

        public List<Pelicula> GetPeliculas()
        {
            string path = helper.MapPath("peliculas.xml", Folders.Documents);

            XDocument document = XDocument.Load(path);
            List<Pelicula> peliculas = new List<Pelicula>();
            var consulta = from datos in document.Descendants("pelicula")
                           select datos;

            foreach (XElement tag in consulta)
            {
                Pelicula pelicula = new Pelicula();

                pelicula.IdPelicula = int.Parse(tag.Attribute("idpelicula").Value);
                pelicula.Titulo = tag.Element("titulo").Value;
                pelicula.TituloOriginal = tag.Element("titulooriginal").Value;
                pelicula.Descripcion = tag.Element("descripcion").Value;
                pelicula.Poster = tag.Element("poster").Value;

                peliculas.Add(pelicula);
            }
            return peliculas;
        }
    }
}
