using System.Collections.Generic;

namespace Models.Dtos
{
    public class LatLngsDto
    {
        public ICollection<LatLngDto> LatLngDtos { get; set; }

        public LatLngsDto(ICollection<LatLngDto> latLngDtos)
        {
            LatLngDtos = latLngDtos;
        }
    }
}