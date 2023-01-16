using DataService.Entities;
using DataService.IService;
using Microsoft.AspNetCore.Components;

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
        public IEnumerable<Book> Books { get; set; }
      
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
            if (BookId > 0)
            {
               // PanelText = $"Related Products";
                Books = (await BookService.GetRelatedRandomBooksAsync(CategoryName, BookId)).ToList();
            }
            else
            {
               // PanelText = "DAFTAR BUKU : TEKNOLOGI";
                Books = (await BookService.GetRandomBooksAsync(CategoryName)).ToList();
            }

        }

        public async void ShowBookCategoryAsync(string cat)
        {
            //PanelText = $"DAFTAR BUKU : {cat.ToUpper()}";
            CategoryName = cat;
            Books = (await BookService.GetRandomBooksAsync(cat)).ToList();
            StateHasChanged();
        }
    }
}
