using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects.DataClasses;

namespace INCIDE.ControlEntidades
{
    class GrupoControl : Controlador
    {
        Dictionary<int, List<Dia_no_laborable_nom>> ExpNoLaborable;
        Dictionary<int, List<Periodo_tiempo>> PeriodosPorGrupo;
        Periodo_tiempoControl periodo_control;
        public GrupoControl()
        {
            if (context == null)
                context = new INCIDEEntities();
            ExpNoLaborable = GenerarDiccionarioDiasNoLaborables();
            PeriodosPorGrupo=GenerarDiccioPeriodos();
            periodo_control = new Periodo_tiempoControl();
        }

        /// <summary>
        /// Crea y retorna un Grupo activo.
        /// </summary>        
        public Grupo Crear(String nombre, int activo)
        {
            Grupo periodoTiempo = new Grupo() { nombre = nombre, activo = activo};
            return periodoTiempo;
        }

        /// <summary>
        /// Adiciona a la BD un Grupo dado
        /// </summary>
        /// <param name="grupo">grupo que se pretende adicionar a la BD</param>
        public void Adicionar(Grupo grupo)
        {
            try
            {
                cnx.Grupo.AddObject(grupo);
                cnx.SaveChanges();
            }
            catch (Exception msg)
            {
                throw new Exception("Ocurrió un error adicionando un grupo. " + msg.Message);
            }
        }

        /// <summary>
        /// Edita un Grupo dado. Debe pasar un mismo Grupo seleccionado previamente el cual mantenga su idGrupo
        /// </summary>
        /// <param name="grupo">Periodo_tiempo con las modificaciones hechas</param>
        public void Editar(Grupo grupo)
        {
            try
            {
                Grupo _grupo = this.GetGrupo(grupo.id_Grupo);
                if (_grupo != null)
                {
                    cnx.Grupo.ApplyCurrentValues(grupo);
                    cnx.SaveChanges();
                }
            }
            catch (Exception msg)
            {
                throw new Exception("Ocurrió un error en el proceso de edición del grupo con id: " + grupo.id_Grupo + ". " + msg.Message);
            }
        }

        /// <summary>
        /// Dado un identificador de Grupo retorna, de existir en la BD, el Grupo en cuestion.
        /// Retorna null de no existir el Grupo en la BD.
        /// </summary>
        /// <param name="idGrupo">id del Periodo_tiempo que se desea seleccionar de la BD</param>        
        public Grupo GetGrupo(int idGrupo)
        {
            Grupo grupo_ = (from grupo in cnx.Grupo
                            where grupo.id_Grupo == idGrupo
                            select grupo).FirstOrDefault();            
            return grupo_;
        }

        /// <summary>
        /// Retorna una lista de Grupo obtenidos de la BD. 
        /// Propiedad de solo lectura
        /// </summary>
        public List<Grupo> Grupos
        {
            get
            {
                List<Grupo> grupos_ = (from gruo in cnx.Grupo
                                                       select gruo).OrderBy(t=>t.nombre).ToList();
                return grupos_;
            }
        }

        /// <summary>
        /// Borra un Periodo_tiempo de la base de datos
        /// </summary>
        public void Borrar(Grupo grupo)
        {
            try
            {
                Grupo _grupo = this.GetGrupo(grupo.id_Grupo);
                if (_grupo != null)
                {
                    _grupo.activo = Utils.NO;
                    cnx.SaveChanges();
                }
                else throw new Exception("El grupo que intenta borrar no existe en la base de datos");
            }
            catch (Exception msg)
            {
                throw new Exception("Ocurrió un error en el proceso de borrado del grupo. " + msg.Message);
            }
        }

        /// <summary>
        /// Retorna una lista de Dia_no_laborable_nom, dado un idGrupo
        /// retorna una lista vacia en caso de o existir el grupo pasado por parametro
        /// </summary>
        public List<Dia_no_laborable_nom> DiasNoLaborablesDelGrupo(int idGrupo)
        {
            if (ExpNoLaborable.ContainsKey(idGrupo))
                return ExpNoLaborable[idGrupo];
            else
                return new List<Dia_no_laborable_nom>();
            //List<Dia_no_laborable_nom> grupos_ = (from dnl in CNX.Dia_no_laborable_nom
            //                                      from grupodnl in dnl.diaNoLaborable_Grupo
            //                                      where grupodnl.Grupo.id_Grupo == idGrupo
            //                                      select dnl).ToList();
            //return grupos_;
           /*
            List<Dia_no_laborable_nom> dias = new List<Dia_no_laborable_nom>();
            Grupo _grupo = this.GetGrupo(idGrupo);
            if (_grupo != null)
            {
                dias = _grupo.Dia_no_laborable_nom.ToList();
            }

            return dias;*/
        }
        Dictionary<int, List<Dia_no_laborable_nom>> GenerarDiccionarioDiasNoLaborables()
        {
            Dictionary<int, List<Dia_no_laborable_nom>> Diccio=new Dictionary<int,List<Dia_no_laborable_nom>>();
            foreach (var a in CNX.Dia_no_laborable_nom)
            {
                foreach(var b in a.diaNoLaborable_Grupo)
                {
                    if(!Diccio.ContainsKey(b.Grupo.id_Grupo))
                        Diccio.Add(b.Grupo.id_Grupo, new List<Dia_no_laborable_nom>());
                    Diccio[b.Grupo.id_Grupo].Add(a);
                }
            }
            return Diccio;
        }


