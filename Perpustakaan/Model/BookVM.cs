using AutoMapper;
using Perpustakaan.Extensions.AutoMapper;
using System.ComponentModel.DataAnnotations;
using DataService.Entities;
using FluentValidation;

namespace Perpustakaan.Model
{
    public class BookVM : IMapFrom<Book>
    {
        public int ID { get; set; }
        [Required]
        public string Judul { get; set; }
        [Required]
        public string Penulis { get; set; }
        public string Deskripsi { get; set; }
        [Required]
        public string Penerbit { get; set; }
        public bool Status { get; set; }
        public string Gambar { get; set; }
        [Required]
        public int CategoryID { get; set; }

        public CategoryVM Category { get; set; }// = new CategoryVM();
      
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Book, BookVM>().ReverseMap();

        }
    }

   
    public class BookValidator : AbstractValidator<BookVM>
    {
        public BookValidator()
        {
            RuleFor(x => x.Judul).NotEmpty();
            RuleFor(x => x.Penulis).NotEmpty();
            RuleFor(x => x.Penerbit).NotEmpty();
            RuleFor(x => x.CategoryID).NotEmpty().WithMessage("'Kategory' must not be empty");
           

        }
    }
}
