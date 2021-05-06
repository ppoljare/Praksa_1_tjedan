using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Unios.Model;
using Unios.Service;

namespace Unios.WebApi.Controllers
{
    public class StudentController : ApiController
    {
        public static StudentService Service = new StudentService();

        [HttpGet]
        public HttpResponseMessage Get()
        {
            try
            {
                List<Student> students = Service.Get();
                return Request.CreateResponse(HttpStatusCode.OK, students);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpGet]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                Student student = Service.Get(id);

                if (student != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, student);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody] StudentSQL value)
        {
            try
            {
                int errm = Service.Add(value);
                switch (errm)
                {
                    case 0:
                        return Request.CreateResponse(HttpStatusCode.OK, value);
                    case -403:
                        return Request.CreateResponse(HttpStatusCode.Forbidden, "The record with the same ID already exists!");
                    default:
                        return Request.CreateResponse(HttpStatusCode.BadRequest, "Adding a new record failed!");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPut]
        public HttpResponseMessage Put(int id, [FromBody] StudentSQL value)
        {
            try
            {
                int errm = Service.Update(id, value);
                switch (errm)
                {
                    case 0:
                        return Request.CreateResponse(HttpStatusCode.OK, value);
                    case -404:
                        return Request.CreateResponse(HttpStatusCode.NotFound, "The record you are trying to update does not exist!");
                    default:
                        return Request.CreateResponse(HttpStatusCode.BadRequest, "Updating a record failed!");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                int errm = Service.Delete(id);
                switch (errm)
                {
                    case 0:
                        return Request.CreateResponse(HttpStatusCode.OK);
                    case -204:
                        return Request.CreateResponse(HttpStatusCode.NoContent);
                    default:
                        return Request.CreateResponse(HttpStatusCode.BadRequest, "Deleting a record failed!");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}