using System.Reflection;
using AutoMapper;
using DataService.Entities;
using Perpustakaan.Model;

namespace Perpustakaan.Extensions.AutoMapper
{
    public static  class MappingExtension
    {
        public static Category ToEntity(this CategoryVM model, IMapper mapper)
        {
            var entity = mapper.Map<Category>(model);

            return entity;
        }

        public static CategoryVM ToModel(this Category entity, IMapper mapper)
        {
            var model = mapper.Map<CategoryVM>(entity);
          

            return model;
        }
        public static Book ToEntity(this BookVM model, IMapper mapper)
        {
            var entity = mapper.Map<Book>(model);
            if (!string.IsNullOrEmpty(model.Gambar) && !model.Gambar.Contains("images/"))
            {
                entity.Gambar = $"images/{model.Gambar}";
            }
            return entity;
        }

        public static BookVM ToModel(this Book entity, IMapper mapper)
        {
            var model = mapper.Map<BookVM>(entity);
            model.Gambar = string.IsNullOrEmpty(entity.Gambar) ? $"images/default.jpg" : $"{model.Gambar}";
           

            return model;
        }
        public static User ToEntity(this UserVM model, IMapper mapper)
        {
            var entity = mapper.Map<User>(model);
            if (!string.IsNullOrEmpty(model.ProfilePicture) && !model.ProfilePicture.Contains("images/profilepicture/"))
            {
                entity.ProfilePicture = $"images/profilepicture/{model.ProfilePicture}";
            }

            return entity;
        }

        public static UserVM ToModel(this User entity, IMapper mapper)
        {
            var model = mapper.Map<UserVM>(entity);
            model.ProfilePicture = string.IsNullOrEmpty(entity.ProfilePicture) ? $"images/profilepicture/default.jpg" : $"{model.ProfilePicture}";


            return model;
        }
    }
    

}
