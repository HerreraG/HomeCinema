using AutoMapper;
using HomeCinema.Data.Infrastructure;
using HomeCinema.Data.Repositories;
using HomeCinema.Entities;
using HomeCinema.Web.Infrastructure.Core;
using HomeCinema.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HomeCinema.Web.Controllers {

    [Authorize(Roles = "Admin")]
    [RoutePrefix("api/movies")]
    public class MoviesController : ApiControllerBase {

        private readonly IEntityBaseRepository<Movie> _moviesRepository;
        private readonly IEntityBaseRepository<Rental> _rentalsRepository;
        private readonly IEntityBaseRepository<Stock> _stocksRepository;
        private readonly IEntityBaseRepository<Customer> _customersRepository;

        public MoviesController(IEntityBaseRepository<Movie> moviesRepository,
             IEntityBaseRepository<Rental> rentalsRepository, IEntityBaseRepository<Stock> stocksRepository,
             IEntityBaseRepository<Customer> customersRepository,
             IEntityBaseRepository<Error> _errorsRepository, IUnitOfWork _unitOfWork)
            : base(_errorsRepository, _unitOfWork) {

            this._moviesRepository = moviesRepository;
            this._rentalsRepository = rentalsRepository;
            this._stocksRepository = stocksRepository;
            this._customersRepository = customersRepository;

        }

        [AllowAnonymous]
        [Route("latest")]
        public HttpResponseMessage Get(HttpRequestMessage request) {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var movies = this._moviesRepository.GetAll().OrderByDescending(m => m.ReleaseDate).Take(6).ToList();
                var moviesVm = Mapper.Map<IEnumerable<Movie>,
                IEnumerable<MovieViewModel>>(movies);
                response = request.CreateResponse<IEnumerable<MovieViewModel>>(HttpStatusCode.OK, moviesVm);
                return response;
            });
        }

    }
}
