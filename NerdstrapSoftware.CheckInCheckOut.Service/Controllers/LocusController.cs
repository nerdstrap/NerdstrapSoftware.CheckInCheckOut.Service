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
    [RoutePrefix("api/locus")]
    public class LocusController : ApiController
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
        public GetLocusResponse GetBySearchQuery(string searchQuery)
        {
            ApiServices.Log.Info("GetBySearchQuery");

            IEnumerable<string> headerValues = Request.Headers.GetValues("U-CICO-MNEMONIC");
            var userId = headerValues.FirstOrDefault();

            var identityRepository = new IdentityRepository();
            var locusRepository = new LocusRepository();

            GetLocusResponse response = new GetLocusResponse();
            response.Identity = AutoMapper.Mapper.Map<Contracts.Identity>(identityRepository.GetById(mobileServiceContext, userId));
            response.LocusList = AutoMapper.Mapper.Map<IEnumerable<Contracts.Locus>>(locusRepository.GetByLocusName(mobileServiceContext, searchQuery));
            return response;
        }

        [HttpGet()]
        [Route("nearby")]
        public GetLocusResponse GetNearby(string latitude, string longitude, string radius)
        {
            ApiServices.Log.Info("GetNearby");

            IEnumerable<string> headerValues = Request.Headers.GetValues("U-CICO-MNEMONIC");
            var userId = headerValues.FirstOrDefault();

            var identityRepository = new IdentityRepository();
            var locusRepository = new LocusRepository();

            GetLocusResponse response = new GetLocusResponse();
            response.Identity = AutoMapper.Mapper.Map<Contracts.Identity>(identityRepository.GetById(mobileServiceContext, userId));
            response.LocusList = AutoMapper.Mapper.Map<IEnumerable<Contracts.Locus>>(locusRepository.GetByPosition(mobileServiceContext, latitude, longitude, radius));
            return response;
        }

        [HttpGet()]
        public GetLocusResponse GetById(string locusId)
        {
            ApiServices.Log.Info("GetById");

            IEnumerable<string> headerValues = Request.Headers.GetValues("U-CICO-MNEMONIC");
            var userId = headerValues.FirstOrDefault();

            var identityRepository = new IdentityRepository();
            var locusRepository = new LocusRepository();

            GetLocusResponse response = new GetLocusResponse();
            response.Identity = AutoMapper.Mapper.Map<Contracts.Identity>(identityRepository.GetById(mobileServiceContext, userId));
            var locus = locusRepository.GetById(mobileServiceContext, locusId);
            if (locus != null) {
                var locusList = new List<Models.Locus> { locus };
                response.LocusList = AutoMapper.Mapper.Map<IEnumerable<Contracts.Locus>>(locusList);
            }
            
            return response;
        }
    }
}
