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

        public Pelicula FindPelicula(int idpelicula)
        {
            string path = helper.MapPath("peliculas.xml", Folders.Documents);

            XDocument document = XDocument.Load(path);
            var consulta = from datos in document.Descendants("pelicula")
                           where datos.Attribute("idpelicula").Value == idpelicula.ToString()
                           select datos;
            XElement tag = consulta.FirstOrDefault();
            Pelicula pelicula = new Pelicula();

            pelicula.IdPelicula = int.Parse(tag.Attribute("idpelicula").Value);
            pelicula.Titulo = tag.Element("titulo").Value;
            pelicula.TituloOriginal = tag.Element("titulooriginal").Value;
            pelicula.Descripcion = tag.Element("descripcion").Value;
            pelicula.Poster = tag.Element("poster").Value;
            return pelicula;
        }

        public List<Escena> GetEscenasPelicula(int idpelicula)
        {
            string path = this.helper.MapPath("escenaspeliculas.xml", Folders.Documents);
            XDocument document = XDocument.Load(path);
            var consulta = from datos in document.Descendants("escena")
                           where datos.Attribute("idpelicula").Value ==
                           idpelicula.ToString()
                           select datos;
            List<Escena> escenas = new List<Escena>();
            foreach(XElement tag in consulta)
            {
                Escena escena = new Escena();
                escena.IdPelicula = int.Parse(tag.Attribute("idpelicula").Value);
                escena.TituloEscena = tag.Element("tituloescena").Value;
                escena.Descripcion = tag.Element("descripcion").Value;
                escena.Imagen = tag.Element("imagen").Value;

                escenas.Add(escena);
            }
            return escenas;
        }

        public Escena GetEscenaPelicula(int idpelicula, int posicion, ref int numeroEscenas)
        {
            string path =
                this.helper.MapPath("escenaspeliculas.xml", Folders.Documents);
            XDocument document = XDocument.Load(path);
            //VOY A RECUPERAR LA COLECCION DE ESCENAS DE UNA PELICULA
            //PARA ELLO, UTILIZAMOS EL METODO ANTERIOR
            List<Escena> escenas = this.GetEscenasPelicula(idpelicula);
            numeroEscenas = escenas.Count;
            //VAMOS A PAGINAR DE UNO EN UNO
            Escena escena = escenas.Skip(posicion).Take(1).FirstOrDefault();
            return escena;
        }
    }
}
