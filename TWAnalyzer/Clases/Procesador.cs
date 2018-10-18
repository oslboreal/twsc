using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Threading;

namespace TWAnalyzer
{
    internal class Procesador
    {
        #region Propiedades, campos y constructor

        public static Procesador Proceso = new Procesador();

        public DateTime fechaProximoPeriodo;
        public DateTime fechaProximaInstancia;

        public int Instancia { get; set; }
        public int LikesDados { get; set; }

        public Timer relojControlador;

        /// <summary>
        /// Método constructor.
        /// </summary>
        private Procesador()
        {

        }
        #endregion

        public void Inicializar()
        {
            this.LeerFechaProximoPeriodo();
            this.LeerFechaProximaInstancia();
            this.LeerUltimaInstancia();
            this.LeerLikesDados();
            this.ActualizarVista();
            relojControlador = new Timer(Proceso.Procesar, null, 0, 1000);
        }

        /// <summary>
        /// Método invocado periodicamente encargado de Procesar.
        /// </summary>
        /// <param name="stateInfo"></param>
        public void Procesar(Object stateInfo)
        {
            try
            {
                // Si la fecha actual es superior a la fecha del nuevo periodo.
                if (DateTime.Now >= fechaProximoPeriodo)
                {
                    // Comenzamos un nuevo periodo.
                    fechaProximoPeriodo = DateTime.Now.AddHours(6);
                    fechaProximaInstancia = DateTime.Now.AddMinutes(15);
                    // Reiniciamos los indicadores
                    Instancia = 0;
                    LikesDados = 0;
                    AlmacenarEstados();

                    using (StreamWriter temp = new StreamWriter("operaciones.txt"))
                    {
                        temp.WriteLine($"|Jarvis - {DateTime.Now}| Periodo siguiente: {fechaProximoPeriodo} -=- NUEVO PERIODO INICIADO.\n");
                    }
                }
                else
                {
                    if (DateTime.Now >= fechaProximaInstancia)
                    {
                        if (LikesDados < 490 && Instancia < 25)
                        {
                            fechaProximaInstancia = DateTime.Now.AddMinutes(15);
                            Instancia++;

                            LikesDados += Jarvis.autoLikeTimeline();
                            AlmacenarEstados();

                            using (StreamWriter temp = new StreamWriter("operaciones.txt"))
                            {
                                temp.WriteLine($"|Jarvis - {DateTime.Now}| Periodo siguiente: {fechaProximoPeriodo} - Instancia: {Instancia} - Likes dados: {LikesDados}\n");
                            }
                        }
                        else
                        {
                            using (StreamWriter temp = new StreamWriter("operaciones.txt"))
                            {
                                temp.WriteLine($"|Jarvis - {DateTime.Now}| Periodo siguiente: {fechaProximoPeriodo} - Instancia: {Instancia} - Likes dados: {LikesDados}");
                                temp.WriteLine($"|Jarvis - {DateTime.Now}| Se llegó al limite de Likes dados, esperando el próximo periodo.\n\n");
                            }
                        }

                    }
                }
            }
            catch (Exception e)
            {
                using (StreamWriter temp = new StreamWriter("logs.txt"))
                {
                    temp.WriteLine($"{DateTime.Now} - {e.Message}\n\n");
                }
            }
            finally
            {
                relojControlador.Change(0, 1000);
                ActualizarVista();
            }
        }

        /// <summary>
        /// Método encargado de detener el procesamiento
        /// </summary>
        public void Detener()
        {
            AlmacenarEstados();
            relojControlador.Dispose();
        }

        #region Métodos de comunicación con la vista.
        /// <summary>
        /// Método encargado de Actualizar todos los elementos de la Interface gráfica.
        /// </summary>
        public void ActualizarVista()
        {
            //Contadores
            Vista.CantidadLiked = this.LikesDados;
            Vista.InstanciaActual = this.Instancia;
            //Indicadores intervalos
            Vista.fechaProximoProcesamiento = this.fechaProximaInstancia;
            Vista.fechaNuevoPeriodo = this.fechaProximoPeriodo;
        }
        #endregion

