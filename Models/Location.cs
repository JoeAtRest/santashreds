using System;

namespace SantaShreds.Models
{
    public class Location
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public bool? TrickComplete { get; set; }
        public DateTime? CodeEnteredDate { get; set; }
    }
}
