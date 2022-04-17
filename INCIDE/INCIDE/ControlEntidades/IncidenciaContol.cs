using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INCIDE.ControlEntidades
{
    class IncidenciaContol : Controlador
    {
        public IncidenciaContol() : base() { }

        /// <summary>
        /// Dado un identificador de Incidencia retorna, de existir en la BD, La incidencia en cuestión.
        /// Retorna null de no existir la incidencia en la BD.
        /// </summary>
        /// <param name="idIncidencia">id de la Incidencia que se desea seleccionar de la BD</param>        
        public Incidencia GetIncidencia(int idIncidencia)
        {
            Incidencia plan = (from pers in cnx.Incidencia
                                  where pers.id_incidencia == idIncidencia
                                  select pers).FirstOrDefault();
            return plan;
        }
        /// <summary>
        /// Crea y retorna una Incidencia.
        /// </summary>        
        public Incidencia Crear(Persona persona, Clave_nom clave, DateTime fecha, string observaciones)
        {
            Incidencia plan = new Incidencia()
            {
               Persona = persona,
               Clave_nom = clave,
               fecha= fecha,
               observacion = observaciones
            };

            return plan;
        }
        /// <summary>
        /// Crea y retorna una Incidencia.
        /// </summary>        
        public Incidencia Crear(Persona persona, Clave_nom clave, DateTime fecha, string observaciones, string tipoTraza)
        {
            Incidencia plan = new Incidencia()
            {
                Persona = persona,
                Clave_nom = clave,
                fecha = fecha,
                tipotraza = tipoTraza,
                observacion = observaciones
            };

            return plan;
        }
        
        /// <summary>
        /// Adiciona a la BD una Incidencia dada
        /// </summary>
        /// <param name="plan">Incidencia que se pretende adicionar a la BD</param>
        public void Adicionar(Incidencia plan)
        {
            try
            {
                cnx.Incidencia.AddObject(plan);
                cnx.SaveChanges();
            }
            catch (Exception msg)
            {
                throw new Exception("Ocurrió un error adicionando la Incidencia: " + plan.id_incidencia.ToString() + ". " + msg.Message);
            }
        }
        /// <summary>
        /// Edita una Incidencia dada. Debe pasar una misma Incidencia seleccionada previamente el cual mantenga su id_incidencia
        /// </summary>
        /// <param name="incide">Incidencia con las modificaciones hechas</param>
        public void Editar(Incidencia incide)
        {
            try
            {
                Incidencia pers = this.GetIncidencia(incide.id_incidencia);
                if (pers != null)
                {
                    cnx.Incidencia.ApplyCurrentValues(pers);
                    cnx.SaveChanges();
                }
            }
            catch (Exception msg)
            {
                throw new Exception("Ocurrió un error adicionando la Incidencia: " + incide.id_incidencia.ToString() + ". " + msg.Message);
            }
        }
        /// <summary>
        /// Borra una Planificación de la base de datos
        /// </summary>
        public void Borrar(Incidencia plan)
        {
            try
            {
                Incidencia   dnlborrar = this.GetIncidencia(plan.id_incidencia);
                this.cnx.DeleteObject(dnlborrar);
                this.cnx.SaveChanges();
            }
            catch (Exception msg)
            {
                throw new Exception("Ocurrió un error borrando la incidencia de la bd " + ". " + msg.Message);
            }
        }
        /// <summary>
        /// Retorna una lista de Incidencias de la BD. 
        /// Propiedad de solo lectura
        /// </summary>
        public List<Incidencia> Incidencias
        {
            get
            {
                 List<Incidencia> lista = (from a in cnx.Incidencia
                                          select a).ToList();
                 return lista;
                //return cnx.Incidencia.ToList();
            }
        }
        /// <summary>
        /// Retorna una persona tiene una iNCIDENCIA pasado por parametro.
        /// retorna una persona vacia en caso de o existir la INCIDENDIA pasada por parametro
        /// </summary>
        public Persona PersonasDeUnPlan(Incidencia plan)
        {
            Persona PERSONA = new Persona();
            Incidencia planificacion = this.GetIncidencia(plan.id_incidencia);
            if (planificacion != null)
            {
                PERSONA = planificacion.Persona;
            }

            return PERSONA;
        }

        /// <summary>
        /// Retorna true si existen incidencias para un mes y un año pasado por paramétros
        /// </summary>
        /// <param name="mes"></param>
        /// <param name="anno"></param>
        /// <returns></returns>
        public bool ExistenIncidenciasMes(int mes, int anno)
        {
            bool encontrado = false;
            var consulta = from I in cnx.Incidencia
                           where I.fecha.Month == mes && I.fecha.Year == anno
                           select I;
            if (consulta.Any())
                encontrado = true;           
            return encontrado;
        }
       
    }
}
