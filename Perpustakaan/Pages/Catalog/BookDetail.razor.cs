using AutoMapper;
using DataService.Entities;
using DataService.IService;
using Microsoft.AspNetCore.Components;
using Perpustakaan.Extensions.AutoMapper;
using Perpustakaan.Model;

namespace Perpustakaan.Pages.Catalog
{
    public partial class BookDetail
    {
        [Parameter]
        public string BookId { get; set; }
        public BookVM BookModel { get; set; } = new ();
        [Inject] public IBookService BookService { get; set; }
        protected string NamaCategory = string.Empty;
        [Inject]
        public IMapper _mapper { get; set; }
        private bool _isInitialized;
        protected override async Task OnInitializedAsync()
        {
            await LoadData();
            _isInitialized = false;
        }

        async Task LoadData()
        {
            var result = await BookService.GetByIdAsync(int.Parse(BookId));
            BookModel = result.ToModel(_mapper);
            NamaCategory = BookModel.Category.Nama;
          
        }

        protected override async Task OnParametersSetAsync()
        {
            if (_isInitialized)
            {
                await LoadData();
                StateHasChanged();
            }
            else
            {
                _isInitialized = true;
            }
        }

        //protected override async Task OnAfterRenderAsync(bool firstRender)
        //{
        //    if (firstRender)
        //    {
        //       // await LoadData(); StateHasChanged();
        //    }

        //}
    }
}
