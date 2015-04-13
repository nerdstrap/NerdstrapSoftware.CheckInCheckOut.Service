using Microsoft.WindowsAzure.Mobile.Service;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NerdstrapSoftware.CheckInCheckOut.Service.Models
{
    public class Identity : EntityData
    {
        [Required, StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(1)]
        public string MiddleInitial { get; set; }

        [Required, StringLength(100)]
        public string LastName { get; set; }

        [Required, StringLength(15)]
        public string ContactNumber { get; set; }

        [StringLength(255)]
        public string Email { get; set; }

        [StringLength(100)]
        public string CompanyName { get; set; }

        public ICollection<EntryLog> OpenEntryLogs { get; set; }
    }
}