using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Unios.Model;
using Unios.Model.Common;
using Unios.Service.Common;

namespace Unios.WebApi.Controllers
{
    public class StudentController : ApiController
    {
        protected IStudentService Service { get; private set; }

        public StudentController(IStudentService service)
        {
            Service = service;
        }

        [HttpGet]
        public async Task<HttpResponseMessage> GetAsync()
        {
            try
            {
                List<IStudent> students = await Service.GetAsync();
                return Request.CreateResponse(HttpStatusCode.OK, students);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpGet]
        public async Task<HttpResponseMessage> GetAsync(Guid id)
        {
            try
            {
                IStudent student = await Service.GetAsync(id);

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
        public async Task<HttpResponseMessage> PostAsync([FromBody] StudentInput value)
        {
            try
            {
                IStudentEntity student = await Service.AddAsync(value);
                if (student == null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Adding a new record failed!");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, student);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPut]
        public async Task<HttpResponseMessage> PutAsync(Guid id, [FromBody] StudentInput value)
        {
            try
            {
                IStudentEntity student = await Service.UpdateAsync(id, value);
                if (student == null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Updating a record failed!");
                }
                else if (student.Ime == null && student.Prezime == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "The record you are trying to update does not exist!");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, student);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpDelete]
        public async Task<HttpResponseMessage> DeleteAsync(Guid id)
        {
            try
            {
                int errm = await Service.DeleteAsync(id);
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