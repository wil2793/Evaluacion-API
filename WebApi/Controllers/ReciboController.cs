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
    public class ReciboController : ApiController
    {
        private evaluacionEntities Context = new evaluacionEntities();

        [HttpGet]
        public IEnumerable<Recibo> Get()
        {
            using (evaluacionEntities db = new evaluacionEntities())
            {
                return db.Recibo.ToList();
            }
        }

        [HttpGet]
        public Recibo Get(int id)
        {
            using (evaluacionEntities db = new evaluacionEntities())
            {
                return db.Recibo.FirstOrDefault(x => x.IdRecibo == id);
            }
        }

        [HttpGet]
        public IEnumerable<Recibo> GetReciboUser(int userId)
        {
            using (evaluacionEntities db = new evaluacionEntities())
            {
                return db.Recibo.Where(x => x.IdUser == userId).ToList();
            }
        }





        [HttpPost]
        public IHttpActionResult AgregarRecibo([FromBody] Recibo recibo)
        {
            if (ModelState.IsValid)
            {
                Context.Recibo.Add(recibo);
                Context.SaveChanges();
                return Ok(recibo);
            }
            else
            {
                return BadRequest();
            }
        }





        [HttpPut]
        public IHttpActionResult ActualizarRecibo(int id, [FromBody] Recibo recibo)
        {
            if (ModelState.IsValid)
            {
                var usuarioExiste = Context.Recibo.Count(e => e.IdRecibo == id) > 0;
                if (usuarioExiste)
                {
                    Context.Entry(recibo).State = EntityState.Modified;
                    Context.SaveChanges();
                    return Ok(recibo);
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
        public IHttpActionResult EliminarRecibo(int id)
        {
            var recibo = Context.Recibo.Find(id);
            if (recibo != null)
            {
                Context.Recibo.Remove(recibo);
                Context.SaveChanges();

                return Ok(recibo);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
