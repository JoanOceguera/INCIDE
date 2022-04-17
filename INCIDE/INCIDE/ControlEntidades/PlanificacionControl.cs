using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INCIDE.ControlEntidades
{
    class PlanificacionControl : Controlador
    {
        public PlanificacionControl() : base() { }

        /// <summary>
        /// Dado un identificador de planificacion retorna, de existir en la BD, La planificación en cuestión.
        /// Retorna null de no existir la planificacion en la BD.
        /// </summary>
        /// <param name="idPlan">id de la Planificación que se desea seleccionar de la BD</param>        
        public Planificacion GetPlanificacion(int idPlan)
        {
            Planificacion plan = (from pers in cnx.Planificacion
                                  where pers.id_planificacion == idPlan
                                  select pers).FirstOrDefault();
            return plan;
        }
        /// <summary>
        /// Crea y retorna una Planificación.
        /// </summary>        
        public Planificacion Crear(Persona persona, Clave_nom clave, DateTime fechaIncio, DateTime fechaFin)
        {
            Planificacion plan = new Planificacion()
            {
               Persona = persona,
               Clave_nom = clave,
               fecha_inicio = fechaIncio,
               fecha_fin = fechaFin              

            };

            return plan;
        }
        /// <summary>
        /// Adiciona a la BD una Planificación dada
        /// </summary>
        /// <param name="plan">Planificación que se pretende adicionar a la BD</param>
        public void Adicionar(Planificacion plan)
        {
            try
            {
                cnx.Planificacion.AddObject(plan);
                cnx.SaveChanges();
            }
            catch (Exception msg)
            {
                throw new Exception("Ocurrió un error adicionando la Planificación de la Persona con id de Exp de : " + plan.Persona.exp.ToString() + ". " + msg.Message);
            }
        }
        /// <summary>
        /// Edita una Planificación dada. Debe pasar una misma Planificación seleccionada previamente el cual mantenga su idPlanificacion
        /// </summary>
        /// <param name="Dia_no_laborable_nom">Planificación con las modificaciones hechas</param>
        public void Editar(Planificacion plan)
        {
            try
            {
                Planificacion pers = this.GetPlanificacion(plan.id_planificacion);
                if (pers != null)
                {
                    cnx.Planificacion.ApplyCurrentValues(pers);
                    cnx.SaveChanges();
                }
            }
            catch (Exception msg)
            {
                throw new Exception("Ocurrió un error adicionando la Planificación: " + plan.id_planificacion.ToString() + ". " + msg.Message);
            }
        }
        /// <summary>
        /// Borra una Planificación de la base de datos
        /// </summary>
        public void Borrar(Planificacion plan)
        {
            try
            {
                Planificacion  dnlborrar = this.GetPlanificacion(plan.id_planificacion);
                this.cnx.DeleteObject(dnlborrar);
                this.cnx.SaveChanges();
            }
            catch (Exception msg)
            {
                throw new Exception("Ocurrió un error adicionando la Planificación: " + plan.id_planificacion.ToString() + ". " + msg.Message);
            
            }
        }
        /// <summary>
        /// Retorna una lista de planificaciones de la BD. 
        /// Propiedad de solo lectura
        /// </summary>
        public List<Planificacion> Planificaciones
        {
            get
            {
                List<Planificacion> lista = (from a in cnx.Planificacion
                                              select a).ToList();
                return lista;
            }
        }
        /// <summary>
        /// Retorna una persona tiene una Planificacion pasado por parametro.
        /// retorna una persona vacia en caso de o existir la planificación pasada por parametro
        /// </summary>
        public Persona PersonasDeUnPlan(Planificacion plan)
        {
            Persona lista = new Persona();
            Planificacion planificacion = this.GetPlanificacion(plan.id_planificacion);
            if (planificacion != null)
            {
                lista = planificacion.Persona;
            }

            return lista;
        }
        public List<Planificacion> PlanificacionesPersona(int idPersona)
        {
            
                List<Planificacion> lista = (from a in cnx.Planificacion
                                             where a.id_persona == idPersona
                                             select a).ToList();
                return lista;
            
        }
    }
}
