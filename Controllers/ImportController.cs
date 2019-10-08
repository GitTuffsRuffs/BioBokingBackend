﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using BioBokingMSSQLdatabase.Models;

namespace BioBokingMSSQLdatabase.Controllers
{
    [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
    public class ImportController : ApiController
    {
        public SqlConnection connection()
        {
            var connectionString = "data source=LAPTOP-BM02332H\\SQLEXPRESS;initial catalog=bio_booking;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
            return new SqlConnection(connectionString);
        }

        // GET api/import
        public int Get(string id)
        {
            SqlConnection connection = this.connection();

            var query = "SELECT * FROM [movies] WHERE imdb_tag = @id";
            
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            //movies movies = null;

            if (reader.Read())
            {
                return 1;
            }
            reader.Close();
            return 0;
        }





    }
}
