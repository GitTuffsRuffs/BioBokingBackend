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
    public class MoviesController : ApiController
    {
        private bio_bookingEntities db = new bio_bookingEntities();

        // GET: api/Movies
        public IQueryable<movies> Getmovies()
        {
            return db.movies;
        }

        // GET: api/Movies/5
        [ResponseType(typeof(movies))]
        public IHttpActionResult Getmovies(long id)
        {
            movies movies = db.movies.Find(id);
            if (movies == null)
            {
                return NotFound();
            }

            return Ok(movies);
        }

        // PUT: api/Movies/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putmovies(long id, movies movies)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != movies.id)
            {
                return BadRequest();
            }

            db.Entry(movies).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!moviesExists(id))
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

        // POST: api/Movies
        [ResponseType(typeof(movies))]
        public IHttpActionResult Postmovies(movies movies)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.movies.Add(movies);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = movies.id }, movies);
        }

        // DELETE: api/Movies/5
        [ResponseType(typeof(movies))]
        public IHttpActionResult Deletemovies(long id)
        {
            movies movies = db.movies.Find(id);
            if (movies == null)
            {
                return NotFound();
            }

            db.movies.Remove(movies);
            db.SaveChanges();

            return Ok(movies);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool moviesExists(long id)
        {
            return db.movies.Count(e => e.id == id) > 0;
        }
    }
}