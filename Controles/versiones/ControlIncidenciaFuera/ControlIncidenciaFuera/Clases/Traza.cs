using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlIncidenciaFuera.Clases
{
    public enum TipoTraza
    {
        E,S,N,O //O es cualkier otro
    }
    public class Traza
    {
        public DateTime Fecha { get; set; }
        public TimeSpan Hora { get; set; }
        public string Clave { get; set; }
        public TipoTraza Tipo { get; set; }
        private int idIncidencia = -1;
        private string observaciones = String.Empty;
        
        public string Observaciones
        {
            get { return observaciones; }
            set { observaciones = value; }
        }

        public int IdIncidencia
        {
            get { return idIncidencia; }
            set { idIncidencia = value; }
        }

        public Traza(DateTime pFecha, TimeSpan pHora, string pClave, TipoTraza pTipo, int pIdIncidecia)
        {
            Fecha = pFecha;
            Hora = pHora;
            Clave = pClave;
            Tipo = pTipo;
            idIncidencia = pIdIncidecia;
        }
        public Traza(DateTime pFecha, string pClave, TipoTraza pTipo, int pIdIncidecia)
        {
            Fecha = pFecha;
            Clave = pClave;
            Tipo = pTipo;
            idIncidencia = pIdIncidecia;
        }
        public Traza(DateTime pFecha, string pClave, TipoTraza pTipo)
        {
            Fecha = pFecha;
            Clave = pClave;
            Tipo = pTipo;
        }
        public Traza(DateTime pFecha, TimeSpan pHora, string pClave, TipoTraza pTipo)
        {
            Fecha = pFecha;
            Hora = pHora;
            Clave = pClave;
            Tipo = pTipo;

        }

        public Traza(DateTime pFecha, TimeSpan pHora, string pClave, TipoTraza pTipo, int pIdIncidecia, string pObservaciones)
        {
            Fecha = pFecha;
            Hora = pHora;
            Clave = pClave;
            Tipo = pTipo;
            idIncidencia = pIdIncidecia;
            Observaciones = pObservaciones;
        }
        public Traza(DateTime pFecha, string pClave, TipoTraza pTipo, int pIdIncidecia, string pObservaciones)
        {
            Fecha = pFecha;
            Clave = pClave;
            Tipo = pTipo;
            idIncidencia = pIdIncidecia;
            Observaciones = pObservaciones;
        }
        public Traza(DateTime pFecha, string pClave, TipoTraza pTipo, string pObservaciones)
        {
            Fecha = pFecha;
            Clave = pClave;
            Tipo = pTipo;
            Observaciones = pObservaciones;
        }
        public Traza(DateTime pFecha, TimeSpan pHora, string pClave, TipoTraza pTipo, string pObservaciones)
        {
            Fecha = pFecha;
            Hora = pHora;
            Clave = pClave;
            Tipo = pTipo;
            Observaciones = pObservaciones;

        }

        public static String DescripcionTipo(TipoTraza tipo)
        {
            if (tipo == TipoTraza.E)
                return "E";
            if (tipo == TipoTraza.N)
                return "N";
            if (tipo == TipoTraza.S)
                return "S";
            if(tipo == TipoTraza.O)
                return "O";
            return String.Empty;
        }
        public static TipoTraza TipoTrazaFromDescripcion(String descrip)
        {
            if (descrip == "E")
                return TipoTraza.E;
            if(descrip == "N")
                return TipoTraza.N;
            if(descrip == "S")
                return TipoTraza.S;
            return TipoTraza.O;
        }
    }

}
