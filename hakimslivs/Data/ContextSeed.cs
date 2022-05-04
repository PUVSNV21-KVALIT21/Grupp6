﻿using hakimslivs.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hakimslivs.Data
{
    public class ContextSeed
    {
        public static async Task SeedRolesAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles
            await roleManager.CreateAsync(new IdentityRole(Roles.SuperAdmin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Moderator.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Basic.ToString()));
        }

        public static async Task SeedSuperAdminAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default User
            var defaultUser = new ApplicationUser
            {
                UserName = "superadmin",
                Email = "superadmin@example.com",
                FirstName = "Admin",
                LastName = "Administrator",
                Street = "The Street",
                StreetNumber = 1,
                PostalCode = 12345,
                City = "The city",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "123Pa$$word.");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Basic.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.Moderator.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.SuperAdmin.ToString());
                }

            }
        }
        public static async Task InitializeProductAsync(ApplicationDbContext database)
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
            if (database.Items.Any())
            {
                return;
            }
            string[] itemLines = File.ReadAllLines("Data/Item.csv", Encoding.GetEncoding("ISO-8859-1")).Skip(1).ToArray();

            foreach (string line in itemLines)
            {
                string[] parts = line.Split(';');

                Category? category;
                try
                {
                    category = (Category)Enum.Parse(typeof(Category), parts[0]);
                }
                catch
                {
                    category = null;
                }

                string product = parts[1];
                decimal price = decimal.Parse(parts[2]);
                int stock = int.Parse(parts[3]);
                string description = parts[4];
                string imageURL = parts[5];

                Item i = new Item
                {
                    Category = category,
                    Product = product,
                    Price = price,
                    Stock = stock,
                    Description = description,
                    ImageURL = imageURL
                };

                database.Items.Add(i);
            }

            database.SaveChanges();
        }
    }
}
