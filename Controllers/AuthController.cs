using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using zesKod_mvc_core_auth.Models;
namespace zesKod_mvc_core_auth.Controllers
{
    public class AuthController : Controller
    {
        public AuthController()
        {

        }

        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl = "")
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel login, string returnUrl = null)
        {
            returnUrl = returnUrl ?? "";
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Invalied", "Opps! Please enter proper values.");
            }
            
            if (login.username == "admin" && login.password == "admin")
            {
                var identity = new ClaimsIdentity(new[] {
                            new Claim(ClaimTypes.Name, login.username),
                            //new Claim(ClaimTypes.Role, user.userRole),
                            //new Claim("userToken", user.token, ClaimValueTypes.String),
                        }, CookieAuthenticationDefaults.AuthenticationScheme);

                var principal = new ClaimsPrincipal(identity);
                var authProp = new AuthenticationProperties
                {
                    IsPersistent = login.remember,
                    ExpiresUtc = DateTime.UtcNow.AddDays(29),
                };
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(principal), authProp);
                
                if (string.IsNullOrEmpty(returnUrl))
                    return RedirectToAction("index", "dashboard");
                else
                    return Redirect(returnUrl);
                //var user = new IdentityUser { UserName = login.username, Email = "admin@test.com" };
                //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                //code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                //var callbackUrl = Url.Page(
                //    "/Account/ConfirmEmail",
                //    pageHandler: null,
                //    values: new { area = "Identity", userId = user.Id, code = code },
                //    protocol: Request.Scheme);

                //await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                //if (_userManager.Options.SignIn.RequireConfirmedAccount)
                //{
                //    return RedirectToPage("RegisterConfirmation",
                //                          new { email = Input.Email });
                //}
                //else
                //{
                //await _signInManager.SignInAsync(user, isPersistent: false);
                //return LocalRedirect(returnUrl);
                //}
            }

            return View(login);
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("~/");
        }
    }
}
