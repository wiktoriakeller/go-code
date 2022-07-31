using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoCode.WebAPI.Controllers
{
    [Authorize]
    public class DummyController : ControllerBase
    {
        public async Task<IActionResult> GetDummyData()
        {
            return Ok(await Task.FromResult<string>("dummy data"));
        }
    }
}