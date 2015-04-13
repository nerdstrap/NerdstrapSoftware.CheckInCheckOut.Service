using NerdstrapSoftware.CheckInCheckOut.Service.Contracts;
using NerdstrapSoftware.CheckInCheckOut.Service.Models;
using NerdstrapSoftware.CheckInCheckOut.Service.Repositories;
using Microsoft.WindowsAzure.Mobile.Service;
using Microsoft.WindowsAzure.Mobile.Service.Security;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace NerdstrapSoftware.CheckInCheckOut.Service.Controllers
{
    [RoutePrefix("api/identity")]
    public class IdentityController : ApiController
    {
        public ApiServices ApiServices { get; set; }
        private MobileServiceContext mobileServiceContext;

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            mobileServiceContext = new MobileServiceContext();
        }

        [HttpGet()]
        [Route("search")]
        [AuthorizeLevel(AuthorizationLevel.Application)]
        public GetIdentityResponse GetBySearchQuery(string searchQuery)
        {
            ApiServices.Log.Info("GetBySearchQuery");

            IEnumerable<string> headerValues = Request.Headers.GetValues("U-CICO-MNEMONIC");
            var userId = headerValues.FirstOrDefault();

            var identityRepository = new IdentityRepository();

            GetIdentityResponse response = new GetIdentityResponse();
            response.Identity = AutoMapper.Mapper.Map<Contracts.Identity>(identityRepository.GetById(mobileServiceContext, userId));
            response.IdentityList = AutoMapper.Mapper.Map<IEnumerable<Contracts.Identity>>(identityRepository.GetByIdentityName(mobileServiceContext, searchQuery));
            return response;
        }

        [HttpGet()]
        public GetIdentityResponse GetById(string identityId)
        {
            ApiServices.Log.Info("GetById");

            IEnumerable<string> headerValues = Request.Headers.GetValues("U-CICO-MNEMONIC");
            var userId = headerValues.FirstOrDefault();

            var identityRepository = new IdentityRepository();

            GetIdentityResponse response = new GetIdentityResponse();
            response.Identity = AutoMapper.Mapper.Map<Contracts.Identity>(identityRepository.GetById(mobileServiceContext, userId));
            var identity = identityRepository.GetById(mobileServiceContext, identityId);
            if (identity != null) {
                var identityList = new List<Models.Identity> { identity };
                response.IdentityList = AutoMapper.Mapper.Map<IEnumerable<Contracts.Identity>>(identityList);
            }
            
            return response;
        }
    }
}
