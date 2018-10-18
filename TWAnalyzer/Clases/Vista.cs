using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace TWAnalyzer
{
    public static class Vista
    {
        public static int CantidadLiked { get; set; }
        public static int InstanciaActual { get; set; }
        public static DateTime fechaProximoProcesamiento { get; set; }
        public static DateTime fechaNuevoPeriodo { get; set; }
        
        public static string lastMessage = "";

        public static string InformacionProceso()
        {
            return $"[Cantidad de Likes dados: {CantidadLiked}] \nIntancia actual: {InstanciaActual} \nPróximo procesamiento: {fechaProximoProcesamiento} \nFecha reiniciar contadores: {fechaNuevoPeriodo}";
        }
    }
}
