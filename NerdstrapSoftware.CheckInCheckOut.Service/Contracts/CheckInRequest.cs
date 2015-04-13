
namespace NerdstrapSoftware.CheckInCheckOut.Service.Contracts
{
    public class CheckInRequest
    {
        public string LocusId { get; set; }
        public string LocusName { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string IdentityId { get; set; }
        public string FirstName { get; set; }
        public string MiddleInitial { get; set; }
        public string LastName { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public string CompanyName { get; set; }
        public string Purpose { get; set; }
        public string Duration { get; set; }
        public string HasGroupCheckIn { get; set; }
        public string AdditionalInfo { get; set; }
    }
}