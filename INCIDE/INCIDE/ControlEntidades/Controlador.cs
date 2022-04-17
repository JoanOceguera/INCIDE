using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data;
using INCIDE;

namespace INCIDE.ControlEntidades
{
    public class Controlador
    {
        static public INCIDEEntities context = null;

        public Controlador()
        {
            if (context == null)
                context = new INCIDEEntities();
        }
        private INCIDEEntities GetCNX()
        {
            if (context == null)
                context = new INCIDEEntities();
            return context;
        }
        public INCIDEEntities cnx
        {
            get { return this.GetCNX(); }
            set { context = value; }
        }
        static public INCIDEEntities CNX
        {
            get { return context; }
        }
    }
}
