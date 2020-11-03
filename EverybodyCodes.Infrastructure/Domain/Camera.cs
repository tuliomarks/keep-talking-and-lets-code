using System;
using System.Collections.Generic;
using System.Text;

namespace EverybodyCodes.Infrastructure.Domain
{
    public class Camera
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }
}
