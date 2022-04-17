using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INCIDE.ControlEntidades
{
    class Dias_no_laborables_nomControl : Controlador 
    {
        public Dias_no_laborables_nomControl() : base() { }

        /// <summary>
        /// Crea y retorna un dia no laborable.
        /// </summary>        
        public Dia_no_laborable_nom Crear(DateTime fecha, String descripcion)
        {
            Dia_no_laborable_nom nolaborable = new Dia_no_laborable_nom() { fecha = fecha, descripcion = descripcion };
            return nolaborable;
        }

        /// <summary>
        /// Adiciona a la BD un Dia_no_laborable_nom dado
        /// </summary>
        /// <param name="diaNoLaborale">equipo que se pretende adicionar a la BD</param>
        public void Adicionar(Dia_no_laborable_nom diaNoLaborale)
        {
            try
            {
                cnx.Dia_no_laborable_nom.AddObject(diaNoLaborale);
                cnx.SaveChanges();
            }
            catch (Exception msg)
            {
                throw new Exception("Ocurrió un error adicionando un dia no laborable con fecha: "  + diaNoLaborale.fecha.ToString() + ". " + msg.Message);
            }
        }

        /// <summary>
        /// Edita un Dia_no_laborable_nom dado. Debe pasar un mismo Dia_no_laborable_nom seleccionado previamente el cual mantenga su idDia_no_laborable_nom
        /// </summary>
        /// <param name="Dia_no_laborable_nom">Dia_no_laborable_nom con las modificaciones hechas</param>
        public void Editar(Dia_no_laborable_nom diaNoLaborable)
        {
            try
            {
                Dia_no_laborable_nom dia_NoLaborable = this.GetDia_no_laborable(diaNoLaborable.id_Dia_no_lab);
                if (dia_NoLaborable != null)
                {
                    cnx.Dia_no_laborable_nom.ApplyCurrentValues(diaNoLaborable);
                    cnx.SaveChanges();
                }
            }
            catch (Exception msg)
            {
                throw new Exception("Ocurrió un error en el proceso de edición del dia no laborable con fecha: " + diaNoLaborable.fecha + ". " + msg.Message);
            }
        }

        /// <summary>
        /// Retorna una lista de Dia_no_laborable_nom obtenidos de la BD. 
        /// Propiedad de solo lectura
        /// </summary>
        public List<Dia_no_laborable_nom> Dias_no_laborables
        {
            get
            {
                List<Dia_no_laborable_nom> diaNoLaborable = (from nolabo in cnx.Dia_no_laborable_nom
                                                             select nolabo).ToList();
                return diaNoLaborable;
            }
        }

        /// <summary>
        /// Borra un Dia_no_laborable_nom de la base de datos
        /// </summary>
        public void Borrar(Dia_no_laborable_nom diaNoLaborable)
        {
            try
            {
                Dia_no_laborable_nom dnlborrar = this.GetDia_no_laborable(diaNoLaborable.id_Dia_no_lab);
                this.cnx.DeleteObject(dnlborrar);
                this.cnx.SaveChanges();
            }
            catch (Exception msg)
            {
                throw new Exception("Ocurrió un error borrando el reporte con fecha: " + diaNoLaborable.fecha + ". " + msg.Message);
            }
        }

        /// <summary>
        /// Dado un identificador de Dia_no_laborable_nom retorna, de existir en la BD, el Dia_no_laborable_nom en cuestion.
        /// Retorna null de no existir el Dia_no_laborable_nom en la BD.
        /// </summary>
        /// <param name="idEquipo">id del Dia_no_laborable_nom que se desea seleccionar de la BD</param>        
        public Dia_no_laborable_nom GetDia_no_laborable(int idDia_no_laborable_nom)
        {
            Dia_no_laborable_nom nolaborable = (from nolabo in cnx.Dia_no_laborable_nom
                                                where nolabo.id_Dia_no_lab == idDia_no_laborable_nom
                                                select nolabo).FirstOrDefault();
            return nolaborable;
        }
    }
}
