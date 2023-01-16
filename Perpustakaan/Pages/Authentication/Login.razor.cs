using Blazored.FluentValidation;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Security.Claims;
using Perpustakaan.Model;
using DataService.Entities;
using DataService.IService;
using Perpustakaan.AppSettings;
using Perpustakaan.Pages.UserProfile;

namespace Perpustakaan.Pages.Authentication
{
    public partial class Login
    {
        [Inject]
        public IUserService UserService { get; set; }

      

        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; }

    
      
        ClaimsPrincipal claimsPrincipal;

        [CascadingParameter]
        private Task<AuthenticationState> authenticationStateTask { get; set; }

        private FluentValidationValidator _fluentValidationValidator;
        private bool Validated => _fluentValidationValidator.Validate(options => { options.IncludeAllRuleSets(); });
        private LoginVM LoginModel = new();

        protected override async Task OnInitializedAsync()
        {
         

            claimsPrincipal = (await authenticationStateTask).User;

            if (claimsPrincipal.Identity.IsAuthenticated)
            {

                ((CustomAuthStateProvider)AuthenticationStateProvider)
                    .UserIsLoggedOut();
            }
        }

        private async Task SubmitAsync()
        {
            User user = new User();
            user.Username = LoginModel.Username;
            user.Password = LoginModel.Password;
            var loginResult = await UserService.CheckLoginAsync(user);
            if (loginResult != null)
            {
                if (loginResult.Status)
                {
                    ((CustomAuthStateProvider)AuthenticationStateProvider).
                        UserAuthenticated(loginResult);
                    _navigationManager.NavigateTo("/");
                }
                else
                {
                    _snackBar.Add("User Inactivated", Severity.Error);

                }
            }
            else
            {
                _snackBar.Add("Invalid username or password", Severity.Error);

            }

        }

        private bool _passwordVisibility;
        private InputType _passwordInput = InputType.Password;
        private string _passwordInputIcon = Icons.Material.Filled.VisibilityOff;

        void TogglePasswordVisibility()
        {
            if (_passwordVisibility)
            {
                _passwordVisibility = false;
                _passwordInputIcon = Icons.Material.Filled.VisibilityOff;
                _passwordInput = InputType.Password;
            }
            else
            {
                _passwordVisibility = true;
                _passwordInputIcon = Icons.Material.Filled.Visibility;
                _passwordInput = InputType.Text;
            }
        }

       
    }
}
