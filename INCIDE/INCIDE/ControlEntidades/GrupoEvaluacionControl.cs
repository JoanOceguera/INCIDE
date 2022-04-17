using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INCIDE.ControlEntidades
{
    class GrupoEvaluacionControl : Controlador
    {
        public GrupoEvaluacionControl () : base() { }

        /// <summary>
        /// Crea y retorna un GrupoEvaluacionControl.
        /// </summary>        
        public GrupoEvaluacion Crear(String pDescripcion, decimal cdsReferencial)
        {
            GrupoEvaluacion grupo = new GrupoEvaluacion() { descripcion = pDescripcion, cdsReferencial = cdsReferencial };
            return grupo;
        }

        /// <summary>
        /// Adiciona a la BD un GrupoEvaluacion dado
        /// </summary>
        /// <param name="grupo">GrupoEvaluacion que se pretende adicionar a la BD</param>
        public bool Adicionar(GrupoEvaluacion grupo)
        {
            try
            {
                cnx.GrupoEvaluacion.AddObject(grupo);

                if (cnx.SaveChanges() > 0)
                {
                    return true;
                }
                else
                    return false;
            }
            catch (Exception msg)
            {
                throw new Exception("Ocurrió un error adicionando un Grupo de Evaluación: " + ". " + msg.Message);
            }
        }

        /// <summary>
        /// Dado un identificador de GrupoEvaluacion retorna, de existir en la BD, el GrupoEvaluacion en cuestion.
        /// Retorna null de no existir el GrupoEvaluacion en la BD.
        /// </summary>
        /// <param name="idGrupoEvaluacion">id del GrupoEvaluacion que se desea seleccionar de la BD</param>        
        public GrupoEvaluacion GetGrupoEvaluacion(int idGrupoEvaluacion)
        {
            GrupoEvaluacion GrupoEvalauacion = (from GrupoEvaluacion in cnx.GrupoEvaluacion
                                                where GrupoEvaluacion.idGrupoEvaluacion == idGrupoEvaluacion
                                                select GrupoEvaluacion).FirstOrDefault();
            return GrupoEvalauacion;
        }

        /// <summary>
        /// Edita un Clave_nom dado. Debe pasar un mismo Clave_nom seleccionado previamente el cual mantenga su idClave_nom
        /// </summary>
        /// <param name="clave">Clave_nom con las modificaciones hechas</param>
        public bool Editar_GrupoEvaluacion(GrupoEvaluacion grupo)
        {
            try
            {
                GrupoEvaluacion grupo_ = this.GetGrupoEvaluacion(grupo.idGrupoEvaluacion);
                if (grupo_ != null)
                {
                    cnx.GrupoEvaluacion.ApplyCurrentValues(grupo);
                    if (cnx.SaveChanges() > 0)
                        return true;
                    else
                        return false;

                }
                else
                    return false;
            }
            catch (Exception msg)
            {
                throw new Exception("Ocurrió un error en el proceso de edición del Grupo de Evaluacion con id: " + grupo.idGrupoEvaluacion + ". " + msg.Message);
            }
        }

        public List<GrupoEvaluacion> Lista_GruposEvaluacion()
        {
            List<GrupoEvaluacion> lista = (from clave in cnx.GrupoEvaluacion
                                           select clave).OrderByDescending(t => t.cdsReferencial).ToList();
            return lista;
        }
    }
}
