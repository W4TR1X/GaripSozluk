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

        public IList<KeyValuePair<int, string>> GetCategories()
        {
            return _categoryRepository.GetAll().Select(x => new KeyValuePair<int, string>(x.Id, x.Title)).ToList();
        }

        //ToDo: OK! Public Servis methodlarına küçük harfle başlanmaz.
        public string GetCategoryNameById(int categoryId)
        {
            return _categoryRepository.Get(x => x.Id == categoryId).Title;
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
