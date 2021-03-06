﻿using System;

namespace NerdstrapSoftware.CheckInCheckOut.Service.Contracts
{
    public class EntryLog
    {
        public string Id { get; set; }
        public string LocusId { get; set; }
        public string LocusName { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string HasHazard { get; set; }
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
        public string InTime { get; set; }
        public string OutTime { get; set; }
    }
}