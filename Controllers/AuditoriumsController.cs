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
    public class AuditoriumsController : ApiController
    {
        private bio_bookingEntities db = new bio_bookingEntities();

        //Get: api/auditoriums
        [Route("api/auditoriums")]
        public IQueryable<auditoriums> GetAuditoriums()
        {
            return db.auditoriums;
        }

        //Get api/aduitorium/id
        [Route("api/auditoriums/{id}")]
        [ResponseType(typeof(auditoriums))]
        public IHttpActionResult GetAuditoriums(long id)
        {
            auditoriums auditoriums = db.auditoriums.Find(id);
            if (auditoriums == null)
            {
                return NotFound();
            }

            return Ok(auditoriums);
        }
    }
}
