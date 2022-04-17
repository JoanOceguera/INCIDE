using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INCIDE.ControlEntidades
{
    class Mes_cerradoControl : Controlador
    {
        public Mes_cerradoControl() : base() { }

        public Mes_cerrado Crear(DateTime fecha, int cerrado)
        {
            Mes_cerrado mc = new Mes_cerrado()
            {
                fecha = fecha,
                cerrado = cerrado
            };
            return mc;
        }

        public Mes_cerrado GetMesCerrado(int idMes)
        {
            Mes_cerrado mc = (from mes in cnx.Mes_cerrado
                                  where mes.id == idMes
                                  select mes).FirstOrDefault();
            return mc;
        }
        /// <summary>
        /// Tiene en cuenta para la seleccion del Mes cerrado las componentes Month y Year del campo 
        /// fecha pasado por parámetro
        /// </summary>
        public Mes_cerrado GetMesCerradoPorFecha(DateTime fecha)
        {
            Mes_cerrado mc = (from mes in cnx.Mes_cerrado
                              where mes.fecha.Month == fecha.Month && 
                              mes.fecha.Year == fecha.Year
                              select mes).FirstOrDefault();
            return mc;
        }

        /// <summary>
        /// Tiene en cuenta para la seleccion del Mes cerrado las componentes Month y Year del campo 
        /// fecha pasado por parámetro
        /// </summary>
        public bool EstaCerrado(DateTime fecha)
        {
            Mes_cerrado mes = new Mes_cerrado();
            if (this.GetMesCerradoPorFecha(fecha) != null)
            {
                mes = this.GetMesCerradoPorFecha(fecha);
                if (mes.cerrado == Utils.SI)
                    return true;                
                else
                    return false;                
            }
            else
                return false;
        }

        /// <summary>
        /// Adiciona a la BD un Mes_cerrado dado
        /// </summary>
        public void Adicionar(Mes_cerrado mescerrado)
        {
            try
            {
                cnx.Mes_cerrado.AddObject(mescerrado);
                cnx.SaveChanges();
            }
            catch (Exception msg)
            {
                throw new Exception("Ocurrió un error adicionando un mes cerrado. " + msg.Message);
            }
        }
        /// <summary>
        /// Edita una persona dada. Debe pasar la misma Persona seleccionado previamente el cual mantenga su idPersona
        /// </summary>
        /// <param name="persona">persona con las modificaciones hechas</param>
        public void Editar(Mes_cerrado mescerrado)
        {
            try
            {
                Mes_cerrado mes = this.GetMesCerrado(mescerrado.id);
                if (mes != null)
                {
                    cnx.Mes_cerrado.ApplyCurrentValues(mescerrado);
                    cnx.SaveChanges();
                }
            }
            catch (Exception msg)
            {
                throw new Exception("Ocurrió un error adicionando un mes cerrado. " + msg.Message);
            }
        }
    }
}