        #region Métodos de serialización de información.


        /// <summary>
        /// Método encargado de Almacenar todos los estados.
        /// </summary>
        public void AlmacenarEstados()
        {
            AlmacenarFechaProximoPeriodo();
            AlmacenarInstanciaActual();
            AlmacenarLikesDadosAcual();
            AlmacenarFechaProximaInstancia();
        }

        /// <summary>
        /// Lee la UltimaFecha almacenada.
        /// </summary>
        public void LeerFechaProximoPeriodo()
        {
            using (StreamReader sreader = new StreamReader("fechaproxpe.txt"))
            {
                string content = sreader.ReadLine();
                if (string.IsNullOrEmpty(content) || string.IsNullOrWhiteSpace(content))
                {
                    this.fechaProximoPeriodo = DateTime.Now.AddHours(6);
                }
                else
                {
                    this.fechaProximoPeriodo = DateTime.Parse(content);
                }
                sreader.Close();
            }
        }

        /// <summary>
        /// Almacena la ultima fecha.
        /// </summary>
        public void AlmacenarFechaProximoPeriodo()
        {
            string fecha = this.fechaProximoPeriodo.ToString();
            using (StreamWriter swriter = new StreamWriter("fechaproxpe.txt"))
            {
                swriter.Write(fecha);
                swriter.Close();
            }
        }

        /// <summary>
        /// Leer la proxima fecha almacenada.
        /// </summary>
        public void LeerFechaProximaInstancia()
        {
            using (StreamReader sreader = new StreamReader("fechaproxinstancia.txt"))
            {
                string content = sreader.ReadLine();
                if (string.IsNullOrEmpty(content) || string.IsNullOrWhiteSpace(content))
                {
                    this.fechaProximaInstancia = DateTime.Now.AddMinutes(15);
                }
                else
                {
                    this.fechaProximaInstancia = DateTime.Parse(content);
                }
                sreader.Close();
            }
        }

        /// <summary>
        /// Almacenar proxMima fecha
        /// </summary>
        public void AlmacenarFechaProximaInstancia()
        {
            string fecha = this.fechaProximaInstancia.ToString();
            using (StreamWriter swriter = new StreamWriter("fechaproxinstancia.txt"))
            {
                swriter.Write(fecha);
                swriter.Close();
            }
        }

        /// <summary>
        /// Lee la ultima instancia almacenada.
        /// </summary>
        public void LeerUltimaInstancia()
        {
            using (StreamReader sreader = new StreamReader("instancia.txt"))
            {
                string content = sreader.ReadLine();
                this.Instancia = int.Parse(content);
                sreader.Close();
            }
        }

        /// <summary>
        /// Almacena la ultima instancia.
        /// </summary>
        public void AlmacenarInstanciaActual()
        {
            string instanciaActual = this.Instancia.ToString();
            using (StreamWriter swriter = new StreamWriter("instancia.txt"))
            {
                swriter.Write(instanciaActual);
                swriter.Close();
            }
        }


        /// <summary>
        /// Lee los Likesdados
        /// </summary>
        public void LeerLikesDados()
        {
            using (StreamReader sreader = new StreamReader("lkcnt.txt"))
            {
                string content = sreader.ReadLine();
                this.LikesDados = int.Parse(content);
                sreader.Close();
            }
        }

        /// <summary>
        /// Almacena los likes dados.
        /// </summary>
        public void AlmacenarLikesDadosAcual()
        {
            string valor = this.LikesDados.ToString();
            using (StreamWriter swriter = new StreamWriter("lkcnt.txt"))
            {
                swriter.Write(valor);
                swriter.Close();
            }
        }
        #endregion
    }
}
