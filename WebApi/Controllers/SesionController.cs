using AccesoDatos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class SesionController : ApiController
    {
        private evaluacionEntities Context = new evaluacionEntities();

        [HttpGet]
        public IEnumerable<Sesiones> Get()
        {
            using (evaluacionEntities db = new evaluacionEntities())
            {
                return db.Sesiones.ToList();
            }
        }

        [HttpGet]
        public Sesiones Get(int id)
        {
            using (evaluacionEntities db = new evaluacionEntities())
            {
                return db.Sesiones.FirstOrDefault(x => x.IdSesion == id);
            }
        }

        [HttpGet]
        public Sesiones GetSesionUser(int userId)
        {
            using (evaluacionEntities db = new evaluacionEntities())
            {
                var z = db.Sesiones.FirstOrDefault(x => x.IdUser == userId && x.lSession == true);
                return z;
            }
        }





        [HttpPost]
        public IHttpActionResult AgregarSesiones([FromBody] Sesiones sesion)
        {
            if (ModelState.IsValid)
            {
                Context.Sesiones.Add(sesion);
                Context.SaveChanges();
                return Ok(sesion);
            }
            else
            {
                return BadRequest();
            }
        }





        [HttpPut]
        public IHttpActionResult ActualizarSesiones(int id, [FromBody] Sesiones sesion)
        {
            if (ModelState.IsValid)
            {
                var usuarioExiste = Context.Sesiones.Count(e => e.IdSesion == id) > 0;
                if (usuarioExiste)
                {
                    Context.Entry(sesion).State = EntityState.Modified;
                    Context.SaveChanges();
                    return Ok(sesion);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return BadRequest();

            }
        }





        [HttpDelete]
        public IHttpActionResult EliminarSesiones(int id)
        {
            var sesion = Context.Sesiones.Find(id);
            if (sesion != null)
            {
                Context.Sesiones.Remove(sesion);
                Context.SaveChanges();

                return Ok(sesion);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
