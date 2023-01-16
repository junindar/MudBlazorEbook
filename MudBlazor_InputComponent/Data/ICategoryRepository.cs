﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MudBlazor_InputComponent.Data.ViewModel;


namespace MudBlazor_InputComponent.Data
{
  public  interface ICategoryRepository
    {
        Task<List<Category>> GetAll();
        Task<Category> GetById(int categoryId);
    }

   
}
