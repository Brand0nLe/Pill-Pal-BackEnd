using Microsoft.AspNetCore.Mvc;

namespace pillpalbackend.Controllers;

[ApiController]
[Route("[controller]")]
public class PillPalController : ControllerBase
{
    [HttpGet]
    [Route ("PillPal")]

}