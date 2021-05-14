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
    public class StudentController : ApiController
    {
        protected IStudentService Service { get; private set; }
        private readonly IMapper Mapper;

        public StudentController(IStudentService service, IMapper mapper)
        {
            Service = service;
            Mapper = mapper;
        }

        [HttpGet]
        public async Task<HttpResponseMessage> FindAsync(
            [FromUri]StudentFilteringParams filteringParams,
            [FromUri]StudentSortingParams sortingParams,
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
                List<IStudent> serviceResult = await Service.FindAsync(filteringParams, sortingParams, paginationParams);

                if (serviceResult == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NoContent);
                }

                var studentsView = Mapper.Map<List<StudentViewModel>>(serviceResult);
                return Request.CreateResponse(HttpStatusCode.OK, studentsView);
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
                IStudent serviceResult = await Service.GetAsync(id);

                if (serviceResult != null)
                {
                    var studentView = Mapper.Map<StudentViewModel>(serviceResult);
                    return Request.CreateResponse(HttpStatusCode.OK, studentView);
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
        public async Task<HttpResponseMessage> PostAsync([FromBody] StudentInputModel value)
        {
            var student = Mapper.Map<IStudent>(value);
            student.StudentID = Guid.NewGuid();

            try
            {
                IStudent serviceResult = await Service.AddAsync(student);
                
                if (serviceResult == null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Adding a new record failed!");
                }
                else
                {
                    var studentView = Mapper.Map<StudentViewModel>(serviceResult);
                    return Request.CreateResponse(HttpStatusCode.OK, studentView);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPut]
        public async Task<HttpResponseMessage> PutAsync(Guid id, [FromBody] StudentInputModel value)
        {
            var student = Mapper.Map<IStudent>(value);
            student.StudentID = id;

            try
            {
                IStudent serviceResult = await Service.UpdateAsync(student);
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
                    var studentView = Mapper.Map<StudentViewModel>(serviceResult);
                    return Request.CreateResponse(HttpStatusCode.OK, studentView);
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