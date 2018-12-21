using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;

namespace MCReg
{
   public class usuarios
    {
        public ObjectId _id { get; set; }
        public string tipo { get; set; }
        public string usuario { get; set; }
        public string pass { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string email { get; set; }

    }

    public class pacientes
    {
        public ObjectId _id { get; set; }

        public string documento { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string email { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public string movil { get; set; }
}

}
