using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using BioBokingMSSQLdatabase.Models;

namespace BioBokingMSSQLdatabase.Controllers
{
    [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
    public class ReservationsController : ApiController
    {
        private bio_bookingEntities db = new bio_bookingEntities();

        //Get formData "Key" value
        private string formData(string key)
        {
            return HttpContext.Current.Request.Form[key];
        }

        // POST: api/reservations
        [Route("api/reservations")]
        [ResponseType(typeof(IQueryable<reservations>))]
        public IHttpActionResult reservations()
        {
            string token = formData("token");
            users user = UserController.userByToken(db, token);
            
            if (user == null)
            {
                return Content(HttpStatusCode.Forbidden, "Incorrect token");
            }

            long userId = user.id;

            IQueryable<reservations> reservationQuerry = db.reservations
                .Where(reservations => reservations.user_id == userId)
                .Include("show.movie");
            return base.Ok(reservationQuerry);
        }

        // POST: api/reservation/3
        [Route("api/reservation/{id}")]
        [ResponseType(typeof(reservations))]
        public IHttpActionResult reservation(long id)
        {
            string token = formData("token");

            reservations reservation = db.reservations.Find(id);
            if (reservation == null)
            {
                return NotFound();
            }

            users user = db.users.Find(reservation.user_id);

            if(user.email != token)
            {
                return NotFound();
            }

            return Ok(reservation);
        }

        // POST: api/book
        [Route("api/book")]
        [ResponseType(typeof(reservations))]
        public IHttpActionResult booking()
        {
            string token = formData("token");
            string email = formData("email");
            long show_id = long.Parse(formData("show_id"));
            int seats = int.Parse(formData("seats"));

            users user;

            if (token == null)
            {
                user = UserController.userByEmail(db, email);

                if (user == null)
                {
                    //Creat new user
                    user = new users();
                    user.email = email;

                    db.users.Add(user);
                    db.SaveChanges();
                }
            }
            else
            {
                user = UserController.userByToken(db, token);

                if (user == null)
                {
                    return Content( HttpStatusCode.Forbidden, "You took a wrong turn somewere" );
                }
            }

            shows show = db.shows.Find(show_id);

            if (show == null)
            {
                return Content(HttpStatusCode.BadRequest, "Show not fund");
            }

            if(DateTime.Compare(show.start_at, DateTime.Now) < 0 )
            {
                return Content(HttpStatusCode.Gone, "Show alredy started");
            }

            auditoriums auditorium = db.auditoriums.Find(show.auditorium_id);

            IQueryable<reservations> querry = db.reservations.Where(reservations => reservations.show_id == show_id);
            int seatsTaken = querry.Sum(reservations => reservations.seat_count);
            int seatsRemaining = auditorium.seats_total - seatsTaken;
            
            if (seats > seatsRemaining)
            {
                return Content(HttpStatusCode.Conflict, "Not enough seats available");
            }

            reservations reservation = new reservations();
            reservation.user_id = user.id;
            reservation.show_id = show_id;
            reservation.seat_count = seats;
            reservation.first_seat_number = seatsTaken + 1;
            reservation.reserved_at = DateTime.Now;
            
            db.reservations.Add(reservation);
            db.SaveChanges();

            return Ok(reservation);
        }












    }
}
