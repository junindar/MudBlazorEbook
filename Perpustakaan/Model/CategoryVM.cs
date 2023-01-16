using AutoMapper;
using Perpustakaan.Extensions.AutoMapper;
using System.ComponentModel.DataAnnotations;
using DataService.Entities;
using FluentValidation;

namespace Perpustakaan.Model
{
    public class CategoryVM : IMapFrom<Category>
    {
        public int ID { get; set; }
        [Required] 
      
        public string Nama { get; set; }
       
        public string Deskripsi { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Category, CategoryVM>().ReverseMap();

        }
    }
    public class CategoryValidator : AbstractValidator<CategoryVM>
    {
        public CategoryValidator()
        {
            RuleFor(x => x.Nama).NotEmpty().WithMessage("Please specify a category name");

        }
    }
}
