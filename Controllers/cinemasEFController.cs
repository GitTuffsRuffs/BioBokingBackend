using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using BioBokingMSSQLdatabase.Models;

namespace BioBokingMSSQLdatabase.Controllers
{
    [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
    public class cinemasEFController : ApiController
    {
        private bio_bookingEntities db = new bio_bookingEntities();

        // GET: api/cinemasEF
        public IQueryable<cinemas> Getcinemas()
        {
            return db.cinemas;
        }

        // GET: api/cinemasEF/5
        [ResponseType(typeof(cinemas))]
        public IHttpActionResult Getcinemas(long id)
        {
            cinemas cinemas = db.cinemas.Find(id);
            if (cinemas == null)
            {
                return NotFound();
            }

            return Ok(cinemas);
        }

        // PUT: api/cinemasEF/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putcinemas(long id, cinemas cinemas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cinemas.id)
            {
                return BadRequest();
            }

            db.Entry(cinemas).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!cinemasExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/cinemasEF
        [ResponseType(typeof(cinemas))]
        public IHttpActionResult Postcinemas(cinemas cinemas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.cinemas.Add(cinemas);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = cinemas.id }, cinemas);
        }

        // DELETE: api/cinemasEF/5
        [ResponseType(typeof(cinemas))]
        public IHttpActionResult Deletecinemas(long id)
        {
            cinemas cinemas = db.cinemas.Find(id);
            if (cinemas == null)
            {
                return NotFound();
            }

            db.cinemas.Remove(cinemas);
            db.SaveChanges();

            return Ok(cinemas);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool cinemasExists(long id)
        {
            return db.cinemas.Count(e => e.id == id) > 0;
        }
    }
}