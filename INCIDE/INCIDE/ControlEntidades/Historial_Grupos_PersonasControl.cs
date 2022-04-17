using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INCIDE.ControlEntidades
{
    class Historial_Grupos_PersonasControl : Controlador
    {
        public Historial_Grupos_PersonasControl() : base() { }

        //Historial_Grupos_Personas => hgp
         
        /// <summary>
        /// Dado un identificador de hgp  retorna, de existir en la BD, el hgp en cuestión.
        /// Retorna null de no existir el hgp en la BD.
        /// </summary>
        /// <param name="id_historial"> id_historial que se desea seleccionar de la BD</param>        
        public Historial_grupos_persona GetHistorial(int id_historial)
        {
            Historial_grupos_persona plan = (from pers in cnx.Historial_grupos_persona
                                  where pers.id_historial == id_historial
                                  select pers).FirstOrDefault();
            return plan;
        }
        /// <summary>
        /// Crea y retorna una hgp.
        /// </summary>        
        public Historial_grupos_persona Crear(Persona persona, Grupo grupo, DateTime fechaI, DateTime fechaF)
        {
            Historial_grupos_persona plan = new Historial_grupos_persona()
            {
               Persona = persona,
               Grupo = grupo,
               fecha_inicio= fechaI,
               fecha_fin = fechaF
                            

            };

            return plan;
        }
        /// <summary>
        /// Adiciona a la BD una hgp dada
        /// </summary>
        /// <param name="plan">Incidencia que se pretende adicionar a la BD</param>
        public void Adicionar(Historial_grupos_persona hgp)
        {
            try
            {
                cnx.Historial_grupos_persona.AddObject(hgp);
                cnx.SaveChanges();
            }
            catch (Exception msg)
            {
                throw new Exception("Ocurrió un error adicionando el Historial: " + hgp.id_historial.ToString() + ". " + msg.Message);
            }
        }
    
        /// <summary>
        /// Retorna una lista de Historial de la BD. 
        /// Propiedad de solo lectura
        /// </summary>
        public List<Historial_grupos_persona> Historiales
        {
            get
            {
                List<Historial_grupos_persona> lista = (from a in cnx.Historial_grupos_persona
                                              select a).ToList();
                return lista;
            }
        }
        /// <summary>
        /// Retorna una persona tiene una iNCIDENCIA pasado por parametro.
        /// retorna una persona vacia en caso de o existir la INCIDENDIA pasada por parametro
        /// </summary>
        public Persona PersonasDeUnPlan(Historial_grupos_persona historial)
        {
            Persona PERSONA = new Persona();
            Historial_grupos_persona planificacion = this.GetHistorial(historial.id_historial);
            if (planificacion != null)
            {
                PERSONA = planificacion.Persona;
            }

            return PERSONA;
        }

        public List<Historial_grupos_persona> Historiales_Rango_Fechas(DateTime fecha1, DateTime fecha2)
        {
             List<Historial_grupos_persona> lista = new List<Historial_grupos_persona>();

             lista = (from a in cnx.Historial_grupos_persona
                                                    where a.fecha_inicio >= fecha1 && a.fecha_fin <= fecha2
                                                    select a).ToList();
            
            return lista;
        }
    }
}
