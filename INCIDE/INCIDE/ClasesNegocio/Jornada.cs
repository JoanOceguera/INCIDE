using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ControlIncidenciaFuera.Clases;

namespace INCIDE.ClasesNegocio
{
    public class Jornada
    {
        List<Traza> listaTraza;

        public List<Traza> ListaTraza
        {
            get { return listaTraza; }
            set { listaTraza = value; }
        }
        public Jornada()
        { 
        }
        public Jornada(List<Traza> pListaTraza)
        {
            ListaTraza = pListaTraza;
        }

    }
}
