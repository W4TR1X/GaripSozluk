using GaripSozluk.Data.Domain;
using GaripSozluk.Data.Domain.Interfaces;
using GaripSozluk.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace GaripSozluk.Data.Interfaces
{
    public class BlockedUserRepository : BaseRepository<BlockedUser>, IBlockedUserRepository
    {
        public BlockedUserRepository(GaripSozlukDbContext context) : base(context)
        {
        }

    }
}
