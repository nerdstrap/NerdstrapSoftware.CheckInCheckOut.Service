using NerdstrapSoftware.CheckInCheckOut.Service.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.Spatial;
using System.Data.Entity.SqlServer;
using System.Linq;

namespace NerdstrapSoftware.CheckInCheckOut.Service.Repositories
{
    public class LocusRepository : IDisposable
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Locus GetById(MobileServiceContext db, string id)
        {
            var locus = db.LocusDbSet.First(l => l.Id == id);
            db.Entry(locus)
                .Collection(i => i.OpenEntryLogs)
                .Query()
                .Where(e => e.OutTime.HasValue == false)
                .Load();
            return locus;
        }

        public IQueryable<Locus> GetByLocusName(MobileServiceContext db, string locusName)
        {
            return db.LocusDbSet
                .Where(l => l.LocusName.ToLower().Contains(locusName.ToLower()));
        }

        public IQueryable<Locus> GetByRegionName(MobileServiceContext db, string regionName)
        {
            return db.LocusDbSet
                .Where(l => l.RegionName.ToLower().Contains(regionName.ToLower()));
        }

        public IQueryable<Locus> GetByAreaName(MobileServiceContext db, string areaName)
        {
            return db.LocusDbSet
                .Where(l => l.AreaName.ToLower().Contains(areaName.ToLower()));
        }

        public IQueryable<Locus> GetByPosition(MobileServiceContext db, string latitude, string longitude, string radiusInMiles)
        {
            var radius = Double.Parse(radiusInMiles) * 1609.34;
            var point = DbGeography.FromText(string.Format("POINT({1} {0})", latitude, longitude), 4326);
            var query = from locus in db.LocusDbSet
                        let region = point.Buffer(radius)
                        where SqlSpatialFunctions.Filter(locus.Position, region) == true
                        select locus;
            return query.AsQueryable();
        }
    }
}