using System.Collections.Generic;

namespace Models.Dtos
{
    public class LatLngsDto
    {
        public ICollection<LatLngDto> LatLngs { get; set; }

        public LatLngsDto(ICollection<LatLngDto> latLngDtos)
        {
            LatLngs = latLngDtos;
        }
    }
}