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
                        var hmac = new System.Security.Cryptography.HMACSHA512();
                        context.Users.Add(new User() {Email = "sample@gmail.com",FirstName = Faker.Name.First(), LastName = Faker.Name.Last(), PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes("sample123")), PasswordSalt = hmac.Key });
                    }

                    context.SaveChanges();

                    if (!context.Categories.Any()) {
                        
                        context.Categories.Add(new Category{Name= "School", Limit=3200, UserId=1});
                        context.Categories.Add(new Category{Name= "General", Limit=3200, UserId=1});
                        context.Categories.Add(new Category{Name= "Work", Limit=3200, UserId=1});
                        context.Categories.Add(new Category{Name= "Hobby", Limit=3200, UserId=1});
                        context.Categories.Add(new Category{Name= "Emergency", Limit=3200, UserId=1});
                    }

                    context.SaveChanges(); 

                    if (!context.Expenses.Any()) {
                        for (int i = 0; i < 100; i++) 
                        {
                            var types = new[] { ExpenseType.Cash, ExpenseType.Cheque, ExpenseType.CreditCard, ExpenseType.DebitCard };
                            Random rnd = new Random();
                            DateTime date = new DateTime(2022, 1, 1);
                            int range = (DateTime.Today - date).Days;
                            context.Expenses.Add(new Expense{Name= Faker.Lorem.Sentence(2), 
                                Cost= Faker.RandomNumber.Next(1, 10000), 
                                Description = Faker.Lorem.Paragraph(3), Type = types[rnd.Next(types.Length)], 
                                UserId = 1, CategoryId = Faker.RandomNumber.Next(1,5), Date = date.AddDays(rnd.Next(range))});
                        }
                    }

                    context.SaveChanges();
                }
            }
        }

    }
}