using System.Collections.Generic;
using System.Linq;

namespace SantaShreds.Models
{
    public class Scorecard
    {
        public string HunterName { get; set; }
        public List<Location> Locations {get;set;}
        public bool? FinalLocationUnlocked { get; set; }
        public bool? BonusRoundUnlocked { get; set; }

        public Location GetLocation(string name)
        {
            if ( Locations != null )
            {
                if ( Locations.Count( j => j.Name == name ) > 0 )
                {
                    return Locations.Single( j => j.Name == name );
                }
            }

            return new Location()
            {
                Name = name
            };
        }

        public void UpdateLocation(Location loc)
        {
            if (!loc.CodeEnteredDate.HasValue)
                loc.CodeEnteredDate = System.DateTime.Now;            

            if (Locations != null)
            {
                if (Locations.Count(j => j.Name == loc.Name) > 0)
                {
                    var thisLocation = Locations.Single(j => j.Name == loc.Name);
                    thisLocation.Code = loc.Code;
                    thisLocation.CodeEnteredDate = loc.CodeEnteredDate;
                    thisLocation.TrickComplete = loc.TrickComplete;
                }
                else
                {
                    Locations.Add(loc);
                }
            }
            else
            {

                Locations = new List<Location>()
                    {
                        loc
                    };
            }          
        }

    }
}
