using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace GaripSozluk.Business.Interfaces
{
    public interface ICategoryService
    {
        IList<KeyValuePair<int, string>> GetCategories();

        string GetCategoryNameById(int categoryId);
        List<SelectListItem> GetCategoriesOptionList();
    }
}