﻿

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace E_Commerece_PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSettings emailSettings;
        private readonly ISmsService smsService;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager
         , IEmailSettings emailSettings
            , ISmsService smsService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            this.emailSettings = emailSettings;
            this.smsService = smsService;
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel Model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    UserName = Model.Email.Split('@')[0],
                    Email = Model.Email,
                    FName = Model.FName,
                    LName = Model.LName,
                };
                var result = await _userManager.CreateAsync(user, Model.Password);
                if (result.Succeeded)
                    return RedirectToAction(nameof(Login));
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(Model);
        }
        public IActionResult Login()
        {
            return View();
        }
        public async Task<IActionResult> Login(LoginViewModel Model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Model.Email);
                if (user is not null)
                {
                    var flag = await _userManager.CheckPasswordAsync(user, Model.Password);
                    if (flag)
                    {
                        await _signInManager.PasswordSignInAsync(user, Model.Password, false, false);
                        return RedirectToAction("Index", "Home");
                    }
                    ModelState.AddModelError(string.Empty, "Invalid Password");
                }
                ModelState.AddModelError(string.Empty, "Email is not exists");
            }
            return View();
        }

        public new async Task<IActionResult> SingOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
        public IActionResult ForgetPassword()
        {
            return View();
        }

        // baseUrl/Account/SendEmail
        [HttpPost]
        public async Task<IActionResult> SendEmail(ForgetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user is not null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user); // Valid Just Only One Time Per User
                    var passwordResetLink = Url.Action("ResetPassword", "Account", new { Email = model.Email, Token = token }, Request.Scheme);
                    var email = new Email()
                    {
                        Subject = "Reset Password",
                        To = model.Email,
                        Body = passwordResetLink
                    };
                    emailSettings.SendEmail(email);
                    return RedirectToAction(nameof(CheckYourInbox));
                }
                ModelState.AddModelError(string.Empty, "Email is not Existed");
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SendSms(ForgetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user is not null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user); // Valid Just Only One Time Per User
                    var passwordResetLink = Url.Action("ResetPassword", "Account", new { Email = model.Email, Token = token }, Request.Scheme);
                    var sms = new SmsMessage()
                    {
                        phoneNumber = user.PhoneNumber,
                        Body = passwordResetLink
                    };
                    smsService.Send(sms);
                    return Ok("Check your Phone");
                }
                ModelState.AddModelError(string.Empty, "Email is not Existed");
            }
            return View(model);
        }


        public IActionResult CheckYourInbox()
        {
            return View();
        }
        public IActionResult ResetPassword(string email, string token)
        {
            TempData["email"] = email;
            TempData["token"] = token;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var email = TempData["email"] as string;
                var token = TempData["token"] as string;

                var user = await _userManager.FindByEmailAsync(email);
                var result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);
                if (result.Succeeded)
                    return RedirectToAction(nameof(Login));
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(model);
        }
        public IActionResult GoogleLogin()
        {
            var prop = new AuthenticationProperties { RedirectUri = Url.Action("GoogleResponse") };
            return Challenge(prop, GoogleDefaults.AuthenticationScheme);
        }
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);
            var Claims = result.Principal.Identities.FirstOrDefault().Claims.Select(cliam => new
            {
                cliam.Issuer,
                cliam.OriginalIssuer,
                cliam.Type,
                cliam.Value
            });
            return Json(Claims);
        }

    }
}
