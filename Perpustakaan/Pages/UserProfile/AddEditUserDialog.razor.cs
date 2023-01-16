using AutoMapper;
using Blazored.FluentValidation;

using DataService.IService;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using Perpustakaan.Extensions.AutoMapper;
using Perpustakaan.Model;


namespace Perpustakaan.Pages.UserProfile
{
    public partial class AddEditUserDialog
    {
      
        [Inject] public IUserService UserService { get; set; }
       
        [Inject]
        public IMapper _mapper { get; set; }

        [Parameter] public UserVM UserModel { get; set; } = new();

  
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }

        private FluentValidationValidator _fluentValidationValidator;
        private bool Validated => _fluentValidationValidator.Validate(options => { options.IncludeAllRuleSets(); });

        private async Task LoadDataAsync()
        {

            await Task.CompletedTask;
        }

        public void Cancel()
        {
          
             MudDialog.Cancel();
        }
        protected override async Task OnInitializedAsync()
        {
            await LoadDataAsync();
           
        }

        private async Task SaveAsync()
        {
            try
            {

                if (UserModel.ID == 0)
                {
                    await UserService.InsertAsync(UserModel.ToEntity(_mapper));
                }
                else
                {
                    await UserService.UpdateAsync(UserModel.ToEntity(_mapper));
                }
               

                await UserService.ClearTrackerAsync();
                MudDialog.Close();
            }
            catch (Exception e)
            {
                _snackBar.Add(e.Message, Severity.Error);
            }
          
        }

       
    }
}
