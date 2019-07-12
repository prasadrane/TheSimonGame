using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Simon.Models
{
    public class SeedData
    {
        /// <summary>
        /// Initializes the specified service provider.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new SimonDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<SimonDbContext>>()))
            {
                // Look for any data recieved from DB.
                if (context.Users.Any())
                {
                    return;   // DB has been seeded
                }

                // Add Schedules
                context.Users.AddRange(
                    new User
                    {
                        Id = 1,
                        Name = "Prasad",
                        Scores = new List<Score>() { new Score(1, DifficultyLevel.Easy, "1,2,4,6,9,11"), new Score(2, DifficultyLevel.Medium, "7,8,4,2"), new Score(12, DifficultyLevel.Hard, "3,7,8,4,2") }
                    },
                    new User
                    {
                        Id = 2,
                        Name = "Pallavi",
                        Scores = new List<Score>() { new Score(3, DifficultyLevel.Hard, "3,7,8,4,2"), new Score(13, DifficultyLevel.Medium, "9,5,7,8,4,2"), new Score(14, DifficultyLevel.Medium, "6,8,4,2") }
                    },
                    new User
                    {
                        Id = 3,
                        Name = "Jacob",
                        Scores = new List<Score>() { new Score(4, DifficultyLevel.Medium, "9,5,7,8,4,2") }
                    },
                    new User
                    {
                        Id = 4,
                        Name = "Ethan",
                        Scores = new List<Score>() { new Score(5, DifficultyLevel.Medium, "6,8,4,2") }
                    },
                    new User
                    {
                        Id = 5,
                        Name = "Elijah",
                        Scores = new List<Score>() { new Score(6, DifficultyLevel.Hard, "3,6,7,8,4,2") }
                    },
                    new User
                    {
                        Id = 6,
                        Name = "Mason",
                        Scores = new List<Score>() { new Score(7, DifficultyLevel.Easy, "1,7,8,4,2") }
                    },
                    new User
                    {
                        Id = 7,
                        Name = "Benjamin",
                        Scores = new List<Score>() { new Score(8, DifficultyLevel.Medium, "2,7,8,4,2") }
                    },
                    new User
                    {
                        Id = 8,
                        Name = "Logan",
                        Scores = new List<Score>() { new Score(9, DifficultyLevel.Hard, "1,6,7,8,4,2") }
                    },
                    new User
                    {
                        Id = 9,
                        Name = "James",
                        Scores = new List<Score>() { new Score(10, DifficultyLevel.Easy, "2,7,8,4,2") }
                    },
                    new User
                    {
                        Id = 10,
                        Name = "Noah",
                        Scores = new List<Score>() { new Score(11, DifficultyLevel.Medium, "4,7,8,4,2") }
                    }
                    );                   

                context.SaveChanges();
            }
        }
    }
}
