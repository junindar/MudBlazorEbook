using AutoMapper;
using DataService.Entities;
using DataService.IService;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Perpustakaan.Extensions.AutoMapper;
using Perpustakaan.Model;
using static MudBlazor.Icons.Custom;
using static System.Reflection.Metadata.BlobBuilder;

namespace Perpustakaan.Pages.Catalog
{
    public partial class Books
    {
        [Inject] public IBookService BookService { get; set; }

        [Inject]
        public IMapper _mapper { get; set; }
        private List<BookVM> _bookList = new();
        private BookVM _book = new();
      
        private bool _loaded;
        private string _searchString = "";

     

        protected override async Task OnInitializedAsync()
        {
            await GetBooksAsync();
            _loaded = true;
        }
       
        private async Task GetBooksAsync()
        {
            try
            {
                var result = await BookService.GetAllAsNoTrackingAsync();
                _bookList = result.Select(c => c.ToModel(_mapper)).ToList(); 
            }
            catch (Exception e)
            {
                _snackBar.Add(e.Message, Severity.Error);
            }
          
            
        }

       

        private async Task InvokeModal(int id = 0)
        {
            var parameters = new DialogParameters();
            if (id != 0)
            {

                _book = _bookList.FirstOrDefault(c => c.ID == id);
                if (_book != null)
                {
                    parameters.Add(nameof(AddEditBookDialog.BookModel), new BookVM()
                    {
                        ID =_book.ID,
                        Judul = _book.Judul,
                        Penulis = _book.Penulis,
                        Deskripsi = _book.Deskripsi,
                        Status=_book.Status,
                        Penerbit=_book.Penerbit,
                        Gambar=_book.Gambar,CategoryID=_book.CategoryID
                    });

                }
            }
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<AddEditBookDialog>(id == 0 ? "Create" :"Edit", parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                await Reset();
            }
        }

        private async Task Delete(int id)
        {
            string deleteContent = "Delete Item";
            var parameters = new DialogParameters
            {
                { nameof(Shared.Components.DeleteDialog.ContentText), string.Format(deleteContent, id) }
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<Shared.Components.DeleteDialog>("Delete", parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                try
                {
                    await BookService.DeleteAsync(id);
                    _book = _bookList.FirstOrDefault(c => c.ID == id);
                    _snackBar.Add($"Book : {_book.Judul} Deleted", Severity.Success);
                    await Reset();
                  
                }
                catch (Exception e)
                {
                    _snackBar.Add(e.Message, Severity.Error);
                }
                
                
            }
        }

        private async Task Reset()
        {
            _book = new BookVM();
            await GetBooksAsync();
        }
        private bool Filter(BookVM book)
        {
            if (string.IsNullOrWhiteSpace(_searchString)) return true;
            if (book.Judul.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            return book.Penerbit?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true;
         
        }
    }
}
