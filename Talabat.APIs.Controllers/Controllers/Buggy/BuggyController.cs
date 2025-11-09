
namespace Talabat.APIs.Controllers.Controllers.Buggy
{
    public class BuggyController : BaseApiController
    {
        public BuggyController(IServiceManager servicesManager)
            : base(servicesManager)
        {


        }

        [HttpGet("not-found")]
        public IActionResult GetNotFoundRequest()
        {
            return NotFound(new ApiResponse(404));
        }


        [HttpGet("server-error")]
        public IActionResult GetServerErrorRequest()
        {
            throw new Exception();
        }

        [HttpGet("bad-request")]
        public IActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }

        [HttpGet("bad-request/{id}")]
        public IActionResult GetValidationErrorRequest(int id)
        {
            return Ok();
        }

        [Authorize]
        [HttpGet("unauthorized")]
        public IActionResult GetUnauthorizedErrorRequest()
        {
            return Unauthorized();

        }
       
        [HttpGet("forbidden")]
        public IActionResult GetForbiddenErrorRequest()
        {
            return Forbid();

        }



    }
}