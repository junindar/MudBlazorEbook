using MudBlazor;
using Perpustakaan.AppSettings;

namespace Perpustakaan.Shared
{
    public partial class MainLayout 
    {


        private MudTheme _currentTheme;

        protected override async Task OnInitializedAsync()
        {
            _currentTheme = BlazorTheme.DarkTheme;
            await Task.CompletedTask;
        }


    }
}
