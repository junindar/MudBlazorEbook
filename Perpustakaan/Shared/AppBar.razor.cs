using DataService.Entities;
using DataService.IService;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using MudBlazor;
using Perpustakaan.AppSettings;
using System.Security.Claims;
using static MudBlazor.CategoryTypes;

namespace Perpustakaan.Shared
{
    public partial class AppBar
    {
       

        ClaimsPrincipal user;

        [CascadingParameter]
        private Task<AuthenticationState> authenticationStateTask { get; set; }

       
        private string ImageDataUrl { get; set; }
        private string Name { get; set; }
        private string Username { get; set; }

    
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await LoadDataAsync();
            }
        }

        private async Task LoadDataAsync()
        {
            user = (await authenticationStateTask).User;

            if (user == null) return;
            if (user.Identity?.IsAuthenticated == true)
            {

              
                Name = user.GetName();

                Username = user.GetUsername();
                ImageDataUrl = string.IsNullOrEmpty(user.GetImageUrl()) ? "images/profilepicture/default.jpg" : user.GetImageUrl();
                StateHasChanged();


            }
        }

      
        private void Logout()
        {
            var parameters = new DialogParameters
            {
                {nameof(Components.Logout.ContentText), "Logout Confirmation"},
                {nameof(Components.Logout.ButtonText), "Logout"},
                {nameof(Components.Logout.Color), Color.Error},
              
             
            };

            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true };

            _dialogService.Show<Components.Logout>("Logout", parameters, options);
        }

     
    }
}

