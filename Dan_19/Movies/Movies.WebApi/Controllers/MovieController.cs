using AutoMapper;
using Movies.Common.Classes;
using Movies.Common.Classes.Movie;
using Movies.Model.Common;
using Movies.Service.Common;
using Movies.WebApi.Models.Movie;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Movies.WebApi.Controllers
{
    [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
    public class MovieController : ApiController
    {
        protected IMovieService Service { get; private set; }
        private readonly IMapper Mapper;

        public MovieController(IMovieService service, IMapper mapper)
        {
            Service = service;
            Mapper = mapper;
        }

        [HttpGet]
        public async Task<HttpResponseMessage> FindAsync(
            [FromUri] MovieFilteringParams filteringParams,
            [FromUri] MovieSortingParams sortingParams,
            [FromUri] PaginationParams paginationParams
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

            List<IMovie> serviceResult;

            try
            {
                serviceResult = await Service.FindAsync(filteringParams, sortingParams, paginationParams);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }

            if (serviceResult == null)
            {
                return Request.CreateResponse(HttpStatusCode.NoContent);
            }

            var moviesView = Mapper.Map<List<MovieViewModel>>(serviceResult);
            return Request.CreateResponse(HttpStatusCode.OK, moviesView);
        }

        [HttpGet]
        public async Task<HttpResponseMessage> GetAsync(Guid id)
        {
            IMovie serviceResult;

            try
            {
                serviceResult = await Service.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }

            if (serviceResult != null)
            {
                var movieView = Mapper.Map<MovieViewModel>(serviceResult);
                return Request.CreateResponse(HttpStatusCode.OK, movieView);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        [HttpPost]
        public async Task<HttpResponseMessage> AddAsync([FromBody] MovieInputModel input)
        {
            var movie = Mapper.Map<IMovie>(input);
            Service.InitMovie(movie);

            IMovie serviceResult;

            try
            {
                serviceResult = await Service.AddAsync(movie);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }

            if (serviceResult == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Adding a new record failed!");
            }
            else if (serviceResult.Found)
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden, "The record you are trying to add already exists!");
            }
            else
            {
                var movieView = Mapper.Map<MovieViewModel>(serviceResult);
                return Request.CreateResponse(HttpStatusCode.OK, movieView);
            }
        }

        [HttpPut]
        public async Task<HttpResponseMessage> UpdateAsync(Guid id, [FromBody] MovieInputModel input)
        {
            var movie = Mapper.Map<IMovie>(input);
            movie.MovieId = id;

            IMovie serviceResult;

            try
            {
                serviceResult = await Service.UpdateAsync(movie);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }

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
                var movieView = Mapper.Map<MovieViewModel>(serviceResult);
                return Request.CreateResponse(HttpStatusCode.OK, movieView);
            }
        }

        [HttpDelete]
        public async Task<HttpResponseMessage> DeleteAsync(Guid id)
        {
            bool deleteSuccessful;
            try
            {
                deleteSuccessful = await Service.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }

            if (deleteSuccessful)
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Deleting a record failed!");
            }
        }
    }
}