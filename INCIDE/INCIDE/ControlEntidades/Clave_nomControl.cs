using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INCIDE.ControlEntidades
{
    class Clave_nomControl : Controlador
    {
        public Clave_nomControl() : base() { }

        /// <summary>
        /// Crea y retorna un Clave_nomControl.
        /// </summary>        
        public Clave_nom Crear(String pDescripcion, int pActivo, int pPlanificable, string pCodigo, int pDescuenta)
        {
            Clave_nom clave = new Clave_nom() {descripcion = pDescripcion, activo = pActivo, planificable = pPlanificable, codigo = pCodigo, descuenta = pDescuenta};
            return clave;
        }

        /// <summary>
        /// Adiciona a la BD un Clave_nom dado
        /// </summary>
        /// <param name="clave">Clave_nom que se pretende adicionar a la BD</param>
        public bool Adicionar(Clave_nom clave)
        {
            try
            {
                cnx.Clave_nom.AddObject(clave);

                if (cnx.SaveChanges() > 0)
                {
                    return true;
                }
                else
                    return false;
            }
            catch (Exception msg)
            {
                throw new Exception("Ocurrió un error adicionando una Clave_nom: " + ". " + msg.Message);
            }
        }

        /// <summary>
        /// Dado un identificador de Clave_nom retorna, de existir en la BD, el Clave_nom en cuestion.
        /// Retorna null de no existir el Clave_nom en la BD.
        /// </summary>
        /// <param name="idEquipo">id del Clave_nom que se desea seleccionar de la BD</param>        
        public Clave_nom GetClave_nom(int idClave_nom)
        {
            Clave_nom claveNom = (from clave in cnx.Clave_nom
                                  where clave.id_clave == idClave_nom
                                                select clave).FirstOrDefault();
            return claveNom;
        }

        public Clave_nom GetClave_nom_Codigo(string Codigo)
        {
            Clave_nom claveNom = (from clave in cnx.Clave_nom
                                  where clave.codigo == Codigo
                                  select clave).FirstOrDefault();
            return claveNom;
        }

        /// <summary>
        /// Edita un Clave_nom dado. Debe pasar un mismo Clave_nom seleccionado previamente el cual mantenga su idClave_nom
        /// </summary>
        /// <param name="clave">Clave_nom con las modificaciones hechas</param>
        public void Editar(Clave_nom clave)
        {
            try
            {
                Clave_nom clave_ = this.GetClave_nom(clave.id_clave);
                if (clave_ != null)
                {
                    cnx.Clave_nom.ApplyCurrentValues(clave);
                    cnx.SaveChanges();
                     
                    
                }
            }
            catch (Exception msg)
            {
                throw new Exception("Ocurrió un error en el proceso de edición de Clave_nom con id: " + clave.id_clave + ". " + msg.Message);
            }
        }

        public bool Editar_Clave(Clave_nom clave)
        {
            try
            {
                Clave_nom clave_ = this.GetClave_nom(clave.id_clave);
                if (clave_ != null)
                {
                    cnx.Clave_nom.ApplyCurrentValues(clave);
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
                throw new Exception("Ocurrió un error en el proceso de edición de Clave_nom con id: " + clave.id_clave + ". " + msg.Message);
            }
        }

        /// <summary>
        /// Borra un Clave_nom de la base de datos
        /// </summary>
        public void Borrar(Clave_nom clave)
        {
            try
            {
                Clave_nom _clave = this.GetClave_nom(clave.id_clave);
                if (_clave != null)
                {
                    _clave.activo = Utils.NO;
                    cnx.SaveChanges();
                }
                else throw new Exception("La Clave_nom que intenta borrar no existe en la base de datos");
            }
            catch (Exception msg)
            {
                throw new Exception("Ocurrió un error en el proceso de borrado de Clave_nom. " + msg.Message);
            }
        }

        public List<Clave_nom> Lista_Claves_Activas()
        {
            List<Clave_nom> lista = (from clave in cnx.Clave_nom
                                     where clave.activo == 1
                                     select clave).ToList();
            return lista; 
        }
        public List<Clave_nom> Lista_Claves()
        {
            List<Clave_nom> lista = (from clave in cnx.Clave_nom                                     
                                     select clave).OrderBy(t=>t.descripcion).ToList();
            return lista;
        }
        public List<Clave_nom> Lista_Claves_Activas_Planificables()
        {
            List<Clave_nom> lista = (from clave in cnx.Clave_nom
                                     where clave.activo == 1 && clave.planificable == 1
                                     select clave).ToList();
            return lista;
        }

        /// <summary>
        /// Dada una Clave_nom retorna true en caso de ke dicha clave descuente para economia.        
        /// </summary>
        /// <returns></returns>
        public bool Descuenta(Clave_nom clave)
        {
            if (clave.descuenta != null && clave.descuenta==1)
                return true;
            return false;
        }

        /// <summary>
        /// Devuelve true si encuentra una clave con el mismo código que la que se está pasando por parámetro.
        /// </summary>
        /// <param name="clave"></param>
        /// <returns></returns>
        public bool ExisteClave(Clave_nom clave)
        {
            bool encontrado = false;
            Clave_nom claveNueva = GetClave_nom_Codigo(clave.codigo);

            if (claveNueva != null)
            {
                encontrado = true;
            }
            return encontrado;
        }
    }
}
