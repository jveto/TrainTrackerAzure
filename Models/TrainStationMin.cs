using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainTracker.Models{
    public class TrainStationMin{
        [Key]
        public int TrainStationId{get; set;}

        public float Latitude{get; set;}

        public float Longitude{get; set;}

        public string Direction{get; set;}

        public string MapId{get; set;}

        public string DescStationName{get; set;}

        public string StationName{get; set;}

        public string StopId{get; set;}

        public string StopName{get; set;}

        public Boolean ada{get; set;}
        public Boolean Blue{get; set;}
        public Boolean Brown{get; set;}
        public Boolean Green{get; set;}
        public Boolean Orange{get; set;}
        public Boolean Purple{get; set;}
        public Boolean PurpleExpress{get; set;}
        public Boolean Pink{get; set;}
        public Boolean Red{get; set;}
        public Boolean Yellow{get; set;}
    }
}