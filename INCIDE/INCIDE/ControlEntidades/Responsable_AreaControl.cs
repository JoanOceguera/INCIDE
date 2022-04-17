using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INCIDE.ControlEntidades
{
    class Responsable_AreaControl : Controlador
    {
        public Responsable_AreaControl() : base() { }

        /// <summary>
        /// Crea y retorna un Clave_nomControl.
        /// </summary>        
        public Responsable_area Crear(Area area, Persona persona)
        {
            Responsable_area respCon = new Responsable_area() { Area = area, Persona = persona };
            return respCon;
        }


        /// <summary>
        /// Dado un identificador de Responsable_area , de existir en la BD, el Responsable_area en cuestion.
        /// Retorna null de no existir el Responsable_area en la BD.
        /// </summary>       
        public Responsable_area GetResponsable_area(int idResponsable_area)
        {
            Responsable_area respo = (from resp in cnx.Responsable_area
                                      where resp.id_responsable_area == idResponsable_area
                                      select resp).FirstOrDefault();
            return respo;
        }

        /// <summary>
        /// Dado id persona retorna una lista con objetos Responsables_area
        /// Retorna lista vacia de no existir el Responsable_area en la BD.
        /// </summary>
        /// <param name="idPersona"></param>
        public List<Responsable_area> GetResponsable_area_por_idPersona(int idPersona)
        {
            List<Responsable_area> respo = (from resp in cnx.Responsable_area
                                      where resp.id_persona == idPersona
                                      select resp).ToList();
            return respo;
        }

        /// <summary>
        /// Dado id area retorna una lista con objetos Responsables_area
        /// Retorna lista vacia de no existir el Responsable_area en la BD.
        /// </summary>
        /// <param name="idPersona"></param>
        public List<Responsable_area> GetResponsable_area_por_idArea(int idArea)
        {
            List<Responsable_area> respo = (from resp in cnx.Responsable_area
                                            where resp.id_area == idArea
                                            select resp).ToList();
            return respo;
        }

        /// <summary>
        /// Dado un identificador de Responsable_area , de existir en la BD, el Responsable_area en cuestion.
        /// Se le pasa como criterio de seleccion el idArea y idPersona, deben coincidar ambos campos.
        /// Retorna null de no existir el Responsable_area en la BD.
        /// </summary>       
        public Responsable_area GetResponsable_area(int idArea, int idPersona)
        {
            Responsable_area respo = (from res in cnx.Responsable_area
                                              where res.Area.id_area == idArea && res.Persona.id_Persona == idPersona
                                              select res).FirstOrDefault();
            return respo;
        }

        public int Adicionar(Responsable_area responsableArea)
        {
            try
            {
                cnx.Responsable_area.AddObject(responsableArea);
                cnx.SaveChanges();

               /* Responsable_area dnl = (from dnlg in cnx.Dia_no_laborable_nom
                                            select dnlg).OrderByDescending(c => c.id_Dia_no_lab).FirstOrDefault();*/
                if (responsableArea != null)
                    return responsableArea.id_responsable_area;
                else return -1;

            }
            catch (Exception msg)
            {
                throw new Exception("Ocurrió un error adicionando un Responsable_area. " + msg.Message);
            }
        }

        /// <summary>
        /// Borra un Responsable_area
        /// </summary>
        public void Borrar(Responsable_area responsable_area)
        {
            try
            {
                Responsable_area res = this.GetResponsable_area(responsable_area.id_responsable_area);
                this.cnx.DeleteObject(res);
                this.cnx.SaveChanges();
            }
            catch (Exception msg)
            {
                throw new Exception("Ocurrió un error borrando el Responsable_area  : " + responsable_area.id_responsable_area + ". " + msg.Message);
            }
        }
    }
}
