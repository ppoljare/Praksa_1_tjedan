using AutoMapper;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Unios.Common;
using Unios.Model.Common;
using Unios.Service.Common;
using Unios.WebApi.Models;

namespace Unios.WebApi.Controllers
{
    public class FakultetController : ApiController
    {
        protected IFakultetService Service { get; private set; }
        private readonly IMapper Mapper;

        public FakultetController(IFakultetService service, IMapper mapper)
        {
            Service = service;
            Mapper = mapper;
        }

        [HttpGet]
        public async Task<HttpResponseMessage> FindAsync(
            [FromUri]FakultetFilteringParams filteringParams,
            [FromUri]FakultetSortingParams sortingParams,
            [FromUri]PaginationParams paginationParams
        )
        {
            if (!paginationParams.IsValid())
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid pagination parameters!");
            }

            if (!sortingParams.IsValid())
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid sorting parameters!");
            }

            try
            {
                List<IFakultet> serviceResult = await Service.FindAsync(filteringParams, sortingParams, paginationParams);

                if (serviceResult == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NoContent);
                }

                var fakultetsView = Mapper.Map<List<FakultetViewModel>>(serviceResult);
                return Request.CreateResponse(HttpStatusCode.OK, fakultetsView);
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
                IFakultet serviceResult = await Service.GetAsync(id);

                if (serviceResult != null)
                {
                    var fakultetView = Mapper.Map<FakultetAndStudentsViewModel>(serviceResult);
                    return Request.CreateResponse(HttpStatusCode.OK, fakultetView);
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
        public async Task<HttpResponseMessage> PostAsync([FromBody] FakultetInputModel value)
        {
            var fakultet = Mapper.Map<IFakultet>(value);
            fakultet.FakultetID = Guid.NewGuid();

            try
            {
                IFakultet serviceResult = await Service.AddAsync(fakultet);
                if (serviceResult == null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Adding a new record failed!");
                }
                else
                {
                    var fakultetView = Mapper.Map<FakultetViewModel>(serviceResult);
                    return Request.CreateResponse(HttpStatusCode.Created, fakultetView);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPut]
        public async Task<HttpResponseMessage> PutAsync(Guid id, [FromBody] FakultetInputModel value)
        {
            var fakultet = Mapper.Map<IFakultet>(value);
            fakultet.FakultetID = id;

            try
            {
                IFakultet serviceResult = await Service.UpdateAsync(fakultet);
                if (serviceResult == null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Updating a record failed!");
                }
                else if (!serviceResult.Found)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "The record you are trying to update does not exist!");
                }
                else
                {
                    var fakultetView = Mapper.Map<FakultetViewModel>(serviceResult);
                    return Request.CreateResponse(HttpStatusCode.OK, fakultetView);
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