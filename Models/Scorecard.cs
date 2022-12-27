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

            return null;
        }

    }
}
