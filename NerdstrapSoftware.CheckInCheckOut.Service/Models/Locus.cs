using Microsoft.WindowsAzure.Mobile.Service;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Spatial;

namespace NerdstrapSoftware.CheckInCheckOut.Service.Models
{
    public class Locus : EntityData
    {
        [Required, StringLength(100)]
        public string LocusName { get; set; }

        public DbGeography Position { get; set; }

        [Required, StringLength(100)]
        public string RegionName { get; set; }

        [Required, StringLength(100)]
        public string AreaName { get; set; }

        [StringLength(15)]
        public string Phone { get; set; }

        [Required]
        public bool HasHazard { get; set; }

        public ICollection<EntryLog> OpenEntryLogs { get; set; }
    }
}