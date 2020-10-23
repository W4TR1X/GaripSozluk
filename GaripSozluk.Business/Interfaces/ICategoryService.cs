using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace GaripSozluk.Business.Interfaces
{
    public interface ICategoryService
    {
        IList<KeyValuePair<string, string>> GetCategories();

        string GetCategoryNameByIdCode(string categoryCode);
        string GetCategoryIdCodeById(int categoryId);
        List<SelectListItem> GetCategoriesOptionList();
    }
}