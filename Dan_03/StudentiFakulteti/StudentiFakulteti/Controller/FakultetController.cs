using StudentiFakulteti.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StudentiFakulteti.Controller
{
    public class FakultetController : ApiController
    {
        readonly SqlConnection Connection = new SqlConnection(@"Data Source=(LocalDB)\LocalDBPraksa;Initial Catalog=UniosDB;Integrated Security=True");

        [HttpGet]
        public HttpResponseMessage Get()
        {
            string queryString =
                "SELECT *" +
                "FROM Fakultet";
            SqlDataAdapter adapter = new SqlDataAdapter(queryString, Connection);
            DataTable fakultets = new DataTable();

            Connection.Open();
            adapter.Fill(fakultets);
            Connection.Close();

            return Request.CreateResponse(HttpStatusCode.OK, fakultets);
        }

        [HttpGet]
        public HttpResponseMessage Get(int id)
        {
            Fakultet fakultet = new Fakultet();
            string idString = IdToString(id);

            string queryString =
                "SELECT Fakultet.FakultetID, Naziv, StudentID, Ime, Prezime " +
                "FROM Fakultet JOIN Student " +
                "ON (Fakultet.FakultetID = Student.FakultetID)" +
                "WHERE (Fakultet.FakultetID = '" + idString + "')";

            SqlCommand comm = new SqlCommand(queryString, Connection);
            int noOfRows = 0;

            Connection.Open();
            SqlDataReader dataReader = comm.ExecuteReader();

            while (dataReader.Read())
            {
                fakultet.FakultetID = dataReader[0].ToString();
                fakultet.Naziv = dataReader[1].ToString();

                Student student = new Student(
                    dataReader[2].ToString(),
                    dataReader[3].ToString(),
                    dataReader[4].ToString(),
                    dataReader[1].ToString()
                );

                fakultet.Append(student);
                noOfRows++;
            }

            Connection.Close();

            if (noOfRows == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, fakultet);
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody] FakultetSQL value)
        {
            string nonQueryString =
                "INSERT INTO Fakultet VALUES ('" +
                value.FakultetID + "', '" +
                value.Naziv + "')";

            SqlCommand comm = new SqlCommand(nonQueryString, Connection);

            Connection.Open();
            try
            {
                comm.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Connection.Close();
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }

            Connection.Close();

            return Request.CreateResponse(HttpStatusCode.Created, value);
        }

        [HttpPut]
        public HttpResponseMessage Put(int id, [FromBody] FakultetSQL value)
        {
            string idString = IdToString(id);

            string nonQueryString =
                "UPDATE Fakultet " +
                "SET Naziv = '" + value.Naziv + "' " +
                "WHERE FakultetID = '" + idString + "'";

            SqlCommand comm = new SqlCommand(nonQueryString, Connection);

            Connection.Open();
            try
            {
                comm.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Connection.Close();
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }

            Connection.Close();

            FakultetSQL student = new FakultetSQL(idString, value.Naziv);

            return Request.CreateResponse(HttpStatusCode.OK, student);
        }

        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            string idString = IdToString(id);

            string nonQueryString =
                "DELETE FROM Fakultet " +
                "WHERE FakultetID = '" + idString + "'";

            SqlCommand comm = new SqlCommand(nonQueryString, Connection);

            Connection.Open();
            try
            {
                comm.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Connection.Close();
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }

            Connection.Close();

            return Request.CreateResponse(HttpStatusCode.OK, "Delete successful!");
        }

        public string IdToString(int id)
        {
            if (id < 1 || id > 999)
            {
                throw new ArgumentOutOfRangeException("ID must be between 1 and 999");
            }
            if (id < 10)
            {
                return "F00" + id.ToString();
            }
            if (id < 100)
            {
                return "F0" + id.ToString();
            }
            return "F" + id.ToString();
        }
    }
}