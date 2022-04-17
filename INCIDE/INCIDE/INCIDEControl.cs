using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using INCIDE.ControlEntidades;
using INCIDE.ClasesNegocio;
using ControlIncidenciaFuera.Clases;
using INCIDE.ServiceReference1;
using Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Windows.Forms;
using HMAC;
using System.Security.Cryptography;
using System.Data;
using TeamNet.Data.FileExport;

namespace INCIDE
{
    public class INCIDEControl
    {
        //----objetos para el excellll----------------
        private Microsoft.Office.Interop.Excel.Application objApp;
        private _Workbook objBook;
        //private Workbooks objBooks;
        //private Sheets objSheets;
        //private _Worksheet objSheet;
        //private Range range;
        //---------------------------------------------------
        AgrupacionControl agrupC;
        AreaControl areaC;
        Clave_nomControl clavesNomC;
        GrupoEvaluacionControl GrupoEvaluacionC;
        Dias_no_laborables_nomControl noLaboralesC;
        Periodo_tiempoControl periodosTiempoC;
        public PersonaControl personaC;
        GrupoControl grupoC;
        Historial_Grupos_PersonasControl historialC;
        IncidenciaContol incidenciaC;
        PlanificacionControl planificacionAnticipadaC;
        PasswordControl passwordC;
        DiaNoLaborable_GrupoControl diaNoLaborable_GrupoC;//controlador de la tabla relacion entre dianolaborable y grupo
        Mes_cerradoControl mesCerradoC;
        Responsable_AreaControl responsableAreaC;
        Forma_trabajoControl formaTrabajoC;
        double horasMaximas = 0;
        public Service1Client servicio;
        Dictionary<int, Persona> ExpPersona;

        string salt = "ailin es bella";

        Persona logueado;
        public List<RegistroESpejo> Trazas;

        public AreaControl AreaC
        {
            get { return areaC; }
            set { areaC = value; }
        }

        public INCIDEControl()
        {
            this.agrupC = new AgrupacionControl();
            this.areaC = new AreaControl();
            this.clavesNomC = new Clave_nomControl();
            this.GrupoEvaluacionC = new GrupoEvaluacionControl();
            this.noLaboralesC = new Dias_no_laborables_nomControl();
            this.periodosTiempoC = new Periodo_tiempoControl();
            this.personaC = new PersonaControl();
            this.grupoC = new GrupoControl();
            this.historialC = new Historial_Grupos_PersonasControl();
            this.incidenciaC = new IncidenciaContol();
            this.planificacionAnticipadaC = new PlanificacionControl();
            this.passwordC = new PasswordControl();
            this.diaNoLaborable_GrupoC = new DiaNoLaborable_GrupoControl();
            this.mesCerradoC = new Mes_cerradoControl();
            this.servicio = new Service1Client();
            this.responsableAreaC = new Responsable_AreaControl();
            this.formaTrabajoC = new Forma_trabajoControl();
        }
        public void LLenarHorasMAX(int mes, int ano)
        {
            Dictionary<DayOfWeek, int> HorasDia = new Dictionary<DayOfWeek, int>();
            HorasDia.Add(DayOfWeek.Friday, 8);
            HorasDia.Add(DayOfWeek.Saturday, 0);
            HorasDia.Add(DayOfWeek.Sunday, 0);
            int horassemanas = 40 * 4;
            int diasMEs = DateTime.DaysInMonth(ano, mes);
            DateTime PrimerDia = new DateTime(ano, mes, 1);
            for (int i = 0; i < diasMEs % 7; i++)
            {
                if (HorasDia.ContainsKey(PrimerDia.DayOfWeek))
                {
                    horassemanas += HorasDia[PrimerDia.DayOfWeek];
                }
                else
                {
                    horassemanas += 8;
                }
                PrimerDia = PrimerDia.AddDays(1);

            }
            horasMaximas = horassemanas;
        }

        /// <summary>
        /// obtiene el usuario desde la bd dado un numero de expediente y setea la propiedad global Logueado
        /// </summary>
        /// <param name="expediente"></param>
        public void SetLogginUsuario(int expediente)
        {
            this.Logueado = this.personaC.GetPersonaPorExpediente(expediente);
        }
        public Persona Logueado
        {
            get { return this.logueado; }
            set { this.logueado = value; }
        }
        /// <summary>
        /// Retorna lista de Areas ke controla la persona pasada por parametro.
        /// Retorna lista vacia de no existir la persona pasada por parametro
        /// </summary>        
        public List<Area> ObtenerAreasControlaPersona(Persona persona)
        {
            return this.personaC.AreasControladasPorPersona(persona);
        }

        public List<Area> ObtenerAreasTodas()
        {
            return this.AreaC.Areas;
        }
        public List<Grupo> ObtenerGruposTodos()
        {
            return this.grupoC.Grupos;
        }
        public List<GrupoEvaluacion> ObtenerGruposEvaluacionTodos()
        {
            return this.GrupoEvaluacionC.Lista_GruposEvaluacion();
        }

        /// <summary>
        /// Dado un numero de expediente y un password retorna true de estar correcta dicha informacion en la base.
        /// Retorna false de no coincidir el password pasado con el almacenado en la base. Debe hacer la llamada a este metodo dentro de un 
        /// bloque try catch para obtener mensajes en caso de no existir la persona con expediente pasado o no tener dicha persona un password asociado en la base de datos.
        /// </summary>
        public bool CheckearCredenciales(int exp, String pass)
        {
            Persona person = this.personaC.GetPersonaPorExpediente(exp);
            if (person == null)
                throw new Exception("La persona con expediente " + exp.ToString() + " no existe en la base de datos.");

            Password password = this.passwordC.GetPasswordDePersona(person);
            if (password == null)
                throw new Exception("Usted no tiene permisos para acceder al sistema. No tiene un password asociado a su cuenta de usuario.");

            if (password.password1 == this.GetHMAC(pass))
                return true;
            else return false;
        }
        public bool CambiarContrasena(int exp, String newPass)
        {
            bool retorno = false;
            Persona person = this.personaC.GetPersonaPorExpediente(exp);
            if (person == null)
                throw new Exception("La persona con expediente " + exp.ToString() + " no existe en la base de datos.");

            Password password = this.passwordC.GetPasswordDePersona(person);
            String hmac = this.GetHMAC(newPass);
            if (hmac != String.Empty)
            {
                retorno = this.passwordC.CambiarContrasena(password, hmac);
            }
            else throw new Exception("Ocurrio un error cambiando la contraseña.");

            return retorno;
        }

        /// <summary>
        /// Devuelve un resumen hmac representado en una cadena Hexadedicmal, a partir de un literal cadena. Devuelve String.Empty de ocurrir error
        /// </summary>
        public String GetHMAC(String pass)
        {
            string hmacout = string.Empty;
            try
            {
                HMACAlgorithm hmac = new HMACAlgorithm();
                hmac.Hash = new SHA1Managed(); //se especifica el hash a utilizar
                hmacout = HMACAlgorithm.ToHex(hmac.HMAC(pass, salt));
            }
            catch (Exception)
            {
                return string.Empty;
            }
            return hmacout;
        }

        public static String GetHMACstatic(String pass)
        {
            string sec = "ailin es bella";
            string hmacout = string.Empty;
            try
            {
                HMACAlgorithm hmac = new HMACAlgorithm();
                hmac.Hash = new SHA1Managed(); //se especifica el hash a utilizar
                hmacout = HMACAlgorithm.ToHex(hmac.HMAC(pass, sec));
            }
            catch (Exception)
            {
                return string.Empty;
            }
            return hmacout;
        }

        /// <summary>
        /// Carga una lista con todas las claves que estan activas y son planificables
        /// </summary>
        /// <returns></returns>
        public List<Clave_nom> ListaClavesPlanificables()
        {
            List<Clave_nom> lista = new List<Clave_nom>();
            clavesNomC = new Clave_nomControl();
            lista = clavesNomC.Lista_Claves_Activas_Planificables();
            return lista;

        }
        /// <summary>
        /// Carga la Lista de Planificaciones de una persona dado el expediente de la misma
        /// </summary>
        /// <param name="Exp"></param>
        /// <returns></returns>
        public List<Planificacion> Lista_Planificaciones_Persona(int Exp)
        {
            List<Planificacion> planificaciones = new List<Planificacion>();
            planificacionAnticipadaC = new PlanificacionControl();
            personaC = new PersonaControl();
            Persona perso = personaC.GetPersonaPorExpediente(Exp);
            int idPersona;
            if (perso != null)
            {
                idPersona = perso.id_Persona;
                planificaciones = planificacionAnticipadaC.PlanificacionesPersona(idPersona);
            }
            return planificaciones;
        }
        /// <summary>
        /// Devuelve la ClaveNom segun el id que se le apse por parametros
        /// </summary>
        /// <param name="idClave"></param>
        /// <returns></returns>
        public Clave_nom Devolver_Clave(int idClave)
        {
            clavesNomC = new Clave_nomControl();
            if (idClave != null)
            {
                return clavesNomC.GetClave_nom(idClave);
            }
            else
                return null;
        }

        /// <summary>
        /// Inserta la Planificacion de una persona en la BD. Retorna de no ocurrir error el id de la panificacion insertada
        /// </summary>
        /// <param name="plan"></param>
        /// <param name="exp"></param>
        public int Insertar_Planificacion_Persona(Planificacion plan, int exp)
        {
            Persona person = this.personaC.GetPersonaPorExpediente(exp);
            if (person == null)
                throw new Exception("No existe la persona con expediente: " + exp);

            return this.personaC.InsertarPlanificacionAPersona(person, plan);
        }
        /// <summary>
        /// Crea la Planificacion con los datos pasados por paramentro
        /// </summary>
        /// <param name="clave"></param>
        /// <param name="FI"></param>
        /// <param name="Ff"></param>
        /// <param name="Exp"></param>
        /// <returns></returns>
        public Planificacion Crear_Planificacion(string clave, DateTime FI, DateTime Ff, int Exp)
        {
            Persona person = this.personaC.GetPersonaPorExpediente(Exp);
            if (person == null)
                throw new Exception("No existe la persona con expediente: " + Exp);
            Clave_nom Clave = this.clavesNomC.GetClave_nom_Codigo(clave);
            if (clave == null)
                throw new Exception("No existe la clave con código: " + clave);

            Planificacion plan = this.planificacionAnticipadaC.Crear(person, Clave, FI, Ff);
            return plan;
        }
        public void Eliminar_Planificacion_Persona(Planificacion plan)
        {
            this.planificacionAnticipadaC.Borrar(plan);
        }
        public Planificacion Devolver_planificacion(int idPlanficacion)
        {
            Planificacion plan = planificacionAnticipadaC.GetPlanificacion(idPlanficacion);
            if (plan == null)
                throw new Exception("No existe la Planificación con id: " + idPlanficacion);
            return plan;
        }

        public List<Dia_no_laborable_nom> Obtener_DiasNolaborablesDeGrupo(int idGrupo)
        {
            return this.grupoC.DiasNoLaborablesDelGrupo(idGrupo);
        }
        public Dia_no_laborable_nom Obtener_DiaNoLaborable(int idDiaNoLaborable)
        {
            Dia_no_laborable_nom dia = this.noLaboralesC.GetDia_no_laborable(idDiaNoLaborable);
            if (dia == null)
                throw new Exception("No existe el dia no laborable con id: " + idDiaNoLaborable);

            return dia;
        }
        public void Eliminar_DiaNoLaborableDelGrupoConID(int idGrupo, Dia_no_laborable_nom diaNoLaborable)
        {
            diaNoLaborable_Grupo dnlg = this.diaNoLaborable_GrupoC.GetdiaNoLaborable_Grupo(idGrupo, diaNoLaborable.id_Dia_no_lab);
            if (dnlg != null)
            {
                this.diaNoLaborable_GrupoC.Borrar(dnlg);
                Dia_no_laborable_nom dnl = noLaboralesC.GetDia_no_laborable(diaNoLaborable.id_Dia_no_lab);
                if (dnl != null)
                    this.noLaboralesC.Borrar(dnl);
                else MessageBox.Show("No se pudo eliminar el dia no laborable. Comunike este error al personal de informática.");
            }
            else throw new Exception("No se pudo eliminar el dia no laborable");
            /*
            Grupo grupo = this.grupoC.GetGrupo(idGrupo);
            if (grupo != null)
                this.grupoC.BorrarDiaNoLaborableDelGrupo(grupo, diaNoLaborable);
            else throw new Exception("No existe un grupo con id: " + idGrupo.ToString());*/
        }
        public int Insertar_DiaNoLaborableEnGrupo(int idGrupo, Dia_no_laborable_nom diaNoLaborable)
        {
            Grupo grupo = this.grupoC.GetGrupo(idGrupo);
            Dia_no_laborable_nom dnl = diaNoLaborable;
            if (grupo != null)
            {
                if (dnl != null)
                {
                    diaNoLaborable_Grupo dnlg = this.diaNoLaborable_GrupoC.Crear(grupo, dnl);
                    return this.diaNoLaborable_GrupoC.Adicionar(dnlg);
                }
                else throw new Exception("Ocurre algún problema con el dia no laborable que se intenta adicionar.");
            }
            else throw new Exception("No existe un grupo con el id: " + idGrupo.ToString());
        }

        public Dia_no_laborable_nom CrearNuevoDiaNoLaborable(DateTime fecha, String descripcion)
        {
            return this.noLaboralesC.Crear(fecha, descripcion);
        }
        /// <summary>
        /// Retorna una lista de claves ke esten activas en la bd. Retorna una lista vacia
        /// de no existir claves en la base.
        /// </summary>
        /// <returns></returns>
        public List<String> Obtener_ClavesActivas()
        {
            List<Clave_nom> claves = this.clavesNomC.Lista_Claves_Activas();
            List<String> clavesString = new List<string>();
            if (claves != null)
            {
                clavesString = (from clav in claves
                                select clav.codigo).ToList();
            }
            return clavesString;
        }

        public bool EstaCerrado(DateTime fecha)
        {
            return this.mesCerradoC.EstaCerrado(fecha);
        }
        public bool ExistenIncidenciasMes(int mes, int anno)
        {
            bool result = false;
            try
            {
                result = this.incidenciaC.ExistenIncidenciasMes(mes, anno);
            }
            catch (Exception)
            {
                return false;
            }
            return result;
        }

