using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace INCIDE.ControlEntidades
{
    static class Utils
    {
        enum Activo : int { si = 1, no = 0};

        public static Int32 SI { get { return (Int32)Activo.si; } }
        public static Int32 NO { get { return (Int32)Activo.no; } }

        public static String[] GetMesesAno()
        {
            String[] meses = CultureInfo.CurrentCulture.DateTimeFormat.MonthNames;
            return meses;
        }

        /// <summary>
        /// Dado un valor tipo object evalua si esta representado de forma Entero 32bit.
        /// </summary>
        public static bool EsNumeroEntero32(object valor)
        {
            try
            {
                Convert.ToInt32(valor);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Dado un valor string evalua si todos los caracteres son numeros Enteros
        /// </summary>
        public static bool TodosNumeros(String valor)
        {
            foreach (var c in valor)
            {
                if (!EsNumeroEntero32(c))
                    return false;
            }
            return true;
        }
    }
}
