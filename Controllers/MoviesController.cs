﻿using System;
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
        [Route("api/movies")]
        public IQueryable<movies> Getmovies()
        {
            return db.movies;
        }

        // GET: api/Movies/5
        [Route("api/movie/{id}")]
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
    }
}