        /// <summary>
        /// Retorna true de poder cerrra correctamente el mes. False en caso contrario
        /// </summary>
        public bool CerrarMes(DateTime fecha)
        {
            try
            {
                Mes_cerrado cerrado = this.mesCerradoC.GetMesCerradoPorFecha(fecha);
                if (cerrado != null)
                {
                    cerrado.cerrado = Utils.SI;
                    this.mesCerradoC.Editar(cerrado);
                }
                else //sino existe un mes cerrado con la fecha pasada por parametro entonces se adiciona a la bd 
                {
                    cerrado = this.mesCerradoC.Crear(fecha, Utils.SI);
                    this.mesCerradoC.Adicionar(cerrado);
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// Abre un mes seteando atributo cerrado en 0 en la bd.
        /// Retorna true en caso de abrir correctamente
        /// <param name="fecha"></param>
        /// <returns></returns>
        public bool AbrirMes(DateTime fecha)
        {
            try
            {
                Mes_cerrado cerrado = this.mesCerradoC.GetMesCerradoPorFecha(fecha);
                if (cerrado != null)
                {
                    cerrado.cerrado = Utils.NO;
                    this.mesCerradoC.Editar(cerrado);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public PersonalRH GetPersonalRHPersonaFromService(String exp)
        {
            PersonalRH persona = null;
            try
            {
                persona = this.servicio.DamePersonaxExpDeep(Convert.ToInt32(exp));
            }
            catch (Exception)
            {
                return null;
            }
            return persona;
        }
        public PersonalRH GetPersonalRHPersonaFromServiceCI(String ci)
        {
            PersonalRH persona = null;
            try
            {
                persona = this.servicio.DamePersonaxCiDeep(ci);

            }
            catch (Exception)
            {
                return null;
            }
            return persona;
        }

        /// <summary>
        /// Obtiene un area dado su id. Retorna null de no existir el area en la bd con el id pasado por parametro
        /// </summary>
        public Area ObtenerAreaPorId(int idArea)
        {
            return this.areaC.GetAreaDiccio(idArea);
        }
        /// <summary>
        /// Obtiene un grupo dado su id. Retorna null de no existir el grupo en la bd con el id pasado por parametro
        /// </summary>
        public Grupo ObtenerGrupoPorId(int idGrupo)
        {
            return this.grupoC.GetGrupo(idGrupo);
        }

        public GrupoEvaluacion ObtenerGrupoEvaluacionPorId(int idGrupoEvaluacion)
        {
            return this.GrupoEvaluacionC.GetGrupoEvaluacion(idGrupoEvaluacion);
        }

        /// <summary>
        /// Tiene en cuenta solo las personas activas
        /// </summary>
        /// <returns></returns>
        public Persona ObtenerPersonaPorExp(int exp)
        {
            return this.personaC.GetPersonaPorExpediente(exp);
        }
        /// <summary>
        /// Retorna una Persona, activas o no
        /// Retorna null de no existir la persona en la bd
        /// </summary>
        public Persona ObtenerPersonaPorExp_Activa_o_noAct(int exp)
        {
            return this.personaC.GetPersonaPorExpediente_Activa_o_NoAct(exp);
        }

        /// <summary>
        /// Arroja una exception de no existir una de las personas con exp de la lista de expedientes pasado por parametro
        /// </summary>
        /// <param name="expedientes"></param>
        /// <returns></returns>
        public List<Persona> ObtenerListPersonasPorExp_Activa_o_noAct(List<int> expedientes)
        {
            List<Persona> personas = new List<Persona>();
            Persona person;
            foreach (var exp in expedientes)
            {
                person = this.ObtenerPersonaPorExp_Activa_o_noAct(Convert.ToInt32(exp));
                if (person != null)
                    personas.Add(person);
                else throw new Exception("No existe en la base de datos la persona con exp: " + exp.ToString());
            }

            return personas;
        }

        public List<Persona> ObtenerListPersonasPorExp_Activa_o_noAct(int exp)
        {
            return this.personaC.GetPersonasPorExp(exp);
        }

        public bool EditarPersona(Persona person)
        {
            try
            {
                this.personaC.Editar(person);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool InsertarPersona(Grupo grupo, int exp, string ci, Area area, GrupoEvaluacion grupoEvaluacion)
        {
            try
            {
                Persona person = this.personaC.Crear(grupo, exp, ci, area, grupoEvaluacion);
                this.personaC.Adicionar(person);
                Password pass = new Password();
                pass.password1 = this.GetHMAC(person.exp.ToString());
                person.Password.Add(pass);
                this.personaC.Editar(person);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public List<Persona> ObtenerPersonasBajoControlDe(Persona secretaria)
        {
            List<Area> areas = this.ObtenerAreasControlaPersona(logueado);
            List<Persona> personas = new List<Persona>();

            foreach (var area in areas)
            {
                personas.AddRange(this.AreaC.PersonasActivasQuePertenecenArea(area));
            }
            return personas;
        }

        public Agrupacion_nom ObtenerAgrupacionId(int idAgrup)
        {
            return agrupC.GetAgrupacion_nom(idAgrup);
        }
        public Persona ObtenerPersonaId(int idPersona)
        {
            return personaC.GetPersona(idPersona, true);
        }
        public Area CrearNuevaArea(Agrupacion_nom agrupacion, Persona personaJefe, string nombreArea)
        {
            return areaC.Crear(agrupacion, nombreArea, personaJefe);
        }
        public void AdicionarArea(Area area)
        {
            areaC.Adicionar(area);
        }
        public bool llenar_Lista_personas_Controlan_Area(Area area, List<Persona> personas)
        {
            Responsable_area resp = new Responsable_area();
            List<Responsable_area> responsablesArea = new List<Responsable_area>();
            foreach (Persona per in personas)
            {
                resp = this.responsableAreaC.Crear(area, per);
                responsablesArea.Add(resp);
            }

            if (responsablesArea.Count > 0)
                return this.areaC.AdicionarResponsables_Area(area, responsablesArea);
            return false;
        }
        public bool Update_PersonasControlarArea(Area area, List<Persona> personas)
        {
            try
            {
                List<Responsable_area> responsablesActual = this.responsableAreaC.GetResponsable_area_por_idArea(area.id_area);

                Responsable_area resp;
                List<Responsable_area> respNuevoAdicionar = new List<Responsable_area>();
                foreach (Persona person in personas)//los nuevos los inserto
                {
                    resp = this.responsableAreaC.GetResponsable_area(area.id_area, person.id_Persona);
                    if (resp == null)//sino esta lo inserto
                    {
                        resp = this.responsableAreaC.Crear(area, person);
                        respNuevoAdicionar.Add(resp);
                    }
                }
                this.areaC.AdicionarResponsables_Area(area, respNuevoAdicionar);

                foreach (Responsable_area respons in responsablesActual)//los ke no vienen del visual es porke el usuario no los desea por lo tanto se eliminan
                {
                    if (!personas.Exists(p => p.id_Persona == respons.id_persona))
                        this.responsableAreaC.Borrar(respons);
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public List<Persona> ObtenerPersonasControlanArea(Area area)
        {
            List<Persona> responsables = new List<Persona>();
            foreach (var item in this.areaC.ObtenerResponsablesArea(area))
            {
                responsables.Add(item.Persona);
            }
            return responsables;
        }
        public bool EditarArea(Area area)
        {
            try
            {
                this.areaC.Editar(area);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        public void SetearJefeArea(Area area, Persona jefe)
        {
            this.areaC.SetearJefeArea(area, jefe);
        }
        /// <summary>
        /// Devuelve un listado con todas las agrupaciones que están en la base de datos.
        /// </summary>
        /// <returns></returns>
        public List<Agrupacion_nom> ObtenerListaAgrupaciones()
        {
            List<Agrupacion_nom> lista = new List<Agrupacion_nom>();
            lista = agrupC.Agrupaciones.ToList();
            return lista;
        }

        /// <summary>
        /// Devuelve un listado con todas las personas que están en la base de datos.
        /// </summary>
        /// <returns></returns>
        public List<Persona> ObtenerListaPersonas()
        {
            List<Persona> lista = new List<Persona>();
            lista = personaC.Personas.OrderBy(t => t.exp).ToList(); ;
            return lista;
        }

        #region Nomenclador de claves
        public Clave_nom CrearNuevaClave(string codigo, string descripcion, int activo, int planificable, int descuento)
        {
            return clavesNomC.Crear(descripcion, activo, planificable, codigo, descuento);
        }
        public bool AdicionarClave(Clave_nom clave)
        {
            return clavesNomC.Adicionar(clave);
        }
        public List<Clave_nom> Listado_Claves()
        {
            return clavesNomC.Lista_Claves();
        }
        public bool Modificar_Clave(Clave_nom clave)
        {
            return clavesNomC.Editar_Clave(clave);
        }
        public bool Econtrar_Clave(Clave_nom clave)
        {
            return clavesNomC.ExisteClave(clave);
        }
        public List<GrupoEvaluacion> Listado_Grupos()
        {
            return GrupoEvaluacionC.Lista_GruposEvaluacion();
        }

        public GrupoEvaluacion CrearNuevoGrupoEvaluacion(string descripcion, decimal cds)
        {
            return GrupoEvaluacionC.Crear(descripcion, cds);
        }

        public bool AdicionarGrupoEvaluacion(GrupoEvaluacion grupo)
        {
            return GrupoEvaluacionC.Adicionar(grupo);
        }
        public bool Modificar_GrupoEvaluacion(GrupoEvaluacion grupo)
        {
            return GrupoEvaluacionC.Editar_GrupoEvaluacion(grupo);
        }




        #endregion

        #region Agrupación
        public Agrupacion_nom CrearAgrupacion(string nombre, string codigo)
        {
            return agrupC.Crear(codigo, nombre);
        }
        public bool Insertar_Agrupacion(Agrupacion_nom agrup)
        {
            return agrupC.Adicionar(agrup);
        }
        public bool Editar_Agrupacion(Agrupacion_nom agrup)
        {
            return agrupC.Editar(agrup);
        }
        #endregion

        #region Grupo y período de tiempo

        public bool AdicionarGrupo(String nombreGrupo, int activo, List<Periodo_tiempo> periodosTiempo)
        {
            try
            {
                Grupo grupo = this.grupoC.Crear(nombreGrupo, activo);
                foreach (Periodo_tiempo periodo in periodosTiempo)
                {
                    Forma_trabajo forma = this.formaTrabajoC.Crear(grupo, periodo);
                    this.grupoC.SetFormaTrabajoAGrupo(forma, grupo);
                }
                this.grupoC.Adicionar(grupo);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool EditarGrupo(String nombre, int activo, List<Periodo_tiempo> periodosTiempo, Grupo grupo)
        {
            try
            {
                grupo.nombre = nombre;
                grupo.activo = activo;
                List<Forma_trabajo> formasT = new List<Forma_trabajo>();
                this.BorrarFormasDeTrabajoDelGrupo(grupo);
                foreach (Periodo_tiempo periodo in periodosTiempo)
                {
                    Forma_trabajo forma = this.formaTrabajoC.Crear(grupo, periodo);
                    formasT.Add(forma);
                }
                this.grupoC.SetFormasTrabajoAGrupo(formasT, grupo);
                this.grupoC.Editar(grupo);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public void BorrarFormasDeTrabajoDelGrupo(Grupo grupo)
        {
            foreach (Forma_trabajo format in this.grupoC.GetFormasDeTrabajo(grupo))
            {
                this.formaTrabajoC.Borrar(format);
            }
        }

        public List<Periodo_tiempo> GetPeriodosDeTiempoDeGrupo(int idgrupo)
        {
            return this.grupoC.GetPeriodoDeTiempoDeGrupo(idgrupo);
        }

        public List<Periodo_tiempo> GetPeriodosDeTiempoNoDeGrupo(int idgrupo)
        {
            return this.grupoC.GetPeriodoDeTiempoNoDeGrupo(idgrupo);
        }

        public List<Grupo> Lista_Grupos()
        {
            return grupoC.Grupos;
        }

        #endregion

        public RegistroESpejo[] GetTrazasRangoXListadoExpediente(DateTime fechaInicio, DateTime fechaFin, int exp)
        {
            RegistroESpejo[] trazasIncidencias; //se obtiene del uso del servicio web

            var serv = new Service1Client();
            int[] exped = new int[] { exp };

            trazasIncidencias = serv.TrazasEnRangoxListadoExp(fechaInicio, fechaFin, exped);
            return trazasIncidencias;
        }

        /**Carlo´s code **/
        /// <summary>
        /// Función para determinar las incidencias de una persona en un periodo de tiempo. 
        /// </summary>
        /// <param name="exp"> el expediente de la persona a analizar</param>
        /// <param name="inicial">limite inferior del rango de fechas</param>
        /// <param name="final">limite superior del rango de fechas</param>
        /// <param name="trazasusuario">lista de las trazas de esa persona en ese rango de fecha devuelto por la funcion TrazasEnRangoxListadoExp() del servicio web </param>
        /// <returns>Devuelve la estructura PersonaNegocio que contiene el expediente de la persona y una lista de jornadas laborales con sus incidencias para cada jornada. </returns>
        public PersonaNegocio CargarIncidencias(int exp, DateTime inicial, DateTime final, List<RegistroESpejo> trazasusuario)
        {
            Persona Trabajador = personaC.GetPersonaPorExpediente(exp);//Busco la persona a la que corresponde el expediente a priori
            var GrupoPeriodo = grupoC.GetPeriodoDeTiempoDeGrupo(Trabajador.Grupo);//Genero los grupos de períodos antes de recorrerlos, fuera de todo ciclo
            List<Jornada> listajornada = new List<Jornada>();
            DateTime dia = inicial;
            List<Planificacion> planificaciones = new List<Planificacion>();
            foreach (var a in planificacionAnticipadaC.Planificaciones)
                if (a.Persona.exp == exp && a.fecha_fin >= inicial && a.fecha_inicio <= final)
                    planificaciones.Add(a);//Disminuye significativamente las planificaciones a revisar más adelante de forma repetitiva ya que las acota por fecha y expediente

            List<Incidencia> incidencias = incidenciaC.Incidencias.FindAll(t => t.fecha.Date >= inicial && t.fecha.Date <= final && t.Persona.exp == exp);

            while (dia <= final)
            {
                List<Traza> listatraza = new List<Traza>();
                #region dias planificados o feriados
                var diaplanificado = from todos in planificaciones
                                     where
                                         ((todos.fecha_inicio.Value.Date < dia.Date && todos.fecha_fin.Value.Date > dia.Date) ||
                                          dia.Date == todos.fecha_inicio.Value.Date || dia.Date == todos.fecha_fin.Value.Date)
                                     select todos;
                if (diaplanificado.Any()) //tienes planificaciones este dia
                {
                    dia = dia.AddDays(1);//hablar con carlo. Se adiciono esta linea para incrementar el dia y se cambio break por continue.
                    //break;
                    continue; //salto al proximo
                }
                /*var diaferiado= from todos in  noLaboralesC.Dias_no_laborables //esto es para el análisis de los dias feriados
                                where todos.diaNoLaborable_Grupo.*/
                Persona perso = personaC.GetPersonaPorExpediente(exp);
                if (perso != null)
                    if (perso != null && Obtener_DiasNolaborablesDeGrupo((int)perso.id_Grupo).Find(t => t.fecha.Date == dia.Date) != null)
                    {
                        dia = dia.AddDays(1);
                        continue;
                    }
                #endregion
                #region incidencias ya existen
                foreach (var incidencia in incidencias.FindAll(t => t.fecha.Date == dia.Date))
                {
                    DateTime fecha = incidencia.fecha;
                    TimeSpan span = new TimeSpan(incidencia.fecha.Hour, incidencia.fecha.Minute,
                        incidencia.fecha.Second);
                    string clavita = string.Empty;
                    if (incidencia.Clave_nom != null)
                    {
                        clavita = incidencia.Clave_nom.codigo;
                    }
                    TipoTraza tipito = Traza.TipoTrazaFromDescripcion(incidencia.tipotraza);
                    int id = incidencia.id_incidencia;
                    string observacion = string.Empty;
                    if (incidencia.observacion != null)
                    {
                        observacion = incidencia.observacion;
                    }
                    Traza nueva = new Traza(fecha, span, clavita, tipito, id, observacion);
                    listatraza.Add(nueva);
                }
                if (listatraza.Count > 0)
                {
                    Jornada existe = new Jornada(listatraza);
                    listajornada.Add(existe);
                }
                #endregion

                else
                {
                    #region Periodos
                    if (perso != null)
                        foreach (var periodos in GrupoPeriodo)
                        {
                            DateTime limitebajo = new DateTime(dia.Year, dia.Month, dia.Day,
                                periodos.hora_entrada.Value.Hours, periodos.hora_entrada.Value.Minutes,
                                periodos.hora_entrada.Value.Seconds);
                            limitebajo = limitebajo.AddHours(Convert.ToDouble(24 - periodos.cantidad_horas) / -2);
                            DateTime limitealto =
                                limitebajo.AddHours(Convert.ToDouble(periodos.cantidad_horas))
                                    .AddMinutes(Convert.ToDouble(periodos.minuto_almuerzo))
                                    .AddHours(Convert.ToDouble(24 - periodos.cantidad_horas));
                            if (dia.Subtract(periodos.fecha_inicio.Value).Days % periodos.dias_periodo == 0)
                            //entonces debes venir, es un dia laborable  
                            {
                                DateTime entrar = new DateTime(dia.Year, dia.Month, dia.Day,
                                    periodos.hora_entrada.Value.Hours, periodos.hora_entrada.Value.Minutes,
                                    periodos.hora_entrada.Value.Seconds);

                                DateTime salir = entrar.AddHours(Convert.ToDouble(periodos.cantidad_horas))
                                    .AddMinutes(Convert.ToDouble(periodos.minuto_almuerzo)).AddSeconds(-59);
                                //si es un dia laborable para ti debes tener trazas
                                //traza.fecha debe estar entre el DIA con hora=horaentrada -(24-horastrabajo/2)y el DIA con hora=horaentrada+cantidad de horas+minutos de almuerzo+(dia-horastrabajo/2) 
                                List<RegistroESpejo> aux = trazasusuario.FindAll(t => t.Fecha > limitebajo && t.Fecha < limitealto);
                                // lista de registros que viene por parametro
                                if (aux.Count == 0) //esto es k no viniste y te tocaba
                                {
                                    Traza nueva = new Traza(new DateTime(dia.Year, dia.Month, dia.Day, periodos.hora_entrada.Value.Hours, periodos.hora_entrada.Value.Minutes, periodos.hora_entrada.Value.Seconds), "AI", TipoTraza.N);
                                    //esto es que la persona no vino a trabajar este dia y le pongo Ausencia Injustificada por defecto
                                    listatraza.Add(nueva);
                                }
                                else
                                    if (aux.Count == 1) //esto es k viniste y solo marcaste entrada
                                {
                                    if (aux.First().Fecha > entrar) //llegaste tarde
                                    {
                                        Traza nueva = new Traza(aux.First().Fecha, aux.First().Fecha.TimeOfDay, "TI",
                                            Traza.TipoTrazaFromDescripcion(aux.First().Tipo));
                                        listatraza.Add(nueva);
                                    }
                                    else
                                    {
                                        Traza nueva = new Traza(aux.First().Fecha, aux.First().Fecha.TimeOfDay, "B",
                                           Traza.TipoTrazaFromDescripcion(aux.First().Tipo));
                                        listatraza.Add(nueva);
                                    }
                                    listatraza.Add(new Traza(aux.First().Fecha, "SI", TipoTraza.N));
                                }

                                else
                                {
                                    if (aux.Count == 2)
                                    {
                                        if (aux.First().Fecha > entrar || aux.Last().Fecha < salir)
                                        //llegaste tarde o te fuiste antes
                                        {

                                            if (aux.First().Fecha > entrar) //llegaste tarde
                                            {
                                                Traza nueva = new Traza(aux.First().Fecha, aux.First().Fecha.TimeOfDay, "TI",
                                                    Traza.TipoTrazaFromDescripcion(aux.First().Tipo));
                                                listatraza.Add(nueva);
                                            }
                                            if (aux.Last().Fecha < salir) //te fuiste antes
                                            {
                                                Traza nueva = new Traza(aux.Last().Fecha, aux.Last().Fecha.TimeOfDay, "SI",
                                                    Traza.TipoTrazaFromDescripcion(aux.Last().Tipo));
                                                listatraza.Add(nueva);
                                            }
                                        }
                                    }
                                    else
                                    {//tienes + trazas de las permitidas hay que ponerte clave en todas ..
                                        if (aux.First().Fecha > entrar && aux.First().Tipo == "E") //llegaste tarde
                                        {
                                            Traza nueva = new Traza(aux.First().Fecha, aux.First().Fecha.TimeOfDay, "TI",
                                                Traza.TipoTrazaFromDescripcion(aux.First().Tipo));
                                            listatraza.Add(nueva);
                                        }
                                        else
                                        {
                                            Traza nueva = new Traza(aux.First().Fecha, aux.First().Fecha.TimeOfDay, "B",
                                               Traza.TipoTrazaFromDescripcion(aux.First().Tipo));
                                            listatraza.Add(nueva);
                                        }

                                        for (int i = 1; i < aux.Count; i++)
                                        {
                                            if (aux[i].Tipo == "E")
                                            {
                                                Traza nueva = new Traza(aux[i].Fecha, aux[i].Fecha.TimeOfDay, "B",
                                              Traza.TipoTrazaFromDescripcion(aux[i].Tipo));
                                                listatraza.Add(nueva);
                                            }
                                            else
                                            {
                                                if (aux[i].Fecha < salir) //te fuiste antes
                                                {
                                                    Traza nueva = new Traza(aux[i].Fecha, aux[i].Fecha.TimeOfDay, "SI",
                                                        Traza.TipoTrazaFromDescripcion(aux[i].Tipo));
                                                    listatraza.Add(nueva);
                                                }
                                                else
                                                {
                                                    Traza nueva = new Traza(aux[i].Fecha, aux[i].Fecha.TimeOfDay, "B",
                                              Traza.TipoTrazaFromDescripcion(aux[i].Tipo));
                                                    listatraza.Add(nueva);
                                                }
                                            }
                                        }
                                        if (aux.Count % 2 != 0)
                                        {
                                            listatraza.Add(new Traza(aux.Last().Fecha, "SI", TipoTraza.N));
                                        }
                                    }
                                }

                                if (listatraza.Count > 0)
                                {
                                    Jornada existe = new Jornada(listatraza);
                                    listajornada.Add(existe);
                                }
                                break;
                            }
                        }
                    #endregion
                }
                dia = dia.AddDays(1);
            }
            PersonaNegocio respuesta = new PersonaNegocio(exp, listajornada);
            return respuesta;
        }


        /// <summary>
        /// Funcion para salvar las incidencias cambiadas o no por la secretaria.Es llamada desde el visual
        /// </summary>
        /// <param name="personaje">estructura PersonaNegocio que contiene el expediente de la persona y una lista de jornadas laborales con sus incidencias para cada jornada.</param>
        public bool SalvarIncidencias(PersonaNegocio personaje)
        {
            try
            {
                foreach (var jornada in personaje.ListaJornada)
                {
                    foreach (var traza in jornada.ListaTraza)
                    {
                        Incidencia esta = incidenciaC.GetIncidencia(traza.IdIncidencia);

                        if (esta != null)
                        {
                            esta.Clave_nom = clavesNomC.GetClave_nom_Codigo(traza.Clave);
                            esta.observacion = traza.Observaciones;
                            incidenciaC.Editar(esta);
                        }
                        else
                        {
                            incidenciaC.Adicionar(incidenciaC.Crear(personaC.GetPersonaPorExpedienteDiccio(personaje.Exp), clavesNomC.GetClave_nom_Codigo(traza.Clave), traza.Fecha, null, traza.Tipo.ToString()));
                        }
                        ;//el valor en null son las observaciones

                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public object[,] FilaPrenomina(int mes, int ano, int exp)
        {
            /*
            int vacaciones = CalculaVacaciones(mes, ano, exp);
            int tardanzas = CalculaTardanzas(mes, ano, exp);
            int subsidios = CalculaSubsidios(mes, ano, exp);
            double perdidas = CalculaPerdidas(mes, ano, exp);
            var persona = servicio.DamePersonaxExp(exp);
            string nombre = persona.Nombre + " " + persona.Apellido1 + " " + persona.Apellido2;
            string categoria = servicio.DameCatOcupacionalxExp(exp);
           double nolaboradas = vacaciones + subsidios + perdidas;
            if (nolaboradas>24)
            {
                nolaboradas = 24;
            }
            double laboradas = 24 - nolaboradas;
            object[,] fila = new object[1, 10]
            {
                {
                    exp, nombre, categoria, tardanzas, laboradas, nolaboradas, vacaciones, subsidios, perdidas, 24
                }
            };

            return fila;*/
            DateTime inicial = new DateTime(ano, mes, 1);
            DateTime final = new DateTime(ano, mes, DateTime.DaysInMonth(ano, mes));
            if (exp == 228)
            {
                var a = exp;
            }
            return FuncionGeneral(exp, inicial, final);

        }

        public List<object[,]> FuncionGeneralPorGrupos(List<int> expedientes, DateTime fechainicio, DateTime fechafin, Dictionary<int, List<Incidencia>> DiccioIncidencia1, Dictionary<int, List<Planificacion>> DiccioPlanes1)
        {
            var DiccioIncidencia = GenerarIncidenciaExpDiccioPorFecha(fechainicio, fechafin);
            var DiccioPlanes = GenerarPlanificacionExpDiccioPorFecha(fechainicio, fechafin);

            Dictionary<int, bool> DiccioExped = new Dictionary<int, bool>();
            foreach (int a in expedientes)
            {
                if (!DiccioExped.ContainsKey(a))
                    DiccioExped.Add(a, true);
            }

            List<RegistroESpejo> trazas = new List<RegistroESpejo>();
            foreach (RegistroESpejo a in Trazas)
                if (DiccioExped.ContainsKey(a.Exp))
                    trazas.Add(a);

            //List<RegistroESpejo> trazas=servicio.TrazasEnRangoxListadoExp(fechainicio, fechafin, exped).ToList();
            List<object[,]> retorno = new List<object[,]>();
            foreach (int a in expedientes)
            {
                var temp = FuncionGeneral(a, fechainicio, fechafin, DiccioIncidencia, DiccioPlanes, trazas);
                if (temp != null)
                    retorno.Add(temp);
            }
            return retorno;
        }

        public List<object[]> FuncionGeneralPorGruposAsistencia(List<int> expedientes, DateTime fechainicio)
        {
            List<object[]> retorno = new List<object[]>();

            switch (fechainicio.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    {
                        var DiccioIncidencia = GenerarIncidenciaExpDiccioPorFecha(fechainicio, fechainicio);
                        var DiccioIncidenciaExp = new List<Incidencia>();

                        foreach (int a in expedientes)
                        {
                            if (DiccioIncidencia.ContainsKey(a))
                            {
                                DiccioIncidenciaExp.AddRange(DiccioIncidencia[a]);
                            }
                        }
                        var monday = FuncionGeneralAsistencia(DiccioIncidenciaExp);

                        object[] fila = new object[9]
                        {
                            0, 0, 0, 0, 0, 0, 0, 0, 0
                        };

                        if (monday != null)
                        {
                            retorno.Add(monday);
                        }
                        else
                        {
                            retorno.Add(fila);
                        }
                        retorno.Add(fila);
                        retorno.Add(fila);
                        retorno.Add(fila);
                        retorno.Add(fila);
                        retorno.Add(monday);
                        monday[0] = (int)monday[0] / 5f;
                        monday[1] = (int)monday[1] / 5f;
                        monday[2] = (int)monday[2] / 5f;
                        monday[3] = (int)monday[3] / 5f;
                        monday[4] = (int)monday[4] / 5f;
                        monday[5] = (int)monday[5] / 5f;
                        monday[6] = (int)monday[6] / 5f;
                        monday[7] = (int)monday[7] / 5f;
                        monday[8] = (int)monday[8] / 5f;
                        retorno.Add(monday);
                    }
                    break;
                case DayOfWeek.Tuesday:
                    {
                        var timeMonday = new TimeSpan(1, 0, 0, 0, 0);
                        DateTime mondayDate = fechainicio - timeMonday;

                        var DiccioIncidenciaExp = new List<Incidencia>();
                        var DiccioIncidenciaExpMonday = new List<Incidencia>();
                        var DiccioIncidenciaExpTuesday = new List<Incidencia>();

                        var Inciden = incidenciaC.Incidencias;
                        foreach (var a in Inciden)
                        {
                            if (a.fecha.Date == mondayDate.Date)
                                DiccioIncidenciaExpMonday.Add(a);
                            if (a.fecha.Date == fechainicio.Date)
                                DiccioIncidenciaExpTuesday.Add(a);
                            if (a.fecha.Date >= mondayDate && a.fecha.Date <= fechainicio)
                                DiccioIncidenciaExp.Add(a);

                        }

                        var resumen = FuncionGeneralAsistencia(DiccioIncidenciaExp);
                        var monday = FuncionGeneralAsistencia(DiccioIncidenciaExpMonday);
                        var tuesday = FuncionGeneralAsistencia(DiccioIncidenciaExpTuesday);

                        object[] fila = new object[9]
                        {
                            0, 0, 0, 0, 0, 0, 0, 0, 0
                        };
                        object[] filaResumen = new object[9]
                       {
                           0, 0, 0, 0, 0, 0, 0, 0, 0
                       };

                        if (monday != null)
                        {
                            retorno.Add(monday);
                        }
                        else
                        {
                            retorno.Add(fila);
                        }
                        if (tuesday != null)
                        {
                            retorno.Add(tuesday);
                        }
                        else
                        {
                            retorno.Add(fila);
                        }
                        retorno.Add(fila);
                        retorno.Add(fila);
                        retorno.Add(fila);
                        if (resumen != null)
                        {
                            retorno.Add(resumen);
                            filaResumen[0] = (int)resumen[0] / 5f;
                            filaResumen[1] = (int)resumen[1] / 5f;
                            filaResumen[2] = (int)resumen[2] / 5f;
                            filaResumen[3] = (int)resumen[3] / 5f;
                            filaResumen[4] = (int)resumen[4] / 5f;
                            filaResumen[5] = (int)resumen[5] / 5f;
                            filaResumen[6] = (int)resumen[6] / 5f;
                            filaResumen[7] = (int)resumen[7] / 5f;
                            filaResumen[8] = (int)resumen[8] / 5f;
                            retorno.Add(filaResumen);

                        }
                        else
                        {
                            retorno.Add(fila);
                            retorno.Add(fila);
                        }
                    }
                    break;
                case DayOfWeek.Wednesday:
                    {
                        var timeTuesday = new TimeSpan(1, 0, 0, 0, 0);
                        DateTime tuesdayDate = fechainicio - timeTuesday;
                        var timeMonday = new TimeSpan(2, 0, 0, 0, 0);
                        DateTime mondayDate = fechainicio - timeMonday;

                        var DiccioIncidenciaExp = new List<Incidencia>();
                        var DiccioIncidenciaExpMonday = new List<Incidencia>();
                        var DiccioIncidenciaExpTuesday = new List<Incidencia>();
                        var DiccioIncidenciaExpWednesday = new List<Incidencia>();

                        var Inciden = incidenciaC.Incidencias;
                        foreach (var a in Inciden)
                        {
                            if (a.fecha.Date == mondayDate.Date)
                                DiccioIncidenciaExpMonday.Add(a);
                            if (a.fecha.Date == tuesdayDate.Date)
                                DiccioIncidenciaExpTuesday.Add(a);
                            if (a.fecha.Date == fechainicio.Date)
                                DiccioIncidenciaExpWednesday.Add(a);
                            if (a.fecha.Date >= mondayDate && a.fecha.Date <= fechainicio)
                                DiccioIncidenciaExp.Add(a);
                        }

                        var resumen = FuncionGeneralAsistencia(DiccioIncidenciaExp);
                        var monday = FuncionGeneralAsistencia(DiccioIncidenciaExpMonday);
                        var tuesday = FuncionGeneralAsistencia(DiccioIncidenciaExpTuesday);
                        var wednesday = FuncionGeneralAsistencia(DiccioIncidenciaExpWednesday);

                        object[] fila = new object[9]
                        {
                            0, 0, 0, 0, 0, 0, 0, 0, 0
                        };
                        object[] filaResumen = new object[9]
                       {
                           0, 0, 0, 0, 0, 0, 0, 0, 0
                       };

                        if (monday != null)
                        {
                            retorno.Add(monday);
                        }
                        else
                        {
                            retorno.Add(fila);
                        }
                        if (tuesday != null)
                        {
                            retorno.Add(tuesday);
                        }
                        else
                        {
                            retorno.Add(fila);
                        }
                        if (wednesday != null)
                        {
                            retorno.Add(wednesday);
                        }
                        else
                        {
                            retorno.Add(fila);
                        }
                        retorno.Add(fila);
                        retorno.Add(fila);
                        if (resumen != null)
                        {
                            retorno.Add(resumen);
                            filaResumen[0] = (int)resumen[0] / 5f;
                            filaResumen[1] = (int)resumen[1] / 5f;
                            filaResumen[2] = (int)resumen[2] / 5f;
                            filaResumen[3] = (int)resumen[3] / 5f;
                            filaResumen[4] = (int)resumen[4] / 5f;
                            filaResumen[5] = (int)resumen[5] / 5f;
                            filaResumen[6] = (int)resumen[6] / 5f;
                            filaResumen[7] = (int)resumen[7] / 5f;
                            filaResumen[8] = (int)resumen[8] / 5f;
                            retorno.Add(filaResumen);

                        }
                        else
                        {
                            retorno.Add(fila);
                            retorno.Add(fila);
                        }
                    }
                    break;
                case DayOfWeek.Thursday:
                    {
                        var timeWednesday = new TimeSpan(1, 0, 0, 0, 0);
                        DateTime wednesdayDate = fechainicio - timeWednesday;
                        var timeTuesday = new TimeSpan(2, 0, 0, 0, 0);
                        DateTime tuesdayDate = fechainicio - timeTuesday;
                        var timeMonday = new TimeSpan(3, 0, 0, 0, 0);
                        DateTime mondayDate = fechainicio - timeMonday;

                        var DiccioIncidenciaExp = new List<Incidencia>();
                        var DiccioIncidenciaExpMonday = new List<Incidencia>();
                        var DiccioIncidenciaExpTuesday = new List<Incidencia>();
                        var DiccioIncidenciaExpWednesday = new List<Incidencia>();
                        var DiccioIncidenciaExpThursday = new List<Incidencia>();

                        var Inciden = incidenciaC.Incidencias;
                        foreach (var a in Inciden)
                        {
                            if (a.fecha.Date == mondayDate.Date)
                                DiccioIncidenciaExpMonday.Add(a);
                            if (a.fecha.Date == tuesdayDate.Date)
                                DiccioIncidenciaExpTuesday.Add(a);
                            if (a.fecha.Date == wednesdayDate.Date)
                                DiccioIncidenciaExpWednesday.Add(a);
                            if (a.fecha.Date == fechainicio.Date)
                                DiccioIncidenciaExpThursday.Add(a);
                            if (a.fecha.Date >= mondayDate && a.fecha.Date <= fechainicio)
                                DiccioIncidenciaExp.Add(a);
                        }

                        var resumen = FuncionGeneralAsistencia(DiccioIncidenciaExp);
                        var monday = FuncionGeneralAsistencia(DiccioIncidenciaExpMonday);
                        var tuesday = FuncionGeneralAsistencia(DiccioIncidenciaExpTuesday);
                        var wednesday = FuncionGeneralAsistencia(DiccioIncidenciaExpWednesday);
                        var thursday = FuncionGeneralAsistencia(DiccioIncidenciaExpThursday);

                        object[] fila = new object[9]
                        {
                            0, 0, 0, 0, 0, 0, 0, 0, 0
                        };
                        object[] filaResumen = new object[9]
                       {
                           0, 0, 0, 0, 0, 0, 0, 0, 0
                       };

                        if (monday != null)
                        {
                            retorno.Add(monday);
                        }
                        else
                        {
                            retorno.Add(fila);
                        }
                        if (tuesday != null)
                        {
                            retorno.Add(tuesday);
                        }
                        else
                        {
                            retorno.Add(fila);
                        }
                        if (wednesday != null)
                        {
                            retorno.Add(wednesday);
                        }
                        else
                        {
                            retorno.Add(fila);
                        }
                        if (thursday != null)
                        {
                            retorno.Add(thursday);
                        }
                        else
                        {
                            retorno.Add(fila);
                        }
                        retorno.Add(fila);
                        if (resumen != null)
                        {
                            retorno.Add(resumen);
                            filaResumen[0] = (int)resumen[0] / 5f;
                            filaResumen[1] = (int)resumen[1] / 5f;
                            filaResumen[2] = (int)resumen[2] / 5f;
                            filaResumen[3] = (int)resumen[3] / 5f;
                            filaResumen[4] = (int)resumen[4] / 5f;
                            filaResumen[5] = (int)resumen[5] / 5f;
                            filaResumen[6] = (int)resumen[6] / 5f;
                            filaResumen[7] = (int)resumen[7] / 5f;
                            filaResumen[8] = (int)resumen[8] / 5f;
                            retorno.Add(filaResumen);

                        }
                        else
                        {
                            retorno.Add(fila);
                        }
                    }
                    break;
                case DayOfWeek.Friday:
                    {
                        var timethursday = new TimeSpan(1, 0, 0, 0, 0);
                        DateTime thursdayDate = fechainicio - timethursday;
                        var timeWednesday = new TimeSpan(2, 0, 0, 0, 0);
                        DateTime wednesdayDate = fechainicio - timeWednesday;
                        var timeTuesday = new TimeSpan(3, 0, 0, 0, 0);
                        DateTime tuesdayDate = fechainicio - timeTuesday;
                        var timeMonday = new TimeSpan(4, 0, 0, 0, 0);
                        DateTime mondayDate = fechainicio - timeMonday;

                        var DiccioIncidenciaExp = new List<Incidencia>();
                        var DiccioIncidenciaExpMonday = new List<Incidencia>();
                        var DiccioIncidenciaExpTuesday = new List<Incidencia>();
                        var DiccioIncidenciaExpWednesday = new List<Incidencia>();
                        var DiccioIncidenciaExpThursday = new List<Incidencia>();
                        var DiccioIncidenciaExpFriday = new List<Incidencia>();

                        var Inciden = incidenciaC.Incidencias;
                        foreach (var a in Inciden)
                        {
                            if (a.fecha.Date == mondayDate.Date)
                                DiccioIncidenciaExpMonday.Add(a);
                            if (a.fecha.Date == tuesdayDate.Date)
                                DiccioIncidenciaExpTuesday.Add(a);
                            if (a.fecha.Date == wednesdayDate.Date)
                                DiccioIncidenciaExpWednesday.Add(a);
                            if (a.fecha.Date == thursdayDate.Date)
                                DiccioIncidenciaExpThursday.Add(a);
                            if (a.fecha.Date == fechainicio.Date)
                                DiccioIncidenciaExpFriday.Add(a);
                            if (a.fecha.Date >= mondayDate && a.fecha.Date <= fechainicio)
                                DiccioIncidenciaExp.Add(a);
                        }

                        var resumen = FuncionGeneralAsistencia(DiccioIncidenciaExp);
                        var monday = FuncionGeneralAsistencia(DiccioIncidenciaExpMonday);
                        var tuesday = FuncionGeneralAsistencia(DiccioIncidenciaExpTuesday);
                        var wednesday = FuncionGeneralAsistencia(DiccioIncidenciaExpWednesday);
                        var thursday = FuncionGeneralAsistencia(DiccioIncidenciaExpThursday);
                        var friday = FuncionGeneralAsistencia(DiccioIncidenciaExpFriday);

                        object[] fila = new object[9]
                        {
                            0, 0, 0, 0, 0, 0, 0, 0, 0
                        };
                        object[] filaResumen = new object[9]
                       {
                           0, 0, 0, 0, 0, 0, 0, 0, 0
                       };

                        if (monday != null)
                        {
                            retorno.Add(monday);
                        }
                        else
                        {
                            retorno.Add(fila);
                        }
                        if (tuesday != null)
                        {
                            retorno.Add(tuesday);
                        }
                        else
                        {
                            retorno.Add(fila);
                        }
                        if (wednesday != null)
                        {
                            retorno.Add(wednesday);
                        }
                        else
                        {
                            retorno.Add(fila);
                        }
                        if (thursday != null)
                        {
                            retorno.Add(thursday);
                        }
                        else
                        {
                            retorno.Add(fila);
                        }
                        if (friday != null)
                        {
                            retorno.Add(friday);
                        }
                        else
                        {
                            retorno.Add(fila);
                        }
                        if (resumen != null)
                        {
                            retorno.Add(resumen);
                            filaResumen[0] = (int)resumen[0] / 5f;
                            filaResumen[1] = (int)resumen[1] / 5f;
                            filaResumen[2] = (int)resumen[2] / 5f;
                            filaResumen[3] = (int)resumen[3] / 5f;
                            filaResumen[4] = (int)resumen[4] / 5f;
                            filaResumen[5] = (int)resumen[5] / 5f;
                            filaResumen[6] = (int)resumen[6] / 5f;
                            filaResumen[7] = (int)resumen[7] / 5f;
                            filaResumen[8] = (int)resumen[8] / 5f;
                            retorno.Add(filaResumen);

                        }
                        else
                        {
                            retorno.Add(fila);
                        }
                    }
                    break;
                default:
                    break;
            }
            return retorno;
        }

        public List<object[,]> FuncionGeneralPorGruposTrabajadores(List<int> expedientes)
        {
            List<object[,]> retorno = new List<object[,]>();
            foreach (int a in expedientes)
            {
                var temp = FuncionGeneralTrabajadores(a);
                if (temp != null)
                    retorno.Add(temp);
            }
            return retorno;
        }

        public List<object[,]> FuncionGeneralPorGruposRegistros(List<int> expedientes, DateTime fecha)
        {
            List<object[,]> retorno = new List<object[,]>();
            foreach (int a in expedientes)
            {
                var temp = FuncionGeneralRegistros(a, fecha);
                if (temp != null)
                    retorno.Add(temp);
            }
            return retorno;
        }

        Dictionary<int, List<Incidencia>> GenerarIncidenciaExpDiccioPorFecha(DateTime fechainicio, DateTime fechafin)
        {
            Dictionary<int, List<Incidencia>> Diccio = new Dictionary<int, List<Incidencia>>();
            var Inciden = incidenciaC.Incidencias;
            foreach (var a in Inciden)
            {
                if (!Diccio.ContainsKey(a.Persona.exp))
                    Diccio.Add(a.Persona.exp, new List<Incidencia>());
                if (a.fecha >= fechainicio && a.fecha < fechafin.AddDays(1))
                    Diccio[a.Persona.exp].Add(a);

            }
            return Diccio;
        }

        Dictionary<int, List<Planificacion>> GenerarPlanificacionExpDiccioPorFecha(DateTime fechainicio, DateTime fechafin)
        {
            Dictionary<int, List<Planificacion>> Diccio = new Dictionary<int, List<Planificacion>>();
            foreach (var a in planificacionAnticipadaC.Planificaciones)
            {
                if (!Diccio.ContainsKey(a.Persona.exp))
                    Diccio.Add(a.Persona.exp, new List<Planificacion>());
                if (a.fecha_fin >= fechainicio && a.fecha_inicio <= fechafin)
                    Diccio[a.Persona.exp].Add(a);
            }
            return Diccio;
        }

        public object[,] FuncionGeneral(int exp, DateTime fechainicio, DateTime fechafin, Dictionary<int, List<Incidencia>> ExpIncidencia = null, Dictionary<int, List<Planificacion>> Planes = null, List<RegistroESpejo> TrazasXRango = null)
        {
            List<Jornada> listajornada = new List<Jornada>();
            List<Planificacion> planificaciones;
            if (Planes == null)
            {
                planificaciones = new List<Planificacion>();
                foreach (var a in planificacionAnticipadaC.Planificaciones)
                    if (a.Persona.exp == exp && a.fecha_fin >= fechainicio && a.fecha_inicio <= fechafin) //Genero la lista filtrada por expediente y fecha en rango de planificaciones
                        planificaciones.Add(a);//Disminuye significativamente las planificaciones a revisar más adelante de forma repetitiva
            }
            else if (Planes.ContainsKey(exp))
                planificaciones = Planes[exp];
            else
                planificaciones = new List<Planificacion>();

            List<Incidencia> incidencias;
            if (ExpIncidencia == null)
                incidencias = incidenciaC.Incidencias.FindAll(t => t.fecha.Date >= fechainicio && t.fecha.Date <= fechafin && t.Persona.exp == exp);//Acoto la lista de incidencias por fecha y expediente
            else if (ExpIncidencia.ContainsKey(exp))
                incidencias = ExpIncidencia[exp];
            else
                incidencias = new List<Incidencia>();
            double CantHorasMaximas = horasMaximas;//Hay que arreglar el método para poder pasar el valor por parámetros ya que esta es la cantidad de horas de un mes determinado (En este caso junio)

            double vacaciones = 0;
            double tardanzas = 0;
            double subsidios = 0;
            double EUyMov = 0;
            double diaFeriado = 0;
            double perdidas = 0;
            double interruptos = 0;
            double GS1 = 0;
            double GS6 = 0;
            PersonalRH persona;
            try
            {
                persona = servicio.DamePersonaxExpDeep(exp);
            }
            catch
            {
                return null;
            }
            string nombre = persona.Nombre + " " + persona.Apellido1 + " " + persona.Apellido2;
            int[] exped = new int[] { exp };
            DateTime inicial = fechainicio.Date;
            DateTime final = fechafin.Date;
            tardanzas = this.CalculaTardanzas(inicial.Month, inicial.Year, exp, incidencias, planificaciones);
            List<RegistroESpejo> todas;
            if (TrazasXRango == null)
                todas = servicio.TrazasEnRangoxListadoExp(inicial, final, exped).ToList();
            else
            {
                todas = new List<RegistroESpejo>();
                foreach (var a in TrazasXRango)
                    if (a.Exp == exp)
                        todas.Add(a);

            }
            DateTime dia = inicial;
            var periodostime = grupoC.GetPeriodoDeTiempoDeGrupo(personaC.GetPersonaPorExpedienteDiccio(exp).Grupo);
            #region Principal

            while (dia <= final)
            {

                #region dias planificados o feriados
                //Herramienta utilizada para tracear la función solo para el expediente deseado
                //var a = 0;    
                //if (exp == 299 && dia.Day == 3)
                //{ 
                //    int ya = exp; 
                //}

                int essabado = (int)dia.DayOfWeek;

                //Optimizado, ahora no busca directamente de todas las planificaciones, sino de un conjunto mucho más pequeño y de interés
                var pln = planificaciones.Find(t =>
                            ((t.fecha_inicio.Value.Date < dia.Date && t.fecha_fin.Value.Date > dia.Date) ||
                             dia.Date == t.fecha_inicio.Value.Date || dia.Date == t.fecha_fin.Value.Date));
                //if (pln != null)
                //{
                //    if (pln.Clave_nom.codigo == "VC")
                //    {
                //        vacaciones += periodos.cantidad_horas.GetValueOrDefault(0);
                //    }
                //    else if (pln.Clave_nom.codigo == "CM")
                //    {
                //        subsidios += periodos.cantidad_horas.GetValueOrDefault(0);
                //    }
                //    else if (clavesNomC.Descuenta(pln.Clave_nom))
                //    {
                //        foreach (var periodoTiempo in periodostime)
                //        {
                //            if (dia.Subtract(periodoTiempo.fecha_inicio.Value).Days % periodoTiempo.dias_periodo == 0)
                //            {
                //                if (periodoTiempo.cantidad_horas.GetValueOrDefault(0)==8)
                //                {
                //                    perdidasviernes += periodoTiempo.cantidad_horas.GetValueOrDefault(0);
                //                }
                //                else
                //                {
                //                     perdidas += periodoTiempo.cantidad_horas.GetValueOrDefault(0);
                //                }                               
                //            }

                //        }
                //        if (essabado == 6) { perdidas += 8; }
                //        // perdidas += periodostime.Where(periodoTiempo => dia.Subtract(periodoTiempo.fecha_inicio.Value).Days % periodoTiempo.dias_periodo == 0).Aggregate(perdidas, (current, periodoTiempo) => current + periodoTiempo.cantidad_horas.GetValueOrDefault(0));

                //    }

                //    dia = dia.AddDays(1);
                //    continue; //salto al proximo
                //}
                #endregion

                #region Perdidas Diarias
                foreach (var periodos in periodostime)
                {

                    DateTime limitebajo = new DateTime(dia.Year, dia.Month, dia.Day, periodos.hora_entrada.Value.Hours, periodos.hora_entrada.Value.Minutes, periodos.hora_entrada.Value.Seconds);
                    limitebajo = limitebajo.AddHours(Convert.ToDouble(24 - periodos.cantidad_horas) / -2.0);
                    DateTime limitealto =
                        limitebajo.AddHours(Convert.ToDouble(periodos.cantidad_horas))
                            .AddMinutes(Convert.ToDouble(periodos.minuto_almuerzo))
                            .AddHours(Convert.ToDouble(24 - periodos.cantidad_horas));
                    if (dia.Subtract(periodos.fecha_inicio.Value).Days % periodos.dias_periodo == 0)//entonces debes venir, es un dia laborable  
                    {
                        if (pln != null)
                        {
                            if (pln.Clave_nom.codigo == "VC")
                            {
                                vacaciones += periodos.cantidad_horas.GetValueOrDefault(0);
                            }
                            else if (pln.Clave_nom.codigo == "CM" || pln.Clave_nom.codigo == "LM")
                            {
                                subsidios += periodos.cantidad_horas.GetValueOrDefault(0);
                            }
                            else if (pln.Clave_nom.codigo == "EU" || pln.Clave_nom.codigo == "MV")
                            {
                                EUyMov += periodos.cantidad_horas.GetValueOrDefault(0);
                            }
                            else if (pln.Clave_nom.codigo == "DF")
                            {
                                diaFeriado += periodos.cantidad_horas.GetValueOrDefault(0);
                            }
                            else if (pln.Clave_nom.codigo == "GS1")
                            {
                                GS1 += periodos.cantidad_horas.GetValueOrDefault(0);
                            }
                            else if (pln.Clave_nom.codigo == "GS6")
                            {
                                GS6 += periodos.cantidad_horas.GetValueOrDefault(0);
                            }
                            else if (pln.Clave_nom.codigo == "INT")
                            {
                                interruptos += periodos.cantidad_horas.GetValueOrDefault(0);
                            }
                            else if (clavesNomC.Descuenta(pln.Clave_nom))
                            {
                                foreach (var periodoTiempo in periodostime)
                                {
                                    if (dia.Subtract(periodoTiempo.fecha_inicio.Value).Days % periodoTiempo.dias_periodo == 0)
                                    {

                                        perdidas += periodoTiempo.cantidad_horas.GetValueOrDefault(0);
                                    }

                                }
                                //if (essabado == 6) { perdidas += 8; }
                                // perdidas += periodostime.Where(periodoTiempo => dia.Subtract(periodoTiempo.fecha_inicio.Value).Days % periodoTiempo.dias_periodo == 0).Aggregate(perdidas, (current, periodoTiempo) => current + periodoTiempo.cantidad_horas.GetValueOrDefault(0));

                            }
                            break;
                            //dia = dia.AddDays(1);
                            //continue; //salto al proximo
                        }
                        if (Obtener_DiasNolaborablesDeGrupo((int)personaC.GetPersonaPorExpedienteDiccio(exp).id_Grupo).Find(t => t.fecha.Date == dia.Date) != null)
                        {
                            perdidas += periodos.cantidad_horas.GetValueOrDefault(0);

                            dia = dia.AddDays(1);
                            continue;
                        }

                        //si es un dia laborable para ti debes tener trazas
                        List<RegistroESpejo> aux = new List<RegistroESpejo>();
                        aux = todas.FindAll(t => t.Fecha > limitebajo && t.Fecha < limitealto);
                        aux.OrderBy(o => o.Fecha);// en auxiliar est'an las trazas pertenecientes a la jornada laboral
                        var incidenciashoy = incidencias.FindAll(t => ((t.fecha >= limitebajo && t.fecha <= limitealto) || //Optimizado ahora busca en una lista mucho más chiquita repetitivamente// Ailin le agregue el >= ó <= 1/08/2018
                            (t.tipotraza == TipoTraza.N.ToString() && t.fecha.Date == limitebajo.Date)));
                        if (incidenciashoy.Find(t => t.Clave_nom.codigo == "VC") != null)
                        {
                            vacaciones += periodos.cantidad_horas.GetValueOrDefault(0);
                            break;
                        }
                        if (incidenciashoy.Find(t => t.Clave_nom.codigo == "CM") != null || incidenciashoy.Find(t => t.Clave_nom.codigo == "LM") != null)
                        {
                            subsidios += periodos.cantidad_horas.GetValueOrDefault(0);
                            break;
                        }
                        if (incidenciashoy.Find(t => t.Clave_nom.codigo == "EU") != null || incidenciashoy.Find(t => t.Clave_nom.codigo == "MV") != null)
                        {
                            EUyMov += periodos.cantidad_horas.GetValueOrDefault(0);
                            break;
                        }
                        if (incidenciashoy.Find(t => t.Clave_nom.codigo == "DF") != null)
                        {
                            diaFeriado += periodos.cantidad_horas.GetValueOrDefault(0);
                            break;
                        }
                        if (incidenciashoy.Find(t => t.Clave_nom.codigo == "GS1") != null)
                        {
                            GS1 += periodos.cantidad_horas.GetValueOrDefault(0);
                            break;
                        }
                        if (incidenciashoy.Find(t => t.Clave_nom.codigo == "GS6") != null)
                        {
                            GS6 += periodos.cantidad_horas.GetValueOrDefault(0);
                            break;
                        }
                        if (incidenciashoy.Find(t => t.Clave_nom.codigo == "INT") != null)
                        {
                            interruptos += periodos.cantidad_horas.GetValueOrDefault(0);
                            break;
                        }
                        if (incidenciashoy.Count > 0)
                            if (aux.Count == 0) //esto es k no viniste y te tocaba
                            {
                                if (incidenciashoy.Find(t => clavesNomC.Descuenta(t.Clave_nom)) != null)//t.Clave_nom.codigo == "AI" || t.Clave_nom.codigo == "MV"
                                {
                                    perdidas += periodos.cantidad_horas.GetValueOrDefault(0);
                                }

                            }
                            else
                            {// viniste y tienes 1 o mas de 2 trazas en el dia
                                DateTime entrar = new DateTime(dia.Year, dia.Month, dia.Day,
                               periodos.hora_entrada.Value.Hours, periodos.hora_entrada.Value.Minutes,
                               periodos.hora_entrada.Value.Seconds);
                                DateTime salir = entrar.AddHours(Convert.ToDouble(periodos.cantidad_horas))
                                   .AddMinutes(Convert.ToDouble(periodos.minuto_almuerzo)).AddSeconds(-59);
                                //------------------------cuando estas tarde--------------------------
                                //var tastarde = incidenciashoy.Find(t => t.tipotraza == "E" && clavesNomC.Descuenta(t.Clave_nom));
                                if (incidenciashoy.First().tipotraza == "E" && clavesNomC.Descuenta(incidenciashoy.First().Clave_nom))
                                    if (incidenciashoy.First().fecha > entrar.AddMinutes(1))
                                    {
                                        if (incidenciashoy.First().fecha > salir)
                                        {
                                            perdidas += periodos.cantidad_horas.GetValueOrDefault(0);

                                        }
                                        else if (incidenciashoy.First().fecha.Subtract(entrar).TotalMinutes / 60 > periodos.cantidad_horas.GetValueOrDefault(0) / 2.0)
                                        {
                                            perdidas += periodos.cantidad_horas.GetValueOrDefault(0) / 2.0; //le kito la mitad de la jornada si la tardanza es mayor de la mitad de la jornada
                                        }
                                        else
                                        {
                                            perdidas += incidenciashoy.First().fecha.Subtract(entrar).TotalMinutes / 60;
                                        }

                                    }
                                //}
                                //------------------------cuando no marcas la salida--------------------------
                                var nomar = incidenciashoy.Find(t => t.tipotraza == "N" && clavesNomC.Descuenta(t.Clave_nom));
                                if (nomar != null)//esto es una salida que no se marco al final del dia
                                {
                                    if (aux.Last().Tipo == "E") // me cercioro k lo ultimo de ese dia fue una entrada
                                    {
                                        perdidas += periodos.cantidad_horas.GetValueOrDefault(0) / 2.0;
                                        //aki le kito la mitad de la jornada
                                        //otra variante es  descontando el tiempo entre la hora de salir y su 'ultima entrada  resphoras += salir.Subtract(aux.Last().Fecha).TotalMinutes / 60;

                                    }
                                }
                                //-------------------------cuando sales antes de tiempo definitivo-----------------------------------------------------

                                var sales = incidenciashoy.Find(t => t.tipotraza == "S" && aux.Last().Fecha == t.fecha && clavesNomC.Descuenta(t.Clave_nom));
                                if (sales != null)//esto es una salida que no se marco al final del dia
                                {
                                    DateTime salidita = sales.fecha;
                                    DateTime entradita = aux.ElementAt(aux.IndexOf(aux.Find(o => o.Fecha == sales.fecha)) - 1).Fecha;
                                    if (entradita < entrar && salidita < entrar)
                                    {
                                        perdidas += periodos.cantidad_horas.Value;//aki le estoy descontando el tiempo entre la hora de salir y su salida
                                        //otra variante es kitar la mitad del dia: resphoras += periodos.cantidad_horas.GetValueOrDefault(0)/2;
                                    }
                                    else if (salidita > entrar && salidita < salir)
                                    {
                                        perdidas += salir.Subtract(salidita).TotalMinutes / 60;
                                    }

                                }
                                //-------------------------cuando sales antes de tiempo y vuelves a entrar varias veces-----------------------------------------------------
                                var salesy = incidenciashoy.FindAll(t => t.tipotraza == "S" && clavesNomC.Descuenta(t.Clave_nom) && aux.Last().Fecha != t.fecha);
                                if (salesy.Count > 0)
                                {
                                    foreach (var item in salesy)
                                    {
                                        DateTime salidita = item.fecha;
                                        DateTime entradita = aux.ElementAt(aux.IndexOf(aux.Find(o => o.Fecha == item.fecha)) + 1).Fecha;

                                        if (salidita > entrar && entradita < salir)
                                        {
                                            //aki le estoy descontando el tiempo entre la hora k salió y su proxima entrada
                                            perdidas += entradita.Subtract(salidita).TotalMinutes / 60;
                                        }
                                        if (salidita < entrar && entradita > entrar && entradita < salir)
                                        {
                                            perdidas += entradita.Subtract(entrar).TotalMinutes / 60;
                                        }
                                        if (salidita > entrar && salidita < salir && entradita > salir)
                                        {
                                            perdidas += salir.Subtract(salidita).TotalMinutes / 60;
                                        }
                                        if (salidita < entrar && entradita > salir)
                                        {
                                            perdidas += periodos.cantidad_horas.GetValueOrDefault(0);
                                        }

                                    }
                                }
                            }
                        break;
                    }
                }

                dia = dia.AddDays(1);
                #endregion

            }

            #endregion

            //**Ofrece el resultado en horas*//
            //Math.Round(perdidas / 8, 2) + Math.Round(perdidasviernes / 8, 2);
            double nolaboradas = (vacaciones + subsidios + perdidas + EUyMov + diaFeriado + interruptos + GS1 + GS6);

            //Aqui hay que arreglar lo de las horas con respecto a los periodos
            //vacaciones *= 8;
            //subsidios *= 8;
            //perdidas *= 8;

            if (nolaboradas > CantHorasMaximas)
            {
                nolaboradas = CantHorasMaximas;
            }
            if (vacaciones > CantHorasMaximas)
            {
                vacaciones = CantHorasMaximas;
            }
            if (subsidios > CantHorasMaximas)
            {
                subsidios = CantHorasMaximas;
            }
            if (perdidas > CantHorasMaximas)
            {
                perdidas = CantHorasMaximas;
            }
            if (perdidas > CantHorasMaximas)
            {
                perdidas = CantHorasMaximas;
            }
            if (EUyMov > CantHorasMaximas)
            {
                EUyMov = CantHorasMaximas;
            }
            if (diaFeriado > CantHorasMaximas)
            {
                diaFeriado = CantHorasMaximas;
            }
            if (interruptos > CantHorasMaximas)
            {
                interruptos = CantHorasMaximas;
            }
            if (GS1 > CantHorasMaximas)
            {
                GS1 = CantHorasMaximas;
            }
            if (GS6 > CantHorasMaximas)
            {
                GS6 = CantHorasMaximas;
            }
            double laboradas = CantHorasMaximas - nolaboradas;
            if (laboradas > CantHorasMaximas)
                laboradas = CantHorasMaximas;
            object[,] fila = new object[1, 14]
            {
                {
                    exp, nombre,  tardanzas, Math.Round(laboradas,2), Math.Round(nolaboradas,2), Math.Round(GS1,2), Math.Round(GS6,2), Math.Round(interruptos,2), Math.Round(vacaciones,2) , Math.Round(subsidios,2), Math.Round(EUyMov,2), Math.Round(diaFeriado,2), Math.Round(perdidas,2), Math.Round(laboradas + GS1 + GS6 + interruptos,2)
                }
            };



            /**Descomentariar si se kiere el resultado en dias*/
            //perdidas = Math.Round(perdidas / 8, 2) + Math.Round(perdidasviernes / 8, 2);
            //double nolaboradas = vacaciones + subsidios + perdidas;

            //if (nolaboradas > 24)
            //{
            //    nolaboradas = 24;
            //}
            //if (vacaciones > 24)
            //{
            //    vacaciones = 24;
            //}
            //if (subsidios > 24)
            //{
            //    subsidios = 24;
            //}
            //if (perdidas > 24)
            //{
            //    perdidas = 24;
            //}


            //double laboradas = 24 - nolaboradas;
            //object[,] fila = new object[1, 10]
            //{
            //    {
            //        exp, nombre, categoria, tardanzas, Math.Round(laboradas,2), Math.Round(nolaboradas,2), vacaciones, subsidios, perdidas, 24
            //    }
            //};

            return fila;
        }

        public object[,] FuncionGeneralTrabajadores(int exp)
        {
            var personaRH = servicio.DamePersonaxExpDeep(exp);

            object[,] fila = new object[1, 6]
            {
                {
                    exp, personaRH.Nombre + " " + personaRH.Apellido1 + " " + personaRH.Apellido2,  personaRH.CarneId, personaRH.Direccion, personaRH.Telefono, servicio.Municipio(personaRH.Mpio, personaRH.Provincia, personaRH.Pais)
                }
            };
            return fila;
        }

        public object[,] FuncionGeneralRegistros(int exp, DateTime fecha)
        {
            var personaRH = servicio.DamePersonaxExpDeep(exp);
            var registros = servicio.Registros(exp, fecha);
            object[,] fila = new object[1, 8]
            {
                {
                    exp, personaRH.Nombre + " " + personaRH.Apellido1 + " " + personaRH.Apellido2, "", "","","", "", ""
                }
            };

            for (int i = 2; i < 8; i++)
            {
                if (registros.Length + 2 > i)
                {
                    fila[0, i] = registros[i-2].Fecha.ToShortTimeString();
                }
            }
            return fila;
        }

        public List<object[,]> TiposTrabajador(List<int> trabajadores, DateTime fecha)
        {
            List<object[,]> tipos = new List<object[,]>();
            foreach (var item in trabajadores)
            {
                var registros = servicio.Registros(item, fecha);
                object[,] fila = new object[1, 6]
                {
                    {
                        "", "", "", "", "", ""
                    }
                };

                for(int i = 0; i < 6; i++)
                {
                    if (registros.Length > i)
                    {
                        if (registros[i].Tipo.Equals("E"))
                            fila[0, i] = 0;
                        else
                            fila[0, i] = 1;
                    }
                }
                tipos.Add(fila);
            }
            
            return tipos;
        }

        public List<object[,]> FuncionGeneralAltas(int año, int mes)

        {
            List<object[,]> retorno = new List<object[,]>();
            var altas = servicio.AltasxAño(año, mes);
            foreach (var item in altas)
            {
                object[,] fila = new object[1, 8]
                    {
                        {
                            item.Exp, item.Nombre, item.Plaza, item.Sexo.ToString(), item.Edad, item.Fecha.Date, item.Raza, ""
                        }
                    };
                retorno.Add(fila);
            }
            return retorno;
        }

        public List<object[,]> FuncionGeneralBajas(int año, int mes)

        {
            List<object[,]> retorno = new List<object[,]>();
            var altas = servicio.BajasxAño(año, mes);
            foreach (var item in altas)
            {
                object[,] fila = new object[1, 11]
                    {
                        {
                            item.Exp, item.Nombre, item.catOcup.ToString(), item.Sexo.ToString(), item.Edad, item.FechaAlta.Date, item.FechaBaja.Date, item.motivo, "", "", ""
                        }
                    };
                retorno.Add(fila);
            }
            return retorno;
        }

        public List<object[,]> FuncionGeneralCategorias(string tipo)

        {
            List<object[,]> retorno = new List<object[,]>();
            var altas = servicio.Categorizacion(tipo);
            foreach (var item in altas)
            {
                object[,] fila = new object[1, 8]
                    {
                        {
                            item.Nombre, item.CantidadH, item.CantidadM, item.Total, item.CantidadCDS, item.CantidadCD, item.CantidadCE, item.CantidadTecnico
                        }
                    };
                retorno.Add(fila);
            }
            return retorno;
        }

        public object[] FuncionGeneralAsistencia(List<Incidencia> incidencias)
        {
            var presentes = 0;
            var trabajoDistancia = 0;
            var vacaciones = 0;
            var certificadoMedico = 0;
            var licenciaSueldo = 0;
            var gs1 = 0;
            var gs6 = 0;
            var interrupto = 0;
            var licenciaMaternidad = 0;

            foreach (var incidencia in incidencias)
                {
                    if (incidencia.Clave_nom.codigo.Equals("PSM"))
                    {
                        presentes += 1;
                    }
                    else if (incidencia.Clave_nom.codigo.Equals("TD"))
                    {
                        trabajoDistancia += 1;
                    }
                    else if (incidencia.Clave_nom.codigo.Equals("VC"))
                    {
                        vacaciones += 1;
                    }
                    else if (incidencia.Clave_nom.codigo.Equals("CM"))
                    {
                        certificadoMedico += 1;
                    }
                    else if (incidencia.Clave_nom.codigo.Equals("LC"))
                    {
                        licenciaSueldo += 1;
                    }
                    else if (incidencia.Clave_nom.codigo.Equals("GS1"))
                    {
                        gs1 += 1;
                    }
                    else if (incidencia.Clave_nom.codigo.Equals("GS6"))
                    {
                        gs6 += 1;
                    }
                    else if (incidencia.Clave_nom.codigo.Equals("INT"))
                    {
                        interrupto += 1;
                    }
                    else if (incidencia.Clave_nom.codigo.Equals("LM"))
                    {
                        licenciaMaternidad += 1;
                    }
                }

            object[] fila = new object[9]
            {
                presentes, trabajoDistancia, vacaciones, certificadoMedico, licenciaSueldo, gs1, gs6, interrupto, licenciaMaternidad
            };

            return fila;
        }

        public object[,] FuncionGeneral1(int exp, DateTime fechainicio, DateTime fechafin)
        {

            double vacaciones = 0;
            double tardanzas = 0;
            double subsidios = 0;
            double EUyMV = 0;
            double diaFeriado = 0;
            double perdidas = 0;
            double perdidasviernes = 0;
            //var persona = servicio.DamePersonaxExp(exp);
            string nombre = "CC"; //persona.Nombre + " " + persona.Apellido1 + " " + persona.Apellido2;
            string categoria = "CC"; //servicio.DameCatOcupacionalxExp(exp);
            int[] exped = new int[] { exp };
            DateTime inicial = fechainicio.Date;
            DateTime final = fechafin.Date;
            tardanzas = this.CalculaTardanzas(inicial.Month, inicial.Year, exp);
            List<RegistroESpejo> todas = new Service1Client().TrazasEnRangoxListadoExp(inicial, final, exped).ToList();
            DateTime dia = inicial;
            var periodostime = grupoC.GetPeriodoDeTiempoDeGrupo(personaC.GetPersonaPorExpediente(exp).Grupo);
            #region Principal

            while (dia <= final)
            {
                #region dias planificados o feriados

                int essabado = (int)dia.DayOfWeek;

                var pln = planificacionAnticipadaC.Planificaciones.Find(t =>
                            t.Persona.exp == exp &&
                            ((t.fecha_inicio.Value.Date < dia.Date && t.fecha_fin.Value.Date > dia.Date) ||
                             dia.Date == t.fecha_inicio.Value.Date || dia.Date == t.fecha_fin.Value.Date));
                if (pln != null)
                {
                    if (pln.Clave_nom.codigo == "VC")
                    {
                        vacaciones++;
                    }
                    else if (pln.Clave_nom.codigo == "CM")
                    {
                        subsidios++;
                    }
                    else if (pln.Clave_nom.codigo == "EU" || pln.Clave_nom.codigo == "MV")
                    {
                        EUyMV++;
                    }
                    else if (pln.Clave_nom.codigo == "DF")
                    {
                        diaFeriado++;
                    }
                    else if (clavesNomC.Descuenta(pln.Clave_nom))
                    {
                        foreach (var periodoTiempo in periodostime)
                        {
                            if (dia.Subtract(periodoTiempo.fecha_inicio.Value).Days % periodoTiempo.dias_periodo == 0)
                            {
                                if (periodoTiempo.cantidad_horas.GetValueOrDefault(0) == 8)
                                {
                                    perdidasviernes += periodoTiempo.cantidad_horas.GetValueOrDefault(0);
                                }
                                else
                                {
                                    perdidas += periodoTiempo.cantidad_horas.GetValueOrDefault(0);
                                }

                            }

                        }
                        if (essabado == 6) { perdidas += 8; }
                        // perdidas += periodostime.Where(periodoTiempo => dia.Subtract(periodoTiempo.fecha_inicio.Value).Days % periodoTiempo.dias_periodo == 0).Aggregate(perdidas, (current, periodoTiempo) => current + periodoTiempo.cantidad_horas.GetValueOrDefault(0));

                    }

                    dia = dia.AddDays(1);
                    continue; //salto al proximo
                }
                #endregion

                #region Perdidas Diarias
                foreach (var periodos in periodostime)
                {

                    DateTime limitebajo = new DateTime(dia.Year, dia.Month, dia.Day, periodos.hora_entrada.Value.Hours, periodos.hora_entrada.Value.Minutes, periodos.hora_entrada.Value.Seconds);
                    limitebajo = limitebajo.AddHours(Convert.ToDouble(24 - periodos.cantidad_horas) / -2.0);
                    DateTime limitealto =
                        limitebajo.AddHours(Convert.ToDouble(periodos.cantidad_horas))
                            .AddMinutes(Convert.ToDouble(periodos.minuto_almuerzo))
                            .AddHours(Convert.ToDouble(24 - periodos.cantidad_horas));
                    if (dia.Subtract(periodos.fecha_inicio.Value).Days % periodos.dias_periodo == 0)//entonces debes venir, es un dia laborable  
                    {
                        if (Obtener_DiasNolaborablesDeGrupo((int)personaC.GetPersonaPorExpediente(exp).id_Grupo).Find(t => t.fecha.Date == dia.Date) != null)
                        {
                            if (periodos.cantidad_horas.GetValueOrDefault(0) == 8)
                            {
                                perdidasviernes += periodos.cantidad_horas.GetValueOrDefault(0);
                            }
                            else
                                perdidas += periodos.cantidad_horas.GetValueOrDefault(0);

                            dia = dia.AddDays(1);
                            continue;
                        }

                        //si es un dia laborable para ti debes tener trazas
                        List<RegistroESpejo> aux = new List<RegistroESpejo>();
                        aux = todas.FindAll(t => t.Fecha > limitebajo && t.Fecha < limitealto && t.Exp == exp);
                        aux.OrderBy(o => o.Fecha);// en auxiliar est'an las trazas pertenecientes a la jornada laboral
                        var incidenciashoy = incidenciaC.Incidencias.FindAll(t => t.Persona.exp == exp && ((t.fecha > limitebajo && t.fecha < limitealto) ||
                            (t.tipotraza == TipoTraza.N.ToString() && t.fecha.Date == limitebajo.Date)));
                        if (incidenciashoy.Find(t => t.Clave_nom.codigo == "VC") != null)
                        {
                            vacaciones++;
                            break;
                            //dia = dia.AddDays(1);
                            //continue;
                        }
                        if (incidenciashoy.Find(t => t.Clave_nom.codigo == "CM") != null)
                        {
                            subsidios++;
                            break;
                            //dia = dia.AddDays(1);
                            //continue;
                        }
                        if (incidenciashoy.Find(t => t.Clave_nom.codigo == "EU") != null || incidenciashoy.Find(t => t.Clave_nom.codigo == "MV") != null)
                        {
                            EUyMV++;
                            break;
                            //dia = dia.AddDays(1);
                            //continue;
                        }
                        if (incidenciashoy.Find(t => t.Clave_nom.codigo == "DF") != null)
                        {
                            diaFeriado++;
                            break;
                            //dia = dia.AddDays(1);
                            //continue;
                        }

                        if (incidenciashoy.Count > 0)
                            if (aux.Count == 0) //esto es k no viniste y te tocaba
                            {
                                if (incidenciashoy.Find(t => clavesNomC.Descuenta(t.Clave_nom)) != null)//t.Clave_nom.codigo == "AI" || t.Clave_nom.codigo == "MV"
                                {
                                    if (periodos.cantidad_horas.GetValueOrDefault(0) == 8)
                                    {
                                        perdidasviernes += periodos.cantidad_horas.GetValueOrDefault(0);
                                    }
                                    else
                                    {
                                        perdidas += periodos.cantidad_horas.GetValueOrDefault(0);
                                    }

                                }

                            }
                            else
                            {// viniste y tienes 1 o mas de 2 trazas en el dia
                                DateTime entrar = new DateTime(dia.Year, dia.Month, dia.Day,
                               periodos.hora_entrada.Value.Hours, periodos.hora_entrada.Value.Minutes,
                               periodos.hora_entrada.Value.Seconds);
                                DateTime salir = entrar.AddHours(Convert.ToDouble(periodos.cantidad_horas))
                                   .AddMinutes(Convert.ToDouble(periodos.minuto_almuerzo)).AddSeconds(-59);
                                //------------------------cuando estas tarde--------------------------
                                //var tastarde = incidenciashoy.Find(t => t.tipotraza == "E" && clavesNomC.Descuenta(t.Clave_nom));
                                if (incidenciashoy.First().tipotraza == "E" && clavesNomC.Descuenta(incidenciashoy.First().Clave_nom))
                                    if (incidenciashoy.First().fecha > entrar.AddMinutes(1))
                                    {
                                        if (incidenciashoy.First().fecha > salir)
                                        {
                                            if (periodos.cantidad_horas.GetValueOrDefault(0) == 8)
                                            {
                                                perdidasviernes += periodos.cantidad_horas.GetValueOrDefault(0);
                                            }
                                            else
                                            {
                                                perdidas += periodos.cantidad_horas.GetValueOrDefault(0);
                                            }

                                        }
                                        else if (incidenciashoy.First().fecha.Subtract(entrar).TotalMinutes / 60 > periodos.cantidad_horas.GetValueOrDefault(0) / 2.0)
                                        {
                                            if (periodos.cantidad_horas.GetValueOrDefault(0) == 8)
                                            {
                                                perdidasviernes += periodos.cantidad_horas.GetValueOrDefault(0) / 2.0;
                                            }
                                            else
                                                perdidas += periodos.cantidad_horas.GetValueOrDefault(0) / 2.0; //le kito la mitad de la jornada si la tardanza es mayor de la mitad de la jornada
                                        }
                                        else
                                        {
                                            perdidas += incidenciashoy.First().fecha.Subtract(entrar).TotalMinutes / 60;
                                        }

                                    }
                                //}
                                //------------------------cuando no marcas la salida--------------------------
                                var nomar = incidenciashoy.Find(t => t.tipotraza == "N" && clavesNomC.Descuenta(t.Clave_nom));
                                if (nomar != null)//esto es una salida que no se marco al final del dia
                                {
                                    if (aux.Last().Tipo == "E") // me cercioro k lo ultimo de ese dia fue una entrada
                                    {
                                        if (periodos.cantidad_horas.GetValueOrDefault(0) == 8)
                                        {
                                            perdidasviernes += periodos.cantidad_horas.GetValueOrDefault(0) / 2.0;
                                        }
                                        else
                                            perdidas += periodos.cantidad_horas.GetValueOrDefault(0) / 2.0;
                                        //aki le kito la mitad de la jornada
                                        //otra variante es  descontando el tiempo entre la hora de salir y su 'ultima entrada  resphoras += salir.Subtract(aux.Last().Fecha).TotalMinutes / 60;

                                    }
                                }
                                //-------------------------cuando sales antes de tiempo definitivo-----------------------------------------------------

                                var sales = incidenciashoy.Find(t => t.tipotraza == "S" && aux.Last().Fecha == t.fecha && clavesNomC.Descuenta(t.Clave_nom));
                                if (sales != null)//esto es una salida que no se marco al final del dia
                                {
                                    DateTime salidita = sales.fecha;
                                    DateTime entradita = aux.ElementAt(aux.IndexOf(aux.Find(o => o.Fecha == sales.fecha)) - 1).Fecha;
                                    if (entradita < entrar && salidita < entrar)
                                    {
                                        if (periodos.cantidad_horas.GetValueOrDefault(0) == 8)
                                        {
                                            perdidasviernes += periodos.cantidad_horas.GetValueOrDefault(0);
                                        }
                                        else
                                            perdidas += periodos.cantidad_horas.Value;//aki le estoy descontando el tiempo entre la hora de salir y su salida
                                        //otra variante es kitar la mitad del dia: resphoras += periodos.cantidad_horas.GetValueOrDefault(0)/2;
                                    }
                                    else if (salidita > entrar && salidita < salir)
                                    {
                                        perdidas += salir.Subtract(salidita).TotalMinutes / 60;
                                    }

                                }
                                //-------------------------cuando sales antes de tiempo y vuelves a entrar varias veces-----------------------------------------------------
                                var salesy = incidenciashoy.FindAll(t => t.tipotraza == "S" && clavesNomC.Descuenta(t.Clave_nom) && aux.Last().Fecha != t.fecha);
                                if (salesy.Count > 0)
                                {
                                    foreach (var item in salesy)
                                    {
                                        DateTime salidita = item.fecha;
                                        DateTime entradita = aux.ElementAt(aux.IndexOf(aux.Find(o => o.Fecha == item.fecha)) + 1).Fecha;

                                        if (salidita > entrar && entradita < salir)
                                        {
                                            //aki le estoy descontando el tiempo entre la hora k salió y su proxima entrada
                                            perdidas += entradita.Subtract(salidita).TotalMinutes / 60;
                                        }
                                        if (salidita < entrar && entradita > entrar && entradita < salir)
                                        {
                                            perdidas += entradita.Subtract(entrar).TotalMinutes / 60;
                                        }
                                        if (salidita > entrar && salidita < salir && entradita > salir)
                                        {
                                            perdidas += salir.Subtract(salidita).TotalMinutes / 60;
                                        }
                                        if (salidita < entrar && entradita > salir)
                                        {
                                            if (periodos.cantidad_horas.GetValueOrDefault(0) == 8)
                                            {
                                                perdidasviernes += periodos.cantidad_horas.GetValueOrDefault(0);
                                            }
                                            else
                                                perdidas += periodos.cantidad_horas.GetValueOrDefault(0); ;
                                        }

                                    }
                                }
                            }


                    }


                }

                dia = dia.AddDays(1);
                #endregion

            }




            #endregion

            //perdidas = Math.Round(perdidas / 8, 2) + Math.Round(perdidasviernes / 8, 2);
            //double nolaboradas = vacaciones + subsidios + perdidas;


            //if (nolaboradas > 24)
            //{
            //    nolaboradas = 24;
            //}
            //if (vacaciones > 24)
            //{
            //    vacaciones = 24;
            //}
            //if (subsidios > 24)
            //{
            //    subsidios = 24;
            //}
            //if (perdidas > 24)
            //{
            //    perdidas = 24;
            //}


            //double laboradas = 24 - nolaboradas;
            //object[,] fila = new object[1, 10]
            //{
            //    {
            //        exp, nombre, categoria, tardanzas, Math.Round(laboradas,2), Math.Round(nolaboradas,2), vacaciones, subsidios, perdidas, 24
            //    }
            //};


            //**Ofrece el resultado en horas*//
            perdidas = Math.Round(perdidas / 8, 2) + Math.Round(perdidasviernes / 8, 2);
            double nolaboradas = (vacaciones + subsidios + EUyMV + diaFeriado + perdidas) * 8d;

            vacaciones *= 8;
            subsidios *= 8;
            perdidas *= 8;

            if (nolaboradas > 190.6)
            {
                nolaboradas = 190.6;
            }
            if (vacaciones > 190.6)
            {
                vacaciones = 190.6;
            }
            if (subsidios > 190.6)
            {
                subsidios = 190.6;
            }
            if (EUyMV > 190.6)
            {
                EUyMV = 190.6;
            }
            if (diaFeriado > 190.6)
            {
                diaFeriado = 190.6;
            }
            if (perdidas > 190.6)
            {
                perdidas = 190.6;
            }


            double laboradas = 190.6 - nolaboradas;
            if (laboradas > 190.6)
                laboradas = 190.6;
            object[,] fila = new object[1, 12]
            {
                {
                    exp, nombre, categoria, tardanzas, Math.Round(laboradas,2), Math.Round(nolaboradas,2), vacaciones , subsidios, EUyMV, diaFeriado, perdidas, 190.6
                }
            };
            return fila;
        }

        /// <summary>
        /// Funcion para
        /// </summary>
        /// <param name="mes"></param>
        /// <param name="ano"></param>
        /// <returns></returns>
        /* 
         public bool MesCerradoGeneral(int mes, int ano)
         {
             if (incidenciaC.Incidencias.Find(t => t.fecha.Month == mes && t.fecha.Year == ano && t.Clave_nom == null) != null)
             {
                 return false;
             }
             return true;
         }

         public bool MesCerradoxSecretariaAutenticada(int mes, int ano, int expSecretaria)
         {
             return personaC.AreasControladasPorPersona(personaC.GetPersonaPorExpediente(expSecretaria)).SelectMany(area => personaC.PersonasDelArea(area)).All(persona => incidenciaC.Incidencias.Find(t => t.fecha.Month == mes && t.fecha.Year == ano && t.Clave_nom == null && t.Persona.exp == persona.exp) == null);
         }
         */
        private double CalculaPerdidas(int mes, int ano, int exp)
        {
            double resphoras = 0;
            try
            {
                #region metodo

                DateTime inicial = new DateTime(ano, mes, 1);
                DateTime final = new DateTime(ano, mes, DateTime.DaysInMonth(ano, mes));

                var PrePlanes = planificacionAnticipadaC.Planificaciones.FindAll(
                            t =>
                                t.Persona.exp == exp &&
                                t.fecha_fin >= inicial && t.fecha_inicio <= final);

                int[] exped = new int[] { exp };

                List<RegistroESpejo> todas = new Service1Client().TrazasEnRangoxListadoExp(inicial, final, exped).ToList();
                DateTime dia = inicial;
                var periodostime = grupoC.GetPeriodoDeTiempoDeGrupo(personaC.GetPersonaPorExpediente(exp).Grupo);
                while (dia <= final)
                {
                    #region dias planificados o feriados

                    var pln =
                        PrePlanes.Find(
                            t =>
                                t.Persona.exp == exp &&
                                ((t.fecha_inicio.Value.Date < dia.Date && t.fecha_fin.Value.Date > dia.Date) ||
                                 dia.Date == t.fecha_inicio.Value.Date || dia.Date == t.fecha_fin.Value.Date));
                    if (pln != null)
                    {
                        if (clavesNomC.Descuenta(pln.Clave_nom))
                        {
                            resphoras = periodostime.Where(periodoTiempo => dia.Subtract(periodoTiempo.fecha_inicio.Value).Days % periodoTiempo.dias_periodo == 0).Aggregate(resphoras, (current, periodoTiempo) => current + periodoTiempo.cantidad_horas.GetValueOrDefault(0));

                        }

                        dia = dia.AddDays(1);
                        continue; //salto al proximo
                    }

                    #endregion
                    foreach (var periodos in periodostime)
                    {
                        DateTime limitebajo = new DateTime(dia.Year, dia.Month, dia.Day, periodos.hora_entrada.Value.Hours, periodos.hora_entrada.Value.Minutes, periodos.hora_entrada.Value.Seconds);
                        limitebajo = limitebajo.AddHours(Convert.ToDouble(24 - periodos.cantidad_horas) / -2.0);
                        DateTime limitealto =
                            limitebajo.AddHours(Convert.ToDouble(periodos.cantidad_horas))
                                .AddMinutes(Convert.ToDouble(periodos.minuto_almuerzo))
                                .AddHours(Convert.ToDouble(24 - periodos.cantidad_horas));
                        if (dia.Subtract(periodos.fecha_inicio.Value).Days % periodos.dias_periodo == 0)//entonces debes venir, es un dia laborable  
                        {
                            if (Obtener_DiasNolaborablesDeGrupo((int)personaC.GetPersonaPorExpediente(exp).id_Grupo).Find(t => t.fecha.Date == dia.Date) != null)
                            {
                                resphoras += 8;
                                dia = dia.AddDays(1);
                                continue;
                            }
                            //si es un dia laborable para ti debes tener trazas
                            List<RegistroESpejo> aux = new List<RegistroESpejo>();
                            aux = todas.FindAll(t => t.Fecha > limitebajo && t.Fecha < limitealto && t.Exp == exp);
                            aux.OrderBy(o => o.Fecha);// en auxiliar est'an las trazas pertenecientes a la jornada laboral
                            var incidenciashoy = incidenciaC.Incidencias.FindAll(t => t.Persona.exp == exp && ((t.fecha > limitebajo && t.fecha < limitealto) ||
                                (t.tipotraza == TipoTraza.N.ToString() && t.fecha.Date == limitebajo.Date)));
                            if (incidenciashoy.Count > 0)
                                if (aux.Count == 0) //esto es k no viniste y te tocaba
                                {
                                    if (incidenciashoy.Find(t => clavesNomC.Descuenta(t.Clave_nom)) != null)//t.Clave_nom.codigo == "AI" || t.Clave_nom.codigo == "MV"
                                    {
                                        resphoras += periodos.cantidad_horas.GetValueOrDefault(0);
                                    }
                                }
                                else
                                {// viniste y tienes 1 o mas de 2 trazas en el dia
                                    DateTime entrar = new DateTime(dia.Year, dia.Month, dia.Day,
                                   periodos.hora_entrada.Value.Hours, periodos.hora_entrada.Value.Minutes,
                                   periodos.hora_entrada.Value.Seconds);
                                    DateTime salir = entrar.AddHours(Convert.ToDouble(periodos.cantidad_horas))
                                       .AddMinutes(Convert.ToDouble(periodos.minuto_almuerzo)).AddSeconds(-59);
                                    //------------------------cuando estas tarde--------------------------
                                    //var tastarde = incidenciashoy.Find(t => t.tipotraza == "E" && clavesNomC.Descuenta(t.Clave_nom));
                                    if (incidenciashoy.First().tipotraza == "E" && clavesNomC.Descuenta(incidenciashoy.First().Clave_nom))
                                        if (incidenciashoy.First().fecha > entrar.AddMinutes(1))
                                        {
                                            if (incidenciashoy.First().fecha.Subtract(entrar).TotalMinutes / 60 > periodos.cantidad_horas.GetValueOrDefault(0) / 2.0)
                                            {
                                                resphoras += periodos.cantidad_horas.GetValueOrDefault(0) / 2.0; //le kito la mitad de la jornada si la tardanza es mayor de la mitad de la jornada
                                            }
                                            else
                                            {
                                                resphoras += incidenciashoy.First().fecha.Subtract(entrar).TotalMinutes / 60;
                                            }

                                        }
                                    //}
                                    //------------------------cuando no marcas la salida--------------------------
                                    var nomar = incidenciashoy.Find(t => t.tipotraza == "N" && clavesNomC.Descuenta(t.Clave_nom));
                                    if (nomar != null)//esto es una salida que no se marco al final del dia
                                    {
                                        if (aux.Last().Tipo == "E") // me cercioro k lo ultimo de ese dia fue una entrada
                                        {
                                            resphoras += periodos.cantidad_horas.GetValueOrDefault(0) / 2.0;
                                            //aki le kito la mitad de la jornada
                                            //otra variante es  descontando el tiempo entre la hora de salir y su 'ultima entrada  resphoras += salir.Subtract(aux.Last().Fecha).TotalMinutes / 60;

                                        }
                                    }
                                    //-------------------------cuando sales antes de tiempo definitivo-----------------------------------------------------

                                    var sales = incidenciashoy.Find(t => t.tipotraza == "S" && aux.Last().Fecha == t.fecha && clavesNomC.Descuenta(t.Clave_nom));
                                    if (sales != null)//esto es una salida que no se marco al final del dia
                                    {
                                        if (sales.fecha < salir)
                                        {
                                            resphoras += salir.Subtract(sales.fecha).TotalMinutes / 60;//aki le estoy descontando el tiempo entre la hora de salir y su salida
                                            //otra variante es kitar la mitad del dia: resphoras += periodos.cantidad_horas.GetValueOrDefault(0)/2;
                                        }
                                    }
                                    //-------------------------cuando sales antes de tiempo y vuelves a entrar varias veces-----------------------------------------------------
                                    var salesy = incidenciashoy.FindAll(t => t.tipotraza == "S" && clavesNomC.Descuenta(t.Clave_nom) && aux.Last().Fecha != t.fecha);
                                    if (salesy.Count > 0)
                                    {
                                        foreach (var item in salesy)
                                        {
                                            DateTime salidita = item.fecha;
                                            DateTime entradita = aux.ElementAt(aux.IndexOf(aux.Find(o => o.Fecha == item.fecha)) + 1).Fecha;

                                            if (salidita > entrar && entradita < salir)
                                            {
                                                //aki le estoy descontando el tiempo entre la hora k salió y su proxima entrada
                                                resphoras += entradita.Subtract(salidita).TotalMinutes / 60;
                                            }
                                            if (salidita < entrar && entradita > entrar && entradita < salir)
                                            {
                                                resphoras += entradita.Subtract(entrar).TotalMinutes / 60;
                                            }
                                            if (salidita > entrar && salidita < salir && entradita > salir)
                                            {
                                                resphoras += salir.Subtract(salidita).TotalMinutes / 60;
                                            }
                                            if (salidita < entrar && entradita > salir)
                                            {
                                                resphoras += periodos.cantidad_horas.Value;
                                            }

                                        }
                                    }
                                }


                        }


                    }

                    dia = dia.AddDays(1);
                }

                #endregion
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return Math.Round(resphoras / 8, 2);

        }

        private int CalculaTardanzas(int mes, int ano, int exp, List<Incidencia> incidencias = null, List<Planificacion> planificaciones = null)
        {
            int resp = 0;
            //  int planif = 0;

            try
            {
                DateTime fechainicio = new DateTime(ano, mes, 1);
                DateTime fechafin = new DateTime(ano, mes, DateTime.DaysInMonth(ano, mes));
                List<Incidencia> listresp;
                //devolver en resp la cantidad de tardanzas pero hay k quitar las planificaciones que coincidan en día..
                if (incidencias == null)
                    listresp = incidenciaC.Incidencias.FindAll(t => t.Persona.exp == exp && t.Clave_nom.codigo == "TI" && t.fecha.Month == mes && t.fecha.Year == ano);  //codigo linq
                else
                    listresp = incidencias.FindAll(t => t.Clave_nom.codigo == "TI");

                List<Planificacion> planes;
                if (planificaciones == null)
                {
                    planes = new List<Planificacion>();
                    foreach (var a in planificacionAnticipadaC.Planificaciones)
                        if (a.Persona.exp == exp && a.fecha_fin >= fechainicio && a.fecha_inicio <= fechafin) //Genero la lista filtrada por expediente y fecha en rango de planificaciones
                            planes.Add(a);//Disminuye significativamente las planificaciones a revisar más adelante de forma repetitiva
                }
                else
                    planes = planificaciones;

                #region dias planificados o feriados
                if (listresp.Count > 0)
                {
                    foreach (var tardanza in listresp)
                    {/*
                        if (planificacionAnticipadaC.Planificaciones.Find(  t => (t.fecha_inicio.Value.Date < tardanza.fecha.Date && t.fecha_fin.Value.Date > tardanza.fecha.Date) ||
                            tardanza.fecha.Date == t.fecha_inicio.Value.Date || tardanza.fecha.Date == t.fecha_fin.Value.Date) != null)
                        {
                            planif++;
                        }*/
                        Planificacion p_evaluar = planes.Find(p => (p.fecha_inicio.Value <= tardanza.fecha && p.fecha_fin.Value >= tardanza.fecha));
                        if (p_evaluar == null)
                        {
                            resp++;
                        }
                    }

                }
                #endregion
                //   resp = listresp.Count - planif;
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return resp;
        }

        public int CalculaSubsidios(int mes, int ano, int exp)
        {
            int resp = 0;
            DateTime inicial = new DateTime(ano, mes, 1);
            DateTime final = new DateTime(ano, mes, DateTime.DaysInMonth(ano, mes));
            DateTime dia = inicial;
            //var periodostime = personaC.GetPersonaPorExpediente(exp).Grupo.Periodo_tiempo;
            while (dia <= final)
            {
                #region dias planificados o feriados

                var pln =
                    planificacionAnticipadaC.Planificaciones.Find(
                        t =>
                            t.Persona.exp == exp &&
                            ((t.fecha_inicio.Value.Date < dia.Date && t.fecha_fin.Value.Date > dia.Date) ||
                             dia.Date == t.fecha_inicio.Value.Date || dia.Date == t.fecha_fin.Value.Date));
                if (pln != null)
                {
                    if (pln.Clave_nom.codigo == "CM")
                    {
                        resp++;
                    }

                    dia = dia.AddDays(1);
                    continue; //salto al proximo
                }
                #endregion
                var inc = incidenciaC.Incidencias.Find(t => t.Persona.exp == exp && t.fecha.Date == dia.Date && (t.Clave_nom.codigo == "CM" || t.Clave_nom.codigo == "LM"));
                if (inc != null)
                {
                    resp++;
                }

                dia = dia.AddDays(1);
            }
            return resp;
        }

        /* private int CalculaVacaciones(int mes, int ano, int exp)
         {
             int resp = 0;
             try
             {

                 DateTime limiteinicial = new DateTime(ano, mes, 1);
                 DateTime limitefinal = new DateTime(ano, mes, DateTime.DaysInMonth(ano, mes));
                 var listaplanif =
                     planificacionAnticipadaC.Planificaciones.FindAll(
                         item => item.Persona.exp == exp && item.Clave_nom.codigo == "VC" &&
                                 (item.fecha_inicio >= limiteinicial && item.fecha_inicio <= limitefinal ||
                                  item.fecha_fin >= limiteinicial && item.fecha_fin <= limitefinal));
                 List<DateTime> respPlanificaciones = new List<DateTime>();
                 var respIncidencias = incidenciaC.Incidencias.FindAll(t =>
                              t.Clave_nom.codigo == "VC" && t.Persona.exp == exp && t.fecha >= limiteinicial &&
                              t.fecha <= limitefinal);

                 foreach (var planificacion in listaplanif)
                 {
                     DateTime primerdia;
                     DateTime ultimodia;
                     primerdia = planificacion.fecha_inicio <= limiteinicial ? limiteinicial : planificacion.fecha_inicio.Value;
                     if (planificacion.fecha_fin >= limitefinal)
                     {
                         ultimodia = limitefinal;
                     }
                     else
                     {
                         ultimodia = planificacion.fecha_fin.Value;
                     }
                     DateTime dia = primerdia;
                     while (dia.Date <= ultimodia.Date)
                     {
                         //este codigo debe actualizarse cuando se tenga
                         if (dia.DayOfWeek == DayOfWeek.Sunday)
                         {
                             dia = dia.AddDays(1);
                             continue;
                         }
                         else
                         {
                             respPlanificaciones.Add(dia);
                         }

                         dia = dia.AddDays(1);
                     }

                 }
                 int repetido = 0;
                 foreach (var planificacion in respPlanificaciones)
                 {
                     foreach (var incidencias in respIncidencias)
                     {
                         if (planificacion.Date == incidencias.fecha.Date)
                         {
                             repetido++;
                         }
                     }
                 }

                 resp = respIncidencias.Count + respPlanificaciones.Count - repetido;
             }
             catch (Exception ex)
             {
                 throw ex;
             }
             return resp;
         }*/

        private int CalculaVacaciones(int mes, int ano, int exp)
        {
            int resp = 0;
            DateTime inicial = new DateTime(ano, mes, 1);
            DateTime final = new DateTime(ano, mes, DateTime.DaysInMonth(ano, mes));
            DateTime dia = inicial;
            //var periodostime = personaC.GetPersonaPorExpediente(exp).Grupo.Periodo_tiempo;
            while (dia <= final)
            {
                #region dias planificados o feriados

                var pln =
                    planificacionAnticipadaC.Planificaciones.Find(
                        t =>
                            t.Persona.exp == exp &&
                            ((t.fecha_inicio.Value.Date < dia.Date && t.fecha_fin.Value.Date > dia.Date) ||
                             dia.Date == t.fecha_inicio.Value.Date || dia.Date == t.fecha_fin.Value.Date));
                if (pln != null)
                {
                    if (pln.Clave_nom.codigo == "VC")
                    {
                        resp++;
                    }

                    dia = dia.AddDays(1);
                    continue; //salto al proximo
                }
                #endregion
                var inc = incidenciaC.Incidencias.Find(t => t.Persona.exp == exp && t.fecha.Date == dia.Date && t.Clave_nom.codigo == "VC");
                if (inc != null)
                {
                    resp++;
                }

                dia = dia.AddDays(1);
            }
            return resp;
        }

        //-------------generando el excell de la prenómina--------
        #region construyendo el excel..

        public string DimeMes(int mes)
        {

            switch (mes)
            {
                case (1):
                    {
                        return "Enero";
                    }

                case (2):
                    {
                        return "Febrero";
                    }
                case (3):
                    {
                        return "Marzo";
                    }
                case (4):
                    {
                        return "Abril";
                    }
                case (5):
                    {
                        return "Mayo";
                    }
                case (6):
                    {
                        return "Junio";
                    }
                case (7):
                    {
                        return "Julio";
                    }
                case (8):
                    {
                        return "Agosto";
                    }
                case (9):
                    {
                        return "Septiembre";
                    }
                case (10):
                    {
                        return "Octubre";
                    }
                case (11):
                    {
                        return "Noviembre";
                    }
                case (12):
                    {
                        return "Diciembre";
                    }

            }
            return null;
        }

        public void GenerarExcel(int mes, int ano)
        {
            try
            {
                this.objApp =
                    (Microsoft.Office.Interop.Excel.Application)new Microsoft.Office.Interop.Excel.Application();
                this.objBook = (_Workbook)this.objApp.Workbooks.Add(Missing.Value);
                Sheets worksheets = this.objBook.Worksheets;
                var trabajador = (from todos in personaC.Personas
                                  orderby todos.exp
                                  select todos.exp).ToArray();
                DateTime fechainicio = new DateTime(ano, mes, 1);
                DateTime fechafin = new DateTime(ano, mes, DateTime.DaysInMonth(ano, mes));
                Trazas = servicio.TrazasEnRangoxListadoExp(fechainicio, fechafin, trabajador).ToList();
                // PESTAÑA CIE==========================================
                var listareas = areaC.Areas.Select(t => t.id_area).ToList();
                var primero = listareas.First();
                string nombre = areaC.GetAreaDiccio(Convert.ToInt32(primero)).descripcion;
                worksheets.Add(Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                _Worksheet worksheetcie = (_Worksheet)worksheets.get_Item((1));

                int valencabezado = Encabezado(worksheetcie, 1, 2, mes, ano, nombre);
                int valcuerpo = Cuerpo(worksheetcie, 1, valencabezado);
                var DiccioIncidencia = GenerarIncidenciaExpDiccioPorFecha(fechainicio, fechafin);
                var DiccioPlanes = GenerarPlanificacionExpDiccioPorFecha(fechainicio, fechafin);
                int valfila = FilaxArea(worksheetcie, 1, valcuerpo, primero, mes, ano, DiccioIncidencia, DiccioPlanes);
                PieGeneral(worksheetcie, 1, valfila);

                Range rg = worksheetcie.get_Range("B1", "N1");
                rg.EntireColumn.AutoFit();
                //====================================
                //Range rgDATE = worksheetcie.get_Range("C1", "C1");
                //rgDATE.EntireColumn.NumberFormat = "0";
                //====================================
                Range rgLD = worksheetcie.get_Range("C1", "N1");
                rgLD.EntireColumn.NumberFormat = "0.00";

                worksheetcie.Name = areaC.GetAreaDiccio(primero).descripcion;

                _Worksheet ultpest = (_Worksheet)worksheets.get_Item((2));


                listareas.RemoveAt(0);

                int contpaworksheet = 1;
                foreach (var item in listareas)
                {
                    _Worksheet worksheet = (_Worksheet)worksheets.get_Item((contpaworksheet + 1));
                    object dato = worksheets.Add(Missing.Value, ultpest, Missing.Value, Missing.Value);
                    ultpest = (_Worksheet)dato;


                    int valencabezado2 = Encabezado(worksheet, 1, 2, mes, ano, areaC.GetAreaDiccio(item).descripcion);
                    int valcuerpo2 = Cuerpo(worksheet, 1, valencabezado2);
                    int valfila2 = FilaxArea(worksheet, 1, valcuerpo2, item, mes, ano, DiccioIncidencia, DiccioPlanes);
                    PieGeneral(worksheet, 1, valfila2);

                    rg = worksheet.get_Range("B1", "N1");
                    rg.EntireColumn.AutoFit();
                    //====================================
                    //rgDATE = worksheet.get_Range("C1", "C1");
                    //rgDATE.EntireColumn.NumberFormat = "MM/DD/YY";
                    //====================================
                    rgLD = worksheet.get_Range("C1", "N1");
                    rgLD.EntireColumn.NumberFormat = "0.00";

                    worksheet.Name = areaC.GetAreaDiccio(item).descripcion;
                    contpaworksheet++;
                }
                this.objApp.Visible = true;
                this.objApp.UserControl = true;
            }
            catch (Exception ex)
            {
                int num4 = (int)MessageBox.Show("Error: " + ex.Message + " Line: " + ex.Source, "Error");
            }

        }

        public void GenerarExcelxArea(int mes, int ano, List<int> idareas)
        {
            try
            {
                var trabajador = (from todos in personaC.Personas
                                  orderby todos.exp
                                  select todos.exp).ToList();

                DateTime fechainicio = new DateTime(ano, mes, 1);
                DateTime fechafin = new DateTime(ano, mes, DateTime.DaysInMonth(ano, mes));
                if (idareas.Count == 1)
                {
                    Area area = areaC.GetAreaDiccio(idareas[0]);
                    var pers = areaC.PersonasActivasQuePertenecenArea(area);
                    int[] arr = new int[pers.Count];
                    for (int i = 0; i < pers.Count; i++)
                        arr[i] = pers[i].exp;
                    Trazas = servicio.TrazasEnRangoxListadoExp(fechainicio, fechafin, arr).ToList();
                }
                else
                    Trazas = servicio.TrazasEnRangoxListadoExp(fechainicio, fechafin, trabajador.ToArray()).ToList();
                this.objApp =
                    (Microsoft.Office.Interop.Excel.Application)new Microsoft.Office.Interop.Excel.Application();
                this.objBook = (_Workbook)this.objApp.Workbooks.Add(Missing.Value);
                Sheets worksheets = this.objBook.Worksheets;

                // PESTAÑA CIE==========================================

                var primero = idareas.First();
                string nombre = areaC.GetAreaDiccio(Convert.ToInt32(primero)).descripcion;
                worksheets.Add(Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                _Worksheet worksheetcie = (_Worksheet)worksheets.get_Item((1));

                var DiccioIncidencia = GenerarIncidenciaExpDiccioPorFecha(fechainicio, fechafin);
                var DiccioPlanes = GenerarPlanificacionExpDiccioPorFecha(fechainicio, fechafin);

                int valencabezado = Encabezado(worksheetcie, 1, 2, mes, ano, nombre);
                int valcuerpo = Cuerpo(worksheetcie, 1, valencabezado);
                int valfila = FilaxArea(worksheetcie, 1, valcuerpo, primero, mes, ano, DiccioIncidencia, DiccioPlanes);
                PieGeneral(worksheetcie, 1, valfila);

                Range rg = worksheetcie.get_Range("B1", "N1");
                rg.EntireColumn.AutoFit();
                //====================================
                //Range rgDATE = worksheetcie.get_Range("C1", "C1");
                //rgDATE.EntireColumn.NumberFormat = "MM/DD/YY";
                //====================================
                Range rgLD = worksheetcie.get_Range("C1", "N1");
                rgLD.EntireColumn.NumberFormat = "0.00";

                worksheetcie.Name = areaC.GetAreaDiccio(primero).descripcion;

                _Worksheet ultpest = (_Worksheet)worksheets.get_Item((2));

                if (idareas.Count > 1)
                {
                    idareas.RemoveAt(0);
                    int contpaworksheet = 1;
                    foreach (var item in idareas)
                    {
                        _Worksheet worksheet = (_Worksheet)worksheets.get_Item((contpaworksheet + 1));
                        object dato = worksheets.Add(Missing.Value, ultpest, Missing.Value, Missing.Value);
                        ultpest = (_Worksheet)dato;

                        string nombreArea = areaC.GetAreaDiccio(item).descripcion;
                        int valencabezado2 = Encabezado(worksheet, 1, 2, mes, ano, nombreArea);
                        int valcuerpo2 = Cuerpo(worksheet, 1, valencabezado2);
                        int valfila2 = FilaxArea(worksheet, 1, valcuerpo2, item, mes, ano, DiccioIncidencia, DiccioPlanes);
                        PieGeneral(worksheet, 1, valfila2);

                        rg = worksheet.get_Range("B1", "N1");
                        rg.EntireColumn.AutoFit();
                        //====================================
                        //rgDATE = worksheet.get_Range("C1", "C1");
                        //rgDATE.EntireColumn.NumberFormat = "MM/DD/YY";
                        //====================================
                        rgLD = worksheet.get_Range("C1", "N1");
                        rgLD.EntireColumn.NumberFormat = "0.00";

                        worksheet.Name = areaC.GetAreaDiccio(item).descripcion;
                        contpaworksheet++;
                    }
                }

                this.objApp.Visible = true;
                this.objApp.UserControl = true;
            }
            catch (Exception ex)
            {
                int num4 = (int)MessageBox.Show("Error: " + ex.Message + " Line: " + ex.Source, "Error");
            }
        }

        public void GenerarExcelxAreaTrabajadores(List<int> idareas)
        {
            try
            {
                var trabajador = (from todos in personaC.Personas
                                  orderby todos.exp
                                  select todos.exp).ToList();
                this.objApp =
                    (Microsoft.Office.Interop.Excel.Application)new Microsoft.Office.Interop.Excel.Application();
                this.objBook = (_Workbook)this.objApp.Workbooks.Add(Missing.Value);
                Sheets worksheets = this.objBook.Worksheets;

                var primero = idareas.First();
                string nombre = areaC.GetAreaDiccio(Convert.ToInt32(primero)).descripcion;
                worksheets.Add(Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                _Worksheet worksheetcie = (_Worksheet)worksheets.get_Item((1));

                int valcuerpo = CuerpoTrabajadores(worksheetcie, 1, 3);
                int valfila = FilaxAreaTrabajadores(worksheetcie, 1, valcuerpo, primero);

                Range rg = worksheetcie.get_Range("A3", "G3");
                rg.EntireColumn.AutoFit();
                worksheetcie.Name = areaC.GetAreaDiccio(primero).descripcion;

                _Worksheet ultpest = (_Worksheet)worksheets.get_Item((2));

                if (idareas.Count > 1)
                {
                    idareas.RemoveAt(0);
                    int contpaworksheet = 1;
                    foreach (var item in idareas)
                    {
                        _Worksheet worksheet = (_Worksheet)worksheets.get_Item((contpaworksheet + 1));
                        object dato = worksheets.Add(Missing.Value, ultpest, Missing.Value, Missing.Value);
                        ultpest = (_Worksheet)dato;

                        string nombreArea = areaC.GetAreaDiccio(item).descripcion;
                        int valcuerpo2 = CuerpoTrabajadores(worksheet, 1, 3);
                        int valfila2 = FilaxAreaTrabajadores(worksheet, 1, valcuerpo2, item);
                        rg = worksheet.get_Range("A3", "G3");
                        rg.EntireColumn.AutoFit();
                        worksheet.Name = areaC.GetAreaDiccio(item).descripcion;
                        contpaworksheet++;
                    }
                }

                this.objApp.Visible = true;
                this.objApp.UserControl = true;
            }
            catch (Exception ex)
            {
                int num4 = (int)MessageBox.Show("Error: " + ex.Message + " Line: " + ex.Source, "Error");
            }
        }

        public void GenerarExcelxAreaRegistros(List<int> idareas, DateTime fecha)
        {
            try
            {
                var trabajador = (from todos in personaC.Personas
                                  orderby todos.exp
                                  select todos.exp).ToList();
                this.objApp =
                    (Microsoft.Office.Interop.Excel.Application)new Microsoft.Office.Interop.Excel.Application();
                this.objBook = (_Workbook)this.objApp.Workbooks.Add(Missing.Value);
                Sheets worksheets = this.objBook.Worksheets;

                var primero = idareas.First();
                string nombre = areaC.GetAreaDiccio(Convert.ToInt32(primero)).descripcion;
                worksheets.Add(Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                _Worksheet worksheetcie = (_Worksheet)worksheets.get_Item((1));

                int valcuerpo = CuerpoRegistros(worksheetcie, 1, 3);
                int valfila = FilaxAreaRegistros(worksheetcie, 1, valcuerpo, primero, fecha);

                Range rg = worksheetcie.get_Range("A3", "H3");
                rg.EntireColumn.AutoFit();
                worksheetcie.Name = areaC.GetAreaDiccio(primero).descripcion;

                _Worksheet ultpest = (_Worksheet)worksheets.get_Item((2));

                if (idareas.Count > 1)
                {
                    idareas.RemoveAt(0);
                    int contpaworksheet = 1;
                    foreach (var item in idareas)
                    {
                        _Worksheet worksheet = (_Worksheet)worksheets.get_Item((contpaworksheet + 1));
                        object dato = worksheets.Add(Missing.Value, ultpest, Missing.Value, Missing.Value);
                        ultpest = (_Worksheet)dato;

                        string nombreArea = areaC.GetAreaDiccio(item).descripcion;
                        int valcuerpo2 = CuerpoRegistros(worksheet, 1, 3);
                        int valfila2 = FilaxAreaRegistros(worksheet, 1, valcuerpo2, item, fecha);
                        rg = worksheet.get_Range("A3", "F3");
                        rg.EntireColumn.AutoFit();
                        worksheet.Name = areaC.GetAreaDiccio(item).descripcion;
                        contpaworksheet++;
                    }
                }

                this.objApp.Visible = true;
                this.objApp.UserControl = true;
            }
            catch (Exception ex)
            {
                int num4 = (int)MessageBox.Show("Error: " + ex.Message + " Line: " + ex.Source, "Error");
            }
        }

        public void GenerarExcelAltasBajas(int año, int mes)
        {
            try
            {
                this.objApp =
                    (Microsoft.Office.Interop.Excel.Application)new Microsoft.Office.Interop.Excel.Application();
                this.objBook = (_Workbook)this.objApp.Workbooks.Add(Missing.Value);
                Sheets worksheets = this.objBook.Worksheets;


                //Altas
                worksheets.Add(Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                _Worksheet worksheetcie = (_Worksheet)worksheets.get_Item((1));
                int valcuerpo = CuerpoAlta(worksheetcie, 1, 1, año);
                int valfila = FilaxAreaAlta(worksheetcie, 1, valcuerpo, año, mes);
                Range rg = worksheetcie.get_Range("A2", "H2");
                rg.EntireColumn.AutoFit();
                worksheetcie.Name = "Altas " + año;

                //Bajas
               _Worksheet worksheet = (_Worksheet)worksheets.get_Item((2));
                int valcuerpo2 = CuerpoBaja(worksheet, 1, 1, año);
                int valfila2 = FilaxAreaBaja(worksheet, 1, valcuerpo2, año, mes);
                rg = worksheet.get_Range("A2", "K2");
                rg.EntireColumn.AutoFit();
                worksheet.Name = "Bajas " + año ;

                this.objApp.Visible = true;
                this.objApp.UserControl = true;
            }
            catch (Exception ex)
            {
                int num4 = (int)MessageBox.Show("Error: " + ex.Message + " Line: " + ex.Source, "Error");
            }
        }

        public void GenerarExcelCategorizacion()
        {
            try
            {
                this.objApp =
                    (Microsoft.Office.Interop.Excel.Application)new Microsoft.Office.Interop.Excel.Application();
                this.objBook = (_Workbook)this.objApp.Workbooks.Add(Missing.Value);
                Sheets worksheets = this.objBook.Worksheets;

                _Worksheet worksheetcie = (_Worksheet)worksheets.get_Item((1));

                string[] categorias = new string[] { "Biotecnológicas", "Docentes", "Tecnológicas", "Científicas", "Grado Científico" };

                int valcuerpo = 1;
                int valfila = 1;

                foreach (var item in categorias)
                {
                    valcuerpo = CuerpoCategoria(worksheetcie, 2, valfila, item);
                    valfila = FilaxAreaCategoria(worksheetcie, 2, valcuerpo, item);
                }
                worksheetcie.Name = "Categorización del CIE";
                Range rg = worksheetcie.get_Range("B3", "B3");
                rg.EntireColumn.AutoFit();
                this.objApp.Visible = true;
                this.objApp.UserControl = true;
            }
            catch (Exception ex)
            {
                int num4 = (int)MessageBox.Show("Error: " + ex.Message + " Line: " + ex.Source, "Error");
            }
        }

        public void GenerarExcelAsistenciaDia(DateTime dia, List<int> idareas)
        {
            try
            {

                this.objApp =
                    (Microsoft.Office.Interop.Excel.Application)new Microsoft.Office.Interop.Excel.Application();
                this.objBook = (_Workbook)this.objApp.Workbooks.Add(Missing.Value);
                Sheets worksheets = this.objBook.Worksheets;

                worksheets.Add(Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                _Worksheet worksheetcie = (_Worksheet)worksheets.get_Item((1));

                int valcuerpo = CuerpoAsistenciaDia(worksheetcie, 1, 1);
                int valfila = FilaxAreaAsistenciaDia(worksheetcie, valcuerpo, 2, dia);
               

                Range rg = worksheetcie.get_Range("A1", "G1");
                Range rgl = worksheetcie.get_Range("H1", "H1");
                rg.EntireColumn.NumberFormat = "0";
                rgl.EntireColumn.NumberFormat = "0.00";
                rg.EntireColumn.AutoFit();
                rg = worksheetcie.get_Range("A2", "H2");
                rg.EntireColumn.AutoFit();
                PieAsistencia(worksheetcie, 1, valfila, dia);
                worksheetcie.Name = "Asistencia CIE Resumen";

                _Worksheet ultpest = (_Worksheet)worksheets.get_Item((2));

                if (idareas.Count > 0)
                {
                    
                    int contpaworksheet = 1;
                    foreach (var item in idareas)
                    {
                        _Worksheet worksheet = (_Worksheet)worksheets.get_Item((contpaworksheet + 1));
                        object dato = worksheets.Add(Missing.Value, ultpest, Missing.Value, Missing.Value);
                        ultpest = (_Worksheet)dato;

                        int valcuerpo2 = CuerpoAsistenciaDia(worksheet, 1, 1);
                        int valfila2 = FilaxAreaAsistenciaDia(worksheet, valcuerpo2, 2, dia, item);
                        
                        rg = worksheet.get_Range("A1", "G1");
                        rgl = worksheet.get_Range("H1", "H1");
                        rg.EntireColumn.NumberFormat = "0";
                        rgl.EntireColumn.NumberFormat = "0.00";
                        rg.EntireColumn.AutoFit();
                        rg = worksheet.get_Range("A2", "H2");
                        rg.EntireColumn.AutoFit();
                        PieAsistencia(worksheet, 1, valfila, dia);
                        worksheet.Name = areaC.GetAreaDiccio(item).descripcion;
                        contpaworksheet++;
                    }
                }
                this.objApp.Visible = true;
                this.objApp.UserControl = true;
            }
            catch (Exception ex)
            {
                int num4 = (int)MessageBox.Show("Error: " + ex.Message + " Line: " + ex.Source, "Error");
            }
        }

        public void GenerarExcelAsistenciaMes(int año, int mes, List<int> idareas)
        {
            try
            {
                var trabajador = (from todos in personaC.Personas
                                  orderby todos.exp
                                  select todos.exp).ToList();

                DateTime fechainicio = new DateTime(año, mes, 1);
                DateTime fechafin = new DateTime(año, mes, DateTime.DaysInMonth(año, mes));
                if (idareas.Count == 1)
                {
                    Area area = areaC.GetAreaDiccio(idareas[0]);
                    var pers = areaC.PersonasActivasQuePertenecenArea(area);
                    int[] arr = new int[pers.Count];
                    for (int i = 0; i < pers.Count; i++)
                        arr[i] = pers[i].exp;
                    Trazas = servicio.TrazasEnRangoxListadoExp(fechainicio, fechafin, arr).ToList();
                }
                else
                    Trazas = servicio.TrazasEnRangoxListadoExp(fechainicio, fechafin, trabajador.ToArray()).ToList();
                this.objApp =
                    (Microsoft.Office.Interop.Excel.Application)new Microsoft.Office.Interop.Excel.Application();
                this.objBook = (_Workbook)this.objApp.Workbooks.Add(Missing.Value);
                Sheets worksheets = this.objBook.Worksheets;

                // PESTAÑA CIE==========================================

                var primero = idareas.First();
                string nombre = areaC.GetAreaDiccio(Convert.ToInt32(primero)).descripcion;
                worksheets.Add(Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                _Worksheet worksheetcie = (_Worksheet)worksheets.get_Item((1));

                var DiccioIncidencia = GenerarIncidenciaExpDiccioPorFecha(fechainicio, fechafin);
                var DiccioPlanes = GenerarPlanificacionExpDiccioPorFecha(fechainicio, fechafin);

                int valcuerpo = CuerpoAsistenciaMes(worksheetcie, 1, 2, mes, año, nombre);
                //int valfila = FilaxArea(worksheetcie, 1, valcuerpo, primero, mes, año, DiccioIncidencia, DiccioPlanes);

                //Range rg = worksheetcie.get_Range("B1", "N1");
                //rg.EntireColumn.AutoFit();
                ////====================================
                ////Range rgDATE = worksheetcie.get_Range("C1", "C1");
                ////rgDATE.EntireColumn.NumberFormat = "MM/DD/YY";
                ////====================================
                //Range rgLD = worksheetcie.get_Range("C1", "N1");
                //rgLD.EntireColumn.NumberFormat = "0.00";

                worksheetcie.Name = areaC.GetAreaDiccio(primero).descripcion;

                //_Worksheet ultpest = (_Worksheet)worksheets.get_Item((2));

                //if (idareas.Count > 1)
                //{
                //    idareas.RemoveAt(0);
                //    int contpaworksheet = 1;
                //    foreach (var item in idareas)
                //    {
                //        _Worksheet worksheet = (_Worksheet)worksheets.get_Item((contpaworksheet + 1));
                //        object dato = worksheets.Add(Missing.Value, ultpest, Missing.Value, Missing.Value);
                //        ultpest = (_Worksheet)dato;

                        //string nombreArea = areaC.GetAreaDiccio(item).descripcion;
                        //int valencabezado2 = Encabezado(worksheet, 1, 2, mes, año, nombreArea);
                        //int valcuerpo2 = Cuerpo(worksheet, 1, valencabezado2);
                        //int valfila2 = FilaxArea(worksheet, 1, valcuerpo2, item, mes, año, DiccioIncidencia, DiccioPlanes);

                        //rg = worksheet.get_Range("B1", "N1");
                        //rg.EntireColumn.AutoFit();
                        ////====================================
                        ////rgDATE = worksheet.get_Range("C1", "C1");
                        ////rgDATE.EntireColumn.NumberFormat = "MM/DD/YY";
                        ////====================================
                        //rgLD = worksheet.get_Range("C1", "N1");
                        //rgLD.EntireColumn.NumberFormat = "0.00";

                        //worksheet.Name = areaC.GetAreaDiccio(item).descripcion;
                        //contpaworksheet++;
                    //}
                //}

                this.objApp.Visible = true;
                this.objApp.UserControl = true;
            }
            catch (Exception ex)
            {
                int num4 = (int)MessageBox.Show("Error: " + ex.Message + " Line: " + ex.Source, "Error");
            }
        }

        private int PieGeneral(_Worksheet worksheet, int columna, int fila)
        {
            columna = 64 + columna; //caracter ascii de A
            int pcol = columna;
            int pfil = fila;

            string pos = Convert.ToChar(pcol + 1).ToString() + (pfil + 3).ToString();

            Range range1 = worksheet.get_Range(pos, pos);
            range1.Cells.Merge(Missing.Value);
            range1.set_Value(Missing.Value, "Elaborado por: ");

            pos = Convert.ToChar(pcol + 1).ToString() + (pfil + 5).ToString();
            Range range4 = worksheet.get_Range(pos, pos);
            range4.set_Value(Missing.Value, "Revisado por:");

            pos = Convert.ToChar(pcol + 2).ToString() + (pfil + 3).ToString();
            Range range5 = worksheet.get_Range(pos, pos);
            range5.set_Value(Missing.Value, "Fecha:");

            return pfil + 6;
        }

        private int PieAsistencia(_Worksheet worksheet, int columna, int fila, DateTime dia)
        {
            string pie = "";
            switch (dia.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    {
                        TimeSpan daysFriday = new TimeSpan(4, 0, 0, 0, 0);
                        DateTime friday = dia + daysFriday;
                        pie = "Informe correspondiente a la semana desde el " + dia.ToShortDateString() + " hasta el " + friday.ToShortDateString();
                    }
                    break;
                case DayOfWeek.Tuesday:
                    {
                        TimeSpan daysMonday = new TimeSpan(1, 0, 0, 0, 0);
                        TimeSpan daysFriday = new TimeSpan(3, 0, 0, 0, 0);
                        DateTime friday = dia + daysFriday;
                        DateTime monday = dia - daysMonday;
                        pie = "Informe correspondiente a la semana desde el " + monday.ToShortDateString() + " hasta el " + friday.ToShortDateString();
                    }
                    break;
                case DayOfWeek.Wednesday:
                    {
                        TimeSpan daysMonday = new TimeSpan(2, 0, 0, 0, 0);
                        TimeSpan daysFriday = new TimeSpan(2, 0, 0, 0, 0);
                        DateTime friday = dia + daysFriday;
                        DateTime monday = dia - daysMonday;
                        pie = "Informe correspondiente a la semana desde el " + monday.ToShortDateString() + " hasta el " + friday.ToShortDateString();
                    }
                    break;
                case DayOfWeek.Thursday:
                    {
                        TimeSpan daysMonday = new TimeSpan(3, 0, 0, 0, 0);
                        TimeSpan daysFriday = new TimeSpan(1, 0, 0, 0, 0);
                        DateTime friday = dia + daysFriday;
                        DateTime monday = dia - daysMonday;
                        pie = "Informe correspondiente a la semana desde el " + monday.ToShortDateString() + " hasta el " + friday.ToShortDateString();
                    }
                    break;
                case DayOfWeek.Friday:
                    {
                        TimeSpan daysMonday = new TimeSpan(4, 0, 0, 0, 0);
                        DateTime monday = dia - daysMonday;
                        pie = "Informe correspondiente a la semana desde el " + monday.ToShortDateString() + " hasta el " + dia.ToShortDateString();
                    }
                    break;
                default:
                    break;
            }

            columna = 64 + columna; //caracter ascii de A
            int pcol = columna;
            int pfil = fila;

            string pos = Convert.ToChar(pcol).ToString() + (pfil + 3).ToString();

            Range range1 = worksheet.get_Range(pos, pos);
            range1.Cells.Merge(Missing.Value);
            range1.set_Value(Missing.Value, pie);

            return pfil + 1;
        }

        /// <summary>
        /// Crea el encabezado del documento
        /// </summary>
        /// <param name="worksheet">Pestaña donde se insertará</param>
        /// <param name="columna">Columna de la celda</param>
        /// <param name="fila">Fila de la celda</param>
        /// <returns>Devuelve el número de la fila siguiente</returns>
        private int Encabezado(_Worksheet worksheet, int columna, int fila, int mes, int ano, string nombreArea)
        {
            columna = 64 + columna; //caracter ascii de A
            int pcol = columna;
            int pfil = fila;

            string pos = Convert.ToChar(pcol).ToString() + pfil.ToString();

            Range range1 = worksheet.get_Range(pos, pos);
            range1.Cells.Merge(Missing.Value);
            range1.set_Value(Missing.Value, "No.");
            pcol++;

            pos = Convert.ToChar(pcol).ToString() + pfil.ToString();
            Range range2 = worksheet.get_Range(pos, pos);
            range2.Cells.Merge(Missing.Value);
            string texto = " Reporte de Incidencias del área de " + nombreArea.ToUpper() + "";
            range2.set_Value(Missing.Value, texto.ToUpper());
            range2.Font.Bold = true;
            pfil++;

            pos = Convert.ToChar(pcol).ToString() + pfil.ToString();
            Range range3 = worksheet.get_Range(pos, pos);
            range3.Cells.Merge(Missing.Value);
            range3.set_Value(Missing.Value, "CENTRO DE INMUNOENSAYO       REEUP: 305.0.06676");
            range3.Font.Bold = true;
            pfil++;

            pos = Convert.ToChar(pcol).ToString() + pfil.ToString();
            Range range4 = worksheet.get_Range(pos, pos);
            range4.Cells.Merge(Missing.Value);
            range4.set_Value(Missing.Value, "OSDE: BIOCUBAFARMA");
            range4.Font.Bold = true;
            pfil++;


            pos = Convert.ToChar(pcol).ToString() + pfil.ToString();
            Range range5 = worksheet.get_Range(pos, pos);
            range5.Cells.Merge(Missing.Value);
            range5.set_Value(Missing.Value, "MES: " + DimeMes(mes).ToUpper());
            range5.Font.Bold = true;
            pfil++;

            pos = Convert.ToChar(pcol).ToString() + pfil.ToString();
            Range range6 = worksheet.get_Range(pos, pos);
            range6.Cells.Merge(Missing.Value);
            range6.set_Value(Missing.Value, "AÑO: " + ano);
            range6.Font.Bold = true;

            string pos1 = Convert.ToChar(columna).ToString() + fila.ToString();
            pos = Convert.ToChar(pcol).ToString() + pfil.ToString();

            Range range7 = worksheet.get_Range(pos1, pos);
            range7.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlMedium, XlColorIndex.xlColorIndexAutomatic,
                Missing.Value);
            range7.Interior.Color = Color.White;

            string pos2 = Convert.ToChar(columna).ToString() + (fila + 3).ToString();
            string pos3 = Convert.ToChar(columna).ToString() + (fila + 4).ToString();

            range7 = worksheet.get_Range(pos1, pos2);
            range7.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlMedium, XlColorIndex.xlColorIndexAutomatic,
                Missing.Value);
            range7 = worksheet.get_Range(pos3, pos3);
            range7.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlMedium, XlColorIndex.xlColorIndexAutomatic,
                Missing.Value);

            return fila + 5;
        }

        /// <summary>
        ///  Crea el cuerpo de la tabla a llenar
        /// </summary>
        /// <param name="worksheet">Pestaña donde se insertará</param>
        /// <param name="columna">Columna de la celda</param>
        /// <param name="fila">Fila de la celda</param>
        /// <returns>Devuelve el número de la fila siguiente</returns>
        private int Cuerpo(_Worksheet worksheet, int columna, int fila)
        {
            columna = 64 + columna; //caracter ascii de A
            int pcol = columna;
            int pfil = fila;

            string pos = Convert.ToChar(pcol + 1).ToString() + pfil.ToString();

            Range range1 = worksheet.get_Range(pos, pos);
            range1.Cells.Merge(Missing.Value);
            range1.HorizontalAlignment = HorizontalAlignment.Center;
            range1.set_Value(Missing.Value, "Datos Generales");


            pos = Convert.ToChar(pcol + 5).ToString() + pfil.ToString();
            string pos2 = Convert.ToChar(pcol + 12).ToString() + pfil.ToString();
            Range range17 = worksheet.get_Range(pos, pos2);
            range17.Cells.Merge(Missing.Value);
            range17.HorizontalAlignment = HorizontalAlignment.Center;
            range17.set_Value(Missing.Value, "Tiempo no Laborado");
            range17.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic,
                Missing.Value);

            pos = Convert.ToChar(pcol).ToString() + (pfil + 1).ToString();
            Range range3 = worksheet.get_Range(pos, pos);
            range3.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic,
                Missing.Value);
            range3.set_Value(Missing.Value, "Exp.");

            pos = Convert.ToChar(pcol + 1).ToString() + (pfil + 1).ToString();
            Range range4 = worksheet.get_Range(pos, pos);
            range4.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic,
                Missing.Value);
            range4.set_Value(Missing.Value, "Nombres y Apellidos");
            range4.Font.Bold = true;

            pos = Convert.ToChar(pcol + 2).ToString() + (pfil + 1).ToString();
            Range range6 = worksheet.get_Range(pos, pos);
            range6.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic,
                Missing.Value);
            range6.Cells.Merge(Missing.Value);
            range6.HorizontalAlignment = HorizontalAlignment.Center;
            range6.set_Value(Missing.Value, "Tardanzas");

            pos = Convert.ToChar(pcol + 3).ToString() + (pfil + 1).ToString();
            Range range7 = worksheet.get_Range(pos, pos);
            range7.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic,
                Missing.Value);
            range7.Cells.Merge(Missing.Value);
            range7.HorizontalAlignment = HorizontalAlignment.Center;
            range7.set_Value(Missing.Value, "Laborado");


            pos = Convert.ToChar(pcol + 4).ToString() + (pfil + 1).ToString();
            Range range8 = worksheet.get_Range(pos, pos);
            range8.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic,
                Missing.Value);
            range8.Cells.Merge(Missing.Value);
            range8.HorizontalAlignment = HorizontalAlignment.Center;
            range8.set_Value(Missing.Value, "No Laborado");

            pos = Convert.ToChar(pcol + 5).ToString() + (pfil + 1).ToString();
            Range range20 = worksheet.get_Range(pos, pos);
            range20.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic,
                Missing.Value);
            range20.Cells.Merge(Missing.Value);
            range20.HorizontalAlignment = HorizontalAlignment.Center;
            range20.set_Value(Missing.Value, "GS1");

            pos = Convert.ToChar(pcol + 6).ToString() + (pfil + 1).ToString();
            Range range18 = worksheet.get_Range(pos, pos);
            range18.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic,
                Missing.Value);
            range18.Cells.Merge(Missing.Value);
            range18.HorizontalAlignment = HorizontalAlignment.Center;
            range18.set_Value(Missing.Value, "GS6");


            pos = Convert.ToChar(pcol + 7).ToString() + (pfil + 1).ToString();
            Range range19 = worksheet.get_Range(pos, pos);
            range19.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic,
                Missing.Value);
            range19.Cells.Merge(Missing.Value);
            range19.HorizontalAlignment = HorizontalAlignment.Center;
            range19.set_Value(Missing.Value, "Interrupto");

            pos = Convert.ToChar(pcol + 8).ToString() + (pfil + 1).ToString();
            Range range11 = worksheet.get_Range(pos, pos);
            range11.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic,
                Missing.Value);
            range11.Cells.Merge(Missing.Value);
            range11.HorizontalAlignment = HorizontalAlignment.Center;
            range11.set_Value(Missing.Value, "Vacaciones");

            pos = Convert.ToChar(pcol + 9).ToString() + (pfil + 1).ToString();
            Range range9 = worksheet.get_Range(pos, pos);
            range9.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic,
                Missing.Value);
            range9.Cells.Merge(Missing.Value);
            range9.HorizontalAlignment = HorizontalAlignment.Center;
            range9.set_Value(Missing.Value, "Subsidios");

            pos = Convert.ToChar(pcol + 10).ToString() + (pfil + 1).ToString();
            Range range15 = worksheet.get_Range(pos, pos);
            range15.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic,
                Missing.Value);
            range15.Cells.Merge(Missing.Value);
            range15.HorizontalAlignment = HorizontalAlignment.Center;
            range15.set_Value(Missing.Value, "EU y Mov");

            pos = Convert.ToChar(pcol + 11).ToString() + (pfil + 1).ToString();
            Range range14 = worksheet.get_Range(pos, pos);
            range14.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic,
                Missing.Value);
            range14.Cells.Merge(Missing.Value);
            range14.HorizontalAlignment = HorizontalAlignment.Center;
            range14.set_Value(Missing.Value, "Días Feriados");

            pos = Convert.ToChar(pcol + 12).ToString() + (pfil + 1).ToString();
            Range range12 = worksheet.get_Range(pos, pos);
            range12.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic,
                Missing.Value);
            range12.Cells.Merge(Missing.Value);
            range12.HorizontalAlignment = HorizontalAlignment.Center;
            range12.set_Value(Missing.Value, "Otros");

            pos = Convert.ToChar(pcol + 13).ToString() + (pfil + 1).ToString();
            Range range16 = worksheet.get_Range(pos, pos);
            range16.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic,
                Missing.Value);
            range16.Cells.Merge(Missing.Value);
            range16.HorizontalAlignment = HorizontalAlignment.Center;
            range16.set_Value(Missing.Value, "Total SistCon");


            pos = Convert.ToChar(pcol).ToString() + (pfil).ToString();
            string pos1 = Convert.ToChar(pcol + 2).ToString() + (pfil).ToString();
            Range range10 = worksheet.get_Range(pos, pos1);
            range10.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic,
                Missing.Value);
            range10.Interior.Color = Color.White;
            pos = Convert.ToChar(pcol + 3).ToString() + (pfil).ToString();
            pos1 = Convert.ToChar(pcol + 3).ToString() + (pfil + 1).ToString();
            range10 = worksheet.get_Range(pos, pos1);
            range10.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic,
                Missing.Value);
            pos = Convert.ToChar(pcol + 4).ToString() + (pfil).ToString();
            pos1 = Convert.ToChar(pcol + 13).ToString() + (pfil).ToString();
            range10 = worksheet.get_Range(pos, pos1);
            range10.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic,
                Missing.Value);

            return pfil + 2;

        }

        private int CuerpoAsistenciaDia(_Worksheet worksheet, int columna, int fila)
        {
            columna = 64 + columna; //caracter ascii de A
            int pcol = columna;
            int pfil = fila;

            string pos = Convert.ToChar(pcol + 1).ToString() + pfil.ToString();

            Range range1 = worksheet.get_Range(pos, pos);
            range1.Cells.Merge(Missing.Value);
            range1.HorizontalAlignment = HorizontalAlignment.Center;
            range1.set_Value(Missing.Value, "Lunes");

            pos = Convert.ToChar(pcol + 2).ToString() + pfil.ToString();
            Range range2 = worksheet.get_Range(pos, pos);
            range2.Cells.Merge(Missing.Value);
            range2.HorizontalAlignment = HorizontalAlignment.Center;
            range2.set_Value(Missing.Value, "Martes");

            pos = Convert.ToChar(pcol + 3).ToString() + pfil.ToString();
            Range range3 = worksheet.get_Range(pos, pos);
            range3.Cells.Merge(Missing.Value);
            range3.HorizontalAlignment = HorizontalAlignment.Center;
            range3.set_Value(Missing.Value, "Miércoles");

            pos = Convert.ToChar(pcol + 4).ToString() + pfil.ToString();
            Range range4 = worksheet.get_Range(pos, pos);
            range4.Cells.Merge(Missing.Value);
            range4.HorizontalAlignment = HorizontalAlignment.Center;
            range4.set_Value(Missing.Value, "Jueves");

            pos = Convert.ToChar(pcol + 5).ToString() + pfil.ToString();
            Range range5 = worksheet.get_Range(pos, pos);
            range5.Cells.Merge(Missing.Value);
            range5.HorizontalAlignment = HorizontalAlignment.Center;
            range5.set_Value(Missing.Value, "Viernes");

            pos = Convert.ToChar(pcol + 6).ToString() + pfil.ToString();
            Range range6 = worksheet.get_Range(pos, pos);
            range6.Cells.Merge(Missing.Value);
            range6.HorizontalAlignment = HorizontalAlignment.Center;
            range6.set_Value(Missing.Value, "Total");

            pos = Convert.ToChar(pcol + 7).ToString() + pfil.ToString();
            Range range7 = worksheet.get_Range(pos, pos);
            range7.Cells.Merge(Missing.Value);
            range7.HorizontalAlignment = HorizontalAlignment.Center;
            range7.set_Value(Missing.Value, "Promedio Semanal");

            pos = Convert.ToChar(pcol).ToString() + (pfil + 1).ToString();
            Range range8 = worksheet.get_Range(pos, pos);
            range8.Cells.Merge(Missing.Value);
            range8.HorizontalAlignment = HorizontalAlignment.Center;
            range8.set_Value(Missing.Value, "Presentes");

            pos = Convert.ToChar(pcol).ToString() + (pfil + 2).ToString();
            Range range9 = worksheet.get_Range(pos, pos);
            range9.Cells.Merge(Missing.Value);
            range9.HorizontalAlignment = HorizontalAlignment.Center;
            range9.set_Value(Missing.Value, "Trabajo a Distancia");

            pos = Convert.ToChar(pcol).ToString() + (pfil + 3).ToString();
            Range range10 = worksheet.get_Range(pos, pos);
            range10.Cells.Merge(Missing.Value);
            range10.HorizontalAlignment = HorizontalAlignment.Center;
            range10.set_Value(Missing.Value, "Vacaciones");

            pos = Convert.ToChar(pcol).ToString() + (pfil + 4).ToString();
            Range range11 = worksheet.get_Range(pos, pos);
            range11.Cells.Merge(Missing.Value);
            range11.HorizontalAlignment = HorizontalAlignment.Center;
            range11.set_Value(Missing.Value, "Certificados Médicos");

            pos = Convert.ToChar(pcol).ToString() + (pfil + 5).ToString();
            Range range12 = worksheet.get_Range(pos, pos);
            range12.Cells.Merge(Missing.Value);
            range12.HorizontalAlignment = HorizontalAlignment.Center;
            range12.set_Value(Missing.Value, "Licencia sin Sueldo");

            pos = Convert.ToChar(pcol).ToString() + (pfil + 6).ToString();
            Range range13 = worksheet.get_Range(pos, pos);
            range13.Cells.Merge(Missing.Value);
            range13.HorizontalAlignment = HorizontalAlignment.Center;
            range13.set_Value(Missing.Value, "Garantía Salarial 100%");

            pos = Convert.ToChar(pcol).ToString() + (pfil + 7).ToString();
            Range range14 = worksheet.get_Range(pos, pos);
            range14.Cells.Merge(Missing.Value);
            range14.HorizontalAlignment = HorizontalAlignment.Center;
            range14.set_Value(Missing.Value, "Garantía Salarial 60%");

            pos = Convert.ToChar(pcol).ToString() + (pfil + 8).ToString();
            Range range15 = worksheet.get_Range(pos, pos);
            range15.Cells.Merge(Missing.Value);
            range15.HorizontalAlignment = HorizontalAlignment.Center;
            range15.set_Value(Missing.Value, "Interruptos");

            pos = Convert.ToChar(pcol).ToString() + (pfil + 9).ToString();
            Range range16 = worksheet.get_Range(pos, pos);
            range16.Cells.Merge(Missing.Value);
            range16.HorizontalAlignment = HorizontalAlignment.Center;
            range16.set_Value(Missing.Value, "Licencia de Maternidad");


            return pcol + 1;

        }

        private int CuerpoAsistenciaMes(_Worksheet worksheet, int columna, int fila, int mes, int año, string nombre)
        {
            columna = 64 + columna; //caracter ascii de A
            int pcol = columna;
            int pfil = fila;

            string pos = Convert.ToChar(pcol + 1).ToString() + pfil.ToString();

            Range range1 = worksheet.get_Range(pos, pos);
            range1.Cells.Merge(Missing.Value);
            range1.HorizontalAlignment = HorizontalAlignment.Center;
            range1.set_Value(Missing.Value, "Área: " + nombre);

            pos = Convert.ToChar(pcol + 2).ToString() + pfil.ToString();
            Range range2 = worksheet.get_Range(pos, pos);
            range2.Cells.Merge(Missing.Value);
            range2.HorizontalAlignment = HorizontalAlignment.Center;
            range2.set_Value(Missing.Value, "Mes: " + DimeMes(mes) + " " + año);

            
            pos = Convert.ToChar(pcol).ToString() + (pfil + 2).ToString();
            Range range3 = worksheet.get_Range(pos, pos);
            range3.Cells.Merge(Missing.Value);
            range3.HorizontalAlignment = HorizontalAlignment.Center;
            range3.set_Value(Missing.Value, "Exp.");

            pos = Convert.ToChar(pcol + 1).ToString() + (pfil + 2).ToString();
            Range range4 = worksheet.get_Range(pos, pos);
            range4.Cells.Merge(Missing.Value);
            range4.HorizontalAlignment = HorizontalAlignment.Center;
            range4.set_Value(Missing.Value, "Nombres y Apellidos");

            int valorColumna = 2;
            for (int i = 1; i <= DateTime.DaysInMonth(año, mes); i++)
            {
                DateTime dia = new DateTime(año, mes, i);

                if (!(dia.DayOfWeek == DayOfWeek.Saturday || dia.DayOfWeek == DayOfWeek.Sunday))
                {
                    string nombreDia = "";

                    switch (dia.DayOfWeek)
                    {
                        case DayOfWeek.Monday:
                            nombreDia = "Lunes";
                            break;
                        case DayOfWeek.Tuesday:
                            nombreDia = "Martes";
                            break;
                        case DayOfWeek.Wednesday:
                            nombreDia = "Miercoles";
                            break;
                        case DayOfWeek.Thursday:
                            nombreDia = "Jueves";
                            break;
                        default:
                            nombreDia = "Viernes";
                            break;
                    }

                    pos = Convert.ToChar(pcol + valorColumna).ToString() + (pfil + 2).ToString();
                    Range range5 = worksheet.get_Range(pos, pos);
                    range5.Cells.Merge(Missing.Value);
                    range5.HorizontalAlignment = HorizontalAlignment.Center;
                    range5.set_Value(Missing.Value, nombreDia + " " + i);
                    valorColumna++;
                }                
            }


            //pos = Convert.ToChar(pcol + 5).ToString() + pfil.ToString();
            //Range range5 = worksheet.get_Range(pos, pos);
            //range5.Cells.Merge(Missing.Value);
            //range5.HorizontalAlignment = HorizontalAlignment.Center;
            //range5.set_Value(Missing.Value, "Viernes");

            //pos = Convert.ToChar(pcol + 6).ToString() + pfil.ToString();
            //Range range6 = worksheet.get_Range(pos, pos);
            //range6.Cells.Merge(Missing.Value);
            //range6.HorizontalAlignment = HorizontalAlignment.Center;
            //range6.set_Value(Missing.Value, "Total");

            //pos = Convert.ToChar(pcol + 7).ToString() + pfil.ToString();
            //Range range7 = worksheet.get_Range(pos, pos);
            //range7.Cells.Merge(Missing.Value);
            //range7.HorizontalAlignment = HorizontalAlignment.Center;
            //range7.set_Value(Missing.Value, "Promedio Semanal");

            //pos = Convert.ToChar(pcol).ToString() + (pfil + 1).ToString();
            //Range range8 = worksheet.get_Range(pos, pos);
            //range8.Cells.Merge(Missing.Value);
            //range8.HorizontalAlignment = HorizontalAlignment.Center;
            //range8.set_Value(Missing.Value, "Presentes");

            //pos = Convert.ToChar(pcol).ToString() + (pfil + 2).ToString();
            //Range range9 = worksheet.get_Range(pos, pos);
            //range9.Cells.Merge(Missing.Value);
            //range9.HorizontalAlignment = HorizontalAlignment.Center;
            //range9.set_Value(Missing.Value, "Trabajo a Distancia");

            //pos = Convert.ToChar(pcol).ToString() + (pfil + 3).ToString();
            //Range range10 = worksheet.get_Range(pos, pos);
            //range10.Cells.Merge(Missing.Value);
            //range10.HorizontalAlignment = HorizontalAlignment.Center;
            //range10.set_Value(Missing.Value, "Vacaciones");

            //pos = Convert.ToChar(pcol).ToString() + (pfil + 4).ToString();
            //Range range11 = worksheet.get_Range(pos, pos);
            //range11.Cells.Merge(Missing.Value);
            //range11.HorizontalAlignment = HorizontalAlignment.Center;
            //range11.set_Value(Missing.Value, "Certificados Médicos");

            //pos = Convert.ToChar(pcol).ToString() + (pfil + 5).ToString();
            //Range range12 = worksheet.get_Range(pos, pos);
            //range12.Cells.Merge(Missing.Value);
            //range12.HorizontalAlignment = HorizontalAlignment.Center;
            //range12.set_Value(Missing.Value, "Licencia sin Sueldo");

            //pos = Convert.ToChar(pcol).ToString() + (pfil + 6).ToString();
            //Range range13 = worksheet.get_Range(pos, pos);
            //range13.Cells.Merge(Missing.Value);
            //range13.HorizontalAlignment = HorizontalAlignment.Center;
            //range13.set_Value(Missing.Value, "Garantía Salarial 100%");

            //pos = Convert.ToChar(pcol).ToString() + (pfil + 7).ToString();
            //Range range14 = worksheet.get_Range(pos, pos);
            //range14.Cells.Merge(Missing.Value);
            //range14.HorizontalAlignment = HorizontalAlignment.Center;
            //range14.set_Value(Missing.Value, "Garantía Salarial 60%");

            //pos = Convert.ToChar(pcol).ToString() + (pfil + 8).ToString();
            //Range range15 = worksheet.get_Range(pos, pos);
            //range15.Cells.Merge(Missing.Value);
            //range15.HorizontalAlignment = HorizontalAlignment.Center;
            //range15.set_Value(Missing.Value, "Interruptos");

            //pos = Convert.ToChar(pcol).ToString() + (pfil + 9).ToString();
            //Range range16 = worksheet.get_Range(pos, pos);
            //range16.Cells.Merge(Missing.Value);
            //range16.HorizontalAlignment = HorizontalAlignment.Center;
            //range16.set_Value(Missing.Value, "Licencia de Maternidad");


            return pcol + 1;

        }

        /// <summary>
        ///  Crea el cuerpo de la tabla a llenar
        /// </summary>
        /// <param name="worksheet">Pestaña donde se insertará</param>
        /// <param name="columna">Columna de la celda</param>
        /// <param name="fila">Fila de la celda</param>
        /// <returns>Devuelve el número de la fila siguiente</returns>
        private int CuerpoTrabajadores(_Worksheet worksheet, int columna, int fila)
        {
            columna = 64 + columna; //caracter ascii de A
            int pcol = columna;
            int pfil = fila;

            string pos = Convert.ToChar(pcol).ToString() + pfil.ToString();

            Range range1 = worksheet.get_Range(pos, pos);
            range1.Cells.Merge(Missing.Value);
            range1.HorizontalAlignment = HorizontalAlignment.Center;
            range1.set_Value(Missing.Value, "Exp.");

            pos = Convert.ToChar(pcol + 1).ToString() + pfil.ToString();
            Range range2 = worksheet.get_Range(pos, pos);
            range2.Cells.Merge(Missing.Value);
            range2.HorizontalAlignment = HorizontalAlignment.Center;
            range2.set_Value(Missing.Value, "Nombre");

            pos = Convert.ToChar(pcol + 2).ToString() + pfil.ToString();
            Range range3 = worksheet.get_Range(pos, pos);
            range3.Cells.Merge(Missing.Value);
            range3.HorizontalAlignment = HorizontalAlignment.Center;
            range3.set_Value(Missing.Value, "Carnet de Id.");

            pos = Convert.ToChar(pcol + 3).ToString() + pfil.ToString();
            Range range4 = worksheet.get_Range(pos, pos);
            range4.Cells.Merge(Missing.Value);
            range4.HorizontalAlignment = HorizontalAlignment.Center;
            range4.set_Value(Missing.Value, "Dirección");

            pos = Convert.ToChar(pcol + 4).ToString() + pfil.ToString();
            Range range5 = worksheet.get_Range(pos, pos);
            range5.Cells.Merge(Missing.Value);
            range5.HorizontalAlignment = HorizontalAlignment.Center;
            range5.set_Value(Missing.Value, "Teléfono");

            pos = Convert.ToChar(pcol + 5).ToString() + pfil.ToString();
            Range range6 = worksheet.get_Range(pos, pos);
            range6.Cells.Merge(Missing.Value);
            range6.HorizontalAlignment = HorizontalAlignment.Center;
            range6.set_Value(Missing.Value, "Municipio");


            string posOriginal = Convert.ToChar(pcol).ToString() + pfil.ToString();
            Range range7 = worksheet.get_Range(posOriginal, pos);
            range7.Cells.Interior.Color = Color.FromArgb(255, 168, 208, 142);

            return pfil + 1;
        }

        private int CuerpoRegistros(_Worksheet worksheet, int columna, int fila)
        {
            columna = 64 + columna; //caracter ascii de A
            int pcol = columna;
            int pfil = fila;

            string pos = Convert.ToChar(pcol).ToString() + pfil.ToString();

            Range range1 = worksheet.get_Range(pos, pos);
            range1.Cells.Merge(Missing.Value);
            range1.HorizontalAlignment = HorizontalAlignment.Center;
            range1.set_Value(Missing.Value, "Exp.");

            pos = Convert.ToChar(pcol + 1).ToString() + pfil.ToString();
            Range range2 = worksheet.get_Range(pos, pos);
            range2.Cells.Merge(Missing.Value);
            range2.HorizontalAlignment = HorizontalAlignment.Center;
            range2.set_Value(Missing.Value, "Nombre");

            pos = Convert.ToChar(pcol + 2).ToString() + pfil.ToString();
            string pos2 = Convert.ToChar(pcol + 7).ToString() + pfil.ToString();
            Range range3 = worksheet.get_Range(pos, pos2);
            range3.Cells.Merge(Missing.Value);
            range3.HorizontalAlignment = HorizontalAlignment.Center;
            range3.set_Value(Missing.Value, "Registros");

            string posOriginal = Convert.ToChar(pcol).ToString() + pfil.ToString();
            Range range6 = worksheet.get_Range(posOriginal, pos2);
            range6.Cells.Interior.Color = Color.FromArgb(255, 155, 194, 230);

            return pfil + 1;
        }

        private int CuerpoAlta(_Worksheet worksheet, int columna, int fila, int año)
        {
            columna = 64 + columna; //caracter ascii de A
            int pcol = columna;
            int pfil = fila;

            string pos = Convert.ToChar(pcol).ToString() + pfil.ToString();
            string pos2 = Convert.ToChar(pcol + 7).ToString() + pfil.ToString();
            Range range1 = worksheet.get_Range(pos, pos2);
            range1.Cells.Merge(Missing.Value);
            range1.HorizontalAlignment = HorizontalAlignment.Center;
            range1.set_Value(Missing.Value, "ALTAS " + año);
            range1.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlMedium, XlColorIndex.xlColorIndexAutomatic,
                Missing.Value);

            pos = Convert.ToChar(pcol).ToString() + (pfil+1).ToString();
            Range range2 = worksheet.get_Range(pos, pos);
            range2.HorizontalAlignment = HorizontalAlignment.Center;
            range2.set_Value(Missing.Value, "Exp.");
            range2.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic,
                Missing.Value);

            pos = Convert.ToChar(pcol + 1).ToString() + (pfil + 1).ToString();
            Range range3 = worksheet.get_Range(pos, pos);
            range3.HorizontalAlignment = HorizontalAlignment.Center;
            range3.set_Value(Missing.Value, "Nombre y Apellidos");
            range3.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic,
                Missing.Value);

            pos = Convert.ToChar(pcol + 2).ToString() + (pfil + 1).ToString();
            Range range4 = worksheet.get_Range(pos, pos);
            range4.HorizontalAlignment = HorizontalAlignment.Center;
            range4.set_Value(Missing.Value, "Plaza a Ocupar");
            range4.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic,
                Missing.Value);

            pos = Convert.ToChar(pcol + 3).ToString() + (pfil + 1).ToString();
            Range range5 = worksheet.get_Range(pos, pos);
            range5.HorizontalAlignment = HorizontalAlignment.Center;
            range5.set_Value(Missing.Value, "Sexo");
            range5.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic,
                Missing.Value);

            pos = Convert.ToChar(pcol + 4).ToString() + (pfil + 1).ToString();
            Range range6 = worksheet.get_Range(pos, pos);
            range6.HorizontalAlignment = HorizontalAlignment.Center;
            range6.set_Value(Missing.Value, "Edad");
            range6.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic,
                Missing.Value);

            pos = Convert.ToChar(pcol + 5).ToString() + (pfil + 1).ToString();
            Range range7 = worksheet.get_Range(pos, pos);
            range7.HorizontalAlignment = HorizontalAlignment.Center;
            range7.set_Value(Missing.Value, "Fecha de Alta");
            range7.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic,
                Missing.Value);

            pos = Convert.ToChar(pcol + 6).ToString() + (pfil + 1).ToString();
            Range range8 = worksheet.get_Range(pos, pos);
            range8.HorizontalAlignment = HorizontalAlignment.Center;
            range8.set_Value(Missing.Value, "Raza");
            range8.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic,
                Missing.Value);

            pos = Convert.ToChar(pcol + 7).ToString() + (pfil + 1).ToString();
            Range range9 = worksheet.get_Range(pos, pos);
            range9.HorizontalAlignment = HorizontalAlignment.Center;
            range9.set_Value(Missing.Value, "Tipo de Contrato");
            range9.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic,
                Missing.Value);

            return pfil + 2;
        }

        private int CuerpoBaja(_Worksheet worksheet, int columna, int fila, int año)
        {
            columna = 64 + columna; //caracter ascii de A
            int pcol = columna;
            int pfil = fila;

            string pos = Convert.ToChar(pcol).ToString() + pfil.ToString();
            string pos2 = Convert.ToChar(pcol + 10).ToString() + pfil.ToString();
            Range range1 = worksheet.get_Range(pos, pos2);
            range1.Cells.Merge(Missing.Value);
            range1.HorizontalAlignment = HorizontalAlignment.Center;
            range1.set_Value(Missing.Value, "BAJAS " + año);
            range1.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlMedium, XlColorIndex.xlColorIndexAutomatic,
                Missing.Value);

            pos = Convert.ToChar(pcol).ToString() + (pfil + 1).ToString();
            Range range2 = worksheet.get_Range(pos, pos);
            range2.HorizontalAlignment = HorizontalAlignment.Center;
            range2.set_Value(Missing.Value, "Exp.");
            range2.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic,
                Missing.Value);

            pos = Convert.ToChar(pcol + 1).ToString() + (pfil + 1).ToString();
            Range range3 = worksheet.get_Range(pos, pos);
            range3.HorizontalAlignment = HorizontalAlignment.Center;
            range3.set_Value(Missing.Value, "Nombre y Apellidos");
            range3.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic,
                Missing.Value);

