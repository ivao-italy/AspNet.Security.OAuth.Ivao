using AspNet.Security.OAuth.Ivao;
using Microsoft.AspNetCore.Mvc;

namespace Ivao.It.Authentication.Controllers;

public class AuthController : Controller
{
    public IActionResult Index()
    {
        return Ok("Va bene");
    }

    public IActionResult Login()
    {
        return Challenge(IvaoAuthenticationDefaults.AuthenticationScheme);
    }
}
