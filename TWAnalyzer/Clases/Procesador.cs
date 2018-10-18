using System;
using System.Threading;
using RapiTools.Tools;
using RapiTools.Fields;

namespace TWAnalyzer
{
    internal class Procesador
    {
        #region Propiedades, campos y constructor

        public static Procesador Proceso = new Procesador();

        public DateTime fechaProximoPeriodo;
        public DateTime fechaProximaInstancia;

        public int Instancia { get; set; }
        public int PostsRobados { get; set; }

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
            //Levanto los estados.
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
                    PostsRobados = 0;
                    //AlmacenarEstados();

                    Logger.Info($"|Log - {DateTime.Now}| Periodo siguiente: {fechaProximoPeriodo} -=- NUEVO PERIODO INICIADO.\n");
                }
                else
                {
                    if (DateTime.Now >= fechaProximaInstancia)
                    {
                        // Si la operación no rompe el Rate Limit
                        if (PostsRobados < 300 && Instancia < 20)
                        {
                            // Realizamos la operación.
                            fechaProximaInstancia = DateTime.Now.AddMinutes(15);
                            Instancia++;

                            PostsRobados += Jarvis.saveTimeLinePosts();
                            //AlmacenarEstados(); Cantidad posts, Instancia, etc.

                            Logger.Info($"|Log - {DateTime.Now}| Periodo siguiente: {fechaProximoPeriodo} - Instancia: {Instancia} - Likes dados: {PostsRobados}\n");
                        }
                        else
                        {
                            Logger.Info($"|Log - {DateTime.Now}| Periodo siguiente: {fechaProximoPeriodo} - Instancia: {Instancia} - Likes dados: {PostsRobados}");
                            Logger.Info($"|Log - {DateTime.Now}| Se llegó al limite de Likes dados, esperando el próximo periodo.\n\n");
                        }

                    }
                }
            }
            catch (Exception e)
            {
                Logger.Info($"{DateTime.Now} - {e.Message}\n\n");
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
            Vista.CantidadLiked = this.PostsRobados;
            Vista.InstanciaActual = this.Instancia;
            //Indicadores intervalos
            Vista.fechaProximoProcesamiento = this.fechaProximaInstancia;
            Vista.fechaNuevoPeriodo = this.fechaProximoPeriodo;
        }
        #endregion
    }
}
