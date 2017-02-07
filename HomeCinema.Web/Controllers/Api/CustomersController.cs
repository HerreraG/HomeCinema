using AutoMapper;
using HomeCinema.Data.Extensions;
using HomeCinema.Data.Infrastructure;
using HomeCinema.Data.Repositories;
using HomeCinema.Entities;
using HomeCinema.Web.Infrastructure.Core;
using HomeCinema.Web.Infrastructure.Extensions;
using HomeCinema.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HomeCinema.Web.Controllers.Api {

    [Authorize(Roles = "Admin")]
    [RoutePrefix("api/customers")]
    public class CustomersController : ApiControllerBase {

        private readonly IEntityBaseRepository<Customer> _customersRepository;

        public CustomersController(IEntityBaseRepository<Customer> customersRepository,
                                   IEntityBaseRepository<Error> _errorsRepository, IUnitOfWork _unitOfWork)
                                   : base(_errorsRepository, _unitOfWork) {

            this._customersRepository = customersRepository;
        }

        [HttpGet]
        [Route("search/{page:int=0}/{pageSize=4}/{filter?}")]
        public HttpResponseMessage Search(HttpRequestMessage request, int? page, int? pageSize, string filter = null) {

            int currentPage = page.Value;
            int currentPageSize = pageSize.Value;

            return CreateHttpResponse(request, () => {

                HttpResponseMessage response = null;
                List<Customer> customers = null;
                int totalMovies = new int();

                if (!string.IsNullOrEmpty(filter)) {
                    filter = filter.Trim().ToLower();

                    customers = this._customersRepository.GetAll()
                                    .OrderBy(c => c.Id)
                                    .Where(c => c.LastName.ToLower().Contains(filter) ||
                                        c.IdentityCard.ToLower().Contains(filter) ||
                                        c.FirstName.ToLower().Contains(filter))
                                    .ToList();
                } else {
                    customers = this._customersRepository.GetAll().ToList();
                }

                totalMovies = customers.Count();
                customers = customers.Skip(currentPage * currentPageSize)
                        .Take(currentPageSize)
                        .ToList();

                var customersVm = Mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerViewModel>>(customers);

                PaginationSet<CustomerViewModel> pagedSet = new PaginationSet<CustomerViewModel>() {
                    Page = currentPage,
                    TotalCount = totalMovies,
                    TotalPages = (int)Math.Ceiling((decimal)totalMovies / currentPageSize),
                    Items = customersVm
                };

                response = request.CreateResponse<PaginationSet<CustomerViewModel>>(HttpStatusCode.OK, pagedSet);
                return response;
            });
        }

        public HttpResponseMessage Register(HttpRequestMessage request, CustomerViewModel customerViewModel) {
            return CreateHttpResponse(request, () => {
                HttpResponseMessage response = null;

                if (!ModelState.IsValid) {
                    response = request.CreateResponse(HttpStatusCode.BadRequest,
                          ModelState.Keys.SelectMany(k => ModelState[k].Errors)
                          .Select(m => m.ErrorMessage).ToArray());
                } else {
                    if (this._customersRepository.UserExists(customerViewModel.Email, customerViewModel.IdentityCard)) {
                        ModelState.AddModelError("Invalid user", "Email or Identity Card number already exists");
                        response = request.CreateResponse(HttpStatusCode.BadRequest,
                        ModelState.Keys.SelectMany(k => ModelState[k].Errors)
                              .Select(m => m.ErrorMessage).ToArray());
                    } else {
                        var newCustomer = new Customer();
                        newCustomer.UpdateCustomer(customerViewModel);
                        this._customersRepository.Add(newCustomer);

                        _unitOfWork.Commit();

                        customerViewModel = Mapper.Map<Customer, CustomerViewModel>(newCustomer);
                        response = request.CreateResponse<CustomerViewModel>(HttpStatusCode.Created, customerViewModel);
                    }
                }

                return response;
            });
        }

        [HttpPost]
        [Route("update")]
        public HttpResponseMessage Update(HttpRequestMessage request, CustomerViewModel customerViewModel) {

            return CreateHttpResponse(request, () => {


                HttpResponseMessage response = null;

                if (!ModelState.IsValid) {
                    response = request.CreateResponse(HttpStatusCode.BadRequest,
                                    ModelState.Keys.SelectMany(k => ModelState[k].Errors)
                                    .Select(m => m.ErrorMessage).ToArray());
                } else {
                    var customer = _customersRepository.GetSingle(customerViewModel.Id);
                    customer.UpdateCustomer(customerViewModel);

                    _unitOfWork.Commit();

                    response = request.CreateResponse(HttpStatusCode.OK);
                }

                return response;
            });
        }
    }
}
