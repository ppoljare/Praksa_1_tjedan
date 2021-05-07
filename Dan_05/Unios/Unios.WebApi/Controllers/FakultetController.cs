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
    public class FakultetController : ApiController
    {
        public static FakultetService Service = new FakultetService();

        [HttpGet]
        public async Task<HttpResponseMessage> Get()
        {
            try
            {
                List<FakultetEntity> fakultets = await Service.Get();
                return Request.CreateResponse(HttpStatusCode.OK, fakultets);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpGet]
        public async Task<HttpResponseMessage> Get(Guid id)
        {
            try
            {
                Fakultet fakultet = await Service.Get(id);

                if (fakultet != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, fakultet);
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
        public async Task<HttpResponseMessage> Post([FromBody] FakultetInput value)
        {
            try
            {
                int errm = await Service.Add(value);
                switch (errm)
                {
                    case 0:
                        return Request.CreateResponse(HttpStatusCode.Created, value);
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
        public async Task<HttpResponseMessage> Put(Guid id, [FromBody] FakultetInput value)
        {
            try
            {
                int errm = await Service.Update(id, value);
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
        public async Task<HttpResponseMessage> Delete(Guid id)
        {
            try
            {
                int errm = await Service.Delete(id);
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