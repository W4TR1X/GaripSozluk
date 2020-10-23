using GaripSozluk.Business.Interfaces;
using GaripSozluk.Data.Domain;
using GaripSozluk.Data.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace GaripSozluk.Business.Services
{
    public class CategoryService : ICategoryService
    {
        //ToDo: OK! _categorytRepository yerine _category yapalım
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categorytRepository)
        {
            _categoryRepository = categorytRepository;
        }

        public IList<KeyValuePair<string, string>> GetCategories()
        {
            return _categoryRepository.GetAll().Select(x => new KeyValuePair<string, string>(x.IdCode, x.Title)).ToList();
        }

        //ToDo: OK! Public Servis methodlarına küçük harfle başlanmaz.
        public string GetCategoryNameByIdCode(string categoryCode)
        {
            if (categoryCode == "")
            {
                return _categoryRepository.GetAll().First().Title;
            }
            return _categoryRepository.Get(x => x.IdCode == categoryCode).Title;
        }

        public string GetCategoryIdCodeById(int categoryId)
        {
            if (categoryId == 0)
            {
                return _categoryRepository.GetAll().First().IdCode;
            }
            return _categoryRepository.Get(x => x.Id == categoryId).IdCode;
        }

        public List<SelectListItem> GetCategoriesOptionList()
        {
            var items = new List<SelectListItem>();
            GetCategories().ToList().ForEach(x =>
            {
                items.Add(new SelectListItem(x.Value, x.Key.ToString()));
            });
            return items;
        }


    }
}
