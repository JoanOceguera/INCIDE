using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INCIDE.ControlEntidades
{
    public class PersonaControl: Controlador
    {
        Dictionary<int, Persona> ExpPersona;
       public  PersonaControl()
        {
            if (context == null)
                context = new INCIDEEntities();
            GenerarDiccioExpPersona();
        }

       /// <summary>
       /// Dado un identificador de persona retorna, de existir en la BD, la Persona en cuestion.
       /// Tiene en cuenta solo las personas activas
       /// Retorna null de no existir la persona en la BD.
       /// </summary>
       /// <param name="idPersona">id de la persona que se desea seleccionar de la BD</param>        
       public Persona GetPersonaActiva(int idPersona)
       {
           return this.GetPersona(idPersona, true);
       }

       /// <summary>
       /// Tiene en cuenta todas las personas con el exp pasado por parametro, ya sean activas o no
       /// </summary>
       /// <param name="exp"></param>
       /// <returns></returns>
       public List<Persona> GetPersonasPorExp(int exp)
       {
           List<Persona> personas = (from pers in cnx.Persona
                                     where pers.exp == exp
                                     select pers).ToList();
           return personas;
       }

       /// <summary>
       /// Dado un identificador de persona retorna, de existir en la BD, la Persona en cuestion.
       /// Retorna null de no existir la persona en la BD.
       /// </summary>
       /// <param name="idPersona">id de la persona que se desea seleccionar de la BD</param>        
       public Persona GetPersona(int idPersona, bool activa)
       {
           int activ = (activa) ? Utils.SI : Utils.NO;
           Persona nomPersona = (from pers in cnx.Persona
                                 where pers.id_Persona == idPersona &&
                                 pers.activo == activ
                                 select pers).FirstOrDefault();
           return nomPersona;
       }

       /// <summary>
       /// Dado un identificador de persona retorna, de existir en la BD, la Persona en cuestion.
       /// Retorna null de no existir la persona en la BD.
       /// </summary>
       /// <param name="idPersona">id de la persona que se desea seleccionar de la BD</param>        
       public Persona GetPersona(int idPersona)
       {
           Persona nomPersona = (from pers in cnx.Persona
                                 where pers.id_Persona == idPersona 
                                 select pers).FirstOrDefault();
           return nomPersona;
       }

        void GenerarDiccioExpPersona()
        {
            ExpPersona = new Dictionary<int, Persona>();
            foreach(var pers in cnx.Persona)
            {
                if (pers.activo == Utils.SI && !ExpPersona.ContainsKey(pers.exp))
                    ExpPersona.Add(pers.exp, pers);
            }
        }
        public Persona GetPersonaPorExpedienteDiccio(int expediente)
        {
            if(ExpPersona.ContainsKey(expediente))
               return ExpPersona[expediente];
            return null;
        }

       /// <summary>
       /// Dado un expediente de persona retorna, de existir en la BD, la Persona en cuestion.
       /// Retorna null de no existir la persona en la BD.
       /// Tiene en cuenta solo las personas activas
       /// </summary>
       /// <param name="idPersona">id de la persona que se desea seleccionar de la BD</param>        
        public Persona GetPersonaPorExpediente(int expediente)
       {
           return GetPersonaPorExpedienteDiccio(expediente);
           //Persona nomPersona = (from pers in cnx.Persona
           //                      where pers.exp == expediente &&
           //                      pers.activo == Utils.SI
           //                      select pers).FirstOrDefault();
           //return nomPersona;
       }

       public Persona GetPersonaPorExpediente_Activa_o_NoAct(int expediente)
       {
           Persona nomPersona = (from pers in cnx.Persona
                                 where pers.exp == expediente
                                 select pers).FirstOrDefault();
           return nomPersona;
       }

       /// <summary>
       /// Crea y retorna una Persona. Crea dicha persona sin password y activa
       /// </summary>        
       public Persona Crear(Grupo grupo, int exp, string ci, Area area, GrupoEvaluacion grupoEvaluacion)
       {
           Persona persona = new Persona()
           {
               Grupo = grupo,
               exp = exp,
               ci = ci,
               Area1 = area,
               activo = Utils.SI,
               GrupoEvaluacion = grupoEvaluacion
           };
           
           return persona;
       }
       /// <summary>
       /// Adiciona a la BD un Persona dada
       /// </summary>
       /// <param name="persona">persona que se pretende adicionar a la BD</param>
       public void Adicionar(Persona persona)
       {
           try
           {
               cnx.Persona.AddObject(persona);
               cnx.SaveChanges();
           }
           catch (Exception msg)
           {
               throw new Exception("Ocurrió un error adicionando una persona con expediente: " + persona.exp.ToString() + ". " + msg.Message);
           }
       }
       /// <summary>
       /// Edita una persona dada. Debe pasar la misma Persona seleccionado previamente el cual mantenga su idPersona
       /// </summary>
       /// <param name="persona">persona con las modificaciones hechas</param>
       public void Editar(Persona persona)
       {
           try
           {
               Persona pers = this.GetPersona(persona.id_Persona);
               if (pers != null)
               {
                   cnx.Persona.ApplyCurrentValues(pers);
                   cnx.SaveChanges();
               }
           }
           catch (Exception msg)
           {
               throw new Exception("Ocurrió un error adicionando una persona con expediente: " + persona.exp.ToString() + ". " + msg.Message);
           }
       }
       /// <summary>
       /// Desactiva a una persona
       /// </summary>
       public void Desactivar(Persona persona)
       {
           try
           {
               Persona personaBorrar = this.GetPersonaActiva(persona.id_Persona);
               personaBorrar.activo = Utils.NO;
               Editar(personaBorrar);
           }
           catch (Exception msg)
           {
               throw new Exception("Ocurrió un error adicionando una persona con expediente: " + persona.exp.ToString() + ". " + msg.Message);
           }
       }
       /// <summary>
       /// Retorna una lista de personas de la BD. 
       /// Tiene en cuenta solo las personas activas
       /// Propiedad de solo lectura
       /// </summary>
       public List<Persona> Personas
       {
           get
           {
               List<Persona> listaPersona = (from pers in cnx.Persona
                                             where pers.activo == Utils.SI
                                             select pers).ToList();
               return listaPersona;
           }
       }


       /// <summary>
       /// Retorna una lista de personas de un Área dada.
       /// Tiene en cuenta solo a las personas activas
       /// Propiedad de solo lectura
       /// </summary>
       public List<Persona> PersonasDelArea(Area area)
       {          
               List<Persona> listaPersona = (from pers in cnx.Persona
                                             where pers.Area1.id_area == area.id_area
                                             && pers.activo == Utils.SI
                                             select pers).ToList();           
               return listaPersona;
           
       }
       /// <summary>
       /// Retorna una lista de Areas que una persona detrminada controlan
       /// retorna una lista vacia en caso de o existir la persona pasda por parametro
       /// </summary> 
       /// 

       public List<Area> AreasControladasPorPersona(Persona persona)
       {
           List<Area> lista = new List<Area>();
           Persona per = this.GetPersonaActiva(persona.id_Persona);
           if (per != null)
           {
               foreach (Responsable_area resarea in persona.Responsable_area)
               {
                   lista.Add(resarea.Area);
               }
           }
           return lista;
           //List<Area> lista = new List<Area>();
           //Persona per = this.GetPersonaActiva(persona.id_Persona);
           //if (per != null)
           //{
           //    lista = per.Area.ToList();
           //}
           //return lista;
       }

       /// <summary>
       /// Retorna la lista de planificaciones de una persona Psada por parametros.
       /// retorna una  lista de planificaciones vacia en caso de o existir la persona pasada por parametro
       /// </summary>
       public List<Planificacion> PlanificacionDeUnPersona(Persona pers)
       {
           List<Planificacion> plan = new List<Planificacion>();
           Persona persona = this.GetPersonaActiva(pers.id_Persona);
           if (persona != null)
           {
               plan = persona.Planificacion.ToList();
           }

           return plan;
       }

       /// <summary>
       /// Retorna la lista de Incidencias de una persona Psada por parametros.
       /// retorna una  lista de incidencias  vacia en caso de o existir la persona pasada por parametro
       /// </summary>
       public List<Incidencia> IncidenciasDeUnaPersona(Persona pers)
       {
           List<Incidencia> plan = new List<Incidencia>();
           Persona persona = this.GetPersonaActiva(pers.id_Persona);
           if (persona != null)
           {
               plan = persona.Incidencia.ToList();
           }

           return plan;
       }

       /// <summary>
       /// Retorna la lista con el historial de una persona en grupos pasada por parametros.
       /// retorna una  lista de hISTORIAL_GRUPOS_PERSONA   vacia en caso de o existir la persona pasada por parametro
       /// </summary>
       public List<Historial_grupos_persona> HistorialDeUnaPersona(Persona pers)
       {
           List<Historial_grupos_persona> plan = new List<Historial_grupos_persona>();
           Persona persona = this.GetPersonaActiva(pers.id_Persona);
           if (persona != null)
           {
               plan = persona.Historial_grupos_persona.ToList();
           }

           return plan;
       }

        /// <summary>
        /// Retorna el area a la cual pertenece la persona pasada por parametro.Dada persona debe estar activa
        /// </summary>
        /// <param name="persona"></param>
        /// <returns></returns>
       public Area GetAreaDePersona(Persona persona)
       {
           return this.GetPersonaActiva(persona.id_Persona).Area1;
       }

        /// <summary>
        /// retorna el id de la planificacion insertada, -1 de no poder obtener dicha planificacion despues de insertada
        /// </summary>
        /// <param name="persona"></param>
        /// <param name="planificacion"></param>
        /// <returns></returns>
       public int InsertarPlanificacionAPersona(Persona persona, Planificacion planificacion)
       {
           persona.Planificacion.Add(planificacion);
           Planificacion planif = this.PlanificacionDeUnPersona(persona).OrderBy(plan => plan.id_planificacion).First();
           this.Editar(persona);
           if (planif != null)
               return planif.id_planificacion;
           return -1;
           
       }

        /// <summary>
        /// Devuelve el grupo al cual pertenece una persona dada
        /// </summary>
        /// <param name="persona"></param>
        /// <returns></returns>
       public Grupo GetGrupoDePersona(Persona persona)
       {
           return persona.Grupo;
       }

       /// <summary>
       /// Devuelve el grupo al cual pertenece una persona dada. 
       /// Retorna null de no existir la persona con expediente pasado por parametro.
       /// </summary>           
       public Grupo GetGrupoDePersona(int exp)
       {
           Persona person = this.GetPersonaPorExpedienteDiccio(exp);
           if(person != null)
            return this.GetPersonaPorExpedienteDiccio(exp).Grupo;

           return null;
       }
    }
}
