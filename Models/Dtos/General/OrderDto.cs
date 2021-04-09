using System;
using System.Collections.Generic;

namespace Models.DTOs.General
{
    public class OrderDto
    {
        public DateTime CreationDateTime { get; set; }
        public ICollection<ProductPackDto> ProductPacks { get; set; }
    }
}