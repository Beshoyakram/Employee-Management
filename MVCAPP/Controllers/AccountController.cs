using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using MVCAPP.Models;
using MVCAPP.ViewModels;
using System.Security.Claims;

namespace MVCAPP.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager 
                                ,SignInManager<ApplicationUser> signInManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
        }
        

        [HttpGet]
        [AllowAnonymous]
        //Account/Register
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                /*using IdentityUser class assume username ,email and city */
                var user =new ApplicationUser
                {
                    UserName=model.Email,
                    Email = model.Email,
                    City=model.City
                };
                /*try to create new user*/
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var ConfirmationLink=Url.Action("ConfirmEmail","Account",
                                            new {UserId=user.Id , token=token},Request.Scheme);

                    ViewBag.Title = "Registeration Successful";
                    ViewBag.Message = "Before you can login, please confirm your " +
                        "email, by clicking on confirmation link that we have emailed you";

                    //I will send ConfirmationLink in view but it must send in mail by smtp protocol.
                    return View("RegisterationResult",ConfirmationLink);


                    /*signing in*/
                    //await _signInManager.SignInAsync(user, isPersistent: false);
                    //return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View();
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string UserId, string token)
        {
            if (UserId == null || token == null)
            {
                return RedirectToAction("index", "Home");
            }
            var user = await _userManager.FindByIdAsync(UserId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"The User Id {UserId} is invaild";
                return View("NotFound");
            }
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return View();
            }
            else
            {
                return View("Error");
            }

        }

        [AcceptVerbs("Post","Get")]
        [AllowAnonymous]
        //Check is there is an previous email like this when register.
        public async Task<IActionResult> IsEmailInUse(string Email)
        {
            var user = await _userManager.FindByEmailAsync(Email);
            if (user == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Email {Email} is already in use");
            }
        }


        [HttpGet]
        [AllowAnonymous]
        //Account/Login
        public async Task<IActionResult> Login(string? ReturnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "home");
            }
            LoginViewModel model = new LoginViewModel()
            {
                ReturnUrl = ReturnUrl,
                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };
            return View(model);

        }
        [HttpPost]
        //Account/Login
        //local login
        public async Task<IActionResult> Login(LoginViewModel model,string? ReturnUrL)
        {
            model.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                var user= await _userManager.FindByEmailAsync(model.Email);
                if (user != null && !user.EmailConfirmed && (await _userManager.CheckPasswordAsync(user,model.Password)))
                {
                    ModelState.AddModelError(string.Empty, "Email not confirmed yet");
                    return View(model);
                }

                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe,true);
                
                //IF user fail to login for 5 times
                if (result.IsLockedOut)
                {
                    return View("AccountLocked");
                }
                if (result.Succeeded)
                {
                    if (string.IsNullOrEmpty(ReturnUrL) || !Url.IsLocalUrl(ReturnUrL))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return Redirect(ReturnUrL);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
                }

            }
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult ExternalLogin(string provider ,string ReturnUrl)
        {
            var redirectUrl = Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = ReturnUrl });
            var properties=_signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider,properties);
        }


        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string ReturnUrL = null, string? remoteError = null)
        {
            ReturnUrL = ReturnUrL ?? Url.Content("~/");
            LoginViewModel model = new LoginViewModel()
            {
                ReturnUrl = ReturnUrL,
                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };
            if (remoteError != null)
            {
                ModelState.AddModelError(string.Empty, $"Error from external provider : {remoteError}");
                return View("Login",model);
            }

            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ModelState.AddModelError(string.Empty, $"Error Loading external information");
                return View("Login", model);
            }
            else
            {

                //check if there is account like this in userLogins table
                var signInResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider,
                                            info.ProviderKey, isPersistent:false,bypassTwoFactor: true);

                if (signInResult.Succeeded)
                    { return LocalRedirect(ReturnUrL); }
                else
                {
                    var email = info.Principal.FindFirstValue(ClaimTypes.Email);

                    // check if the user has local account if yes ok if no make a new one in users table
                    if (email != null)
                    {
                        var user = await _userManager.FindByEmailAsync(email);

                        if (user == null)
                        {
                            user = new ApplicationUser()
                            {
                                Email = email,
                                UserName = email,
                                City= ""
                        };
                            await _userManager.CreateAsync(user);
                        }
                        //then add his data to userLogins table.
                        await _userManager.AddLoginAsync(user, info);
                        await _signInManager.SignInAsync(user, isPersistent:false);
                        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        await _userManager.ConfirmEmailAsync(user, token);
                        return LocalRedirect(ReturnUrL);

                    }
                    else
                    {
                        return View("Error");

                    }

                }

            }
            
        }


        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        
        [Authorize]
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return RedirectToAction("Login");
                }

                var result = await _userManager.ChangePasswordAsync(user,model.OldPassword, model.NewPassword);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(String.Empty, error.Description);
                    }
                    return View();
                }
                await _signInManager.SignOutAsync();

                return View("ChangePasswordConfirmation");
            }
            return View(model);
        }


        [AllowAnonymous]
        [HttpGet]
        public IActionResult ForgetPassword() 
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> ForgetPassword( ForgetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null && await _userManager.IsEmailConfirmedAsync(user))
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var PasswordResetLink = Url.Action("ResetPassword", "Account",
                                                new { Email = model.Email, token = token },Request.Scheme);
                    ViewBag.Message = PasswordResetLink; // send Confirmation link in view
                    return View("ForgetPasswordConfirmation");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Email");
                    return View(model);
                }
            }
            return View(model);
        }


        [AllowAnonymous]
        [HttpGet]
        public IActionResult ResetPassword(string Email, string token)
        {
            if(Email == null || token == null) 
            {
                ModelState.AddModelError(string.Empty, "Invalid password reset token");
            }
            ResetPasswordViewModel model = new ResetPasswordViewModel()
            {
                Email = Email,
                token = token,
            };
            return View(model);
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user =await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var result = await _userManager.ResetPasswordAsync(user, model.token, model.Password);
                    if (result.Succeeded)
                    {
                        return View("ResetPasswordConfirmation");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                        return View(model);
                    }

                }
            }
            return View(model);

        }



        [AllowAnonymous]
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View("AccessDenied");
        }

        



    }
}
