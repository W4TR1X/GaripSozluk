using GaripSozluk.Data.Domain;
using GaripSozluk.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GaripSozluk.Data.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        private readonly GaripSozlukDbContext _context;
        public CategoryRepository(GaripSozlukDbContext context) : base(context)
        {
            _context = context;
        }


        // Normalde diğer kategorileri DbContext de default olarak vermiştim bu yöntemi denemek istediğim için ekledim.
        public Category GetOrCreate(string title)
        {
            var category = Get(x => x.Title == title);

            if (category == null)
            {
                category = new Category()
                {
                    CreateDate = DateTime.Now,
                    Title = title
                };

                Add(category);
                Save();
            }

            return category;

        }
    }
}
