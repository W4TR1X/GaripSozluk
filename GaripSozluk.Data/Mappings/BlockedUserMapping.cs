using GaripSozluk.Data.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GaripSozluk.Data.Mappings
{
    public class BlockedUserMapping : BaseMapping<BlockedUser>
    {
        public override void Configure(EntityTypeBuilder<BlockedUser> builder)
        {
            base.Configure(builder);

            builder.HasOne(x => x.User)
                .WithMany(x => x.BlockedUsers)
                .HasForeignKey(x => x.BlockedUserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Blocked)
                .WithMany(x => x.BlockedBy)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
