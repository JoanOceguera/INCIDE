using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace INCIDE.ControlEntidades
{
    enum Rol : int { Administrador = 1, Economia = 2 };
    class PasswordControl :Controlador
    {
        public PasswordControl() : base() { }

        /// <summary>
        /// Dado un identificador de Incidencia retorna, de existir en la BD, La incidencia en cuestión.
        /// Retorna null de no existir la incidencia en la BD.
        /// </summary>
        /// <param name="idIncidencia">id de la Incidencia que se desea seleccionar de la BD</param>        
        public Password GetPassword(int idPassword)
        {
            Password password = (from pass in cnx.Password
                                 where pass.id_password == idPassword
                                 select pass).FirstOrDefault();
            return password;
        }

        /// <summary>
        /// Obtiene un pass dado una persona. Retorna null de no existir el password para la persona pasada por parametro
        /// </summary>
        public Password GetPasswordDePersona(Persona persona)
        {     
            Password pass = (from pas in cnx.Password
                             where pas.Persona.id_Persona == persona.id_Persona
                             select pas).FirstOrDefault();           
            return pass;

        }

        public void Editar(Password pass)
        {
            try
            {
                Password _pass = this.GetPassword(pass.id_password);
                if (_pass  != null)
                {
                    cnx.Password.ApplyCurrentValues(pass);
                    cnx.SaveChanges();
                }
            }
            catch (Exception msg)
            {
                throw new Exception("Ocurrió un error en el proceso de edición del password " + msg.Message);
            }
        }
        public void Adicionar(Password pass)
        {
            try
            {
                cnx.Password.AddObject(pass);
                cnx.SaveChanges();
            }
            catch (Exception msg)
            {
                throw new Exception("Ocurrió un error adicionando la contraseña.\n" + msg.Message);
            }
        }

        /// <summary>
        /// Cambia el valor string al objeto Password pasado por parametro. Retorna true de tener exito, false en caso contrario
        /// </summary>
        public bool CambiarContrasena(Password pass, String nuevaContrasena)
        {
            try
            {
                pass.password1 = nuevaContrasena;
                this.Editar(pass);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
