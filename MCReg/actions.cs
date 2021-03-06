﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;


namespace MCReg
{
    public class actions
    {
        private MongoClient cliente = new MongoClient();

        public List<usuarios> ObtenerUsuarios()
        {
            var db = cliente.GetDatabase("MCReg");
            return db.GetCollection<usuarios>("usuarios").AsQueryable().ToList();
        }
        public List<pacientes> ObtenerPacientes()
        {
            var db = cliente.GetDatabase("MCReg");
            return db.GetCollection<pacientes>("pacientes").AsQueryable().ToList();
        }

        public usuarios Usuarioporid(string id)
        {
            var _id = new ObjectId(id);
            var db = cliente.GetDatabase("MCReg");
            return db.GetCollection<usuarios>("usuarios").Find(p => p._id == _id).SingleOrDefault();
        }


        public int Login(string usuario, string pass)
        {

            /*----------------------CODIGOS DE RESPUESTA-------------------
             * 0 : ERROR DE CONEXION
             * 1 : LOGIN CORRECTO
             * 2 : ERROR DE USUARIO O CONTRASEÑA
             */
            var db = cliente.GetDatabase("MCReg");
            usuarios fuser = db.GetCollection<usuarios>("usuarios").Find(p => p.usuario == usuario).SingleOrDefault();
                      
            try
            {
                
            if(fuser != null && (usuario==fuser.usuario && pass == fuser.pass))
            {
                return 1;
            }
            else
            {
                return 2;
            }
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public usuarios logeado(string usuario)
        {
            var db = cliente.GetDatabase("MCReg");
            return db.GetCollection<usuarios>("usuarios").Find(p => p.usuario == usuario).SingleOrDefault();
        }

        public bool Insertarpaciente(pacientes nuevo)
        {
            try
            {
                var db = cliente.GetDatabase("MCReg");
                db.GetCollection<pacientes>("pacientes").InsertOne(nuevo);
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }
        public bool Actualizarpaciente(pacientes paciente)
        {
            try
            {
                var db = cliente.GetDatabase("MCReg");
                return db.GetCollection<pacientes>("pacientes").ReplaceOne(p => p._id == paciente._id, paciente).ModifiedCount > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Eliminarpaciente(string id)
        {
            try
            {
                var __id = new ObjectId(id);
                var db = cliente.GetDatabase("MCReg");
                return db.GetCollection<pacientes>("pacientes").DeleteOne(p => p._id == __id).DeletedCount > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public pacientes Pacientepordoc(string doc)
        {
            var db = cliente.GetDatabase("MCReg");
            return db.GetCollection<pacientes>("pacientes").Find(p => p.documento == doc).SingleOrDefault();
        }
        public List<pacientes> pacientesdoclist(string doc)
        {
            var db = cliente.GetDatabase("MCReg");
            return db.GetCollection<pacientes>("pacientes").Find(p => p.documento == doc).ToList();
        }


        public List<aut> Obteneraut(string user)
        {
            var db = cliente.GetDatabase("MCReg");
            return db.GetCollection<aut>("aut").Find(p => p.usuario_destino == user).ToList();
        }
        public aut obtenerporid(string id)
        {
            var __id = new ObjectId(id);
            var db = cliente.GetDatabase("MCReg");
            return db.GetCollection<aut>("aut").Find(p => p._id == __id).SingleOrDefault();
        }
        public bool enviarsolicitud(solicitud nueva)
        {
            try
            {
                var db = cliente.GetDatabase("MCReg");
                db.GetCollection<solicitud>("solicitud").InsertOne(nueva);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public DateTime Cadenafecha(string cadenatiempo)
        {
            if(cadenatiempo != "")
            {
                return DateTime.Parse(cadenatiempo);
            }else
            {
                return DateTime.MinValue;
            }
        
        }

            
           

    }
}
