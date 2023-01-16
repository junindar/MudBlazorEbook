using AutoMapper;
using DataService.Entities;
using FluentValidation;
using Perpustakaan.Extensions.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perpustakaan.Model
{
    public class UserVM : IMapFrom<User>
    {
        public int ID { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Nama { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Role { get; set; }
        [Required]
        public bool Status { get; set; }
        public string ProfilePicture { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserVM>().ReverseMap();

        }
    }

    public class ProfileVM
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Nama { get; set; }
        [Required]
        public string Role { get; set; }
        public string ProfilePicture { get; set; }
       
    }
    public class UserValidator : AbstractValidator<UserVM>
    {
        public UserValidator()
        {
            RuleFor(x => x.Username).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
            RuleFor(x => x.Nama).NotEmpty(); RuleFor(x => x.Role).NotEmpty();
          


        }
    }
}
