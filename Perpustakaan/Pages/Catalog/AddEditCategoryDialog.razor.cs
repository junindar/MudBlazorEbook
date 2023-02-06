using AutoMapper;
using Blazored.FluentValidation;
using DataService.Entities;
using DataService.IService;
using Microsoft.AspNetCore.Components;

using MudBlazor;
using Perpustakaan.Extensions.AutoMapper;
using Perpustakaan.Model;

namespace Perpustakaan.Pages.Catalog
{
    public partial class AddEditCategoryDialog
    {

        [Inject] public ICategoryService CategoryService { get; set; }

        [Inject]
        public IMapper _mapper { get; set; }

        [Parameter] public CategoryVM CategoryModel { get; set; } = new();
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }

        private FluentValidationValidator _fluentValidationValidator;
        private bool Validated => _fluentValidationValidator.Validate(options => { options.IncludeAllRuleSets(); });

        public void Cancel()
        {
            MudDialog.Cancel();
        }
        //protected override async Task OnInitializedAsync()
        //{
        //    await LoadDataAsync();

        //}
        private async Task SaveAsync()
        {
            try
            {

                if (CategoryModel.ID == 0)
                {
                    await CategoryService.InsertAsync(CategoryModel.ToEntity(_mapper));
                }
                else
                {
                    await CategoryService.UpdateAsync(CategoryModel.ToEntity(_mapper));
                }
                MudDialog.Close();
            }
            catch (Exception e)
            {
                _snackBar.Add(e.Message, Severity.Error);
            }

        }

        //private async Task LoadDataAsync()
        //{

        //    await Task.CompletedTask;
        //}
    }
}
