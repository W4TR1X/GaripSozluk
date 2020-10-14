using GaripSozluk.Data.Domain;
using GaripSozluk.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GaripSozluk.Data.Repositories
{
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        private readonly GaripSozlukDbContext _context;
        public PostRepository(GaripSozlukDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
