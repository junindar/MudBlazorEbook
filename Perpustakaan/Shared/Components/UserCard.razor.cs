using DataService.IService;
using DataService.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Perpustakaan.AppSettings;
using System.Security.Claims;

namespace Perpustakaan.Shared.Components
{
    public partial class UserCard
    {
          [Parameter] public string Class { get; set; }
      

        [Parameter] public string ImageDataUrl { get; set; }
        [Parameter] public string Name { get; set; }
        [Parameter] public string Username { get; set; }

        ClaimsPrincipal user;

        [CascadingParameter]
        private Task<AuthenticationState> authenticationStateTask { get; set; }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
              
                await LoadDataAsync();
            }
        }

        private async Task LoadDataAsync()
        {
           
            if (string.IsNullOrEmpty(Name))
            {
                user = (await authenticationStateTask).User;
                if (user == null) return;
                if (user.Identity?.IsAuthenticated == false) return;


                Name = user.GetName();

                Username = user.GetUsername();


                ImageDataUrl = string.IsNullOrEmpty(user.GetImageUrl()) ? "images/profilepicture/default.jpg" : user.GetImageUrl();
                if (string.IsNullOrEmpty(ImageDataUrl))
                {
                    ImageDataUrl = "images/profilepicture/default.jpg";
                }
                StateHasChanged();
             
            }

        }
          
        }



    }

