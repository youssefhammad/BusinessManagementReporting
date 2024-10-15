using BusinessManagementReporting.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManagementReporting.Infrastructure.Data
{
    public static class DbInitializer
    {
        public static async Task SeedAsync(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            await context.Database.EnsureCreatedAsync();

            if (await context.Clients.AnyAsync())
            {
                return;
            }

            // Seed Branches
            var branches = new List<Branch>
            {
                new Branch
                {
                    Name = "Downtown Spa",
                    Address = "123 Main St",
                    City = "New York",
                    Country = "USA",
                    Phone = "+1-212-555-1234"
                },
                new Branch
                {
                    Name = "Westside Beauty",
                    Address = "456 Oak Ave",
                    City = "Los Angeles",
                    Country = "USA",
                    Phone = "+1-310-555-5678"
                },
                new Branch
                {
                    Name = "Central Wellness",
                    Address = "789 Maple Rd",
                    City = "Chicago",
                    Country = "USA",
                    Phone = "+1-312-555-9012"
                },
                new Branch
                {
                    Name = "Eastside Relaxation",
                    Address = "321 Pine Ln",
                    City = "Boston",
                    Country = "USA",
                    Phone = "+1-617-555-3456"
                },
                new Branch
                {
                    Name = "Southside Retreat",
                    Address = "654 Cedar Blvd",
                    City = "Miami",
                    Country = "USA",
                    Phone = "+1-305-555-7890"
                }
            };

            await context.Branches.AddRangeAsync(branches);

            // Seed Clients
            var clients = new List<Client>
            {
                new Client
                {
                    FirstName = "John",
                    LastName = "Doe",
                    Gender = "Male",
                    Email = "john.doe@email.com",
                    Phone = "+1-555-123-4567",
                    Address = "123 Elm St",
                    City = "New York",
                    Country = "USA",
                    Birthdate = new DateTime(1985, 3, 15)
                },
                new Client
                {
                    FirstName = "Jane",
                    LastName = "Smith",
                    Gender = "Female",
                    Email = "jane.smith@email.com",
                    Phone = "+1-555-234-5678",
                    Address = "456 Oak Ave",
                    City = "Los Angeles",
                    Country = "USA",
                    Birthdate = new DateTime(1990, 7, 22)
                },
                new Client
                {
                    FirstName = "Michael",
                    LastName = "Johnson",
                    Gender = "Male",
                    Email = "michael.johnson@email.com",
                    Phone = "+1-555-345-6789",
                    Address = "789 Pine Rd",
                    City = "Chicago",
                    Country = "USA",
                    Birthdate = new DateTime(1988, 11, 30)
                },
                new Client
                {
                    FirstName = "Emily",
                    LastName = "Brown",
                    Gender = "Female",
                    Email = "emily.brown@email.com",
                    Phone = "+1-555-456-7890",
                    Address = "321 Maple Ln",
                    City = "Boston",
                    Country = "USA",
                    Birthdate = new DateTime(1992, 5, 18)
                },
                new Client
                {
                    FirstName = "David",
                    LastName = "Wilson",
                    Gender = "Male",
                    Email = "david.wilson@email.com",
                    Phone = "+1-555-567-8901",
                    Address = "654 Cedar Blvd",
                    City = "Miami",
                    Country = "USA",
                    Birthdate = new DateTime(1987, 9, 25)
                }
            };

            await context.Clients.AddRangeAsync(clients);

            // Seed Services
            var services = new List<Service>
            {
                new Service
                {
                    Name = "Swedish Massage",
                    Description = "Relaxing full-body massage",
                    Price = 80.00M,
                    Duration = 60
                },
                new Service
                {
                    Name = "Deep Tissue Massage",
                    Description = "Intense massage for muscle tension",
                    Price = 100.00M,
                    Duration = 90
                },
                new Service
                {
                    Name = "Facial Treatment",
                    Description = "Rejuvenating facial with cleansing and moisturizing",
                    Price = 70.00M,
                    Duration = 45
                },
                new Service
                {
                    Name = "Manicure",
                    Description = "Nail care and polish for hands",
                    Price = 40.00M,
                    Duration = 30
                },
                new Service
                {
                    Name = "Pedicure",
                    Description = "Nail care and polish for feet",
                    Price = 50.00M,
                    Duration = 45
                }
            };

            await context.Services.AddRangeAsync(services);

            // Save changes to generate IDs
            await context.SaveChangesAsync();

            // Seed Bookings using navigation properties
            var bookings = new List<Booking>
            {
                new Booking
                {
                    Client = clients[0],
                    Branch = branches[0],
                    BookingDate = new DateTime(2024, 10, 15),
                    BookingTime = new TimeSpan(10, 0, 0),
                    Status = "Confirmed"
                },
                new Booking
                {
                    Client = clients[1],
                    Branch = branches[1],
                    BookingDate = new DateTime(2024, 10, 16),
                    BookingTime = new TimeSpan(14, 30, 0),
                    Status = "Confirmed"
                },
                new Booking
                {
                    Client = clients[2],
                    Branch = branches[2],
                    BookingDate = new DateTime(2024, 10, 17),
                    BookingTime = new TimeSpan(11, 0, 0),
                    Status = "Pending"
                },
                new Booking
                {
                    Client = clients[3],
                    Branch = branches[3],
                    BookingDate = new DateTime(2024, 10, 18),
                    BookingTime = new TimeSpan(15, 0, 0),
                    Status = "Confirmed"
                },
                new Booking
                {
                    Client = clients[4],
                    Branch = branches[4],
                    BookingDate = new DateTime(2024, 10, 19),
                    BookingTime = new TimeSpan(13, 30, 0),
                    Status = "Confirmed"
                }
            };

            await context.Bookings.AddRangeAsync(bookings);
            await context.SaveChangesAsync();

            // Seed BookingServices using navigation properties
            var bookingServices = new List<BookingService>
            {
                new BookingService
                {
                    Booking = bookings[0],
                    Service = services[0],
                    Price = 80.00M
                },
                new BookingService
                {
                    Booking = bookings[0],
                    Service = services[2],
                    Price = 70.00M
                },
                new BookingService
                {
                    Booking = bookings[1],
                    Service = services[1],
                    Price = 100.00M
                },
                new BookingService
                {
                    Booking = bookings[2],
                    Service = services[3],
                    Price = 40.00M
                },
                new BookingService
                {
                    Booking = bookings[2],
                    Service = services[4],
                    Price = 50.00M
                },
                new BookingService
                {
                    Booking = bookings[3],
                    Service = services[0],
                    Price = 80.00M
                },
                new BookingService
                {
                    Booking = bookings[4],
                    Service = services[2],
                    Price = 70.00M
                }
            };

            await context.BookingServices.AddRangeAsync(bookingServices);
            await context.SaveChangesAsync();

            // Seed Transactions using navigation properties
            var transactions = new List<Transaction>
            {
                new Transaction
                {
                    Booking = bookings[0],
                    Amount = 150.00M,
                    PaymentMethod = "Credit Card",
                    PaymentDate = new DateTime(2024, 10, 15, 10, 30, 0)
                },
                new Transaction
                {
                    Booking = bookings[1],
                    Amount = 100.00M,
                    PaymentMethod = "Cash",
                    PaymentDate = new DateTime(2024, 10, 16, 15, 0, 0)
                },
                new Transaction
                {
                    Booking = bookings[3],
                    Amount = 80.00M,
                    PaymentMethod = "Debit Card",
                    PaymentDate = new DateTime(2024, 10, 18, 15, 30, 0)
                },
                new Transaction
                {
                    Booking = bookings[4],
                    Amount = 70.00M,
                    PaymentMethod = "Credit Card",
                    PaymentDate = new DateTime(2024, 10, 19, 14, 0, 0)
                }
            };

            await context.Transactions.AddRangeAsync(transactions);
            await context.SaveChangesAsync();

            await SeedRolesAsync(roleManager);

            await SeedUsersAsync(userManager);
        }

        private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            var roles = new List<string> { "Admin", "User" };

            foreach (var roleName in roles)
            {
                var roleExists = await roleManager.RoleExistsAsync(roleName);
                if (!roleExists)
                {
                    var role = new IdentityRole(roleName);
                    await roleManager.CreateAsync(role);
                }
            }
        }

        private static async Task SeedUsersAsync(UserManager<ApplicationUser> userManager)
        {
            var testUserEmail = "testuser@example.com";
            var testUserPassword = "Test@123";

            var testUser = await userManager.FindByEmailAsync(testUserEmail);
            if (testUser == null)
            {
                testUser = new ApplicationUser
                {
                    UserName = testUserEmail,
                    Email = testUserEmail,
                    EmailConfirmed = true, 
                };

                var result = await userManager.CreateAsync(testUser, testUserPassword);
                if (result.Succeeded)
                {
                    // Assign role if needed
                    await userManager.AddToRoleAsync(testUser, "User");
                }
                else
                {
                    // Handle errors (log them or throw exception)
                    throw new Exception($"Failed to create test user: {string.Join(", ", result.Errors)}");
                }
            }

            // Optionally create an admin user
            var adminUserEmail = "admin@example.com";
            var adminUserPassword = "Admin@123";

            var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminUserEmail,
                    Email = adminUserEmail,
                    EmailConfirmed = true,
                };

                var result = await userManager.CreateAsync(adminUser, adminUserPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
                else
                {
                    throw new Exception($"Failed to create admin user: {string.Join(", ", result.Errors)}");
                }
            }
        }
    }
}