        public Dictionary<int, List<Periodo_tiempo>> GenerarDiccioPeriodos()
        {
            List<Periodo_tiempo> periodos = cnx.Periodo_tiempo.Where(x => x.Forma_trabajo.Count == 0).ToList();
            Dictionary<int, List<Periodo_tiempo>> Diccio=new Dictionary<int,List<Periodo_tiempo>>();
            Diccio.Add(0, periodos);
            foreach (var dnl in CNX.Periodo_tiempo)
            {
                foreach (var grupodnl in dnl.Forma_trabajo)
                {
                    if (!Diccio.ContainsKey((int)grupodnl.id_grupo))
                    {
                        Diccio.Add((int)grupodnl.id_grupo, new List<Periodo_tiempo>());
                    }
                    Diccio[(int)grupodnl.id_grupo].Add(dnl);
                }
            }
            return Diccio;
        }

        /// <summary>
        /// Retorna los periodos de tiempo de un grupo dado
        /// </summary>
        public List<Periodo_tiempo> GetPeriodoDeTiempoDeGrupo(Grupo grupo)
        {
            return PeriodosPorGrupo[grupo.id_Grupo];
            //List<Periodo_tiempo> periodos = (from dnl in CNX.Periodo_tiempo
            //                                      from grupodnl in dnl.Forma_trabajo
            //                                      where grupodnl.Grupo.id_Grupo == grupo.id_Grupo
            //                                      select dnl).ToList();
            //return periodos;
        }

        /// <summary>
        /// Retorna los periodos de tiempo de un id de Grupo dado. 
        /// Retorna una lista vacia de no encontrar el grupo con el idGrupo pasado por parametro
        /// </summary>
        public List<Periodo_tiempo> GetPeriodoDeTiempoDeGrupo(int idGrupo)
        {
            return PeriodosPorGrupo[idGrupo];
            //List<Periodo_tiempo> periodos = (from dnl in CNX.Periodo_tiempo
            //                                 from grupodnl in dnl.Forma_trabajo
            //                                 where grupodnl.Grupo.id_Grupo == idGrupo
            //                                 select dnl).ToList();
            //return periodos;
        }

        /// <summary>
        /// Retorna los periodos de tiempo que no estan asignados al grupo. 
        /// Retorna una lista vacia de no encontrar el grupo con el idGrupo pasado por parametro
        /// </summary>
        public List<Periodo_tiempo> GetPeriodoDeTiempoNoDeGrupo(int idGrupo)
        {
            List<Periodo_tiempo> periodos = new List<Periodo_tiempo>();
            foreach (var item in PeriodosPorGrupo)
            {
                if (item.Key != idGrupo)
                {
                    periodos.AddRange(item.Value);
                }
            }
            return periodos;
            //List<Periodo_tiempo> periodos = (from dnl in CNX.Periodo_tiempo
            //                                 from grupodnl in dnl.Forma_trabajo
            //                                 where grupodnl.Grupo.id_Grupo == idGrupo
            //                                 select dnl).ToList();
            //return periodos;
        }

        /// <summary>
        /// No Inserta en la base de datos
        /// </summary>
        public void SetFormaTrabajoAGrupo(Forma_trabajo formaTrabajo,Grupo grupo)
        {
            grupo.Forma_trabajo.Add(formaTrabajo);
        }

        /// <summary>
        /// No Inserta en la base de datos
        /// </summary>
        public void SetFormasTrabajoAGrupo(List<Forma_trabajo> formasTrabajo, Grupo grupo)
        {
            foreach (Forma_trabajo format in formasTrabajo)
            {
                this.SetFormaTrabajoAGrupo(format, grupo);
            }
        }

        public List<Forma_trabajo> GetFormasDeTrabajo(Grupo grupo)
        {
            return grupo.Forma_trabajo.ToList();
        }
        
       /* public void CambiarFormasDeTrabajoAGrupo(List<Forma_trabajo> formasTrabajo, Grupo grupo)
        {
            grupo.Forma_trabajo.Clear();
            foreach (Forma_trabajo forma in formasTrabajo)
                this.SetFormaTrabajoAGrupo(forma, grupo);           
        }*/
        //public void SetPeriodoTiempoAGrupo(Periodo_tiempo periodo, Grupo grupo)
        //{ 
            
        //}

    }
}
