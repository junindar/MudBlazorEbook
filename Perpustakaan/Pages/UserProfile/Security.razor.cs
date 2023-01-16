using Blazored.FluentValidation;
using DataService.Entities;
using DataService.IService;
using DataService.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using Perpustakaan.AppSettings;
using Perpustakaan.Model;
using System.Security.Claims;

namespace Perpustakaan.Pages.UserProfile
{
    public partial class Security
    {
        private FluentValidationValidator _fluentValidationValidator;
        private bool Validated => _fluentValidationValidator.Validate(options => { options.IncludeAllRuleSets(); });
        private readonly ChangePasswordVM _passwordModel = new();
        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        ClaimsPrincipal user;

        [CascadingParameter]
        private Task<AuthenticationState> authenticationStateTask { get; set; }
        [Inject] public IUserService UserService { get; set; }
        private async Task ChangePasswordAsync()
        {
            try
            {
                user = (await authenticationStateTask).User;
                var userObj = await UserService.GetByIdAsync(Convert.ToInt32(user.GetUserId()));
                if (userObj.Password != _passwordModel.Password)
                {
                    _snackBar.Add("Current Password Incorrect!", Severity.Error);
                    return;
                }
                if (_passwordModel.ConfirmNewPassword != _passwordModel.NewPassword)
                {
                    _snackBar.Add("The Confirm Password and  New Password does not match with", Severity.Error);
                    return;
                }
                userObj.Password = _passwordModel.NewPassword;
                await UserService.UpdateAsync(userObj);
               
                ((CustomAuthStateProvider)AuthenticationStateProvider)
                    .UserIsLoggedOut();

                _snackBar.Add("Password Changed!", Severity.Success);
                _navigationManager.NavigateTo("/Login");

            }
            catch (Exception ex)
            {
                _snackBar.Add(ex.Message, Severity.Error);
            }
          
        }

        private bool _currentPasswordVisibility;
        private InputType _currentPasswordInput = InputType.Password;
        private string _currentPasswordInputIcon = Icons.Material.Filled.VisibilityOff;

        private bool _newPasswordVisibility;
        private InputType _newPasswordInput = InputType.Password;
        private string _newPasswordInputIcon = Icons.Material.Filled.VisibilityOff;

        private void TogglePasswordVisibility(bool newPassword)
        {
            if (newPassword)
            {
                if (_newPasswordVisibility)
                {
                    _newPasswordVisibility = false;
                    _newPasswordInputIcon = Icons.Material.Filled.VisibilityOff;
                    _newPasswordInput = InputType.Password;
                }
                else
                {
                    _newPasswordVisibility = true;
                    _newPasswordInputIcon = Icons.Material.Filled.Visibility;
                    _newPasswordInput = InputType.Text;
                }
            }
            else
            {
                if (_currentPasswordVisibility)
                {
                    _currentPasswordVisibility = false;
                    _currentPasswordInputIcon = Icons.Material.Filled.VisibilityOff;
                    _currentPasswordInput = InputType.Password;
                }
                else
                {
                    _currentPasswordVisibility = true;
                    _currentPasswordInputIcon = Icons.Material.Filled.Visibility;
                    _currentPasswordInput = InputType.Text;
                }
            }
        }
    }
}
