using GaripSozluk.Data.Domain;
using GaripSozluk.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GaripSozluk.Data.Repositories
{
    public class AppUserRepository : BaseRepository<AppUser>, IAppUserRepository
    {
        private readonly GaripSozlukDbContext _context;
        public AppUserRepository(GaripSozlukDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
