using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace Perpustakaan.Model
{
    public class SearchVM
    {
      
        public string SearchString { get; set; }

     

    }
    public class LoginVM
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

    }
    public class LoginValidator : AbstractValidator<LoginVM>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Username).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
           


        }
    }
    public class ChangePasswordVM
    {
        [Required]
        public string Password { get; set; }

        [Required]
       // [Compare("ConfirmNewPassword")]
        public string NewPassword { get; set; }

        [Required]
        public string ConfirmNewPassword { get; set; }
    }
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordVM>
    {
        public ChangePasswordValidator()
        {
           
            RuleFor(x => x.Password).NotEmpty();
            RuleFor(x => x.NewPassword).NotEmpty();
            RuleFor(x => x.ConfirmNewPassword).NotEmpty();
            RuleFor(x => x.ConfirmNewPassword).Equal(x => x.NewPassword).WithMessage("The Confirm Password and  New Password does not match with");

        }
    }

}
