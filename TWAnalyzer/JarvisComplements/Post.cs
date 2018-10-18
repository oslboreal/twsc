using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JarvisComplements
{
    public class Post
    {
        public int id;
        public DateTime ultimaVezPublicado;
        public string body;

        /// <summary>
        /// Método constructor de un Post.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ultimaVezPublicado"></param>
        /// <param name="body"></param>
        public Post(int id, DateTime ultimaVezPublicado, string body)
        {
            this.id = id;
            this.ultimaVezPublicado = ultimaVezPublicado;
            this.body = body;
        }
    }
}
