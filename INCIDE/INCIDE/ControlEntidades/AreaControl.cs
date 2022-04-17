using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects.DataClasses;

namespace INCIDE.ControlEntidades
{
    public class AreaControl : Controlador
    {
        public AreaControl()//Modifique el constructor para poder crear el diccionario IDArea desde el principio
        {
            if (context == null)
                context = new INCIDEEntities();
            IDArea = GenerarDiccioArea();
        }
        Dictionary<int, Area> IDArea = new Dictionary<int, Area>();

        /// <summary>
        /// Dado un identificador de Área retorna, de existir en la BD, el Área en cuestion.
        /// Retorna null de no existir el área en la BD.
        /// </summary>
        /// <param name="idPersona">id del área que se desea seleccionar de la BD</param>        
        public Area GetArea(int idArea)
        {
            Area area = (from pers in cnx.Area
                                  where pers.id_area == idArea
                                  select pers).FirstOrDefault();
            return area;
        }

        /// <summary>
        /// Genera el Diccionario de idArea, Area correspondiente a IDArea
        /// </summary>
        /// <returns>Diccionario de idArea, Area correspondiente a IDArea</returns>
        public Dictionary<int, Area> GenerarDiccioArea()
        {
            Dictionary<int, Area> Diccio = new Dictionary<int, Area>();
            foreach(var pers in cnx.Area)
            {
                if (!Diccio.ContainsKey(pers.id_area))
                    Diccio.Add(pers.id_area, pers);
            }
            return Diccio;
        }

        /// <summary>
        /// Dado un identificador de Área retorna, de existir en la BD, el Área en cuestion.
        /// Retorna null de no existir el área en la BD. Funciona con el diccionario IDArea, así que hay que inicializarlo antes.
        /// </summary>
        /// <param name="idArea">id del área que se desea seleccionar de la BD</param>
        /// <returns></returns>
        public Area GetAreaDiccio(int idArea)
        {
            if (IDArea.ContainsKey(idArea))
                return IDArea[idArea];
            return null;
        }
        /// <summary>
        /// Crea y retorna un Área.
        /// </summary>        
        public Area Crear(Agrupacion_nom agrupacion, string descripcion, Persona PersonaJefe)
        {
            Area persona = new Area()
            {
                Agrupacion_nom = agrupacion,
                descripcion = descripcion,
                Persona = PersonaJefe

            };

            return persona;
        }

        /// <summary>
        /// Crea y retorna un Área.
        /// </summary>        
        public Area Crear(Agrupacion_nom agrupacion, string descripcion)
        {
            Area persona = new Area()
            {
               Agrupacion_nom = agrupacion,
               descripcion = descripcion
            };

            return persona;
        }
        /// <summary>
        /// Adiciona a la BD un Área dada
        /// </summary>
        /// <param name="persona">Área que se pretende adicionar a la BD</param>
        public void Adicionar(Area area)
        {
            try
            {
                cnx.Area.AddObject(area);
                cnx.SaveChanges();
            }
            catch (Exception msg)
            {
                throw new Exception("Ocurrió un error adicionando el Área de : " + area.descripcion.ToString() + ". " + msg.Message);
            }
        }
        /// <summary>
        /// Edita un Área dada. Debe pasar una misma Área seleccionada previamente el cual mantenga su idArea
        /// </summary>
        /// <param name="Dia_no_laborable_nom">Área con las modificaciones hechas</param>
        public void Editar(Area area)
        {
            try
            {
                Area pers = this.GetAreaDiccio(area.id_area);
                if (pers != null)
                {
                    cnx.Area.ApplyCurrentValues(pers);
                    cnx.SaveChanges();
                }
            }
            catch (Exception msg)
            {
                throw new Exception("Ocurrió un error editando el Área de : " + area.descripcion.ToString() + ". " + msg.Message);
            }
        }
        /// <summary>
        /// Borra una área de la base de datos
        /// </summary>
        public void Borrar(Area area)
        {
            try
            {
                Area dnlborrar = this.GetAreaDiccio(area.id_area);
                this.cnx.DeleteObject(dnlborrar);
                this.cnx.SaveChanges();
            }
            catch (Exception msg)
            {
                throw new Exception("Ocurrió un error borrando el Área de : " + area.descripcion.ToString() + ". " + msg.Message);
            }
        }
        /// <summary>
        /// Retorna una lista de áreas de la BD. 
        /// Propiedad de solo lectura
        /// </summary>
        public List<Area> Areas
        {
            get
            {
                List<Area> listaArea = (from a in cnx.Area
                                              select a).ToList();
                return listaArea;
            }
        }
        /// <summary>
        /// Retorna una lista de personas ke controlan un area pasada por parametro.
        /// retorna una lista vacia en caso de o existir el area pasada por parametro
        /// </summary>
        public List<Persona> PersonasQueControlanArea(Area area)
        {
            List<Persona> personasControlan = new List<Persona>();
            Area _area = this.GetAreaDiccio(area.id_area);
            if (_area != null)
            {
                personasControlan = _area.Persona1.ToList();
            }

            return personasControlan;
        }

        /// <summary>
        /// Retorna una lista de personas ke pertenecen un area pasada por parametro.
        /// retorna una lista vacia en caso de no existir el area pasada por parametro
        /// </summary>
        public List<Persona> PersonasQuePertenecenArea(Area area)
        {
            List<Persona> personasControlan = new List<Persona>();
            Area _area = this.GetAreaDiccio(area.id_area);
            if (_area != null)
            {
                personasControlan = _area.Persona1.ToList().OrderBy(persona => persona.exp).ToList();
            }

            return personasControlan;
        }
        /// <summary>
        /// Retorna una lista de personas activas ke pertenecen un area pasada por parametro.
        /// retorna una lista vacia en caso de no existir el area pasada por parametro
        /// </summary>
        public List<Persona> PersonasActivasQuePertenecenArea(Area area)
        {
            List<Persona> personasControlan = new List<Persona>();
            Area _area = this.GetAreaDiccio(area.id_area);
            if (_area != null)
            {
                personasControlan = _area.Persona1.Where(p=>p.activo == Utils.SI).ToList().OrderBy(persona => persona.exp).ToList();
            }

            return personasControlan;
        }


        public Persona ObtenerJefeArea(Area area)
        {
            return area.Persona;
        }
        public void SetearJefeArea(Area area, Persona jefe)
        {
            area.Persona = jefe;
            this.cnx.SaveChanges();
        }

        public bool AdicionarResponsables_Area(Area area, List<Responsable_area> responsables_area)
        {
            try
            {
                area.Responsable_area.Union(responsables_area);
                this.cnx.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }        
        public List<Responsable_area> ObtenerResponsablesArea(Area area)
        {
            return area.Responsable_area.ToList();
        }

        public Agrupacion_nom ObtenerAgrupacionDeArea(Area area)
        {
            return area.Agrupacion_nom;
        }
    }
}
