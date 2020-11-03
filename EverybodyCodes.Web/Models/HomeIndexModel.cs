using EverybodyCodes.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EverybodyCodes.Web.Models
{
    public class HomeIndexModel
    {
        public IEnumerable<Camera> CamerasDivisor3 { get; set; }
        public IEnumerable<Camera> CamerasDivisor5 { get; set; }
        public IEnumerable<Camera> CamerasDivisor35 { get; set; }
        public IEnumerable<Camera> OtherCameras { get; set; }
    }
}
