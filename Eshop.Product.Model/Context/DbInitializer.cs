using System;
using Eshop.Products.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Products.Model.Context
{
    public static class DbInitializer
    {
        private static bool _isInit;

        internal static void Init(ModelBuilder modelBuilder)
        {
            if (_isInit)
                return;
            _isInit = true;

            modelBuilder.Entity<ProductGroup>()
                .HasData(new[]
                {
                    new ProductGroup
                    {
                        Id = 1,
                        Created = DateTime.Parse("2010-12-10T10:02:33"),
                        Name = "Автомобили"
                    },
                    new ProductGroup
                    {
                        Id = 2,
                        Created = DateTime.Parse("2011-11-10T10:02:33"),
                        Name = "Компьютеры"
                    },
                    new ProductGroup
                    {
                        Id = 3,
                        Created = DateTime.Parse("2020-10-10T10:02:33"),
                        Name = "Бытовая техника"
                    },
                });

            modelBuilder.Entity<Entities.Product>()
                .HasData(new[]
                {
                    new Entities.Product
                    {
                        Id = 1,
                        Created = DateTime.Parse("2010-12-10T10:02:33"),
                        Name = "Мазда",
                        ProductGroupId = 1,
                        Description = "Быстрая и надежная",
                        TotalCount = 100,
                        AvailableCount = 100,
                    },
                    new Entities.Product
                    {
                        Id = 2,
                        Created = DateTime.Parse("2010-12-10T10:02:33"),
                        Name = "LADA GRANTA",
                        ProductGroupId = 1,
                        Description = "Это автоваз",
                        TotalCount = 150,
                        AvailableCount = 150,
                    },
                    new Entities.Product
                    {
                        Id = 3,
                        Created = DateTime.Parse("2010-12-10T10:02:33"),
                        Name = "LADA Vesta",
                        ProductGroupId = 1,
                        Description = "Надежда отечественного автопрома",
                        TotalCount = 250,
                        AvailableCount = 250,
                    },
                });
        }
    }
}
