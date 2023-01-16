using Blazored.FluentValidation;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Perpustakaan.Model;
using static System.Net.Mime.MediaTypeNames;
using DataService.Service;
using Microsoft.AspNetCore.Components.Authorization;
using System.Xml.Linq;
using Perpustakaan.AppSettings;
using DataService.IService;
using System.Security.Claims;

namespace Perpustakaan.Pages.UserProfile
{
    public partial class Profile
    {
        private FluentValidationValidator _fluentValidationValidator;
        private bool Validated => _fluentValidationValidator.Validate(options => { options.IncludeAllRuleSets(); });

        private readonly ProfileVM _profileModel = new();
        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        ClaimsPrincipal user;

        [CascadingParameter]
        private Task<AuthenticationState> authenticationStateTask { get; set; }
        [Inject] public IUserService UserService { get; set; }
        [Inject]
        public IFileUpload fileUpload { get; set; }
        protected MemoryStream fs;
        private async Task<MemoryStream> CopyToMemoryStream()
        {
            var ms = new MemoryStream();
            var stream = _file.OpenReadStream();

            byte[] buffer = new byte[16 * 1024];
            int read;
            while ((read = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
            {
                ms.Write(buffer, 0, read);
            }

            return ms;
        }

        private async Task UpdateProfileAsync()
        {
            try
            {
                user = (await authenticationStateTask).User;
                var userObj = await UserService.GetByIdAsync(Convert.ToInt32(user.GetUserId()));
                userObj.Nama = _profileModel.Nama;
                await UserService.UpdateAsync(userObj);

                ((CustomAuthStateProvider)AuthenticationStateProvider)
                    .UserIsLoggedOut();

                _snackBar.Add("Your Profile has been updated. Please Login to Continue.", Severity.Success);
                _navigationManager.NavigateTo("/Login");
              
            }
            catch (Exception e)
            {
                _snackBar.Add(e.Message, Severity.Error);
            }
          
         
        }

        protected override async Task OnInitializedAsync()
        {
            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {

  
            user = (await authenticationStateTask).User;
            if (user==null) _navigationManager.NavigateTo("/");
            _profileModel.Nama = user.GetName();
            _profileModel.Username = user.GetUsername();
            _profileModel.Role = user.GetRole();


            _profileModel.ProfilePicture = string.IsNullOrEmpty(user.GetImageUrl()) ? "images/profilepicture/default.jpg" : user.GetImageUrl();

        }

        private IBrowserFile _file;

       
        private async Task UploadFiles(InputFileChangeEventArgs e)
        {
            _file = e.File;
            if (_file != null)
            {
                try
                {
                    fs = await CopyToMemoryStream();
                    _profileModel.ProfilePicture = $"images/profilepicture/{_file.Name}";

                    var userObj = await UserService.GetByIdAsync(Convert.ToInt32(user.GetUserId()));
                    userObj.ProfilePicture = _profileModel.ProfilePicture;
                    await UserService.UpdateAsync(userObj);
                  
                    await fileUpload.UploadAsync(fs, _file.Name, "images/profilepicture");

                    ((CustomAuthStateProvider)AuthenticationStateProvider).
                        UserAuthenticated(userObj);
                    _snackBar.Add("Profile picture added..", Severity.Success);
                 
                    _navigationManager.NavigateTo("/");
                }
                catch (Exception ex)
                {
                    _snackBar.Add(ex.Message, Severity.Error);
                }

              
              
            }
        }

        private async Task DeleteAsync()
        {
            var parameters = new DialogParameters
            {
                {nameof(Shared.Components.DeleteDialog.ContentText), $"Do you want to delete the profile picture of {_profileModel.Nama}?"}
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<Shared.Components.DeleteDialog>("Delete", parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                try
                {
                    user = (await authenticationStateTask).User;
                    var userObj = await UserService.GetByIdAsync(Convert.ToInt32(user.GetUserId()));
                    userObj.ProfilePicture = "images/profilepicture/default.jpg";
                    await UserService.UpdateAsync(userObj);
                    ((CustomAuthStateProvider)AuthenticationStateProvider).
                        UserAuthenticated(userObj);
                    _snackBar.Add("Profile picture deleted.", Severity.Success);
                    _navigationManager.NavigateTo("/");
                }
                catch (Exception ex)
                {
                    _snackBar.Add(ex.Message, Severity.Error);
                }
               
                
            }
        }
    }
}
