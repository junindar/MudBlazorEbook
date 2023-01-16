using AutoMapper;
using DataService.IService;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using MudBlazor;
using Perpustakaan.Extensions.AutoMapper;
using Perpustakaan.Model;

namespace Perpustakaan.Pages.Catalog
{
    public partial class BookSearch
    {
        public SearchVM searchModel { get; set; } = new SearchVM();
        private async Task SearchAsync()
        {
            await Task.Delay(100);
            _navigationManager.NavigateTo($"/BookSearch/{searchModel.SearchString}");
        }

        [Parameter]
        public string SearchParameter { get; set; }

        [Inject] public IBookService BookService { get; set; }

        [Inject]
        public IMapper _mapper { get; set; }
        private List<BookVM> _bookList = new();
        private BookVM _book = new();

        private bool _loaded;
        private bool _isInitialized;
        protected override async Task OnInitializedAsync()
        {
        
          await GetSearchAsync();
            _loaded = true;
            _isInitialized = false;
        }
        private async Task GetSearchAsync()
        {
            try
            {
                var result = await BookService.SearchBookAsync(SearchParameter);
  
                _bookList = result.Select(c => c.ToModel(_mapper)).ToList();
            }
            catch (Exception e)
            {
                _snackBar.Add(e.Message, Severity.Error);
            }


        }
        protected override async Task OnParametersSetAsync()
        {
            if (_isInitialized)
            {
                _loaded = false;
                await GetSearchAsync();
                _loaded = true;
                StateHasChanged();
            }
            else
            {
                _isInitialized = true;
            }
        }
        //protected override async Task OnAfterRenderAsync(bool firstRender)
        //{
        //    if (!firstRender)
        //    {
        //        await GetSearchAsync();
        //       // StateHasChanged();
        //    }

        //}
    }
}
