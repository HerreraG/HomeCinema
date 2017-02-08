using AutoMapper;
using HomeCinema.Data.Infrastructure;
using HomeCinema.Data.Repositories;
using HomeCinema.Entities;
using HomeCinema.Web.Infrastructure.Core;
using HomeCinema.Web.Infrastructure.Extensions;
using HomeCinema.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
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
        [Route("{page:int=0}/{pageSize=3}/{filter=}")]
        public HttpResponseMessage Get(HttpRequestMessage request, int? page, int? pageSize, string filter = null) {

            int currentPage = page.Value;
            int currentPageSize = pageSize.Value;

            return CreateHttpResponse(request, () => {
                HttpResponseMessage response = null;
                List<Movie> movies = null;
                int totalMovies = new int();

                if (!string.IsNullOrEmpty(filter)) {
                    movies = this._moviesRepository.GetAll()
                                    .OrderBy(m => m.Id)
                                    .Where(m => m.Title.ToLower().Contains(filter.ToLower().Trim()))
                                    .ToList();
                } else {
                    movies = this._moviesRepository.GetAll().ToList();
                }

                totalMovies = movies.Count();
                movies = movies.Skip(currentPage * currentPage)
                                .Take(currentPageSize)
                                .ToList();

                IEnumerable<MovieViewModel> moviesVm = Mapper.Map<IEnumerable<Movie>, IEnumerable<MovieViewModel>>(movies);


                PaginationSet<MovieViewModel> pagedSet = new PaginationSet<MovieViewModel>() {
                    Page = currentPage,
                    TotalCount = totalMovies,
                    TotalPages = (int)Math.Ceiling((decimal)totalMovies / currentPageSize),
                    Items = moviesVm
                };

                response = request.CreateResponse<PaginationSet<MovieViewModel>>(HttpStatusCode.OK, pagedSet);

                return response;
            });
        }

        [Route("details/{id:int}")]
        public HttpResponseMessage Get(HttpRequestMessage request, int id) {
            return CreateHttpResponse(request, () => {
                HttpResponseMessage response = null;

                var movie = this._moviesRepository.GetSingle(id);
                MovieViewModel movieVm = Mapper.Map<Movie, MovieViewModel>(movie);

                response = request.CreateResponse<MovieViewModel>(HttpStatusCode.OK, movieVm);
                return response;
            });
        }

        [AllowAnonymous]
        [Route("latest")]
        public HttpResponseMessage Get(HttpRequestMessage request) {
            return CreateHttpResponse(request, () => {
                HttpResponseMessage response = null;
                var movies = this._moviesRepository.GetAll().OrderByDescending(m => m.ReleaseDate).Take(6).ToList();
                var moviesVm = Mapper.Map<IEnumerable<Movie>,
                IEnumerable<MovieViewModel>>(movies);
                response = request.CreateResponse<IEnumerable<MovieViewModel>>(HttpStatusCode.OK, moviesVm);
                return response;
            });
        }


        [HttpPost]
        [Route("update")]
        public HttpResponseMessage Update(HttpRequestMessage request, MovieViewModel movie) {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (!ModelState.IsValid) {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                } else {
                    var movieDb = _moviesRepository.GetSingle(movie.Id);
                    if (movieDb == null)
                        response = request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid movie.");
                    else {
                        movieDb.UpdateMovie(movie);
                        movie.Image = movieDb.Image;
                        _moviesRepository.Edit(movieDb);

                        _unitOfWork.Commit();
                        response = request.CreateResponse<MovieViewModel>(HttpStatusCode.OK, movie);
                    }
                }

                return response;
            });
        }

        [MimeMultipart]
        [Route("images/upload")]
        public HttpResponseMessage Post(HttpRequestMessage request, int movieId) {
            return CreateHttpResponse(request, () => {
                HttpResponseMessage response = null;

                var movieOld = this._moviesRepository.GetSingle(movieId);

                if (movieOld == null) {
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid Movie");
                } else {
                    var uploadPath = HttpContext.Current.Server.MapPath("~/Content/images/movies");
                    var multipartFormDataStreamProvider = new UploadMultipartFormProvider(uploadPath);

                    //read the MIME multipar asynchronusly
                    Request.Content.ReadAsMultipartAsync(multipartFormDataStreamProvider);

                    string _localFileName = multipartFormDataStreamProvider
                            .FileData.Select(multiPartData => multiPartData.LocalFileName).FirstOrDefault();

                    // Create response
                    FileUploadResult fileUploadResult = new FileUploadResult {
                        LocalFilePath = _localFileName,

                        FileName = Path.GetFileName(_localFileName),

                        FileLength = new FileInfo(_localFileName).Length
                    };

                    // update database
                    movieOld.Image = fileUploadResult.FileName;
                    _moviesRepository.Edit(movieOld);

                    _unitOfWork.Commit();

                    response = request.CreateResponse(HttpStatusCode.OK, fileUploadResult);
                }

                return response;
            });
        }
    }
}
