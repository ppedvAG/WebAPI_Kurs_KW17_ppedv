using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPIKurs.Services;

namespace WebAPIKurs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private readonly IVideoService _videoService;

        public VideoController(IVideoService videoService)
        {
            _videoService = videoService;
        }

        //https://localhost:12345/Video/Fugees
        [HttpGet("{name}")]
        public async Task<ActionResult> Index(string name)
        {
            if (string.IsNullOrEmpty(name))
                return BadRequest();

            Stream stream = await _videoService.GetVideoByName(name);

            return new FileStreamResult(stream, "video/mp4");
        }

    }
}
