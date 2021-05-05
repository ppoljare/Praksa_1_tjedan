using StudentiFakulteti.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StudentiFakulteti.Controller
{
    public class StudentController : ApiController
    {
        readonly SqlConnection Connection = new SqlConnection(@"Data Source=(LocalDB)\LocalDBPraksa;Initial Catalog=UniosDB;Integrated Security=True");

        [HttpGet]
        public HttpResponseMessage Get()
        {
            List<Student> students = new List<Student>();

            string queryString =
                "SELECT StudentID, Ime, Prezime, Naziv " +
                "FROM Student JOIN Fakultet " +
                "ON (Student.FakultetID = Fakultet.FakultetID)";

            SqlCommand comm = new SqlCommand(queryString, Connection);

            Connection.Open();
            SqlDataReader dataReader = comm.ExecuteReader();

            while (dataReader.Read())
            {
                Student student = new Student(
                    dataReader[0].ToString(),
                    dataReader[1].ToString(),
                    dataReader[2].ToString(),
                    dataReader[3].ToString()
                );

                students.Add(student);
            }

            Connection.Close();

            return Request.CreateResponse(HttpStatusCode.OK, students);
        }

        [HttpGet]
        public HttpResponseMessage Get(int id)
        {
            List<Student> students = new List<Student>();

            string queryString =
                "SELECT StudentID, Ime, Prezime, Naziv " +
                "FROM Student JOIN Fakultet " +
                "ON (Student.FakultetID = Fakultet.FakultetID) " +
                "WHERE (StudentID = '" + IdToString(id) + "')";

            SqlCommand comm = new SqlCommand(queryString, Connection);

            Connection.Open();
            SqlDataReader dataReader = comm.ExecuteReader();

            if (dataReader.Read())
            {
                Student student = new Student(
                    dataReader[0].ToString(),
                    dataReader[1].ToString(),
                    dataReader[2].ToString(),
                    dataReader[3].ToString()
                );

                students.Add(student);
            }
            else
            {
                Connection.Close();
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            Connection.Close();

            return Request.CreateResponse(HttpStatusCode.OK, students);
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody] StudentSQL value)
        {
            string nonQueryString =
                "INSERT INTO Student VALUES ('" +
                value.StudentID + "', '" +
                value.FakultetID + "', '" +
                value.Ime + "', '" +
                value.Prezime + "')";

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
        public HttpResponseMessage Put(int id, [FromBody] StudentSQL value)
        {
            string idString = IdToString(id);

            string nonQueryString =
                "UPDATE Student " +
                "SET FakultetID = '" + value.FakultetID + "', " +
                "Ime = '" + value.Ime + "', " +
                "Prezime = '" + value.Prezime + "' " +
                "WHERE StudentID = '" + idString + "'";

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

            StudentSQL student = new StudentSQL(idString, value.FakultetID, value.Ime, value.Prezime);

            return Request.CreateResponse(HttpStatusCode.OK, student);
        }

        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            string idString = IdToString(id);

            string nonQueryString =
                "DELETE FROM Student " +
                "WHERE StudentID = '" + idString + "'";

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
                return "S00" + id.ToString();
            }
            if (id < 100)
            {
                return "S0" + id.ToString();
            }
            return "S" + id.ToString();
        }
    }
}