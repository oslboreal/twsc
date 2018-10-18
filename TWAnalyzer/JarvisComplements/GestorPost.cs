using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RapiTools.Fields;

namespace TWAnalyzer.JarvisComplements
{
    public class GestorPost
    {
        NumericField PublicacionActual { get; set; } = new NumericField($"{Jarvis.Nombre}-GestorPost-PublicacionActual");
        NumericField ProximaPublicacion { get; set; } = new NumericField($"{Jarvis.Nombre}-GestorPost-ProximaPublicacion");

        /// <summary>
        /// Método encargado de almacenar una nueva publicación en el fichero.
        /// </summary>
        /// <param name="publicacion"></param>
        /// <returns></returns>
        public bool GuardarPost(Post publicacion)
        {
            return false;
        }

        /// <summary>
        /// Método encargado de retornar un post según su ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retorna una publicación, en caso de no encontrarla retornará nulo.</returns>
        private Post ObtenerPostPorId(int id)
        {
            return null;
        }

        /// <summary>
        /// Obtiene la próxima publicación.
        /// </summary>
        /// <returns></returns>
        public Post ObtenerProximoPost()
        {
            return null;
        }

        /// <summary>
        /// Actualiza una publicación según su ID.
        /// </summary>
        /// <param name="publicacion"></param>
        /// <returns></returns>
        private bool ActualizarPost(Post publicacion)
        {
            return false;
        }
    }
}
