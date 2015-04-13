using System.Collections.Generic;

namespace NerdstrapSoftware.CheckInCheckOut.Service.Contracts
{
    public class GetIdentityResponse : BaseResponse
    {
        public IEnumerable<Identity> IdentityList { get; set; }
    }
}