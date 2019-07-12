using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Simon.Models;

namespace Simon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly SimonDbContext _context;

        private List<User> Users;

        public UsersController(SimonDbContext context)
        {
            _context = context;
            Users = _context.Users
                .Include(c => c.Scores)
                .ToList();
        }

        private bool UserExists(string userName)
        {
            return _context.Users.Any(user => user.Name == userName);
        }

        [HttpPost]
        public async Task<IActionResult> SaveScore(SimonResponseData data)
        {
            if (string.IsNullOrEmpty(data.UserName))
            {
                return BadRequest("Name cannot be empty");
            }
            if (data.Score == 0)
            {
                return BadRequest("Enter a valid score");
            }
            if (data.DifficultyLevel == 0)
            {
                return BadRequest("Enter a valid DifficultyLevel");
            }


            // Check if user exist
            if (!UserExists(data.UserName))
            {
                // Create New User with score
                User newUser = new User
                {
                    Name = data.UserName,
                    Scores = new List<Score>() { new Score(data.DifficultyLevel, data.Score.ToString()) }
                };

                // Add New User into DB
                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();
                return Ok(newUser);
            }
            else
            {
                // Update existing user
                var existingUser = Users.Where(u => u.Name.Equals(data.UserName)).FirstOrDefault();

                // Check if existing score
                var existingScore = existingUser.Scores.Where(s => s.DifficultyLevel == data.DifficultyLevel).FirstOrDefault();
                if (existingScore != null)
                {
                    // Then we have existing score for given difficulty. Update the score in the same 
                    existingScore.PreviousScores += "," + data.Score;
                }
                else
                {
                    // Add new score of given difficulty level
                    existingUser.Scores.Add(new Score(data.DifficultyLevel, data.Score.ToString()));
                }

                _context.Entry(existingUser).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok(existingUser);
            }
        }

        [HttpGet("getGamePattern")]
        public int[] GetPattern()
        {
            return SimonGame.GeneratePattern();
        }

        [HttpGet("GetUserScore")]
        public IActionResult GetUserScore(string userName, DifficultyLevel difficultyLevel)
        {
            List<string> userScores = new List<string>();

            if (UserExists(userName))
            {
                var existingUser = Users.Where(u => u.Name.Equals(userName)).FirstOrDefault();
                var score = existingUser.Scores.Where(s => s.DifficultyLevel == difficultyLevel).FirstOrDefault();
                if (score != null)
                {
                    userScores = score.PreviousScores.Split(",").ToList();
                }
            }
            else
            {
                return NotFound();
            }

            return Ok(userScores.ToArray());
        }

        [HttpGet("getTopScorers/")]
        public List<SimonResponseData> GetTopFiveUsers(DifficultyLevel difficultyLevel)
        {
            List<SimonResponseData> userWithScores = new List<SimonResponseData>();

            // Get all users as per given difficulty level
            var difficultyLevelUsers = _context.Users.Where(u => u.Scores.Any(s => s.DifficultyLevel == difficultyLevel));

            // Get top 5 users with Highest scores
            var sortedUsersByScores = difficultyLevelUsers.OrderByDescending(u => u.GetTopScoreByDifficultyLevel(difficultyLevel));

            // Get top 5 users
            foreach (var user in sortedUsersByScores.Take(5))
            {
                SimonResponseData data = new SimonResponseData
                {
                    UserName = user.Name,
                    DifficultyLevel = difficultyLevel,
                    Score = user.GetTopScoreByDifficultyLevel(difficultyLevel)
                };

                userWithScores.Add(data);
            }

            return userWithScores;
        }
    }
}