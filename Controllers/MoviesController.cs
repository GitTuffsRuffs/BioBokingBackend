using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using BioBokingMSSQLdatabase.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BioBokingMSSQLdatabase.Controllers
{
    [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
    public class MoviesController : ApiController
    {
        private bio_bookingEntities db = new bio_bookingEntities();

        // test
        [Route("test")]
        public DateTime GetTest()
        {
            string movieTag = "tt1049413";

            string apikey = ConfigurationManager.AppSettings["apikey"];
            string baseUrl = "https://api.themoviedb.org/3/find/%IMDBID%?external_source=imdb_id&api_key=%APIKEY%";
            string url = baseUrl.Replace("%IMDBID%", movieTag).Replace("%APIKEY%", apikey);
            

            return DateTime.ParseExact("1989-05-05", "yyyy-MM-dd", CultureInfo.InvariantCulture);

            //return data.movie_results[0].title + ": "+ data.movie_results.Count;
            //return ConfigurationManager.AppSettings["apikey"];
        }

        // GET: api/Movies
        [Route("api/import/{movieTag}")]
        [ResponseType(typeof(movies))]
        public IHttpActionResult GetImport(string movieTag)
        {
            string apikey = ConfigurationManager.AppSettings["apikey"];
            string baseUrl = "https://api.themoviedb.org/3/find/%IMDBID%?external_source=imdb_id&api_key=%APIKEY%";
            string url = baseUrl.Replace("%IMDBID%", movieTag).Replace("%APIKEY%", apikey);
            string json = "";

            using (WebClient fetcher = new WebClient())
            {
                json = fetcher.DownloadString(url);
            }

            if (json == "")
            {
                return Content(HttpStatusCode.ServiceUnavailable, "Api not responding");
            }

            dynamic data = JsonConvert.DeserializeObject(json);

            if (data.movie_results == null || data.movie_results.Count < 1)
            {
                return Content(HttpStatusCode.NotFound, "Movie tag not fund");
            }
            dynamic movieResult = data.movie_results[0];

            //if movie is alredy in data base do not make a duplicate
            IQueryable<movies> querry = db.movies.Where(movieFind => movieFind.imdb_tag == movieTag);
            int existingMovie = querry.Count();
            movies movie = null;

            if (existingMovie < 1)
            {
                //Add new movie
                movie = new movies();
                movie.imdb_tag = movieTag;
                db.movies.Add(movie);
            } else
            {
                movie = querry.First<movies>();
            }

            movie.title = movieResult.title;
            movie.description = movieResult.overview;
            movie.language = movieResult.original_language;

            movie.premiere = DateTime.ParseExact(""+movieResult.release_date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            
            movie.image_url = "https://image.tmdb.org/t/p/w185_and_h278_bestv2/"+ movieResult.poster_path;
            db.SaveChanges();
            return Ok(movie);
        }

        // GET: api/Movies
        [Route("api/movies")]
        public IQueryable<movies> GetMovies()
        {
            return db.movies;
        }

        // GET: api/Movies/5
        [Route("api/movie/{id}")]
        [ResponseType(typeof(movies))]
        public IHttpActionResult GetMovies(long id)
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