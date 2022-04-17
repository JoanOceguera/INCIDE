using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INCIDE.ControlEntidades
{
    class Periodo_tiempoControl : Controlador
    {
        public Periodo_tiempoControl() : base() { }

        /// <summary>
        /// Crea y retorna un Periodo_tiempo.
        /// </summary>        
        public Periodo_tiempo Crear(Int32 cantidadHoras, String descripcion, Int32 diasPeriodo, DateTime fechaInicio, Int32 minutoAlmuerzo, TimeSpan horaEntrada )
        {
            Periodo_tiempo periodoTiempo = new Periodo_tiempo() { activo = Utils.SI, cantidad_horas = cantidadHoras, descripcion = descripcion, dias_periodo = diasPeriodo, fecha_inicio = fechaInicio, hora_entrada = horaEntrada, minuto_almuerzo = minutoAlmuerzo};
            return periodoTiempo;
        }

        /// <summary>
        /// Adiciona a la BD unPeriodo_tiempo dado
        /// </summary>
        /// <param name="peridoTiempo">Periodo_tiempo que se pretende adicionar a la BD</param>
        public void Adicionar(Periodo_tiempo peridoTiempo)
        {
            try
            {
                cnx.Periodo_tiempo.AddObject(peridoTiempo);
                cnx.SaveChanges();
            }
            catch (Exception msg)
            {
                throw new Exception("Ocurrió un error adicionando un peridoTiempo. " + msg.Message);
            }
        }

        /// <summary>
        /// Edita un Periodo_tiempo dado. Debe pasar un mismo Periodo_tiempo seleccionado previamente el cual mantenga su idPeriodo_tiempo
        /// </summary>
        /// <param name="Dia_no_laborable_nom">Periodo_tiempo con las modificaciones hechas</param>
        public void Editar(Periodo_tiempo periodoTiempo)
        {
            try
            {
                Periodo_tiempo periodo = this.GetPeriodo_tiempo(periodoTiempo.id_periodo_tiempo);
                if (periodo != null)
                {
                    cnx.Periodo_tiempo.ApplyCurrentValues(periodoTiempo);
                    cnx.SaveChanges();
                }
            }
            catch (Exception msg)
            {
                throw new Exception("Ocurrió un error en el proceso de edición del período de tiempo con id: " + periodoTiempo.id_periodo_tiempo + ". " + msg.Message);
            }
        }

        /// <summary>
        /// Dado un identificador de Periodo_tiempo retorna, de existir en la BD, el Periodo_tiempo en cuestion.
        /// Retorna null de no existir el Dia_no_laborable_nom en la BD.
        /// </summary>
        /// <param name="idPeriodo_tiempo">id del Periodo_tiempo que se desea seleccionar de la BD</param>        
        public Periodo_tiempo GetPeriodo_tiempo(int idPeriodo_tiempo)
        {
            Periodo_tiempo periodoTiempo = (from periodo in cnx.Periodo_tiempo
                                            where periodo.id_periodo_tiempo == idPeriodo_tiempo
                                                select periodo).FirstOrDefault();
            return periodoTiempo;
        }

        /// <summary>
        /// Retorna una lista de Periodo_tiempo obtenidos de la BD. 
        /// Propiedad de solo lectura
        /// </summary>
        public List<Periodo_tiempo> Periodos_tiempo
        {
            get
            {
                List<Periodo_tiempo> periodosTiempo = (from periodo in cnx.Periodo_tiempo
                                                             select periodo).ToList();
                return periodosTiempo;
            }
        }

        /// <summary>
        /// Borra un Periodo_tiempo de la base de datos
        /// </summary>
        public void Borrar(Periodo_tiempo periodoTiempo)
        {
            try
            {
                Periodo_tiempo periodo = this.GetPeriodo_tiempo(periodoTiempo.id_periodo_tiempo);
                if (periodo != null)
                {
                    periodo.activo = Utils.NO;
                    cnx.SaveChanges();
                }
                else throw new Exception("El Periodo_tiempo que intenta borrar no existe en la base de datos");
            }
            catch (Exception msg)
            {
                throw new Exception("Ocurrió un error en el proceso de borrado del periodo_tiempo. " + msg.Message);
            }
        }
    }
}
