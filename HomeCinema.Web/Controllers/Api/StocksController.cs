using AutoMapper;
using HomeCinema.Data.Extensions;
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

namespace HomeCinema.Web.Controllers.Api {
    [Authorize(Roles="Admin")]
    [RoutePrefix("api/stocks")]
    public class StocksController : ApiControllerBase {

        private readonly IEntityBaseRepository<Stock> _stocksRepository;

        public StocksController(IEntityBaseRepository<Stock> stocksRepository, 
                                IEntityBaseRepository<Error> _errorsRepository,
                                IUnitOfWork _unitOfWork)
                                : base(_errorsRepository, _unitOfWork) {

            this._stocksRepository = stocksRepository;
        }

        [Route("movie/{id:int}")]
        public HttpResponseMessage Get(HttpRequestMessage request, int id) {

            IEnumerable<Stock> stocks = null;

            return CreateHttpResponse(request, () => {
                HttpResponseMessage response = null;

                stocks = this._stocksRepository.GetAvailableItems(id);
                IEnumerable<StockViewModel> stocksVm = Mapper.Map<IEnumerable<Stock>, IEnumerable<StockViewModel>>(stocks);

                response = request.CreateResponse<IEnumerable<StockViewModel>>(HttpStatusCode.OK, stocksVm);
                return response;
            });
        }
    }
}
