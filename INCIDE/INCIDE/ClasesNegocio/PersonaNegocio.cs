using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INCIDE.ClasesNegocio
{
   public class PersonaNegocio
    {
        private int exp = -1;
        private List<Jornada> listaJornada;

        public List<Jornada> ListaJornada
        {
            get { return listaJornada; }
            set { listaJornada = value; }
        }

        public int Exp
        {
            get { return exp; }
            set { exp = value; }
        }

        public PersonaNegocio() { }
        public PersonaNegocio(int pExp, List<Jornada> pLista)
        {
            Exp = pExp;
            ListaJornada = pLista;
        }
    }
}
