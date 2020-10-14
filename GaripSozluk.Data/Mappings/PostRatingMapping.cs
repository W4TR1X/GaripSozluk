﻿using GaripSozluk.Data.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GaripSozluk.Data.Mappings
{
    public class PostRatingMapping : BaseMapping<PostRating>
    {
        public override void Configure(EntityTypeBuilder<PostRating> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.IsLiked)
                .IsRequired();



            builder.HasOne(x => x.Post)
                .WithMany(x => x.Ratings)
                .HasForeignKey(x => x.PostId)
                .IsRequired();

            builder.HasOne(x => x.User)
                .WithMany(x => x.Ratings)
                .HasForeignKey(x => x.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
