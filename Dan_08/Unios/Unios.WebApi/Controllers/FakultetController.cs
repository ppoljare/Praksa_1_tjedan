using AutoMapper;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Unios.Model.Common;
using Unios.Service.Common;
using Unios.WebApi.Models;

namespace Unios.WebApi.Controllers
{
    public class FakultetController : ApiController
    {
        protected IFakultetService Service { get; private set; }

        public FakultetController(IFakultetService service)
        {
            Service = service;
        }

        [HttpGet]
        public async Task<HttpResponseMessage> FindAsync()
        {
            var config = new MapperConfiguration(cfg =>
                cfg.CreateMap<IFakultet, FakultetViewModel>()
            );
            var mapper = new Mapper(config);

            try
            {
                List<IFakultet> serviceResult = await Service.FindAsync();
                var fakultetsView = mapper.Map<List<FakultetViewModel>>(serviceResult);
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
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<IFakultet, FakultetAndStudentsViewModel>();
                cfg.CreateMap<IStudent, StudentViewModel>();
            });
            var mapper = new Mapper(config);

            try
            {
                IFakultet serviceResult = await Service.GetAsync(id);

                if (serviceResult != null)
                {
                    var fakultetView = mapper.Map<FakultetAndStudentsViewModel>(serviceResult);
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
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<FakultetInputModel, IFakultet>();
                cfg.CreateMap<IFakultet, FakultetViewModel>();
            });
            var mapper = new Mapper(config);

            var fakultet = mapper.Map<IFakultet>(value);
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
                    var fakultetView = mapper.Map<FakultetViewModel>(serviceResult);
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
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<FakultetInputModel, IFakultet>();
                cfg.CreateMap<IFakultet, FakultetViewModel>();
            });
            var mapper = new Mapper(config);

            var fakultet = mapper.Map<IFakultet>(value);
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
                    var fakultetView = mapper.Map<FakultetViewModel>(serviceResult);
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