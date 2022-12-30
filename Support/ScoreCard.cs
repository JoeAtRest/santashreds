using Newtonsoft.Json;
using SantaShreds.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SantaShreds.Support
{
    public class ScoreCard
    {
        public Models.Scorecard card { get; private set; }
        private List<string> codes { get; set; }

        private bool IsLoaded = false;

        public ScoreCard() 
        {
            codes = new List<string>()
            {
                "BIK",
                "COH",
                "TWO",
                "BAK",
                "FIN",
                "LNJ"
            };
        }
        public void Load(string hunterName)
        {
            try
            {
                using (StreamReader file = File.OpenText($"{hunterName}"))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    card = (Models.Scorecard)serializer.Deserialize(file, typeof(Models.Scorecard));
                }
                IsLoaded = true;
            } 
            catch(Exception ex)
            {
               card = new Models.Scorecard() { HunterName = hunterName };
               IsLoaded = true;
            }
        }

        public void Update()
        {
            if (IsAllComplete())
                this.card.FinalLocationUnlocked = true;

            // serialize JSON directly to a file
            using (StreamWriter file = File.CreateText($"{card.HunterName}"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, card);
            }            
        }
       
        public bool QueryLocation(string location)
        {
            if(IsLoaded)
            {
                if (card.Locations != null && card.Locations.Count(j => j.Name == location) > 0)
                {
                    var thisCard = card.Locations.Single(j => j.Name == location);

                    if (thisCard.Code == thisCard.Name)
                        return true;
                }
            }
            
            return false;
        }

        public bool QueryLocationAndTrick(string location)
        {
            if (IsLoaded)
            {
                if (card.Locations != null && card.Locations.Count(j => j.Name == location) > 0)
                {
                    var thisCard = card.Locations.Single(j => j.Name == location);

                    if (thisCard.Code == thisCard.Name)
                    {
                        if (thisCard.TrickComplete.HasValue && thisCard.TrickComplete.Value)
                            return true;
                    }
                        return false;
                }
            }

            return false;
        }

        private bool IsAllComplete()
        {
            var progress = QueryProgress();

            if (progress.Item1 >=5  && progress.Item2 >= 5)
                return true;

            return false;

        }

        private bool IsFinalComplete()
        {
            var progress = QueryProgress();

            if (progress.Item1 == 6 && progress.Item2 == 6)
                return true;

            return false;

        }

        public (int,int) QueryProgress()
        {
            var cluesFound = 0;
            var tricksCompleted = 0;

            if (IsLoaded)
            {                             
                foreach(var l in codes)
                {
                    if (card != null && card.Locations !=null && card.Locations.Count(j => j.Name == l && j.Code == j.Name) > 0)
                    {
                        cluesFound++;
                    } 

                    if (card != null && card.Locations != null && card.Locations.Count(j => j.Name == l && j.TrickComplete.HasValue && j.TrickComplete.Value) > 0)
                    {
                        tricksCompleted++;
                    }
                }
            }

            return (cluesFound, tricksCompleted);
        }

        public Progress CheckProgress()
        {
            Progress progress = new Progress()
            {
                FinalUnlocked = IsAllComplete(),
                BonusUnlocked = IsFinalComplete(),
                OneCompleted = QueryLocationAndTrick("BIK"),
                TwoCompleted = QueryLocationAndTrick("COH"),
                ThreeCompleted = QueryLocationAndTrick("TWO"),
                FourCompleted = QueryLocationAndTrick("BAK"),
                FiveCompleted = QueryLocationAndTrick("LNJ"),
                SixCompleted = QueryLocationAndTrick("FIN"),
            };

            return progress;

        }

        public string CompleteLocation(string location)
        {
            if (IsLoaded)
            {
                if (codes.Contains(location))
                {
                    if (card.Locations != null)
                    {
                        if (card.Locations.Count(j => j.Name == location) > 0)
                        {
                            var thisCard = card.Locations.Single(j => j.Name == location);

                            if (thisCard.Code == location)
                            {
                                return "completed";
                            }
                            else
                            {
                                thisCard.Code = location;
                                thisCard.CodeEnteredDate = DateTime.Now;
                            }
                        }
                        else
                        {
                            card.Locations.Add(new SantaShreds.Models.Location()
                            {
                                Name = location,
                                Code = location,
                                CodeEnteredDate = DateTime.Now
                            });
                        }
                    } 
                    else
                    {
                        card.Locations = new List<Models.Location>()
                        {
                            new Models.Location ()
                            {
                                Name = location,
                                Code = location,
                                CodeEnteredDate = DateTime.Now
                            }
                         };
                    }

                    if (IsAllComplete())
                    {
                        card.FinalLocationUnlocked = true;
                    } 
                    else
                    {
                        card.FinalLocationUnlocked = false;
                    }

                    Update();
                    return "success";
                }
                return "fail";
            }

            return "fail";
        }
    }
}
