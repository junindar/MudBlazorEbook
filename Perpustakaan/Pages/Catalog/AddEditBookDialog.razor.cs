using AutoMapper;
using Blazored.FluentValidation;
using DataService.Entities;
using DataService.IService;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using Perpustakaan.Extensions.AutoMapper;
using Perpustakaan.Model;
using static MudBlazor.Icons.Custom;
using static System.Net.Mime.MediaTypeNames;

namespace Perpustakaan.Pages.Catalog
{
    public partial class AddEditBookDialog
    {
        [Inject]
        public IFileUpload fileUpload { get; set; }
        [Inject] public IBookService BookService { get; set; }
        [Inject] public ICategoryService CategoryService { get; set; }
        [Inject]
        public IMapper _mapper { get; set; }

        [Parameter] public BookVM BookModel { get; set; } = new();

  
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }

        private FluentValidationValidator _fluentValidationValidator;
        private bool Validated => _fluentValidationValidator.Validate(options => { options.IncludeAllRuleSets(); });
        private List<CategoryVM> _categories = new();
        private async Task<IEnumerable<int>> SearchCategories(string value)
        {
            // In real life use an asynchronous function for fetching data from an api.
            await Task.Delay(5);

            // if text is null or empty, show complete list
            if (string.IsNullOrEmpty(value))
                return _categories.Select(x => x.ID);

            return _categories.Where(x => x.Nama.Contains(value, StringComparison.InvariantCultureIgnoreCase))
                .Select(x => x.ID);
        }

        private IBrowserFile _file;
        protected MemoryStream fs;
        protected string ImageData = string.Empty;
        private async Task<MemoryStream> CopyToMemoryStream()
        {
            var ms = new MemoryStream();
            var stream = _file.OpenReadStream();

            byte[] buffer = new byte[16 * 1024];
            int read;
            while ((read = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
            {
                ms.Write(buffer, 0, read);
            }

            return ms;
        }

        protected async Task UploadFiles(InputFileChangeEventArgs e)
        {
            _file = e.File;
            if (_file != null)
            {
               
                fs = await CopyToMemoryStream(); 
                var format = "image/png";
                var imageFile = await e.File.RequestImageFileAsync(format, 400, 400);
                var buffer = new byte[imageFile.Size];
                await imageFile.OpenReadStream().ReadAsync(buffer);
                ImageData = $"data:{format};base64,{Convert.ToBase64String(buffer)}";
                BookModel.Gambar = _file.Name;
              
            }
        }
        private void DeleteAsync()
        {
            BookModel.Gambar = null;
           
        }

        public void Cancel()
        {
          
             MudDialog.Cancel();
        }
        protected override async Task OnInitializedAsync()
        {
            await LoadCategoryDataAsync();
            ImageData = string.IsNullOrEmpty(BookModel.Gambar) ? $"images/default.jpg" : BookModel.Gambar;
        }

        private async Task SaveAsync()
        {
            try
            {

                if (BookModel.ID == 0)
                {
                    await BookService.InsertAsync(BookModel.ToEntity(_mapper));
                }
                else
                {
                    await BookService.UpdateAsync(BookModel.ToEntity(_mapper));
                }
                if (_file != null && !string.IsNullOrEmpty(BookModel.Gambar))
                {
                    await fileUpload.UploadAsync(fs, BookModel.Gambar);
                }

                await BookService.ClearTrackerAsync();
                MudDialog.Close();
            }
            catch (Exception e)
            {
                _snackBar.Add(e.Message, Severity.Error);
            }
          
        }

        private async Task LoadCategoryDataAsync()
        {
            var categories = await CategoryService.GetAllAsNoTrackingAsync();
           
            _categories = categories.Select(c => c.ToModel(_mapper)).ToList(); 
          
          
        }
    }
}
