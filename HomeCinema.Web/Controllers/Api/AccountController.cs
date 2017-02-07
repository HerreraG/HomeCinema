using HomeCinema.Data.Infrastructure;
using HomeCinema.Data.Repositories;
using HomeCinema.Entities;
using HomeCinema.Services;
using HomeCinema.Services.Abstract;
using HomeCinema.Web.Infrastructure.Core;
using HomeCinema.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HomeCinema.Web.Controllers.Api {

    [Authorize(Roles = "Admin")]
    [RoutePrefix("api/account")]
    public class AccountController : ApiControllerBase {

        private readonly IMembershipService _membershipService;

        public AccountController(IMembershipService membershipService,
            IEntityBaseRepository<Error> _errorsRepository, IUnitOfWork _unitOfWork)
            : base(_errorsRepository, _unitOfWork) {
            _membershipService = membershipService;
        }

        [AllowAnonymous]
        [Route("authenticate")]
        [HttpPost]
        public HttpResponseMessage Login(HttpRequestMessage request, LoginViewModel user) {

            return CreateHttpResponse(request, () => {

                HttpResponseMessage response = null;

                if (ModelState.IsValid) {
                    MembershipContext _userContext = this._membershipService.ValidateUser(user.Username, user.Password);

                    if (_userContext != null) {
                        response = request.CreateResponse(HttpStatusCode.OK, new { success = true });
                    } else {
                        response = request.CreateResponse(HttpStatusCode.OK, new { success = false });
                    }
                } else {
                    response = request.CreateResponse(HttpStatusCode.OK, new { success = false });
                }
                return response;
            });
        }


        [AllowAnonymous]
        public HttpResponseMessage Get(HttpRequestMessage request) {
            return CreateHttpResponse(request, () => {

                HttpResponseMessage response = null;
                response = request.CreateResponse(HttpStatusCode.OK, new { success = false });
                return response;
            });
        }


        [AllowAnonymous]
        [Route("register")]
        [HttpPost]
        public HttpResponseMessage Register(HttpRequestMessage request, RegistrationViewModel user) {
            return CreateHttpResponse(request, () => {

                HttpResponseMessage response = null;

                if (!ModelState.IsValid) {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, new { success = false });
                } else {
                    User _user = this._membershipService.CreateUser(user.Username, user.Email, user.Password,
                                                                    new int[] { 1 });

                    if (_user != null) {
                        response = request.CreateResponse(HttpStatusCode.OK, new { success = true });
                    } else {
                        response = request.CreateResponse(HttpStatusCode.OK, new { success = false });
                    }
                }

                return response;
            });
        }
    }
}
