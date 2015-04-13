using Microsoft.WindowsAzure.Mobile.Service;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Spatial;

namespace NerdstrapSoftware.CheckInCheckOut.Service.Models
{
    public class EntryLog : EntityData
    {
        [Required]
        public Locus Locus { get; set; }

        [StringLength(1000)]
        public string LocusName { get; set; }

        public DbGeography Position { get; set; }

        [Required]
        public Identity Identity { get; set; }

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

        [Required]
        public string Purpose { get; set; }

        [Required]
        public int Duration { get; set; }

        [Required]
        public bool HasGroupCheckIn { get; set; }

        [StringLength(1000)]
        public string AdditionalInfo { get; set; }

        [Required]
        public DateTimeOffset InTime { get; set; }

        public DateTimeOffset? OutTime { get; set; }
    }
}