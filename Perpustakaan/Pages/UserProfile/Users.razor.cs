using AutoMapper;
using DataService.IService;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Perpustakaan.Extensions.AutoMapper;
using Perpustakaan.Model;


namespace Perpustakaan.Pages.UserProfile
{
    public partial class Users
    {
        [Inject] public IUserService UserService { get; set; }

        [Inject]
        public IMapper _mapper { get; set; }
        private List<UserVM> _userList = new();
        private UserVM _user = new();
      
        private bool _loaded;
        private string _searchString = "";

     

        protected override async Task OnInitializedAsync()
        {

            await GetUsersAsync();
            _loaded = true;
        }


        private async Task GetUsersAsync()
        {
            try
            {
                var result = await UserService.GetAllAsNoTrackingAsync();
                _userList = result.Select(c => c.ToModel(_mapper)).ToList();
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

                _user = _userList.FirstOrDefault(c => c.ID == id);
                if (_user != null)
                {
                    parameters.Add(nameof(AddEditUserDialog.UserModel), new UserVM()
                    {
                        ID = _user.ID,
                        Username = _user.Username,
                        Password = _user.Password,
                        Status = _user.Status,
                        Role = _user.Role,
                        Nama = _user.Nama,
                        ProfilePicture = _user.ProfilePicture
                    });

                }
            }
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<AddEditUserDialog>(id == 0 ? "Create" : "Edit", parameters, options);
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
                    await UserService.DeleteAsync(id);
                    _user = _userList.FirstOrDefault(c => c.ID == id);
                    _snackBar.Add($"User : {_user.Username} Deleted", Severity.Success);
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
            _user = new UserVM();
            await GetUsersAsync();

        }
        private bool Filter(UserVM book)
        {
            if (string.IsNullOrWhiteSpace(_searchString)) return true;
            if (book.Username?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            return book.Nama.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true;
         
        }
    }
}
