using System.Collections.Generic;

namespace NerdstrapSoftware.CheckInCheckOut.Service.Contracts
{
    public class Identity
    {
        public string Id { get; set; }
        public string Mnemonic { get; set; }
        public string FirstName { get; set; }
        public string MiddleInitial { get; set; }
        public string LastName { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public string CompanyName { get; set; }
        public IEnumerable<EntryLog> OpenEntryLogs { get; set; }
    }
}