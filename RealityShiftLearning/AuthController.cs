using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using RealityShiftLearning.Areas.Identity.Pages.Account;

namespace RealityShiftLearning
{
    [Route("controller/auth")]
    public class AuthController : Controller
    {
        private SignInManager<MotiWadeUser> _signInManager;
        private ILogger<LogoutModel> _logger;
        public AuthController(SignInManager<MotiWadeUser> signInManager, ILogger<LogoutModel> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Post(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return Redirect("/");
            }
        }
    }
}
