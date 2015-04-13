using System.Collections.Generic;

namespace NerdstrapSoftware.CheckInCheckOut.Service.Contracts
{
    public class GetLocusResponse : BaseResponse
    {
        public IEnumerable<Locus> LocusList { get; set; }
    }
}