            pos = Convert.ToChar(pcol + 2).ToString() + (pfil + 1).ToString();
            Range range4 = worksheet.get_Range(pos, pos);
            range4.HorizontalAlignment = HorizontalAlignment.Center;
            range4.set_Value(Missing.Value, "Categoría Ocupacional");
            range4.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic,
                Missing.Value);

            pos = Convert.ToChar(pcol + 3).ToString() + (pfil + 1).ToString();
            Range range5 = worksheet.get_Range(pos, pos);
            range5.HorizontalAlignment = HorizontalAlignment.Center;
            range5.set_Value(Missing.Value, "Sexo");
            range5.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic,
                Missing.Value);

            pos = Convert.ToChar(pcol + 4).ToString() + (pfil + 1).ToString();
            Range range6 = worksheet.get_Range(pos, pos);
            range6.HorizontalAlignment = HorizontalAlignment.Center;
            range6.set_Value(Missing.Value, "Edad");
            range6.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic,
                Missing.Value);

            pos = Convert.ToChar(pcol + 5).ToString() + (pfil + 1).ToString();
            Range range7 = worksheet.get_Range(pos, pos);
            range7.HorizontalAlignment = HorizontalAlignment.Center;
            range7.set_Value(Missing.Value, "Fecha de Alta");
            range7.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic,
                Missing.Value);

            pos = Convert.ToChar(pcol + 6).ToString() + (pfil + 1).ToString();
            Range range8 = worksheet.get_Range(pos, pos);
            range8.HorizontalAlignment = HorizontalAlignment.Center;
            range8.set_Value(Missing.Value, "Fecha de Baja");
            range8.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic,
                Missing.Value);

            pos = Convert.ToChar(pcol + 7).ToString() + (pfil + 1).ToString();
            Range range9 = worksheet.get_Range(pos, pos);
            range9.HorizontalAlignment = HorizontalAlignment.Center;
            range9.set_Value(Missing.Value, "Motivo");
            range9.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic,
                Missing.Value);

            pos = Convert.ToChar(pcol + 8).ToString() + (pfil + 1).ToString();
            Range range10 = worksheet.get_Range(pos, pos);
            range10.HorizontalAlignment = HorizontalAlignment.Center;
            range10.set_Value(Missing.Value, "Nivel Escolar");
            range10.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic,
                Missing.Value);

            pos = Convert.ToChar(pcol + 9).ToString() + (pfil + 1).ToString();
            Range range11 = worksheet.get_Range(pos, pos);
            range11.HorizontalAlignment = HorizontalAlignment.Center;
            range11.set_Value(Missing.Value, "Raza");
            range11.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic,
                Missing.Value);

            pos = Convert.ToChar(pcol + 10).ToString() + (pfil + 1).ToString();
            Range range12 = worksheet.get_Range(pos, pos);
            range12.HorizontalAlignment = HorizontalAlignment.Center;
            range12.set_Value(Missing.Value, "Tipo de Contrato");
            range12.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic,
                Missing.Value);

            return pfil + 2;
        }

        private int CuerpoCategoria(_Worksheet worksheet, int columna, int fila, string tipo)
        {
            columna = 64 + columna; //caracter ascii de A
            int pcol = columna;
            int pfil = fila;

            string pos = Convert.ToChar(pcol + 4).ToString() + (pfil + 1).ToString();
            string pos2 = Convert.ToChar(pcol + 7).ToString() + (pfil + 1).ToString();
            Range range1 = worksheet.get_Range(pos, pos2);
            range1.Cells.Merge(Missing.Value);
            range1.HorizontalAlignment = HorizontalAlignment.Center;
            range1.set_Value(Missing.Value, "CATEGORÍA OCUPACIONAL");
            range1.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlMedium, XlColorIndex.xlColorIndexAutomatic,
                Missing.Value);
            range1.Cells.Interior.Color = Color.FromArgb(255, 255, 255, 0);

            if (tipo.Equals("Grado Científico"))
            {
                pos = Convert.ToChar(pcol).ToString() + (pfil + 2).ToString();
                Range range2 = worksheet.get_Range(pos, pos);
                range2.HorizontalAlignment = HorizontalAlignment.Center;
                range2.set_Value(Missing.Value, tipo);
                range2.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic,
                    Missing.Value);
            }
            else
            {
                pos = Convert.ToChar(pcol).ToString() + (pfil + 2).ToString();
                Range range2 = worksheet.get_Range(pos, pos);
                range2.HorizontalAlignment = HorizontalAlignment.Center;
                range2.set_Value(Missing.Value, "Categorías " + tipo);
                range2.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic,
                    Missing.Value);
            }

            pos = Convert.ToChar(pcol + 1).ToString() + (pfil + 2).ToString();
            Range range3 = worksheet.get_Range(pos, pos);
            range3.HorizontalAlignment = HorizontalAlignment.Center;
            range3.set_Value(Missing.Value, "H");
            range3.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic,
                Missing.Value);

            pos = Convert.ToChar(pcol + 2).ToString() + (pfil + 2).ToString();
            Range range4 = worksheet.get_Range(pos, pos);
            range4.HorizontalAlignment = HorizontalAlignment.Center;
            range4.set_Value(Missing.Value, "M");
            range4.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic,
                Missing.Value);

            pos = Convert.ToChar(pcol + 3).ToString() + (pfil + 2).ToString();
            Range range5 = worksheet.get_Range(pos, pos);
            range5.HorizontalAlignment = HorizontalAlignment.Center;
            range5.set_Value(Missing.Value, "T");
            range5.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic,
                Missing.Value);

            pos = Convert.ToChar(pcol + 4).ToString() + (pfil + 2).ToString();
            Range range6 = worksheet.get_Range(pos, pos);
            range6.HorizontalAlignment = HorizontalAlignment.Center;
            range6.set_Value(Missing.Value, "CDS");
            range6.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic,
                Missing.Value);

            pos = Convert.ToChar(pcol + 5).ToString() + (pfil + 2).ToString();
            Range range7 = worksheet.get_Range(pos, pos);
            range7.HorizontalAlignment = HorizontalAlignment.Center;
            range7.set_Value(Missing.Value, "CD");
            range7.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic,
                Missing.Value);

            pos = Convert.ToChar(pcol + 6).ToString() + (pfil + 2).ToString();
            Range range8 = worksheet.get_Range(pos, pos);
            range8.HorizontalAlignment = HorizontalAlignment.Center;
            range8.set_Value(Missing.Value, "CE");
            range8.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic,
                Missing.Value);

            pos = Convert.ToChar(pcol + 7).ToString() + (pfil + 2).ToString();
            Range range9 = worksheet.get_Range(pos, pos);
            range9.HorizontalAlignment = HorizontalAlignment.Center;
            range9.set_Value(Missing.Value, "Técnico");
            range9.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic,
                Missing.Value);

            pos = Convert.ToChar(pcol).ToString() + (pfil + 2).ToString();
            pos2 = Convert.ToChar(pcol + 7).ToString() + (pfil + 2).ToString();
            Range range10 = worksheet.get_Range(pos, pos2);
            range10.Cells.Interior.Color = Color.FromArgb(255, 255, 255, 0);


            return pfil + 3;
        }

        /// <summary>
        /// Inserta un valor en una celda
        /// </summary>
        /// <param name="worksheet">Pestaña donde se insertará</param>
        /// <param name="columna">Columna de la celda</param>
        /// <param name="fila">Fila de la celda</param>
        /// <param name="valor">Valor a agregar</param>
        /// <returns>Devuelve el número de la fila siguiente</returns>
        private int Celda(_Worksheet worksheet, int columna, int fila, string valor)
        {
            columna = 64 + columna; //caracter ascii de A
            int pcol = columna;
            int pfil = fila;

            string pos = Convert.ToChar(pcol).ToString() + (pfil).ToString();

            Range range1 = worksheet.get_Range(pos, pos);
            range1.set_Value(Missing.Value, valor);
            range1.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic,
                Missing.Value);

            return pfil + 1;
        }

        /// <summary>
        /// Inserta una fila con datos en el cuerpo del documento
        /// </summary>
        /// <param name="worksheet">Pestaña donde se insertará</param>
        /// <param name="columna">Columna de la celda</param>
        /// <param name="fila">Fila de la celda</param>
        /// <param name="valor">Valores a agregar</param>
        /// <returns>Devuelve el número de la fila siguiente</returns>
        private int FilaTotal(_Worksheet worksheet, int columna, int fila, int mes, int ano, Dictionary<int, List<Incidencia>> Incidencias, Dictionary<int, List<Planificacion>> Planes)
        {
            int pfil = fila;
            int cont = 0;
            try
            {
                columna = 64 + columna; //caracter ascii de A
                int pcol = columna;

                //---creo una lista con todos los expedientes activos y los llamo uno a uno..

                var trabajador = from todos in personaC.Personas
                                 orderby todos.exp
                                 select todos;
                List<int> trabajadores = trabajador.Select(t => t.exp).ToList();

                DateTime inicial = new DateTime(ano, mes, 1);
                DateTime final = new DateTime(ano, mes, DateTime.DaysInMonth(ano, mes));
                List<object[,]> resultados = FuncionGeneralPorGrupos(trabajadores, inicial, final, Incidencias, Planes);

                for (int i = 0; i < trabajadores.Count; i++)
                {
                    string pos = Convert.ToChar(pcol) + (pfil + cont).ToString();
                    string pos1 = Convert.ToChar(pcol + 9) + (pfil + cont).ToString();

                    Range range1 = worksheet.get_Range(pos, pos1);
                    object[,] objArray = resultados[i];
                    range1.set_Value(Missing.Value, objArray);
                    range1.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic,
                        Missing.Value);
                    cont++;
                }

                return pfil + cont + 1;
            }
            catch (Exception ex)
            {
                //  CExcepcionesyTrazas.ExcepcionyTraza(ex);
                MessageBox.Show("ERROR, Fila " + ex.Message);
            }
            return pfil + cont + 1;

        }

        private int FilaxArea(_Worksheet worksheet, int columna, int fila, int area, int mes, int ano, Dictionary<int, List<Incidencia>> Incidencias, Dictionary<int, List<Planificacion>> Planes)
        {
            int pfil = fila;
            int cont = 0;
            try
            {

                columna = 64 + columna; //caracter ascii de A
                int pcol = columna;

                //---creo una lista con todos los expedientes activos y los llamo uno a uno..

                List<int> trabajadores = areaC.PersonasActivasQuePertenecenArea(areaC.GetAreaDiccio(area)).Select(t => t.exp).ToList();

                DateTime inicial = new DateTime(ano, mes, 1);
                DateTime final = new DateTime(ano, mes, DateTime.DaysInMonth(ano, mes));
                List<object[,]> resultados = FuncionGeneralPorGrupos(trabajadores, inicial, final, Incidencias, Planes);

                foreach (var item in trabajadores)
                {


                    string pos = Convert.ToChar(pcol) + (pfil + cont).ToString();
                    string pos1 = Convert.ToChar(pcol + 13) + (pfil + cont).ToString();

                    Range range1 = worksheet.get_Range(pos, pos1);

                    if (item != null)
                    {
                        object[,] objArray = resultados[cont];
                        range1.set_Value(Missing.Value, objArray);
                        range1.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic,
                            Missing.Value);
                    }

                    cont++;
                }

                return pfil + cont + 1;
            }
            catch (Exception ex)
            {
                //  CExcepcionesyTrazas.ExcepcionyTraza(ex);
                MessageBox.Show("ERROR, Fila " + ex.Message);
            }
            return pfil + cont + 1;

        }

        private int FilaxAreaAsistenciaDia(_Worksheet worksheet, int columna, int fila, DateTime fechainicio, int area = 0)
        {
            int pfil = fila;
            int cont = 0;
            try
            {
                int pcol = columna;
                List<int> trabajadores = new List<int>();
                if (area == 0)
                {
                    foreach (var item in areaC.Areas)
                    {
                        trabajadores.AddRange(areaC.PersonasActivasQuePertenecenArea(item).Select(t => t.exp).ToList());
                    }
                }
                else
                {
                    var areaSelecionada = areaC.GetArea(area);
                    trabajadores.AddRange(areaC.PersonasActivasQuePertenecenArea(areaSelecionada).Select(t => t.exp).ToList());
                }

                List<object[]> resultados = FuncionGeneralPorGruposAsistencia(trabajadores, fechainicio);

                foreach (var item in resultados)
                {
                    for (int i = 0; i < 9; i++)
                    {
                        string pos = Convert.ToChar(pcol + cont) + (pfil + i).ToString();
                        string pos1 = Convert.ToChar(pcol + cont) + (pfil + i).ToString();

                        Range range1 = worksheet.get_Range(pos, pos1);

                        if (item != null)
                        {
                            range1.set_Value(Missing.Value, item[i]);
                            range1.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic,
                                Missing.Value);
                        }
                    }
                    cont++;
                }

                return pfil + 8;
            }
            catch (Exception ex)
            {
                //  CExcepcionesyTrazas.ExcepcionyTraza(ex);
                MessageBox.Show("ERROR, Fila " + ex.Message);
            }
            return pfil + cont + 1;

        }

        private int FilaxAreaTrabajadores(_Worksheet worksheet, int columna, int fila, int area)
        {
            int pfil = fila;
            int cont = 0;
            try
            {

                columna = 64 + columna; //caracter ascii de A
                int pcol = columna;
                List<int> trabajadores = areaC.PersonasActivasQuePertenecenArea(areaC.GetAreaDiccio(area)).Select(t => t.exp).ToList();

                for (int i = 0; i < trabajadores.Count; i++)
                {
                    PersonalRH persona;
                    try
                    {
                        persona = servicio.DamePersonaxExp(trabajadores[i]);
                    }
                    catch
                    {
                        trabajadores.RemoveAt(i);
                        i--;
                    }
                }

                List<object[,]> resultados = FuncionGeneralPorGruposTrabajadores(trabajadores);


                foreach (var item in trabajadores)
                {


                    string pos = Convert.ToChar(pcol) + (pfil + cont).ToString();
                    string pos1 = Convert.ToChar(pcol + 5) + (pfil + cont).ToString();

                    Range range1 = worksheet.get_Range(pos, pos1);

                    if (item != null)
                    {
                        object[,] objArray = resultados[cont];
                        range1.set_Value(Missing.Value, objArray);
                    }

                    cont++;
                }

                return pfil + cont + 1;
            }
            catch (Exception ex)
            {
                //  CExcepcionesyTrazas.ExcepcionyTraza(ex);
                MessageBox.Show("ERROR, Fila " + ex.Message);
            }
            return pfil + cont + 1;

        }

        private int FilaxAreaRegistros(_Worksheet worksheet, int columna, int fila, int area, DateTime fecha)
        {
            int pfil = fila;
            int cont = 0;
            try
            {
                columna = 64 + columna; //caracter ascii de A
                int pcol = columna;
                List<int> trabajadores = areaC.PersonasActivasQuePertenecenArea(areaC.GetAreaDiccio(area)).Select(t => t.exp).ToList();

                for (int i = 0; i < trabajadores.Count; i++)
                {
                    PersonalRH persona;
                    try
                    {
                        persona = servicio.DamePersonaxExp(trabajadores[i]);
                    }
                    catch
                    {
                        trabajadores.RemoveAt(i);
                        i--;
                    }
                }

                List<object[,]> resultados = FuncionGeneralPorGruposRegistros(trabajadores, fecha);

                List<object[,]> tiposTrabajador = TiposTrabajador(trabajadores, fecha);
                foreach (var item in trabajadores)
                {
                    if (item != null)
                    {
                        object[,] objArray = resultados[cont];
                        string pos = Convert.ToChar(pcol) + (pfil + cont).ToString();
                        string pos1 = Convert.ToChar(pcol + objArray.Length-1) + (pfil + cont).ToString();
                        string pos2 = Convert.ToChar(pcol + 1) + (pfil + cont).ToString();
                        Range range1 = worksheet.get_Range(pos, pos1);
                        Range range2 = worksheet.get_Range(pos, pos2);
                        range2.Cells.Interior.Color = Color.FromArgb(255, 189, 215, 238);
                        for (int i = 3; i < range1.Cells.Count+1; i++)
                        {
                            if (tiposTrabajador[cont].Length + 3 > i )
                            {
                                if (tiposTrabajador[cont][0, i - 3].ToString().Equals("0"))
                                {
                                    ((Range)range1.Cells[1, i]).Interior.Color = Color.FromArgb(255, 198, 224, 180);
                                }
                                else
                                if (tiposTrabajador[cont][0, i - 3].ToString().Equals("1"))
                                {
                                    ((Range)range1.Cells[1, i]).Interior.Color = Color.FromArgb(255, 255, 113, 113);
                                }
                            }
                        }
                        range1.set_Value(Missing.Value, objArray);
                    }
                    cont++;
                }
                return pfil + cont + 1;
            }
            catch (Exception ex)
            {
                //  CExcepcionesyTrazas.ExcepcionyTraza(ex);
                MessageBox.Show("ERROR, Fila " + ex.Message);
            }
            return pfil + cont + 1;

        }

        private int FilaxAreaAlta(_Worksheet worksheet, int columna, int fila, int año, int mes)
        {
            int pfil = fila;
            int cont = 0;
            try
            {
                columna = 64 + columna; //caracter ascii de A
                int pcol = columna;
                if (mes == 0)
                {
                    for (int i = 1; i <= 12; i++)
                    {
                        string pos = Convert.ToChar(pcol) + (pfil + cont).ToString();
                        string pos1 = Convert.ToChar(pcol + 7) + (pfil + cont).ToString();
                        Range range1 = worksheet.get_Range(pos, pos1);
                        range1.Cells.Merge(Missing.Value);
                        range1.HorizontalAlignment = HorizontalAlignment.Center;
                        range1.set_Value(Missing.Value, DimeMes(i).ToUpper());
                        range1.Cells.Borders.LineStyle = XlLineStyle.xlContinuous;
                        range1.Cells.Borders.Weight = XlBorderWeight.xlMedium;
                        range1.Cells.Borders.ColorIndex = XlColorIndex.xlColorIndexAutomatic;
                        range1.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlMedium, XlColorIndex.xlColorIndexAutomatic,
                            Missing.Value);
                        cont++;
                        List<object[,]> resultados = FuncionGeneralAltas(año, i);
                        foreach (var item in resultados)
                        {
                            pos = Convert.ToChar(pcol) + (pfil + cont).ToString();
                            pos1 = Convert.ToChar(pcol + 7) + (pfil + cont).ToString();
                            range1 = worksheet.get_Range(pos, pos1);
                            range1.set_Value(Missing.Value, item);
                            range1.Cells.Borders.LineStyle = XlLineStyle.xlContinuous;
                            range1.Cells.Borders.Weight = XlBorderWeight.xlThin;
                            range1.Cells.Borders.ColorIndex = XlColorIndex.xlColorIndexAutomatic;
                            range1.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic,
                                Missing.Value);
                            cont++;
                        }                        
                    }
                }
                else
                {
                    string pos = Convert.ToChar(pcol) + (pfil + cont).ToString();
                    string pos1 = Convert.ToChar(pcol + 7) + (pfil + cont).ToString();
                    Range range1 = worksheet.get_Range(pos, pos1);
                    range1.Cells.Merge(Missing.Value);
                    range1.HorizontalAlignment = HorizontalAlignment.Center;
                    range1.set_Value(Missing.Value, DimeMes(mes).ToUpper());
                    range1.Cells.Borders.LineStyle = XlLineStyle.xlContinuous;
                    range1.Cells.Borders.Weight = XlBorderWeight.xlMedium;
                    range1.Cells.Borders.ColorIndex = XlColorIndex.xlColorIndexAutomatic;
                    range1.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlMedium, XlColorIndex.xlColorIndexAutomatic,
                            Missing.Value);
                    cont++;
                    List<object[,]> resultados = FuncionGeneralAltas(año, mes);
                    foreach (var item in resultados)
                    {
                        pos = Convert.ToChar(pcol) + (pfil + cont).ToString();
                        pos1 = Convert.ToChar(pcol + 7) + (pfil + cont).ToString();
                        range1 = worksheet.get_Range(pos, pos1);
                        range1.set_Value(Missing.Value, item);
                        range1.Cells.Borders.LineStyle = XlLineStyle.xlContinuous;
                        range1.Cells.Borders.Weight = XlBorderWeight.xlThin;
                        range1.Cells.Borders.ColorIndex = XlColorIndex.xlColorIndexAutomatic;
                        range1.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic,
                            Missing.Value);
                        cont++;
                    }
                }
            return pfil + cont + 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR, Fila " + ex.Message);
            }
            return pfil + cont + 1;
        }

        private int FilaxAreaBaja(_Worksheet worksheet, int columna, int fila, int año, int mes)
        {
            int pfil = fila;
            int cont = 0;
            try
            {
                columna = 64 + columna; //caracter ascii de A
                int pcol = columna;
                if (mes == 0)
                {
                    for (int i = 1; i <= 12; i++)
                    {
                        string pos = Convert.ToChar(pcol) + (pfil + cont).ToString();
                        string pos1 = Convert.ToChar(pcol + 10) + (pfil + cont).ToString();
                        Range range1 = worksheet.get_Range(pos, pos1);
                        range1.Cells.Merge(Missing.Value);
                        range1.HorizontalAlignment = HorizontalAlignment.Center;
                        range1.set_Value(Missing.Value, DimeMes(i).ToUpper());
                        range1.Cells.Borders.LineStyle = XlLineStyle.xlContinuous;
                        range1.Cells.Borders.Weight = XlBorderWeight.xlMedium;
                        range1.Cells.Borders.ColorIndex = XlColorIndex.xlColorIndexAutomatic;
                        range1.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlMedium, XlColorIndex.xlColorIndexAutomatic,
                            Missing.Value);
                        cont++;
                        List<object[,]> resultados = FuncionGeneralBajas(año, i);
                        foreach (var item in resultados)
                        {
                            pos = Convert.ToChar(pcol) + (pfil + cont).ToString();
                            pos1 = Convert.ToChar(pcol + 10) + (pfil + cont).ToString();
                            range1 = worksheet.get_Range(pos, pos1);
                            range1.set_Value(Missing.Value, item);
                            range1.Cells.Borders.LineStyle = XlLineStyle.xlContinuous;
                            range1.Cells.Borders.Weight = XlBorderWeight.xlThin;
                            range1.Cells.Borders.ColorIndex = XlColorIndex.xlColorIndexAutomatic;
                            range1.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic,
                                Missing.Value);
                            cont++;
                        }
                    }
                }
                else
                {
                    string pos = Convert.ToChar(pcol) + (pfil + cont).ToString();
                    string pos1 = Convert.ToChar(pcol + 10) + (pfil + cont).ToString();
                    Range range1 = worksheet.get_Range(pos, pos1);
                    range1.Cells.Merge(Missing.Value);
                    range1.HorizontalAlignment = HorizontalAlignment.Center;
                    range1.set_Value(Missing.Value, DimeMes(mes).ToUpper());
                    range1.Cells.Borders.LineStyle = XlLineStyle.xlContinuous;
                    range1.Cells.Borders.Weight = XlBorderWeight.xlMedium;
                    range1.Cells.Borders.ColorIndex = XlColorIndex.xlColorIndexAutomatic;
                    range1.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlMedium, XlColorIndex.xlColorIndexAutomatic,
                            Missing.Value);
                    cont++;
                    List<object[,]> resultados = FuncionGeneralBajas(año, mes);
                    foreach (var item in resultados)
                    {
                        pos = Convert.ToChar(pcol) + (pfil + cont).ToString();
                        pos1 = Convert.ToChar(pcol + 10) + (pfil + cont).ToString();
                        range1 = worksheet.get_Range(pos, pos1);
                        range1.set_Value(Missing.Value, item);
                        range1.Cells.Borders.LineStyle = XlLineStyle.xlContinuous;
                        range1.Cells.Borders.Weight = XlBorderWeight.xlThin;
                        range1.Cells.Borders.ColorIndex = XlColorIndex.xlColorIndexAutomatic;
                        range1.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic,
                            Missing.Value);
                        cont++;
                    }
                }
                return pfil + cont + 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR, Fila " + ex.Message);
            }
            return pfil + cont + 1;

        }

        private int FilaxAreaCategoria(_Worksheet worksheet, int columna, int fila, string tipo)
        {
            int pfil = fila;
            int cont = 0;
            try
            {
                columna = 64 + columna; //caracter ascii de A
                int pcol = columna;
                List<object[,]> resultados = FuncionGeneralCategorias(tipo);
                foreach (var item in resultados)
                {
                    string pos = Convert.ToChar(pcol) + (pfil + cont).ToString();
                    string pos1 = Convert.ToChar(pcol + 7) + (pfil + cont).ToString();
                    Range range1 = worksheet.get_Range(pos, pos1);
                    range1.set_Value(Missing.Value, item);
                    range1.Cells.Borders.LineStyle = XlLineStyle.xlContinuous;
                    range1.Cells.Borders.Weight = XlBorderWeight.xlThin;
                    range1.Cells.Borders.ColorIndex = XlColorIndex.xlColorIndexAutomatic;
                    range1.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic,
                        Missing.Value);
                    cont++;
                }
                return pfil + cont + 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR, Fila " + ex.Message);
            }
            return pfil + cont + 2;
        }

        /// <summary>
        /// Pie de firma al final de la hoja
        /// </summary>
        /// <param name="worksheet">Pestaña donde se insertará</param>
        /// <param name="columna">Columna de la celda</param>
        /// <param name="fila">Fila de la celda</param>
        /// <returns>Devuelve el número de la fila siguiente</returns>
        //private int Pie(_Worksheet worksheet, int columna, int fila, int idarea)
        //{
        //    columna = 64 + columna; //caracter ascii de A
        //    int pcol = columna;
        //    int pfil = fila;

        //    string pos = Convert.ToChar(pcol + 1).ToString() + (pfil + 3).ToString();

        //    Range range1 = worksheet.get_Range(pos, pos);
        //    range1.Cells.Merge(Missing.Value);

        //    var este = servicio.DamePersonaxExp(personaC.GetPersona(this.Logueado.id_Persona).exp);
        //    var elaborador = (este.Nombre + " " + este.Apellido1 + " " + este.Apellido2);




        //    //foreach (var per in areaC.PersonasQueControlanArea(areaC.GetArea(idarea)))
        //    //{
        //    //    var este = servicio.DamePersonaxExp(personaC.GetPersona(per.id_Persona).exp);
        //    //    elaboradores.Add(este.Nombre + " " + este.Apellido1 + " " + este.Apellido2);
        //    //}

        //    range1.set_Value(Missing.Value, "Elaborado por: " + " ");//aki solo pongo el primero de los responsables del area, si se quieren poner todos hay k recorrer la lista


        //    pos = Convert.ToChar(pcol + 1).ToString() + (pfil + 5).ToString();
        //    Range range4 = worksheet.get_Range(pos, pos);
        //    int pers = areaC.ObtenerJefeArea(areaC.GetAreaDiccio(idarea)).exp;
        //    var tipo = servicio.DamePersonaxExpDeep(pers);
        //    string aprobador = tipo.Nombre + " " + tipo.Apellido1 + " " + tipo.Apellido2;
        //    range4.set_Value(Missing.Value, "Aprobado por: " + " ");

        //    pos = Convert.ToChar(pcol + 2).ToString() + (pfil + 3).ToString();
        //    Range range5 = worksheet.get_Range(pos, pos);
        //    range5.set_Value(Missing.Value, "Fecha: " + DateTime.Now.Date.ToShortDateString());

        //    return pfil + 6;
        //}

        

        #endregion

        //-------------generando el DBF para Lucas----------
        #region construyendo el DBF..
        public void ExportarDBF(string filepath, int mes, int ano)
        {
            //SaveFileDialog saveFileDialog = new SaveFileDialog();
            //saveFileDialog.Filter = "dBase file (*.dbf)|*.dbf";
            //saveFileDialog.FileName = "Nomina";

            try
            {
                DataSet dataSource = CrearDataSource(mes, ano);
                DataFileExport nuevo = DataFileExport.CreateDbf(dataSource);
                nuevo.AddNumericField("EXPEDIENTE", 3, 0)
                        .AddNumericField("NOTRABAJO", 5, 2)
                        .Write(filepath);

            }
            catch (Exception)
            {
                throw;
            }


        }

        private DataSet CrearDataSource(int mes, int ano)
        {
            DataSet sourceDataSet = new DataSet("DataSetNomina");
            System.Data.DataTable table = sourceDataSet.Tables.Add("DataTableNomina");
            table.Columns.Add("EXPEDIENTE", typeof(int));
            table.Columns.Add("NOTRABAJO", typeof(decimal));

            List<int> trabajadores = personaC.Personas.OrderBy(t => t.exp).Select(t => t.exp).ToList();
            foreach (var item in trabajadores)
            {
                table.Rows.Add(item, CalcularPerdidasTotalesxMes(301, ano, mes));
            }
            return sourceDataSet;
        }

        private double CalcularPerdidasTotalesxMes(int item, int ano, int mes)
        {
            DateTime inicial = new DateTime(ano, mes, 1);
            DateTime final = new DateTime(ano, mes, DateTime.DaysInMonth(ano, mes));
            object[,] arreglo = FuncionGeneral(item, inicial, final);
            //int subsidios = arreglo[7];
            //int vacaciones = arreglo[6];
            //double perdidas = arreglo[8];

            //double resp=CalculaSubsidios(mes, ano, item) + CalculaVacaciones(mes, ano, item) +
            //       CalculaPerdidas(mes, ano, item);
            //if (resp>24)
            //{
            //    resp = 24;
            //}
            return Convert.ToDouble(arreglo[0, 5]);



        }
        #endregion

        #region ExcelLucas
        public void GenerarExcelLucas(int mes, int ano)
        {

            try
            {
                this.objApp =
                    (Microsoft.Office.Interop.Excel.Application)new Microsoft.Office.Interop.Excel.Application();
                this.objBook = (_Workbook)this.objApp.Workbooks.Add(Missing.Value);
                Sheets worksheets = this.objBook.Worksheets;
                List<string> list1 = new List<string>();
                //int num1 = 2;
                int num2 = 1;
                List<string> list3 = new List<string>();
                //List<string> list4 = Agrupaciones(); // TConeccion.Devolver_Consulta_List(TipoCon.LHC, pquery);

                // PESTAÑA CIE==========================================
                worksheets.Add(Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                _Worksheet worksheetcie = (_Worksheet)worksheets.get_Item((1));

                List<string> list5 = new List<string>(); //LISTA DE PERSONAL 
                ++num2;

                int valNomCol = EncabezaExcelLucas(worksheetcie, 1, 1);
                /*FilaxArea*/
                FilaxLucas(worksheetcie, 1, valNomCol, mes, ano);
                //ExcelConsultaPie(worksheetcie, 1, valfila, exp, fechainicial, fechafinal);

                Range rg = worksheetcie.get_Range("B1", "G1");
                rg.EntireColumn.AutoFit();
                //==========poner formato de fecha para una columna==========================
                //Range rgDATE = worksheetcie.get_Range("C1", "C1");
                //rgDATE.EntireColumn.NumberFormat = "MM/DD/YY";
                //==========poner formato decimal para una columna==========================
                //Range rgLD = worksheetcie.get_Range("F1", "G1");
                //rgLD.EntireColumn.NumberFormat = "0.00";

                //   worksheetcie.Name = exp.ToString();

                _Worksheet ultpest = (_Worksheet)worksheets.get_Item((2));


                this.objApp.Visible = true;
                this.objApp.UserControl = true;
            }
            catch (Exception ex)
            {
                int num4 = (int)MessageBox.Show("Error: " + ex.Message + " Line: " + ex.Source, "Error");
            }
        }

        private void FilaxLucas(_Worksheet worksheetcie, int p, int valNomCol, int mes, int ano)
        {
            DateTime inicial = new DateTime(ano, mes, 1);
            DateTime final = new DateTime(ano, mes, DateTime.DaysInMonth(ano, mes));
            int pfil = valNomCol;
            int cont = 0;
            try
            {

                int pcol = 64 + p;

                //---creo una lista con todos los expedientes activos y los llamo uno a uno..

                var trabajador = from todos in personaC.Personas
                                 orderby todos.exp
                                 select todos;
                List<int> trabajadores = trabajador.Select(t => t.exp).ToList();
                foreach (var item in trabajadores)
                {
                    string pos = Convert.ToChar(pcol) + (pfil + cont).ToString();
                    string pos1 = Convert.ToChar(pcol + 1) + (pfil + cont).ToString();
                    Range range1 = worksheetcie.get_Range(pos, pos1);
                    object[,] objArray = FuncionGeneralLucas(item, inicial, final);
                    range1.set_Value(Missing.Value, objArray);
                    //range1.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic,
                    //    Missing.Value);
                    cont++;
                }

            }

            //    int pfil = fila;
            //int cont = 0;


            //    columna = 64 + columna; //caracter ascii de A
            //    int pcol = columna;

            //    //---creo una lista con todos los expedientes activos y los llamo uno a uno..

            //    List<int> trabajadores = areaC.PersonasActivasQuePertenecenArea(areaC.GetArea(area)).Select(t => t.exp).ToList();
            //    foreach (var item in trabajadores)
            //    {
            //        string pos = Convert.ToChar(pcol) + (pfil + cont).ToString();
            //        string pos1 = Convert.ToChar(pcol + 8) + (pfil + cont).ToString();

            //        Range range1 = worksheet.get_Range(pos, pos1);

            //        object[,] objArray = FilaPrenomina(mes, ano, item);
            //        range1.set_Value(Missing.Value, objArray);
            //        range1.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic,
            //            Missing.Value);
            //        cont++;


            //    }



            catch (Exception ex)
            {
                //  CExcepcionesyTrazas.ExcepcionyTraza(ex);
                MessageBox.Show("ERROR , Fila " + ex.Message);
            }

        }

        private object[,] FuncionGeneralLucas(int item, DateTime inicial, DateTime final)
        {

            object[,] arreglo = FuncionGeneral1(item, inicial, final);
            var nolaboradas = arreglo[0, 5];

            object[,] fila = new object[1, 2]
            {
                {
                    item, nolaboradas
                }
            };
            return fila;

        }

        private int EncabezaExcelLucas(_Worksheet worksheetcie, int col, int fil)
        {
            col = 64 + col; //caracter ascii de A
            int pcol = col;
            int pfil = fil;

            string pos = Convert.ToChar(pcol).ToString() + (pfil).ToString();
            Range range3 = worksheetcie.get_Range(pos, pos);
            range3.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic,
                Missing.Value);
            range3.set_Value(Missing.Value, "EXPEDIENTE");

            pos = Convert.ToChar(pcol + 1).ToString() + pfil.ToString();

            Range range1 = worksheetcie.get_Range(pos, pos);
            range1.Cells.Merge(Missing.Value);
            range1.HorizontalAlignment = HorizontalAlignment.Center;
            range1.set_Value(Missing.Value, "NOTRABAJA");

            return pfil + 1;
        }

        #endregion
        //-------------generando excel para la consulta Incidentes de un exp en un período----------------

        #region excel consultante

        public void GenerarExcelConsultante(int exp, DateTime fechainicial, DateTime fechafinal)
        {

            try
            {
                this.objApp =
                    (Microsoft.Office.Interop.Excel.Application)new Microsoft.Office.Interop.Excel.Application();
                this.objBook = (_Workbook)this.objApp.Workbooks.Add(Missing.Value);
                Sheets worksheets = this.objBook.Worksheets;
                List<string> list1 = new List<string>();
                //int num1 = 2;
                int num2 = 1;
                List<string> list3 = new List<string>();
                //List<string> list4 = Agrupaciones(); // TConeccion.Devolver_Consulta_List(TipoCon.LHC, pquery);

                // PESTAÑA CIE==========================================


                worksheets.Add(Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                _Worksheet worksheetcie = (_Worksheet)worksheets.get_Item((1));

                List<string> list5 = new List<string>(); //LISTA DE PERSONAL 
                ++num2;
                int valencabezado = ExcelConsultaEncabezado(worksheetcie, 1, 2, exp, fechainicial, fechafinal);
                int valcuerpo = ExcelConsultaCuerpo(worksheetcie, 1, valencabezado);
                int valfila = ExcelConsultaFilaIncidencias(worksheetcie, 1, valcuerpo, exp, fechainicial, fechafinal);
                ExcelConsultaPie(worksheetcie, 1, valfila, exp, fechainicial, fechafinal);

                Range rg = worksheetcie.get_Range("B1", "G1");
                rg.EntireColumn.AutoFit();
                //==========poner formato de fecha para una columna==========================
                //Range rgDATE = worksheetcie.get_Range("C1", "C1");
                //rgDATE.EntireColumn.NumberFormat = "MM/DD/YY";
                //==========poner formato decimal para una columna==========================
                //Range rgLD = worksheetcie.get_Range("F1", "G1");
                //rgLD.EntireColumn.NumberFormat = "0.00";

                worksheetcie.Name = exp.ToString();

                _Worksheet ultpest = (_Worksheet)worksheets.get_Item((2));


                this.objApp.Visible = true;
                this.objApp.UserControl = true;
            }
            catch (Exception ex)
            {
                int num4 = (int)MessageBox.Show("Error: " + ex.Message + " Line: " + ex.Source, "Error");
            }
        }

        //ExcelConsultaEncabezado
        private int ExcelConsultaEncabezado(_Worksheet worksheet, int columna, int fila, int exp, DateTime fechainicial, DateTime fechafinal)
        {
            columna = 64 + columna; //caracter ascii de A
            int pcol = columna;
            int pfil = fila;

            string pos = Convert.ToChar(pcol).ToString() + pfil.ToString();

            Range range1 = worksheet.get_Range(pos, pos);
            range1.Cells.Merge(Missing.Value);
            range1.set_Value(Missing.Value, "No:" + exp.ToString());
            pcol++;

            pos = Convert.ToChar(pcol).ToString() + pfil.ToString();
            Range range2 = worksheet.get_Range(pos, pos);
            range2.Cells.Merge(Missing.Value);
            string nombre = servicio.DamePersonaxExp(exp).Nombre + " " + servicio.DamePersonaxExp(exp).Apellido1 + " " +
                            servicio.DamePersonaxExp(exp).Apellido2;
            range2.set_Value(Missing.Value, nombre);
            range2.Font.Bold = true;
            pfil++;

            pos = Convert.ToChar(pcol).ToString() + pfil.ToString();
            Range range3 = worksheet.get_Range(pos, pos);
            range3.Cells.Merge(Missing.Value);
            string agrupa = areaC.ObtenerAgrupacionDeArea(personaC.GetPersonaPorExpediente(exp).Area1).descripcion;
            range3.set_Value(Missing.Value, "Agrupación: " + agrupa);
            range3.Font.Bold = true;
            pfil++;

            pos = Convert.ToChar(pcol).ToString() + pfil.ToString();
            Range range4 = worksheet.get_Range(pos, pos);
            range4.Cells.Merge(Missing.Value);
            var area = areaC.GetAreaDiccio((int)personaC.GetPersonaPorExpediente(exp).id_area).descripcion;
            range4.set_Value(Missing.Value, "Area: " + area);
            range4.Font.Bold = true;
            pfil++;


            pos = Convert.ToChar(pcol).ToString() + pfil.ToString();
            Range range5 = worksheet.get_Range(pos, pos);
            range5.Cells.Merge(Missing.Value);
            range5.set_Value(Missing.Value, "Incidencias y Planificaciones Guardadas: ");
            range5.Font.Bold = true;
            pfil++;

            pos = Convert.ToChar(pcol).ToString() + pfil.ToString();
            Range range6 = worksheet.get_Range(pos, pos);
            range6.Cells.Merge(Missing.Value);
            range6.set_Value(Missing.Value, "Desde: " + fechainicial + ". Hasta: " + fechafinal);
            range6.Font.Bold = true;

            string pos1 = Convert.ToChar(columna).ToString() + fila.ToString();
            pos = Convert.ToChar(pcol).ToString() + pfil.ToString();

            Range range7 = worksheet.get_Range(pos1, pos);
            range7.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlMedium, XlColorIndex.xlColorIndexAutomatic,
                Missing.Value);
            range7.Interior.Color = Color.White;

            string pos2 = Convert.ToChar(columna).ToString() + (fila + 3).ToString();
            string pos3 = Convert.ToChar(columna).ToString() + (fila + 4).ToString();

            range7 = worksheet.get_Range(pos1, pos2);
            range7.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlMedium, XlColorIndex.xlColorIndexAutomatic,
                Missing.Value);
            range7 = worksheet.get_Range(pos3, pos3);
            range7.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlMedium, XlColorIndex.xlColorIndexAutomatic,
                Missing.Value);

            return fila + 5;
        }
        //ExcelConsultaCuerpo
        private int ExcelConsultaCuerpo(_Worksheet worksheet, int columna, int fila)
        {
            columna = 64 + columna; //caracter ascii de A
            int pcol = columna;
            int pfil = fila;

            string pos = Convert.ToChar(pcol).ToString() + (pfil).ToString();
            Range range3 = worksheet.get_Range(pos, pos);
            range3.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic,
                Missing.Value);
            range3.set_Value(Missing.Value, "Fecha");

            pos = Convert.ToChar(pcol + 1).ToString() + pfil.ToString();

            Range range1 = worksheet.get_Range(pos, pos);
            range1.Cells.Merge(Missing.Value);
            range1.HorizontalAlignment = HorizontalAlignment.Center;
            range1.set_Value(Missing.Value, "Incidencias");

            return pfil + 1;

        }

        //ExcelConsultaFilaIncidencias
        private int ExcelConsultaFilaIncidencias(_Worksheet worksheet, int columna, int fila, int exp, DateTime fechainicial, DateTime fechafinal)
        {
            int pfil = fila;
            int cont = 0;
            try
            {

                columna = 64 + columna; //caracter ascii de A
                int pcol = columna;
                List<Planificacion> PlanesReducido = new List<Planificacion>();
                foreach (var a in planificacionAnticipadaC.Planificaciones)
                {
                    if (a.Persona.exp == exp && a.fecha_inicio <= fechafinal && a.fecha_fin >= fechainicial)
                        PlanesReducido.Add(a);

                }
                List<Incidencia> IncidenciaReducido = incidenciaC.Incidencias.FindAll(t => t.Persona.exp == exp && t.fecha.Date >= fechainicial.Date && t.fecha.Date <= fechafinal.Date);
                //foreach (var a in incidenciaC.Incidencias)
                //{
                //    if (a.Persona.exp == exp && a.fecha >= fechafinal && a.fecha <= fechafinal)
                //        IncidenciaReducido.Add(a);
                //}

                if (fechainicial < fechafinal)
                {
                    DateTime dia = fechainicial.Date;
                    while (dia <= fechafinal.Date)
                    {
                        int largo = FilaIncidencias(exp, dia, PlanesReducido, IncidenciaReducido).Length;
                        if (largo > 0)
                        {
                            string pos = Convert.ToChar(pcol) + (pfil + cont).ToString();
                            string pos1 = Convert.ToChar(pcol + largo - 1) + (pfil + cont).ToString();
                            Range range1 = worksheet.get_Range(pos, pos1);
                            object[,] objArray = FilaIncidencias(exp, dia, PlanesReducido, IncidenciaReducido);
                            range1.set_Value(Missing.Value, objArray);
                            range1.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin,
                                XlColorIndex.xlColorIndexAutomatic,
                                Missing.Value);
                            cont++;
                        }
                        dia = dia.AddDays(1);

                    }

                    return pfil + cont + 1;
                }
                else
                {
                    throw new Exception("ERROR!! Ha introducido la fecha inicial del periodo menor que la final");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private object[,] FilaIncidencias(int exp, DateTime dia, List<Planificacion> PlanificacionReducido, List<Incidencia> IncidenciaReducido)
        {
            Planificacion planificadas = PlanificacionReducido.Find(
                t =>
                    (t.fecha_inicio.Value.Date < dia.Date && t.fecha_fin.Value.Date > dia.Date || t.fecha_inicio.Value.Date == dia.Date || t.fecha_fin.Value.Date == dia.Date));
            if (planificadas != null)
            {
                object[,] fila = new object[1, 2]
                {
                    {
                        dia, "N    "+dia.TimeOfDay+"    "+clavesNomC.GetClave_nom(planificadas.id_clave.Value).descripcion 
                    }
                };

                return fila;
            }
            else
            {
                List<Incidencia> incidentesxdia = IncidenciaReducido.FindAll(t => t.fecha.Date == dia.Date);
                if (incidentesxdia.Any())
                {

                    object[,] fila = new object[1, incidentesxdia.Count + 1];
                    fila[0, 0] = dia;
                    for (int i = 1; i <= incidentesxdia.Count; i++)
                    {
                        fila[0, i] = incidentesxdia[i - 1].tipotraza + "    " + incidentesxdia[i - 1].fecha.TimeOfDay + "    " + clavesNomC.GetClave_nom(incidentesxdia[i - 1].id_clave.Value).descripcion;
                    }

                    return fila;
                }
                else
                {
                    object[,] fila = new object[0, 0];
                    return fila;
                }
            }


        }

        //ExcelConsultaPie escribir totales
        private int ExcelConsultaPie(_Worksheet worksheet, int columna, int fila, int exp, DateTime fechaini, DateTime fechafin)
        {

            object[,] arreglo = FuncionGeneral(exp, fechaini, fechafin);
            var horas = arreglo[0, 7];
            var vacaciones = arreglo[0, 5];
            var certificados = arreglo[0, 6];
            var nolaborados = arreglo[0, 4];
            columna = 64 + columna; //caracter ascii de A
            int pcol = columna;
            int pfil = fila;

            string pos = Convert.ToChar(pcol + 1).ToString() + (pfil + 3).ToString();

            Range range1 = worksheet.get_Range(pos, pos);
            range1.Cells.Merge(Missing.Value);
            //double horas = CalculaHorasPerdidasPeriodo(exp, fechaini, fechafin);

            range1.set_Value(Missing.Value, "Certificados en el período: " + certificados.ToString());

            pos = Convert.ToChar(pcol + 1).ToString() + (pfil + 4).ToString();
            Range range4 = worksheet.get_Range(pos, pos);
            // int vacaciones = CalculaVacacionesPeriodo(exp, fechaini, fechafin);
            range4.set_Value(Missing.Value, "Vacaciones en el período: " + vacaciones.ToString());

            pos = Convert.ToChar(pcol + 1).ToString() + (pfil + 5).ToString();
            Range range5 = worksheet.get_Range(pos, pos);
            //int certificados = CalculaCertificadosPeriodo(exp, fechaini, fechafin);
            range5.set_Value(Missing.Value, "Días perdidos por otras razones en el período: " + horas.ToString());


            pos = Convert.ToChar(pcol + 1).ToString() + (pfil + 6).ToString();
            Range range6 = worksheet.get_Range(pos, pos);
            //int certificados = CalculaCertificadosPeriodo(exp, fechaini, fechafin);
            range6.set_Value(Missing.Value, "Total de horas no laboradas en el período: " + nolaborados.ToString());

            return pfil + 6;
        }
        //private int CalculaVacacionesPeriodo(int exp, DateTime fechainicio, DateTime fechafin)
        //{

        //    int resp = 0;
        //    try
        //    {

        //        DateTime limiteinicial = fechainicio;
        //        DateTime limitefinal = fechafin;
        //        var listaplanif =
        //            planificacionAnticipadaC.Planificaciones.FindAll(
        //                item => item.Persona.exp == exp && item.Clave_nom.codigo == "VC" &&
        //                        (item.fecha_inicio >= limiteinicial && item.fecha_inicio <= limitefinal ||
        //                         item.fecha_fin >= limiteinicial && item.fecha_fin <= limitefinal));
        //        List<DateTime> respPlanificaciones = new List<DateTime>();
        //        var respIncidencias = incidenciaC.Incidencias.FindAll(t =>
        //                     t.Clave_nom.codigo == "VC" && t.Persona.exp == exp && t.fecha >= limiteinicial &&
        //                     t.fecha <= limitefinal);

        //        foreach (var planificacion in listaplanif)
        //        {
        //            DateTime primerdia;
        //            DateTime ultimodia;
        //            primerdia = planificacion.fecha_inicio <= limiteinicial ? limiteinicial : planificacion.fecha_inicio.Value;
        //            if (planificacion.fecha_fin >= limitefinal)
        //            {
        //                ultimodia = limitefinal;
        //            }
        //            else
        //            {
        //                ultimodia = planificacion.fecha_fin.Value;
        //            }
        //            DateTime dia = primerdia;
        //            while (dia.Date <= ultimodia.Date)
        //            {
        //                //este codigo debe actualizarse cuando se tenga
        //                if (dia.DayOfWeek == DayOfWeek.Sunday)
        //                {
        //                    dia = dia.AddDays(1);
        //                    continue;
        //                }
        //                else
        //                {
        //                    respPlanificaciones.Add(dia);
        //                }

        //                dia = dia.AddDays(1);
        //            }

        //        }
        //        int repetido = 0;
        //        foreach (var planificacion in respPlanificaciones)
        //        {
        //            foreach (var incidencias in respIncidencias)
        //            {
        //                if (planificacion.Date == incidencias.fecha.Date)
        //                {
        //                    repetido++;
        //                }
        //            }
        //        }

        //        resp = respIncidencias.Count + respPlanificaciones.Count - repetido;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return resp;

        //}

        //este es el c'odigo mejorado
        private int CalculaVacacionesPeriodo(int exp, DateTime fechainicio, DateTime fechafin)
        {
            int resp = 0;
            DateTime inicial = fechainicio;
            DateTime final = fechafin;
            DateTime dia = inicial;
            //var periodostime = personaC.GetPersonaPorExpediente(exp).Grupo.Periodo_tiempo;
            while (dia <= final)
            {
                #region dias planificados o feriados

                var pln =
                    planificacionAnticipadaC.Planificaciones.Find(
                        t =>
                            t.Persona.exp == exp &&
                            ((t.fecha_inicio.Value.Date < dia.Date && t.fecha_fin.Value.Date > dia.Date) ||
                             dia.Date == t.fecha_inicio.Value.Date || dia.Date == t.fecha_fin.Value.Date));
                if (pln != null)
                {
                    if (pln.Clave_nom.codigo == "VC")
                    {
                        resp++;
                    }

                    dia = dia.AddDays(1);
                    continue; //salto al proximo
                }
                #endregion
                var inc = incidenciaC.Incidencias.Find(t => t.Persona.exp == exp && t.fecha.Date == dia.Date && t.Clave_nom.codigo == "VC");
                if (inc != null)
                {
                    resp++;
                }

                dia = dia.AddDays(1);
            }
            return resp;
        }

        private int CalculaCertificadosPeriodo(int exp, DateTime fechainicio, DateTime fechafin)
        {

            int resp = 0;
            DateTime inicial = fechainicio;
            DateTime final = fechafin;
            DateTime dia = inicial;
            //var periodostime = personaC.GetPersonaPorExpediente(exp).Grupo.Periodo_tiempo;
            while (dia <= final)
            {
                #region dias planificados o feriados

                var pln =
                    planificacionAnticipadaC.Planificaciones.Find(
                        t =>
                            t.Persona.exp == exp &&
                            ((t.fecha_inicio.Value.Date < dia.Date && t.fecha_fin.Value.Date > dia.Date) ||
                             dia.Date == t.fecha_inicio.Value.Date || dia.Date == t.fecha_fin.Value.Date));
                if (pln != null)
                {
                    if (pln.Clave_nom.codigo == "CM")
                    {
                        resp++;
                    }

                    dia = dia.AddDays(1);
                    continue; //salto al proximo
                }
                #endregion
                var inc = incidenciaC.Incidencias.Find(t => t.Persona.exp == exp && t.fecha.Date == dia.Date && t.Clave_nom.codigo == "CM");
                if (inc != null)
                {
                    resp++;
                }

                dia = dia.AddDays(1);
            }
            return resp;
        }

        private double CalculaHorasPerdidasPeriodo(int exp, DateTime fechainicio, DateTime fechafin)
        {
            double resphoras = 0;
            try
            {
                #region metodo

                int[] exped = new int[] { exp };
                DateTime inicial = fechainicio.Date;
                DateTime final = fechafin.Date;

                List<RegistroESpejo> todas = new Service1Client().TrazasEnRangoxListadoExp(inicial, final, exped).ToList();
                DateTime dia = inicial;
                var periodostime = grupoC.GetPeriodoDeTiempoDeGrupo(personaC.GetPersonaPorExpediente(exp).Grupo);
                while (dia <= final)
                {
                    //if (planificacionAnticipadaC.Planificaciones.Find(t => t.Persona.exp == exp && ((t.fecha_inicio.Value.Date < dia.Date && t.fecha_fin.Value.Date > dia.Date) ||
                    //                           dia.Date == t.fecha_inicio.Value.Date || dia.Date == t.fecha_fin.Value.Date)) != null)
                    //{
                    //    dia = dia.AddDays(1);
                    //    continue; //salto al proximo
                    //}
                    #region dias planificados o feriados

                    var pln =
                        planificacionAnticipadaC.Planificaciones.Find(
                            t =>
                                t.Persona.exp == exp &&
                                ((t.fecha_inicio.Value.Date < dia.Date && t.fecha_fin.Value.Date > dia.Date) ||
                                 dia.Date == t.fecha_inicio.Value.Date || dia.Date == t.fecha_fin.Value.Date));
                    if (pln != null)
                    {
                        if (clavesNomC.Descuenta(pln.Clave_nom))
                        {
                            foreach (var periodoTiempo in periodostime)
                            {
                                if (dia.Subtract(periodoTiempo.fecha_inicio.Value).Days % periodoTiempo.dias_periodo == 0)
                                {
                                    resphoras += periodoTiempo.cantidad_horas.GetValueOrDefault(0);
                                }

                            }
                        }
                        dia = dia.AddDays(1);
                        continue; //salto al proximo dia y no reviso nada mas porke tenias algo planificado
                    }

                    #endregion
                    foreach (var periodos in periodostime)
                    {
                        DateTime limitebajo = new DateTime(dia.Year, dia.Month, dia.Day, periodos.hora_entrada.Value.Hours, periodos.hora_entrada.Value.Minutes, periodos.hora_entrada.Value.Seconds);
                        limitebajo = limitebajo.AddHours(Convert.ToDouble(24 - periodos.cantidad_horas) / -2.0);
                        DateTime limitealto =
                            limitebajo.AddHours(Convert.ToDouble(periodos.cantidad_horas))
                                .AddMinutes(Convert.ToDouble(periodos.minuto_almuerzo))
                                .AddHours(Convert.ToDouble(24 - periodos.cantidad_horas));
                        if (dia.Subtract(periodos.fecha_inicio.Value).Days % periodos.dias_periodo == 0)//entonces debes venir, es un dia laborable  
                        {
                            if (Obtener_DiasNolaborablesDeGrupo((int)personaC.GetPersonaPorExpediente(exp).id_Grupo).Find(t => t.fecha.Date == dia.Date) != null)
                            {
                                resphoras += 8;
                                dia = dia.AddDays(1);
                                continue;
                            }
                            //si es un dia laborable para ti debes tener trazas
                            List<RegistroESpejo> aux = new List<RegistroESpejo>();
                            aux = todas.FindAll(t => t.Fecha > limitebajo && t.Fecha < limitealto && t.Exp == exp);
                            aux.OrderBy(o => o.Fecha);// en auxiliar est'an las trazas pertenecientes a la jornada laboral
                            var incidenciashoy = incidenciaC.Incidencias.FindAll(t => t.Persona.exp == exp && t.fecha > limitebajo && t.fecha < limitealto);
                            if (incidenciashoy.Count > 0)
                                if (aux.Count == 0) //esto es k no viniste y te tocaba
                                {
                                    if (incidenciashoy.Find(t => clavesNomC.Descuenta(t.Clave_nom)) != null)//t.Clave_nom.codigo == "AI" || t.Clave_nom.codigo == "MV"
                                    {
                                        resphoras += periodos.cantidad_horas.GetValueOrDefault(0);
                                    }
                                }
                                else
                                {// viniste y tienes 1 o mas de 2 trazas en el dia
                                    DateTime entrar = new DateTime(dia.Year, dia.Month, dia.Day,
                                   periodos.hora_entrada.Value.Hours, periodos.hora_entrada.Value.Minutes,
                                   periodos.hora_entrada.Value.Seconds);
                                    DateTime salir = entrar.AddHours(Convert.ToDouble(periodos.cantidad_horas))
                                       .AddMinutes(Convert.ToDouble(periodos.minuto_almuerzo)).AddSeconds(-59);
                                    //------------------------cuando estas tarde--------------------------
                                    //var tastarde = incidenciashoy.Find(t => t.tipotraza == "E" && clavesNomC.Descuenta(t.Clave_nom));
                                    if (incidenciashoy.First().tipotraza == "E" && clavesNomC.Descuenta(incidenciashoy.First().Clave_nom))
                                        if (incidenciashoy.First().fecha > entrar.AddMinutes(30))
                                        {
                                            if (incidenciashoy.First().fecha.Subtract(entrar).TotalMinutes / 60 > periodos.cantidad_horas.GetValueOrDefault(0) / 2)
                                            {
                                                resphoras += periodos.cantidad_horas.GetValueOrDefault(0) / 2.0; //le kito la mitad de la jornada si la tardanza es mayor de la mitad de la jornada
                                            }
                                            else
                                            {
                                                resphoras += incidenciashoy.First().fecha.Subtract(entrar).TotalMinutes / 60;
                                            }

                                        }
                                    //}
                                    //------------------------cuando no marcas la salida--------------------------
                                    var nomar = incidenciashoy.Find(t => t.tipotraza == "N" && clavesNomC.Descuenta(t.Clave_nom));
                                    if (nomar != null)//esto es una salida que no se marco al final del dia
                                    {
                                        if (aux.Last().Tipo == "E") // me cercioro k lo ultimo de ese dia fue una entrada
                                        {
                                            resphoras += periodos.cantidad_horas.GetValueOrDefault(0) / 2.0;
                                            //aki le kito la mitad de la jornada
                                            //otra variante es  descontando el tiempo entre la hora de salir y su 'ultima entrada  resphoras += salir.Subtract(aux.Last().Fecha).TotalMinutes / 60;

                                        }
                                    }
                                    //-------------------------cuando sales antes de tiempo definitivo-----------------------------------------------------

                                    var sales = incidenciashoy.Find(t => t.tipotraza == "S" && aux.Last().Fecha == t.fecha && clavesNomC.Descuenta(t.Clave_nom));
                                    if (sales != null)//esto es una salida que no se marco al final del dia
                                    {
                                        if (sales.fecha < salir)
                                        {
                                            resphoras += salir.Subtract(sales.fecha).TotalMinutes / 60;//aki le estoy descontando el tiempo entre la hora de salir y su salida
                                            //otra variante es kitar la mitad del dia: resphoras += periodos.cantidad_horas.GetValueOrDefault(0)/2;
                                        }
                                    }
                                    //-------------------------cuando sales antes de tiempo y vuelves a entrar varias veces-----------------------------------------------------
                                    var salesy = incidenciashoy.FindAll(t => t.tipotraza == "S" && clavesNomC.Descuenta(t.Clave_nom) && aux.Last().Fecha != t.fecha);
                                    if (salesy.Count > 0)
                                    {
                                        foreach (var item in salesy)
                                        {
                                            DateTime salidita = item.fecha;
                                            DateTime entradita = aux.ElementAt(aux.IndexOf(aux.Find(o => o.Fecha == item.fecha)) + 1).Fecha;

                                            if (salidita > entrar && entradita < salir)
                                            {
                                                //aki le estoy descontando el tiempo entre la hora k salió y su proxima entrada
                                                resphoras += entradita.Subtract(salidita).TotalMinutes / 60;
                                            }
                                            if (salidita < entrar && entradita > entrar && entradita < salir)
                                            {
                                                resphoras += entradita.Subtract(entrar).TotalMinutes / 60;
                                            }
                                            if (salidita > entrar && salidita < salir && entradita > salir)
                                            {
                                                resphoras += salir.Subtract(salidita).TotalMinutes / 60;
                                            }
                                            if (salidita < entrar && entradita > salir)
                                            {
                                                resphoras += periodos.cantidad_horas.Value;
                                            }

                                        }
                                    }
                                }


                        }


                    }

                    dia = dia.AddDays(1);
                }

                #endregion
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return Math.Round(resphoras / 8, 2);

        }


        #endregion

        //-------------funciones auxiliares---------------------------------
        /// <summary>
        ///  //Determina si la fecha del parametro esta contenida en alguna jornada laboral del trabajador.
        ///  Devuelve la fecha y hora de inicio de la jornada laboral o un datetime con el valor 01/01/0001 00:00:00 en caso de no estar la fecha comprendida dentro de una jornada laborable.
        /// </summary>
        /// <param name="exp">expediente del trabajador</param>
        /// <param name="fecha"> fecha y hora que desea conocer si es laborable por ese trabajador</param>
        /// <returns>devuelve la fecha y hora de inicio de la jornada laboral o un datetime con el valor 01/01/0001 00:00:00</returns>
        public DateTime SaberSiTocaTrabajar(int exp, DateTime fecha)
        {

            var periodostime = grupoC.GetPeriodoDeTiempoDeGrupo(personaC.GetPersonaPorExpediente(exp).Grupo);
            foreach (var periodoTiempo in periodostime)
            {
                int hora = periodoTiempo.hora_entrada.Value.Hours;
                int minuto = periodoTiempo.hora_entrada.Value.Minutes;
                int segundo = periodoTiempo.hora_entrada.Value.Seconds;
                int dia = periodoTiempo.fecha_inicio.Value.Day;
                int mes = periodoTiempo.fecha_inicio.Value.Month;
                int ano = periodoTiempo.fecha_inicio.Value.Year;
                DateTime inicio = new DateTime(ano, mes, dia, hora, minuto, segundo);

                double horas = fecha.Subtract(inicio).TotalMinutes;
                int diferenciahoras = Convert.ToInt16(fecha.Subtract(inicio).TotalMinutes % (periodoTiempo.dias_periodo * 24 * 60));
                if (diferenciahoras >= 0 && diferenciahoras <= (periodoTiempo.cantidad_horas * 60 + periodoTiempo.minuto_almuerzo))
                {
                    return new DateTime(fecha.Year, fecha.Month, fecha.Day, hora, minuto, segundo);
                }
                else
                if (fecha.Date.Equals(DateTime.Now.Date))
                {
                    return new DateTime(fecha.Year, fecha.Month, fecha.Day, hora, minuto, segundo);
                }

            }

            return new DateTime();
        }
    }
}
