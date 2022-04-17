using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INCIDE.ControlEntidades
{
    class Forma_trabajoControl : Controlador
    {
        public Forma_trabajoControl() : base() { }
        /// <summary>
        /// Crea y retorna un Grupo activo.
        /// </summary>        
        public Forma_trabajo Crear(Grupo grupo, Periodo_tiempo periodoDeTiempo)
        {
            Forma_trabajo forma = new Forma_trabajo() { Grupo = grupo, Periodo_tiempo = periodoDeTiempo};
            return forma;
        }

        /// <summary>
        /// Dado un identificador de Grupo retorna, de existir en la BD, el Grupo en cuestion.
        /// Retorna null de no existir el Grupo en la BD.
        /// </summary>
        /// <param name="idGrupo">id del Periodo_tiempo que se desea seleccionar de la BD</param>        
        public Forma_trabajo GetForma_trabajo(int idForma_trabajo)
        {
            Forma_trabajo f_ = (from f in cnx.Forma_trabajo
                                    where f.id == idForma_trabajo
                            select f).FirstOrDefault();
            return f_;
        }

        /// <summary>
        /// Borra un Forma_trabajo de la base de datos
        /// </summary>
        public void Borrar(Forma_trabajo formaTrabajo)
        {
            try
            {
                Forma_trabajo dnlborrar = this.GetForma_trabajo(formaTrabajo.id);
                this.cnx.DeleteObject(dnlborrar);
                this.cnx.SaveChanges();
            }
            catch (Exception msg)
            {
                throw new Exception("Ocurrió un error eliminando forma de trabajo. " + msg.Message);
            }
        }
    }
}
