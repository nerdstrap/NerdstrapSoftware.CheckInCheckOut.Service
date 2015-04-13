using NerdstrapSoftware.CheckInCheckOut.Service.Contracts;
using NerdstrapSoftware.CheckInCheckOut.Service.Models;
using NerdstrapSoftware.CheckInCheckOut.Service.Repositories;
using Microsoft.WindowsAzure.Mobile.Service;
using Microsoft.WindowsAzure.Mobile.Service.Security;
using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace NerdstrapSoftware.CheckInCheckOut.Service.Controllers
{
    [RoutePrefix("api/entryLog")]
    public class EntryLogController : ApiController
    {
        public ApiServices ApiServices { get; set; }
        private MobileServiceContext mobileServiceContext;

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            mobileServiceContext = new MobileServiceContext();
        }

        [HttpGet()]
        [Route("open/locus")]
        public GetEntryLogResponse GetOpenByLocusId(string locusId)
        {
            ApiServices.Log.Info("GetOpen");

            IEnumerable<string> headerValues = Request.Headers.GetValues("U-CICO-MNEMONIC");
            var userIdentityId = headerValues.FirstOrDefault();

            var identityRepository = new IdentityRepository();
            var entryLogRepository = new EntryLogRepository();

            GetEntryLogResponse response = new GetEntryLogResponse();
            response.Identity = AutoMapper.Mapper.Map<Contracts.Identity>(identityRepository.GetById(mobileServiceContext, userIdentityId));
            response.EntryLogList = AutoMapper.Mapper.Map<IEnumerable<Contracts.EntryLog>>(entryLogRepository.GetOpenByLocusId(mobileServiceContext, locusId));

            return response;
        }

        [HttpGet()]
        [Route("open/identity")]
        public GetEntryLogResponse GetOpenByIdentityId(string identityId)
        {
            ApiServices.Log.Info("GetOpen");

            IEnumerable<string> headerValues = Request.Headers.GetValues("U-CICO-MNEMONIC");
            var userIdentityId = headerValues.FirstOrDefault();

            var identityRepository = new IdentityRepository();
            var entryLogRepository = new EntryLogRepository();

            GetEntryLogResponse response = new GetEntryLogResponse();
            response.Identity = AutoMapper.Mapper.Map<Contracts.Identity>(identityRepository.GetById(mobileServiceContext, userIdentityId));
            response.EntryLogList = AutoMapper.Mapper.Map<IEnumerable<Contracts.EntryLog>>(entryLogRepository.GetOpenByIdentityId(mobileServiceContext, identityId));

            return response;
        }

        [HttpGet()]
        [Route("nearby")]
        public GetEntryLogResponse GetNearby(string latitude, string longitude, string radius)
        {
            ApiServices.Log.Info("GetOpenNearby");

            IEnumerable<string> headerValues = Request.Headers.GetValues("U-CICO-MNEMONIC");
            var userId = headerValues.FirstOrDefault();

            var identityRepository = new IdentityRepository();
            var entryLogRepository = new EntryLogRepository();

            GetEntryLogResponse response = new GetEntryLogResponse();
            response.Identity = AutoMapper.Mapper.Map<Contracts.Identity>(identityRepository.GetById(mobileServiceContext, userId));
            response.EntryLogList = AutoMapper.Mapper.Map<IEnumerable<Contracts.EntryLog>>(entryLogRepository.GetOpenByPosition(mobileServiceContext, latitude, longitude, radius));
            return response;
        }

        [HttpGet()]
        [Route("historical/locus")]
        public GetEntryLogResponse GetHistoricalByLocusIdAndDate(string locusId, string maxDateTimeOffset)
        {
            ApiServices.Log.Info("GetHistoricalByDate");

            IEnumerable<string> headerValues = Request.Headers.GetValues("U-CICO-MNEMONIC");
            var userId = headerValues.FirstOrDefault();

            var identityRepository = new IdentityRepository();
            var entryLogRepository = new EntryLogRepository();

            GetEntryLogResponse response = new GetEntryLogResponse();
            response.Identity = AutoMapper.Mapper.Map<Contracts.Identity>(identityRepository.GetById(mobileServiceContext, userId));
            var maxDateTimeOffsetValue = DateTimeOffset.Parse(maxDateTimeOffset);
            response.EntryLogList = AutoMapper.Mapper.Map<IEnumerable<Contracts.EntryLog>>(entryLogRepository.GetHistoricalByLocusIdAndDate(mobileServiceContext, locusId, maxDateTimeOffsetValue));
            return response;
        }

        [HttpGet()]
        [Route("historical/identity")]
        public GetEntryLogResponse GetHistoricalByIdentityIdAndDate(string identityId, string maxDateTimeOffset)
        {
            ApiServices.Log.Info("GetHistoricalByDate");

            IEnumerable<string> headerValues = Request.Headers.GetValues("U-CICO-MNEMONIC");
            var userId = headerValues.FirstOrDefault();

            var identityRepository = new IdentityRepository();
            var entryLogRepository = new EntryLogRepository();

            GetEntryLogResponse response = new GetEntryLogResponse();
            response.Identity = AutoMapper.Mapper.Map<Contracts.Identity>(identityRepository.GetById(mobileServiceContext, userId));
            var maxDateTimeOffsetValue = DateTimeOffset.Parse(maxDateTimeOffset);
            response.EntryLogList = AutoMapper.Mapper.Map<IEnumerable<Contracts.EntryLog>>(entryLogRepository.GetHistoricalByIdentityIdAndDate(mobileServiceContext, identityId, maxDateTimeOffsetValue));
            return response;
        }

        [HttpGet()]
        public GetEntryLogResponse GetById(string entryLogId)
        {
            ApiServices.Log.Info("GetById");

            IEnumerable<string> headerValues = Request.Headers.GetValues("U-CICO-MNEMONIC");
            var userId = headerValues.FirstOrDefault();

            var identityRepository = new IdentityRepository();
            var entryLogRepository = new EntryLogRepository();

            GetEntryLogResponse response = new GetEntryLogResponse();
            response.Identity = AutoMapper.Mapper.Map<Contracts.Identity>(identityRepository.GetById(mobileServiceContext, userId));
            var entryLog = entryLogRepository.GetById(mobileServiceContext, entryLogId);
            if (entryLog != null)
            {
                var entryLogList = new List<Models.EntryLog> { entryLog };
                response.EntryLogList = AutoMapper.Mapper.Map<IEnumerable<Contracts.EntryLog>>(entryLogList);
            }

            return response;
        }

        [HttpPost()]
        [Route("checkIn")]
        [AuthorizeLevel(AuthorizationLevel.Application)]
        public GetEntryLogResponse CheckIn(CheckInRequest checkInRequest)
        {
            ApiServices.Log.Info("CheckIn");

            IEnumerable<string> headerValues = Request.Headers.GetValues("U-CICO-MNEMONIC");
            var userId = headerValues.FirstOrDefault();

            var identityRepository = new IdentityRepository();
            var locusRepository = new LocusRepository();
            var entryLogRepository = new EntryLogRepository();

            GetEntryLogResponse response = new GetEntryLogResponse();
            response.Identity = AutoMapper.Mapper.Map<Contracts.Identity>(identityRepository.GetById(mobileServiceContext, userId));

            var identity = identityRepository.GetById(mobileServiceContext, checkInRequest.IdentityId);
            var locus = locusRepository.GetById(mobileServiceContext, checkInRequest.LocusId);
            var entryLog = AutoMapper.Mapper.Map<Contracts.CheckInRequest, Models.EntryLog>(checkInRequest);
            if (!string.IsNullOrEmpty(checkInRequest.Latitude) && !string.IsNullOrEmpty(checkInRequest.Longitude))
            {
                entryLog.Position = DbGeography.FromText(string.Format("POINT({1} {0})", checkInRequest.Latitude, checkInRequest.Longitude), 4326);
            }
            entryLog.Id = Guid.NewGuid().ToString();
            entryLog.Identity = identity;
            entryLog.Locus = locus;
            entryLog.InTime = DateTimeOffset.Now;
            entryLogRepository.AddEntryLog(mobileServiceContext, entryLog);

            var entryLogList = new List<Models.EntryLog> { entryLog };
            response.EntryLogList = AutoMapper.Mapper.Map<IEnumerable<Contracts.EntryLog>>(entryLogList);

            return response;
        }
    }
}
