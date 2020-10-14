using GaripSozluk.Data.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GaripSozluk.Data.Mappings
{
    public class HeaderMapping : BaseMapping<Header>
    {
        public override void Configure(EntityTypeBuilder<Header> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Title)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.ClickCount)
                .IsRequired();

            builder.HasOne(x => x.User)
                .WithMany(x => x.Headers)
                .HasForeignKey(x => x.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
         
            builder.HasOne(x => x.Category)
                .WithMany(x => x.Headers)
                .HasForeignKey(x => x.CategoryId)
                .IsRequired();
        }
    }    
}        
         