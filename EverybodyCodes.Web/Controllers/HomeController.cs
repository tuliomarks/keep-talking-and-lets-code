using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EverybodyCodes.Infrastructure.Services.Interfaces;
using System.Collections.Generic;
using EverybodyCodes.Infrastructure.Domain;
using System.Linq;
using EverybodyCodes.Web.Models;
using Microsoft.Extensions.Configuration;

namespace EverybodyCodes.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly string _googleMapsAPIKey;
        private readonly ILogger<HomeController> _logger;
        private readonly ICameraService _cameraService;

        public HomeController(ILogger<HomeController> logger, 
                              ICameraService cameraService,
                              IConfiguration configuration)
        {

             _googleMapsAPIKey = configuration.GetSection("GoogleMapsAPI").Value;
            _logger = logger;
            this._cameraService = cameraService;
        }

        public IActionResult Index()
        {
            var cameraDivisor3 = this._cameraService.GetByDivisor(3);
            var cameraDivisor5 = this._cameraService.GetByDivisor(5);

            var cameraDivisor35 = new List<Camera>(cameraDivisor3);
            cameraDivisor35.AddRange(cameraDivisor5);
            // Distinct 
            cameraDivisor35 = cameraDivisor35.GroupBy(p => p.Id)
                                              .Select(g => g.First())
                                              .OrderBy(o=> o.Name)
                                              .ToList();

            var otherCameras = this._cameraService.GetAll().ToList();
            otherCameras.RemoveAll(x => cameraDivisor35.Any(y => y.Id == x.Id));

            ViewBag.GoogleMapsAPIKey = _googleMapsAPIKey;

            return View(new HomeIndexModel(){
                CamerasDivisor3 = cameraDivisor3,
                CamerasDivisor5 = cameraDivisor5,
                CamerasDivisor35 = cameraDivisor35,
                OtherCameras = otherCameras
            });
        }

    }
}
