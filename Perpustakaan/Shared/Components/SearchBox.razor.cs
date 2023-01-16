using Microsoft.AspNetCore.Components;
using MudBlazor;
using Perpustakaan.Model;

namespace Perpustakaan.Shared.Components
{
    public partial class SearchBox
    {
        [Parameter]
        public string Style { get; set; }
        public SearchVM searchModel { get; set; } = new SearchVM();
        private async Task SearchAsync()
        {
            await Task.Delay(100);
            if (string.IsNullOrEmpty(searchModel.SearchString))
            {
                _snackBar.Add("Please enter the Search Keyword", Severity.Error);
            }
            else
            {
                _navigationManager.NavigateTo($"/BookSearch/{searchModel.SearchString}");
                //StateHasChanged();
            }

        }
    }
}
