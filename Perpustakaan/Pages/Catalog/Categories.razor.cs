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
    public partial class Categories
    {
        [Inject] public ICategoryService CategoryService { get; set; }

        [Inject]
        public IMapper _mapper { get; set; }
        private List<CategoryVM> _categoryList = new();
        private CategoryVM _category = new();
      
        private bool _loaded;
        private string _searchString = "";

     

        protected override async Task OnInitializedAsync()
        {


            await GetCategoriesAsync();
            _loaded = true;
        }
       
        private async Task GetCategoriesAsync()
        {
            try
            {
                var result = await CategoryService.GetAllAsNoTrackingAsync();
                _categoryList = result.Select(c => c.ToModel(_mapper)).ToList(); //_mapper.Map<List<CategoryVM>>(result);
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
             
                _category = _categoryList.FirstOrDefault(c => c.ID == id);
                if (_category != null)
                {
                    parameters.Add(nameof(AddEditCategoryDialog.CategoryModel), new CategoryVM()
                    {
                        ID = _category.ID,
                        Nama = _category.Nama,
                        Deskripsi = _category.Deskripsi
                    });
                   
                }
            }
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<AddEditCategoryDialog>(id == 0 ? "Create" :"Edit", parameters, options);
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
                    await CategoryService.DeleteAsync(id);
                    _category = _categoryList.FirstOrDefault(c => c.ID == id);
                    _snackBar.Add($"Category {_category.Nama} Deleted", Severity.Success);
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
            _category = new CategoryVM();
            await GetCategoriesAsync();
        }
        private bool Filter(CategoryVM category)
        {
            if (string.IsNullOrWhiteSpace(_searchString)) return true;
            if (category.Nama?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            return category.Deskripsi?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true;
        }
    }
}
