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
    public class ShowsController : ApiController
    {
        private bio_bookingEntities db = new bio_bookingEntities();

        //GET: api/Shows
        [Route("api/shows")]
        public IQueryable<shows> GetShows()
        {
            return db.shows;
        }

        // GET: api/Shows/5
        [Route("api/show/{id}")]
        [ResponseType(typeof(shows))]
        public IHttpActionResult GetShows(long id)
        {
            shows show = db.shows.Find(id);
            if (show == null)
            {
                return NotFound();
            }

            return Ok(show);
        }

        //GET: api/movie/{id}/shows
        [Route("api/movie/{id}/shows")]
        public IQueryable<shows> GetMovieShows(long id)
        {
            /* 
            movies movie = db.movies.Find(id);
            if (movie == null)
            {
                return Enumerable.Empty<shows>().AsQueryable();
            }
            return db.Entry(movie).Collection<shows>("shows").Query();
            */

            return db.shows.Where(show => show.movie_id == id);
        }
    }
}
