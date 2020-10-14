using GaripSozluk.Data.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GaripSozluk.Data.Mappings
{
    public class PostMapping : BaseMapping<Post>
    {
        public override void Configure(EntityTypeBuilder<Post> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Content)
                .HasMaxLength(2000)
                .IsRequired();



            builder.HasOne(x => x.Header)
                .WithMany(x => x.Posts)
                .HasForeignKey(x => x.HeaderId)
                .IsRequired();

            builder.HasOne(x => x.User)
                .WithMany(x => x.Posts)
                .HasForeignKey(x => x.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
