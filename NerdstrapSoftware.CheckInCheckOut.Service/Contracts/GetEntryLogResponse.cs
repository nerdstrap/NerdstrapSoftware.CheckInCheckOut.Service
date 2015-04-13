using System.Collections.Generic;

namespace NerdstrapSoftware.CheckInCheckOut.Service.Contracts
{
    public class GetEntryLogResponse : BaseResponse
    {
        public IEnumerable<EntryLog> EntryLogList { get; set; }
    }
}