using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using MudBlazor;
using static MudBlazor.CategoryTypes;

namespace Perpustakaan.Shared
{
    public partial class MainBody
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

      

        private bool _drawerOpen = true;
       
       

        private void DrawerToggle()
        {
            _drawerOpen = !_drawerOpen;
        }

       
    }
}

