using MovieAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MovieAPI.Controllers
{
    public class ValuesController : ApiController
    {
        static readonly IMovieRepository repository = new MovieRepository();

        [HttpGet]
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK, repository.GetAll());
        }

        [HttpGet]
        public HttpResponseMessage Get(int id)
        {
            Movie movie = repository.Get(id);
            if (movie == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return Request.CreateResponse(HttpStatusCode.OK, repository.Get(id));
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody] MovieJson value)
        {
            Movie movie = repository.Add(value);
            return Request.CreateResponse(HttpStatusCode.Created, movie);
        }

        [HttpPut]
        public HttpResponseMessage Put(int id, [FromBody] MovieJson value)
        {
            if (!repository.Update(id, value))
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return Request.CreateResponse(HttpStatusCode.OK, value);
        }

        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            Movie movie = repository.Get(id);
            if (movie == null)
            {
                return Request.CreateResponse(HttpStatusCode.NoContent);
            }
            repository.Remove(id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
