using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;
using Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Models;
using RealityShiftLearning.Services;

namespace RealityShiftLearning.Areas.Identity.Pages.Account
{
    [IgnoreAntiforgeryToken(Order = 2000)]
    [AllowAnonymous]
    public class ExternalLoginModel : PageModel
    {
        private readonly SignInManager<MotiWadeUser> _signInManager;
        private readonly UserManager<MotiWadeUser> _userManager;
        private readonly RandomTasksService randomTasksService;
        private readonly LearnDbContext dbContext;
        private readonly ILogger<ExternalLoginModel> _logger;

        public ExternalLoginModel(
            SignInManager<MotiWadeUser> signInManager,
            UserManager<MotiWadeUser> userManager,
            RandomTasksService randomTasksService,
            LearnDbContext dbContext,
            ILogger<ExternalLoginModel> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            this.randomTasksService = randomTasksService;
            this.dbContext = dbContext;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ProviderDisplayName { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }

        public async Task<IActionResult> OnPost(string provider, string isDemo = null, string returnUrl = null)
        {
            if (string.IsNullOrEmpty(isDemo))
            {
                // Request a redirect to the external login provider.
                var redirectUrl = Url.Page("./ExternalLogin", pageHandler: "Callback", values: new { returnUrl });
                var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
                return new ChallengeResult(provider, properties);
            }
            else
            {
                var user = await _userManager.FindByEmailAsync("demo@motiwade.rtuitlab.dev");
                var claims = await _userManager.GetClaimsAsync(user);
                var helloExercise = this.randomTasksService.GetRandomExercise();
                dbContext.Exercises.Add(helloExercise);
                dbContext.UserToExercises.Add(new UserToExercise
                {
                    Exercise = helloExercise,
                    User = user
                });
                await dbContext.SaveChangesAsync();
                await _signInManager.SignInWithClaimsAsync(user, isPersistent: true, claims);
                return LocalRedirect(returnUrl);
            }
        }

        public async Task<IActionResult> OnGetCallbackAsync(string returnUrl = null, string remoteError = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (remoteError != null)
            {
                ErrorMessage = $"Error from external provider: {remoteError}";
                return LocalRedirect(returnUrl);
            }
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ErrorMessage = "Error loading external login information.";
                return LocalRedirect(returnUrl);
            }

            // Sign in the user with this external login provider if the user already has a login.
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: true, bypassTwoFactor: true);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
                await _signInManager.SignInWithClaimsAsync(user, false, info.Principal.Claims);
                var oldClaims = await _userManager.GetClaimsAsync(user);
                await _userManager.RemoveClaimsAsync(user, oldClaims);
                await _userManager.AddClaimsAsync(user, info.Principal.Claims);

                _logger.LogInformation("{Name} logged in with {LoginProvider} provider.", info.Principal.Identity.Name, info.LoginProvider);
                return LocalRedirect(returnUrl);
            }
            if (result.IsLockedOut)
            {
                return RedirectToPage("./Lockout");
            }
            else
            {
                // If the user does not have an account, then ask the user to create an account.
                ReturnUrl = returnUrl;
                ProviderDisplayName = info.ProviderDisplayName;
                if (info.Principal.HasClaim(c => c.Type == ClaimTypes.Email))
                {
                    Input = new InputModel
                    {
                        Email = info.Principal.FindFirstValue(ClaimTypes.Email)
                    };
                }
                return await OnPostConfirmationAsync(returnUrl);
            }
        }

        public async Task<IActionResult> OnPostConfirmationAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            // Get the information about the user from the external login provider
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ErrorMessage = "Error loading external login information during confirmation.";
                return LocalRedirect(returnUrl);
            }

            if (ModelState.IsValid)
            {
                var user = new MotiWadeUser { UserName = Input.Email, Email = Input.Email };

                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await _userManager.AddLoginAsync(user, info);
                    await _userManager.AddClaimsAsync(user, info.Principal.Claims);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User created an account using {Name} provider.", info.LoginProvider);


                        var helloExercise = this.randomTasksService.GetRandomExercise();
                        dbContext.Exercises.Add(helloExercise);
                        dbContext.UserToExercises.Add(new UserToExercise
                        {
                            Exercise = helloExercise,
                            User = user
                        });
                        await dbContext.SaveChangesAsync();
                        await _signInManager.SignInWithClaimsAsync(user, isPersistent: true, info.Principal.Claims);

                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            ProviderDisplayName = info.ProviderDisplayName;
            ReturnUrl = returnUrl;
            return Page();
        }
    }
}
