using CyberCareWeb.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace CyberCareWeb.Infrastructure
{
    public static class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider, ILogger logger)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                try
                {
                    // Проверка, существует ли база данных
                    if (context.Database.EnsureCreated())
                    {
                        // Инициализация данных, если база пуста
                        if (!context.ComponentTypes.Any())
                        {
                            AddComponentTypes(context);
                        }

                        if (!context.Components.Any())
                        {
                            AddComponents(context);
                        }

                        if (!context.Orders.Any())
                        {
                            AddOrders(context);
                        }

                        logger.LogInformation("Database has been initialized successfully.");
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred while initializing the database.");
                }
            }
        }

        private static void AddComponentTypes(AppDbContext context)
        {
            var componentTypes = new[]
            {
                new ComponentType { Id = Guid.NewGuid(), Name = "Процессор", Description = "Электронное устройство для обработки данных." },
                new ComponentType { Id = Guid.NewGuid(), Name = "Оперативная память", Description = "Память для хранения данных во время работы компьютера." },
                new ComponentType { Id = Guid.NewGuid(), Name = "Жесткий диск", Description = "Устройство для долговременного хранения данных." }
            };
            context.ComponentTypes.AddRange(componentTypes);
            context.SaveChanges();
        }

        private static void AddComponents(AppDbContext context)
        {
            var components = new[]
            {
                new Component { Id = Guid.NewGuid(), ComponentTypeId = context.ComponentTypes.First().Id, Brand = "Intel", Manufactorer = "Intel", Specifications = "Intel Core i9", WarrantyPeriod = 24, Price = 500 },
                new Component { Id = Guid.NewGuid(), ComponentTypeId = context.ComponentTypes.Skip(1).First().Id, Brand = "Corsair", Manufactorer = "Corsair", Specifications = "16GB DDR4", WarrantyPeriod = 12, Price = 80 },
                new Component { Id = Guid.NewGuid(), ComponentTypeId = context.ComponentTypes.Skip(2).First().Id, Brand = "Seagate", Manufactorer = "Seagate", Specifications = "1TB HDD", WarrantyPeriod = 36, Price = 100 }
            };
            context.Components.AddRange(components);
            context.SaveChanges();
        }

        private static void AddOrders(AppDbContext context)
        {
            var orders = new[]
            {
                new Order { Id = Guid.NewGuid(), OrderDate = DateTime.Now.AddDays(-10), PaymentStatus = true, CompletionStatus = false, TotalCost = 580, WarrantyPeriod = 24 },
                new Order { Id = Guid.NewGuid(), OrderDate = DateTime.Now.AddDays(-5), PaymentStatus = true, CompletionStatus = true, TotalCost = 180, WarrantyPeriod = 12 }
            };
            context.Orders.AddRange(orders);
            context.SaveChanges();
        }
    }
}
