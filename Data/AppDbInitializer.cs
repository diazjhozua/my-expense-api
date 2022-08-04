using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using my_expense_api.Components.Handlers;
using my_expense_api.Models;

namespace my_expense_api.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder, IWebHostEnvironment env)
        {
            
            using(var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<DataContext>();

                if (!env.IsProduction())
                {
                    if (!context.Users.Any()) {
                        for (int i = 0; i < 5; i++) 
                        {
                            var hmac = new System.Security.Cryptography.HMACSHA512();
                            context.Users.Add(new User() {Email = Faker.Internet.Email(),FirstName = Faker.Name.First(), LastName = Faker.Name.Last(), PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes("sample123")), PasswordSalt = hmac.Key });
                        }
                    }

                    context.SaveChanges();
                }
            }
        }

    }
}