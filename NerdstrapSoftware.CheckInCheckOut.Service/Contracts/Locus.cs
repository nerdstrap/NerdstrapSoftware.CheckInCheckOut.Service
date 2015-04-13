using System.Collections.Generic;

namespace NerdstrapSoftware.CheckInCheckOut.Service.Contracts
{
    public class Locus
    {
        public string Id { get; set; }
        public string LocusName { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string RegionName { get; set; }
        public string AreaName { get; set; }
        public string Phone { get; set; }
        public string HasHazard { get; set; }
        public string HasWarnings { get; set; }
        public string HasOpenCheckIns { get; set; }
        public IEnumerable<EntryLog> OpenEntryLogs { get; set; }
    }
}