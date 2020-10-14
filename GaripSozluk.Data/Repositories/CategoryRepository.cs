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
    }
}
