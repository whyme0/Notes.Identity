using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Notes.Identity.Models;

namespace Notes.Identity.Controllers
{
    public class AuthController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IIdentityServerInteractionService _interactionService;

        public AuthController(SignInManager<User> signInManager, UserManager<User> userManager, IIdentityServerInteractionService interactionService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _interactionService = interactionService;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            var loginModel = new LoginDTO() { ReturnUrl = returnUrl };
            return View(loginModel);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                return View(loginDTO);
            }
            var user = await _userManager.FindByNameAsync(loginDTO.Username);
            if (user == null)
            {
                ModelState.AddModelError(String.Empty, "User not found");
                return View(loginDTO);
            }
            
            var result = await _signInManager.PasswordSignInAsync(user, loginDTO.Password, false, false);
            if (result.Succeeded)
            {
                return Redirect(loginDTO.ReturnUrl);
            }
            
            return View(loginDTO);
        }

        [HttpGet]
        public IActionResult Register(string returnUrl)
        {
            var registrationModel = new RegistrationDTO() { ReturnUrl = returnUrl };
            return View(registrationModel);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationDTO registrationDTO)
        {
            if (!ModelState.IsValid)
            {
                return View(registrationDTO);
            }
            
            var user = new User()
            {
                UserName = registrationDTO.Username,
            };

            var result = await _userManager.CreateAsync(user, registrationDTO.Password);
        
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return Redirect(registrationDTO.ReturnUrl);
            }
            ModelState.AddModelError(string.Empty, "Registration failed");
            return View(registrationDTO);
        }

        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            await _signInManager.SignOutAsync();
            var logoutRequest = await _interactionService.GetLogoutContextAsync(logoutId);
            return Redirect(logoutRequest.PostLogoutRedirectUri);
        }
    }
}
