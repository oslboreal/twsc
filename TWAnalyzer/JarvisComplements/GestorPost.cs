using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RapiTools.Fields;
using TWAnalyzer;

namespace JarvisComplements
{
    public static class GestorPost
    {
        public static NumericField PublicacionActual { get; set; } = new NumericField($"{Jarvis.Nombre}-GestorPost-PublicacionActual");
        public static NumericField ProximaPublicacion { get; set; } = new NumericField($"{Jarvis.Nombre}-GestorPost-ProximaPublicacion");

        /// <summary>
        /// Método encargado de almacenar una nueva publicación en el fichero.
        /// </summary>
        /// <param name="publicacion"></param>
        /// <returns></returns>
        static public bool GuardarPost(Post publicacion)
        {
            return false;
        }

        /// <summary>
        /// Método encargado de retornar un post según su ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retorna una publicación, en caso de no encontrarla retornará nulo.</returns>
        static private Post ObtenerPostPorId(int id)
        {
            return null;
        }

        /// <summary>
        /// Obtiene la próxima publicación.
        /// </summary>
        /// <returns></returns>
        static public Post ObtenerProximoPost()
        {
            return null;
        }

        /// <summary>
        /// Actualiza una publicación según su ID.
        /// </summary>
        /// <param name="publicacion"></param>
        /// <returns></returns>
        static private bool ActualizarPost(Post publicacion)
        {
            return false;
        }

        /// <summary>
        /// Recorre el archivo que contiene las publicaciones e indica si la publicación existe o no.
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        public static bool ExistePost(string body)
        {
            throw new NotImplementedException();
        }
    }
}
