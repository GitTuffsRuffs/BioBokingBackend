using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BioBokingMSSQLdatabase.Models;

namespace BioBokingMSSQLdatabase.Controllers
{
    public class CinemaController : BaseController
    {
        // GET api/valuess
        public IEnumerable<cinemas> Get()
        {
            SqlConnection connection = this.connection();

            var query = "SELECT [id], [name], [location] FROM [cinemas]";
            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            List<cinemas> list = new List<cinemas>();
            while (reader.Read())
            {
                cinemas cinema = new cinemas();
                cinema.id = int.Parse(reader[0].ToString());
                cinema.name = reader[1].ToString();
                cinema.location = reader[2].ToString();
                list.Add(cinema);
            }

            reader.Close();

            return list;
        }

        // GET api/cinema/1
        public cinemas Get(int id)
        {
            SqlConnection connection = this.connection();

            var query = "SELECT [name], [location] FROM [cinemas] WHERE id = @id";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            cinemas cinema = null;
            if (reader.Read())
            {
                cinema = new cinemas();
                cinema.id = id;
                cinema.name = reader[0].ToString();
                cinema.location = reader[1].ToString();
            }

            reader.Close();

            return cinema;
        }
    }
}
