using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceTracker.Domain.Entities;
using ServiceTracker.Models;

namespace ServiceTracker.Controllers;

public class AccountController(SignInManager<ApplicationUser> signInManager) : Controller
{
    [HttpGet]
    public IActionResult Login(string returnUrl = null)
    {
        return View(new LoginViewModel { ReturnUrl = returnUrl });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            var result = await signInManager.PasswordSignInAsync(viewModel.UserName, viewModel.Password, viewModel.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(viewModel.ReturnUrl) && Url.IsLocalUrl(viewModel.ReturnUrl))
                    return Redirect(viewModel.ReturnUrl);

                else
                    return RedirectToAction("Index", "Home");
            }

            else
            {
                ModelState.AddModelError(nameof(viewModel.UserName), "Неправильный логин или пароль");
                return View(viewModel);
            }
        }

        return View(viewModel);
    }

    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}
