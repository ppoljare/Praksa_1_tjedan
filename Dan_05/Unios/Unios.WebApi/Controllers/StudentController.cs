﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Unios.Model;
using Unios.Service;

namespace Unios.WebApi.Controllers
{
    public class StudentController : ApiController
    {
        public static StudentService Service = new StudentService();

        [HttpGet]
        public async Task<HttpResponseMessage> GetAsync()
        {
            try
            {
                List<Student> students = await Service.GetAsync();
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
                Student student = await Service.GetAsync(id);

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
                int errm = await Service.AddAsync(value);
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
        public async Task<HttpResponseMessage> PutAsync(Guid id, [FromBody] StudentInput value)
        {
            try
            {
                int errm = await Service.UpdateAsync(id, value);
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