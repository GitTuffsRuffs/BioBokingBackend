using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using BioBokingMSSQLdatabase.Models;

namespace BioBokingMSSQLdatabase.Controllers
{
    [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
    public class UserController : ApiController
    {
        private bio_bookingEntities db = new bio_bookingEntities();

        //Get formData "Key" value
        private string formData(string key)
        {
            return HttpContext.Current.Request.Form[key];
        }

        //Login
        [Route("api/login")]
        [ResponseType(typeof(Dictionary<string, string>))]
        public IHttpActionResult login()
        {
            string email = formData("email");
            string password = formData("password");

            IQueryable<users> querry = db.users.Where(userFind => userFind.email == email);
            int existingUsers = querry.Count();

            if (existingUsers < 1)
            {
                return Content(HttpStatusCode.Forbidden, "Incorrect username or password");
            }

            users user = querry.First<users>();

            Dictionary<string, string> respons = new Dictionary<string, string>();
            respons.Add("id", user.id.ToString());
            respons.Add("token", user.email);
            respons.Add("email", user.email);

            return Ok(respons);
        }

        //Logut
        [Route("api/logut")]
        [ResponseType(typeof(string))]
        public IHttpActionResult logut()
        {
            return Ok("Ok");
        }

        //Register
        [Route("api/register")]
        [ResponseType(typeof(Dictionary<string, string>))]
        public IHttpActionResult register()
        {
            string email = formData("email");
            string password = formData("password");

            int existingUsers = db.users.Where(userFind => userFind.email == email).Count();

            if(existingUsers > 0)
            {
                return Content(HttpStatusCode.Conflict, "User alredy exist");
            }

            //Creat new user
            users user = new users();
            user.email = email;

            db.users.Add(user);
            db.SaveChanges();

            // TODO: Add password as authentication

            Dictionary<string, string> respons = new Dictionary<string, string>();
            respons.Add("id", user.id.ToString());
            respons.Add("token", user.email);
            respons.Add("email", user.email);

            return Ok(respons);
        }

        public static users userByToken(bio_bookingEntities dataBase, string token) {

            IQueryable<users> userQuerry = dataBase.users.Where(userFind => userFind.email == token);
            int existingUsers = userQuerry.Count();

            if (existingUsers < 1)
            {
                return null;
            }
            users user = userQuerry.First<users>();

            return user;
        }

        public static users userByEmail(bio_bookingEntities dataBase, string email)
        {
            IQueryable<users> userQuerry = dataBase.users.Where(userFind => userFind.email == email);
            int existingUsers = userQuerry.Count();

            if (existingUsers < 1)
            {
                return null;
            }
            users user = userQuerry.First<users>();

            return user;
        }
    }
}
