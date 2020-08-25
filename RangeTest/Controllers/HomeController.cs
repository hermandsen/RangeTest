using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;

namespace RangeTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly string localVideoFile = @"C:\video.mp4";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("GET /Home/Index");
            return View(System.IO.File.Exists(localVideoFile));
        }

        public IActionResult Video()
        {
            _logger.LogInformation("GET /Home/Video");
            var range = HttpContext.Request.Headers[HeaderNames.Range].ToString();
            _logger.LogInformation("Range header: " + range);
            var stream = new FileStream(localVideoFile, FileMode.Open, FileAccess.Read, FileShare.Read);
            var result = new FileStreamResult(stream, "video/mp4");
            result.EnableRangeProcessing = true;
            return result;
        }
    }
}
