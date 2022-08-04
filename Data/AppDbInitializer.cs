using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using my_expense_api.Models;

namespace my_expense_api.Data
{
    public class AppDbInitializer
    {
        public async static void Seed(IApplicationBuilder applicationBuilder, IWebHostEnvironment env) 
        {
            
            using(var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<DataContext>();

                if (!env.IsProduction())
                {
                    if (!context.Users.Any()) {
                        for (int i = 0; i < 5; i++) 
                        {

                            // context.Categories.Add(new Category() {Name = "asd", Limit = 3200, UserId = 1 });
                            // context.Categories.Add(new Category() {Name = "asd", Limit = 3200, UserId = 1 });
                            // context.Categories.Add(new Category() {Name = "asd", Limit = 3200, UserId = 1 });
                            // context.Categories.Add(new Category() {Name = "asd", Limit = 3200, UserId = 1 });
                            // context.Categories.Add(new Category() {Name = "asd", Limit = 3200, UserId = 1 });
                            // context.Categories.Add(new Category() {Name = "asd", Limit = 3200, UserId = 1 });
                            // context.Categories.Add(new Category() {Name = "asd", Limit = 3200, UserId = 1 });
                        }
                    }

                    context.SaveChanges();
                }
            }
        }
    }
}