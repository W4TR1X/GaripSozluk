using GaripSozluk.Data.Domain;
using GaripSozluk.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GaripSozluk.Data.Repositories
{
    public class AppRoleRepository : BaseRepository<AppRole>, IAppRoleRepository
    {
        private readonly GaripSozlukDbContext _context;
        public AppRoleRepository(GaripSozlukDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
