﻿using AutoMapper;
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
    public class StudentController : ApiController
    {
        protected IStudentService Service { get; private set; }

        public StudentController(IStudentService service)
        {
            Service = service;
        }

        [HttpGet]
        public async Task<HttpResponseMessage> FindAsync()
        {
            var config = new MapperConfiguration(cfg =>
                cfg.CreateMap<IStudent, StudentViewModel>()
            );
            var mapper = new Mapper(config);

            try
            {
                List<IStudent> serviceResult = await Service.FindAsync();
                var studentsView = mapper.Map<List<StudentViewModel>>(serviceResult);
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
            var config = new MapperConfiguration(cfg =>
                cfg.CreateMap<IStudent, StudentViewModel>()
            );
            var mapper = new Mapper(config);

            try
            {
                IStudent serviceResult = await Service.GetAsync(id);

                if (serviceResult != null)
                {
                    var studentView = mapper.Map<StudentViewModel>(serviceResult);
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
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<StudentInputModel, IStudent>();
                cfg.CreateMap<IStudent, StudentViewModel>();
            });
            var mapper = new Mapper(config);

            var student = mapper.Map<IStudent>(value);
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
                    var studentView = mapper.Map<StudentViewModel>(serviceResult);
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
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<StudentInputModel, IStudent>();
                cfg.CreateMap<IStudent, StudentViewModel>();
            });
            var mapper = new Mapper(config);

            var student = mapper.Map<IStudent>(value);
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
                    var studentView = mapper.Map<StudentViewModel>(serviceResult);
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