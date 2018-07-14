﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductCatalogAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCatalogAPI.Data
{
    public class CatalogContext : DbContext
    {
        public CatalogContext(DbContextOptions options) : 
            base(options)
        {
        }

        public DbSet<CatalogType> CatalogTypes { get; set; }
        public DbSet<CatalogBrand> CatalogBrands { get; set; }
        public DbSet<CatalogItem> CatalogItems { get; set; }


        protected override void OnModelCreating
            (ModelBuilder builder)
        {
            builder.Entity<CatalogBrand>(ConfigureCatalogBrand);
            builder.Entity<CatalogType>(ConfigureCatalogType);
            builder.Entity<CatalogItem>(ConfigureCatalogItem);
        }

        private void ConfigureCatalogItem
            (EntityTypeBuilder<CatalogItem> builder)
        {
            builder.ToTable("Catalog");
            builder.Property(c => c.Id)
                .ForSqlServerUseSequenceHiLo("catalog_hilo")
                .IsRequired();
            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(c => c.Price)
                .IsRequired();
            builder.Property(c => c.PictureUrl)
                .IsRequired(false);

            builder.HasOne(c => c.CatalogBrand)
                .WithMany()
                .HasForeignKey(c => c.CatalogBrandId);

            builder.HasOne(c => c.CatalogType)
                .WithMany()
                .HasForeignKey(c => c.CatalogTypeId);
        }
        private void ConfigureCatalogType
            (EntityTypeBuilder<CatalogType> builder)
        {
            builder.ToTable("CatalogType");
            builder.Property(c => c.Id)
                .ForSqlServerUseSequenceHiLo("catalog_type_hilo")
                .IsRequired();
            builder.Property(c => c.Type)
                .IsRequired()
                .HasMaxLength(100);
        }

        private void ConfigureCatalogBrand
            (EntityTypeBuilder<CatalogBrand> builder)
        {
            builder.ToTable("CatalogBrand");
            builder.Property(c => c.Id)
                .ForSqlServerUseSequenceHiLo("catalog_brand_hilo")
                .IsRequired();
            builder.Property(c => c.Brand)
                .IsRequired()
                .HasMaxLength(100);

        }

    }
}
