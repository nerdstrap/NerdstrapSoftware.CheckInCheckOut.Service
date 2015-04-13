using NerdstrapSoftware.CheckInCheckOut.Service.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.Spatial;
using System.Data.Entity.SqlServer;
using System.Linq;

namespace NerdstrapSoftware.CheckInCheckOut.Service.Repositories
{
    public class EntryLogRepository : IDisposable
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public EntryLog GetById(MobileServiceContext db, string id)
        {
            return db.EntryLogDbSet
                .Include(e => e.Locus)
                .Include(e => e.Identity)
                .First(e => e.Id == id);
        }

        public IQueryable<EntryLog> GetOpenByLocusId(MobileServiceContext db, string locusId)
        {
            return db.EntryLogDbSet
                .Include(e => e.Locus)
                .Include(e => e.Identity)
                .Where(e => e.Locus.Id == locusId)
                .Where(e => e.OutTime.HasValue == false);
        }

        public IQueryable<EntryLog> GetOpenByIdentityId(MobileServiceContext db, string identityId)
        {
            return db.EntryLogDbSet
                .Include(e => e.Locus)
                .Include(e => e.Identity)
                .Where(e => e.Identity.Id == identityId)
                .Where(e => e.OutTime.HasValue == false);
        }

        public IQueryable<EntryLog> GetOpenByPosition(MobileServiceContext db, string latitude, string longitude, string radiusInMiles)
        {
            var radius = Double.Parse(radiusInMiles) * 1609.34;
            var point = DbGeography.FromText(string.Format("POINT({1} {0})", latitude, longitude), 4326);
            var query = from entryLog in db.EntryLogDbSet.Include(e => e.Locus).Include(e => e.Identity)
                        let region = point.Buffer(radius)
                        where entryLog.OutTime.HasValue == false && SqlSpatialFunctions.Filter(entryLog.Position, region) == true
                        select entryLog;
            return query.AsQueryable();
        }

        public IQueryable<EntryLog> GetHistoricalByLocusIdAndDate(MobileServiceContext db, string locusId, DateTimeOffset maxDateTimeOffset)
        {
            return db.EntryLogDbSet
                .Include(e => e.Locus)
                .Include(e => e.Identity)
                .Where(e => e.Locus.Id == locusId)
                .Where(e => e.OutTime.HasValue == true)
                .Where(e => e.OutTime <= maxDateTimeOffset);
        }

        public IQueryable<EntryLog> GetHistoricalByIdentityIdAndDate(MobileServiceContext db, string identityId, DateTimeOffset maxDateTimeOffset)
        {
            return db.EntryLogDbSet
                .Include(e => e.Locus)
                .Include(e => e.Identity)
                .Where(e => e.Identity.Id == identityId)
                .Where(e => e.OutTime.HasValue == true)
                .Where(e => e.OutTime <= maxDateTimeOffset);
        }

        public void AddEntryLog(MobileServiceContext db, EntryLog entryLog)
        {
            db.EntryLogDbSet.Add(entryLog);
            db.SaveChanges();
        }
    }
}