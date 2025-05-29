using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Repository.Data.Config
{
    internal class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> productBuilder)
        {
            productBuilder.Property(P => P.Name)
                .IsRequired()
                .HasMaxLength(100);
            productBuilder.Property(P => P.Description)
                .IsRequired();
            productBuilder.Property(P => P.PictureUrl)
                .IsRequired();
            productBuilder.Property(P => P.Price)
                .HasColumnType("decimal(18,2)");

            // Relationships
            productBuilder.HasOne(P => P.Brand)
                .WithMany()
                .HasForeignKey(P => P.BrandId);

            productBuilder.HasOne(P => P.Category)
                .WithMany()
                .HasForeignKey(P => P.CategoryId);
        }
    }
}
