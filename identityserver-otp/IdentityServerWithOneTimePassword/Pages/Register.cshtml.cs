using System.ComponentModel.DataAnnotations;
using IdentityServerWithOneTimePassword.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IdentityServerWithOneTimePassword.Pages;

public class RegisterPageModel : PageModel
{
    private readonly UserManager<ApplicationUser> _userManager;

    public RegisterPageModel(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public InputModel Input { get; set; } = new InputModel();

    public class InputModel
    {
        [Required(AllowEmptyStrings = false)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = "";
    }

    public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
    {
        returnUrl ??= Url.Content("~/");

        if (ModelState.IsValid)
        {
            var user = new ApplicationUser { UserName = Input.Email, Email = Input.Email };
            var result = await _userManager.CreateAsync(user);

            if (result.Succeeded)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
        }

        return Page();
    }
}