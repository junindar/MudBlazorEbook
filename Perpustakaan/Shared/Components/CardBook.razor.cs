using AutoMapper;
using DataService.Entities;
using DataService.IService;
using Microsoft.AspNetCore.Components;
using Perpustakaan.Extensions.AutoMapper;
using Perpustakaan.Model;

namespace Perpustakaan.Shared.Components
{
    public partial class CardBook
    {
       
        [Parameter]
        public string CategoryName { get; set; }
        [Parameter] public bool IsShowTab { get; set; } = true;
        [Parameter]
        public int BookId { get; set; } 

        [Inject]
        public IBookService BookService { get; set; }
        public IEnumerable<BookVM> Books { get; set; }
        [Inject]
        public IMapper _mapper { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await LoadData();
        }

        private async Task GoToDetail(int bookid)
        {
            BookId = bookid;
            await LoadData();
            StateHasChanged();
            _navigationManager.NavigateTo($"/bookdetail/{bookid}");

        }

        async Task LoadData()
        {
            Books = BookId > 0 ? (await BookService.GetRelatedRandomBooksAsync(CategoryName, BookId)).Select(c=>c.ToModel(_mapper)).ToList() :
                (await BookService.GetRandomBooksAsync(CategoryName)).Select(c => c.ToModel(_mapper)).ToList();
        }

        public async void ShowBookCategoryAsync(string cat)
        {
            CategoryName = cat;
            Books = (await BookService.GetRandomBooksAsync(cat)).Select(c => c.ToModel(_mapper)).ToList();
            StateHasChanged();
        }
    }
}
