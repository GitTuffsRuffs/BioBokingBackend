using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using System.Data.SqlClient;
using System.Web.Http.Cors;

namespace BioBokingMSSQLdatabase.Controllers
{
    [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
    abstract public class BaseController : ApiController
    {
        public SqlConnection connection()
        {
            var connectionString = "data source=LAPTOP-BM02332H\\SQLEXPRESS;initial catalog=bio_booking;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
            return new SqlConnection(connectionString);
        }
    }
}
