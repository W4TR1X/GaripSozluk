using GaripSozluk.Data.Domain;
using GaripSozluk.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GaripSozluk.Data.Repositories
{
    public class HeaderRepository : BaseRepository<Header>, IHeaderRepository
    {
        private readonly GaripSozlukDbContext _context;
        public HeaderRepository(GaripSozlukDbContext context) : base(context)
        {
            _context = context;
        }
    }
}

    