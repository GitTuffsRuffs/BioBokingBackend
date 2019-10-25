using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
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
    public class CinemaController : BaseController
    {
        private bio_bookingEntities db = new bio_bookingEntities();

        [Route("api/cinemas")]
        public IEnumerable<cinemas> GetCinemas()
        {
            return db.cinemas;
        }

        // GET api/cinema/1
        [Route("api/cinema/{id}")]
        [ResponseType(typeof(cinemas))]
        public IHttpActionResult GetCinema(int id)
        {
            cinemas cinema = db.cinemas.Find(id);
            if (cinema == null)
            {
                return NotFound();
            }

            return Ok(cinema);
        }

    }
}
