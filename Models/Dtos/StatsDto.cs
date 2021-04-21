using System.Collections.Generic;

namespace Models.Dtos
{
    public class StatsDto
    {
        public ICollection<ICollection<LatLngDto>> Paths { get; set; }

        public float TotalDistance { get; set; }
        
        public float AverageDistance { get; set; }

        public float AverageSpeed { get; set; }

        public float AverageDeliveryTime { get; set; }

        public int FinishedDeliveriesCount { get; set; }

        public int CanceledDeliveriesCount { get; set; }
    }
}