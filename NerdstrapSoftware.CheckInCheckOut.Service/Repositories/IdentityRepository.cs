using NerdstrapSoftware.CheckInCheckOut.Service.Models;
using System;
using System.Data.Entity;
using System.Linq;

namespace NerdstrapSoftware.CheckInCheckOut.Service.Repositories
{
    public class IdentityRepository : IDisposable
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Identity GetById(MobileServiceContext db, string id)
        {
            var identity = db.IdentityDbSet.First(i => i.Id == id);
            db.Entry(identity)
                .Collection(i => i.OpenEntryLogs)
                .Query()
                .Where(e => e.OutTime.HasValue == false)
                .Load();
            return identity;
        }

        public IQueryable<Identity> GetByIdentityName(MobileServiceContext db, string identityName)
        {
            return db.IdentityDbSet
                .Where(i => i.FirstName.ToLower().Contains(identityName.ToLower()) || i.LastName.ToLower().Contains(identityName.ToLower()));
        }

        public IQueryable<Identity> GetByContactNumber(MobileServiceContext db, string contactNumber)
        {
            return db.IdentityDbSet
                .Where(i => i.ContactNumber.CompareTo(contactNumber) == 0);
        }

        public IQueryable<Identity> GetByEmail(MobileServiceContext db, string email)
        {
            return db.IdentityDbSet
                .Where(i => i.Email.ToLower().CompareTo(email.ToLower()) == 0);
        }

        public IQueryable<Identity> GetByCompanyName(MobileServiceContext db, string companyName)
        {
            return db.IdentityDbSet
                .Where(i => i.CompanyName.ToLower().CompareTo(companyName.ToLower()) == 0);
        }
    }
}