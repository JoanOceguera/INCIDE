using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INCIDE.ControlEntidades
{
    class AgrupacionControl : Controlador
    {
        public AgrupacionControl() : base() { }

        /// <summary>
        /// Dado un identificador de Agrupacion retorna, de existir en la BD, la Agrupacion en cuestion.
        /// Retorna null de no existir la Agrupacion en la BD.
        /// </summary>
        /// <param name="idAgrup">id del área que se desea seleccionar de la BD</param>        
        public Agrupacion_nom GetAgrupacion_nom(int idAgrup)
        {
            Agrupacion_nom agrup = (from pers in cnx.Agrupacion_nom
                                    where pers.id_agrup == idAgrup
                                    select pers).FirstOrDefault();
            return agrup;
        }

        /// <summary>
        /// Dado una agrupcion me devuelve el listado de las areas asociadas.
        /// </summary>
        /// <param name="agrupacion"></param>
        /// <returns>devuelve null si no se encuentra la Agrupación que se está pasando por parámetro</returns>
        public List<Area> AreasDeUnaAgrupacion(Agrupacion_nom agrupacion)
        {
            List<Area> lista = new List<Area>();
            Agrupacion_nom agrup = this.GetAgrupacion_nom(agrupacion.id_agrup);
            if (agrup != null)
            {
                lista = agrup.Area.ToList();
            }
            return lista;


        }


        /// <summary>
        /// Crea y retorna un Agrupacion_nom.
        /// </summary>                     
        public Agrupacion_nom Crear(string codigo, string descripcion)
        {
            Agrupacion_nom Agrup = new Agrupacion_nom()
            {
                codigo = codigo,
                descripcion = descripcion
            };

            return Agrup;
        }

        /// <summary>
        /// Adiciona a la BD un Agrupacion dada
        /// </summary>
        /// <param name="persona">Agrupacion_nom que se pretende adicionar a la BD</param>
        public bool Adicionar(Agrupacion_nom Agrup)
        {
            try
            {
                
                cnx.Agrupacion_nom.AddObject(Agrup);
                if (cnx.SaveChanges() > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception msg)
            {
                throw new Exception("Ocurrió un error adicionando la Agrupación de : " + Agrup.descripcion.ToString() + ". " + msg.Message);
            }
        }
        /// <summary>
        /// Edita un Agrupacion_nom dada. Debe pasar una misma Agrupacion_nom seleccionada previamente el cual mantenga su id_Agrupacion_nom
        /// </summary>
        /// <param name="Dia_no_laborable_nom">Agrupacion_nom con las modificaciones hechas</param>
        public bool Editar(Agrupacion_nom area)
        {
            try
            {
                Agrupacion_nom pers = this.GetAgrupacion_nom(area.id_agrup);
                if (pers != null)
                {
                    cnx.Agrupacion_nom.ApplyCurrentValues(pers);
                    if (cnx.SaveChanges()>0)
                        return true;
                    else
                        return false;
                }
                return false;
            }
            catch (Exception msg)
            {
                throw new Exception("Ocurrió un error adicionando la Agrupación de : " + area.descripcion.ToString() + ". " + msg.Message);
            }
        }

        /// <summary>
        /// Borra una Agrupacion_nom de la base de datos
        /// </summary>
        public void Borrar(Agrupacion_nom area)
        {
            try
            {
                Agrupacion_nom dnlborrar = this.GetAgrupacion_nom(area.id_agrup);
                this.cnx.DeleteObject(dnlborrar);
                this.cnx.SaveChanges();
            }
            catch (Exception msg)
            {
                throw new Exception("Ocurrió un error adicionando la Agrupación de : " + area.descripcion.ToString() + ". " + msg.Message);
            }

        }
        /// <summary>
        /// Retorna una lista de Agrupaciones de la BD. 
        /// Propiedad de solo lectura
        /// </summary>
        public List<Agrupacion_nom> Areas
        {
            get
            {
                List<Agrupacion_nom> lista = (from a in cnx.Agrupacion_nom
                                        select a).ToList();
                return lista;
            }
        }

        /// <summary>
        /// Retorna una lista de Agrupaciones de la BD. 
        /// Propiedad de solo lectura
        /// </summary>
        public List<Agrupacion_nom> Agrupaciones
        {
            get
            {
                List<Agrupacion_nom> lista = (from a in cnx.Agrupacion_nom
                                              select a).OrderBy(t => t.descripcion).ToList();
                return lista;
            }
        }
    }
}

