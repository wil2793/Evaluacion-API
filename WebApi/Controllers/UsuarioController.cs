using AccesoDatos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class UsuarioController : ApiController
    {
        private evaluacionEntities Context = new evaluacionEntities();

        [HttpGet]
        public IEnumerable<Usuario> Get()
        {
            using (evaluacionEntities db = new evaluacionEntities())
            {
                return db.Usuario.ToList();
            }
        }

        [HttpGet]
        public Usuario Get(int id)
        {
            using (evaluacionEntities db = new evaluacionEntities())
            {
                return db.Usuario.FirstOrDefault(x => x.IdUser == id);
            }
        }

        [HttpGet]
        public Usuario GetUserPass(string user, string pass)
        {
            using (evaluacionEntities db = new evaluacionEntities())
            {
                return db.Usuario.FirstOrDefault(x => x.UserName == user && x.Contrasena == pass);
            }
        }





        [HttpPost]
        public IHttpActionResult AgregarUsuario([FromBody]Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                Context.Usuario.Add(usuario);
                Context.SaveChanges();
                return Ok(usuario);
            }
            else
            {
                return BadRequest();
            }
        }





        [HttpPut]
        public IHttpActionResult ActualizarUsuario(int id, [FromBody] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                var usuarioExiste = Context.Usuario.Count(e => e.IdUser == id) > 0;
                if (usuarioExiste)
                {
                    Context.Entry(usuario).State = EntityState.Modified;
                    Context.SaveChanges();
                    return Ok(usuario);
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
        public IHttpActionResult EliminarUsuario(int id)
        {
            var usuario = Context.Usuario.Find(id);
            if (usuario != null)
            {
                Context.Usuario.Remove(usuario);
                Context.SaveChanges();

                return Ok(usuario);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
