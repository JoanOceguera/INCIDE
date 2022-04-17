using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INCIDE.ControlEntidades
{
    class DiaNoLaborable_GrupoControl : Controlador
    {
        public DiaNoLaborable_GrupoControl() : base() { }

        /// <summary>
        /// Crea y retorna un Clave_nomControl.
        /// </summary>        
        public diaNoLaborable_Grupo Crear(Grupo grupo, Dia_no_laborable_nom diaNoLaborable)
        {
            diaNoLaborable_Grupo dnlg = new diaNoLaborable_Grupo() { Grupo = grupo, Dia_no_laborable_nom = diaNoLaborable };
            return dnlg;
        }

        /// <summary>
        /// Dado un identificador de diaNoLaborable_Grupo , de existir en la BD, el diaNoLaborable_Grupo en cuestion.
        /// Retorna null de no existir el diaNoLaborable_Grupo en la BD.
        /// </summary>       
        public diaNoLaborable_Grupo GetdiaNoLaborable_Grupo(int iddiaNoLaborable_Grupo)
        {
            diaNoLaborable_Grupo diaNLGrup = (from dnlg in cnx.diaNoLaborable_Grupo
                                              where dnlg.id == iddiaNoLaborable_Grupo
                                              select dnlg).FirstOrDefault();
            return diaNLGrup;
        }

        /// <summary>
        /// Dado un identificador de diaNoLaborable_Grupo , de existir en la BD, el diaNoLaborable_Grupo en cuestion.
        /// Retorna null de no existir el diaNoLaborable_Grupo en la BD.
        /// </summary>       
        public diaNoLaborable_Grupo GetdiaNoLaborable_Grupo(int idGrupo, int idDianolaborable)
        {
            diaNoLaborable_Grupo diaNLGrup = (from dnlg in cnx.diaNoLaborable_Grupo
                                              where dnlg.Grupo.id_Grupo == idGrupo && dnlg.Dia_no_laborable_nom.id_Dia_no_lab == idDianolaborable
                                              select dnlg).FirstOrDefault();
            return diaNLGrup;
        }

        /// <summary>
        /// Borra un dianolaborableGrupo
        /// </summary>
        public void Borrar(diaNoLaborable_Grupo diaNoLaborableGrupo)
        {
            try
            {
                diaNoLaborable_Grupo dnlborrar = this.GetdiaNoLaborable_Grupo(diaNoLaborableGrupo.id);
                this.cnx.DeleteObject(dnlborrar);
                this.cnx.SaveChanges();
            }
            catch (Exception msg)
            {
                throw new Exception("Ocurrió un error borrando el diaNoLaborable_Grupo  : " + diaNoLaborableGrupo.id + ". " + msg.Message);
            }
        }

        /// <summary>
        /// Adiciona a la BD un diaNoLaborable_Grupo dado.
        /// Retorna el id del dia no laborable adicionado, retorna -1 de no poder obtener dicho id
        /// </summary>
        public int Adicionar(diaNoLaborable_Grupo diaNoLaborableGrupo)
        {
            try
            {
                cnx.diaNoLaborable_Grupo.AddObject(diaNoLaborableGrupo);                
                cnx.SaveChanges();

                Dia_no_laborable_nom dnl = (from dnlg in cnx.Dia_no_laborable_nom
                           select dnlg).OrderByDescending(c => c.id_Dia_no_lab).FirstOrDefault();
                if (dnl != null)
                    return dnl.id_Dia_no_lab;
                else return -1;
                
            }
            catch (Exception msg)
            {
                throw new Exception("Ocurrió un error adicionando un diaNoLaborable_Grupo. " + msg.Message);
            }
        }


    }
}
