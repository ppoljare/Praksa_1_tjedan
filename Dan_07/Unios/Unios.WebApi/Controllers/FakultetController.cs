using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Unios.Model;
using Unios.Model.Common;
using Unios.Service;

namespace Unios.WebApi.Controllers
{
    public class FakultetController : ApiController
    {
        public static FakultetService Service = new FakultetService();

        [HttpGet]
        public async Task<HttpResponseMessage> GetAsync()
        {
            try
            {
                List<IFakultetEntity> fakultets = await Service.GetAsync();
                return Request.CreateResponse(HttpStatusCode.OK, fakultets);
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
                IFakultet fakultet = await Service.GetAsync(id);

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
        public async Task<HttpResponseMessage> PostAsync([FromBody] FakultetInput value)
        {
            try
            {
                IFakultetEntity fakultet = await Service.AddAsync(value);
                if(fakultet == null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Adding a new record failed!");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Created, fakultet);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPut]
        public async Task<HttpResponseMessage> PutAsync(Guid id, [FromBody] FakultetInput value)
        {
            try
            {
                IFakultetEntity fakultet = await Service.UpdateAsync(id, value);
                if(fakultet == null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Updating a record failed!");
                }
                else if(fakultet.Naziv == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "The record you are trying to update does not exist!");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, fakultet);
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