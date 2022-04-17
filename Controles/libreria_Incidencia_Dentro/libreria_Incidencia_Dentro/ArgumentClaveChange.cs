using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace libreria_Incidencia_Dentro.Clases
{
    public class ArgumentClaveChange : EventArgs
    {
        private int posJornadaLista; // posicion de la Jornada en la lista 

        public int PosJornadaLista
        {
            get { return posJornadaLista; }
            set { posJornadaLista = value; }
        }
        private int posTrazaLista; // posicion de la Traza en la lista

        public int PosTrazaLista
        {
            get { return posTrazaLista; }
            set { posTrazaLista = value; }
        }
      
       
        public String Clave { get; set; }
        public String Observacion { get; set; }
        

    }
}
