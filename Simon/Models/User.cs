using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Simon.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please provide Name")]
        public string Name { get; set; }

        public List<Score> Scores { get; set; }

        public int GetTopScoreByDifficultyLevel(DifficultyLevel difficultyLevel)
        {
            int topScore = 0;
            var difficulyLevelScore = Scores.Where(s => s.DifficultyLevel == difficultyLevel).FirstOrDefault();
            var stringScores = difficulyLevelScore.PreviousScores.Split(",");
            List<int> integerScores = new List<int>();
            foreach (var strScorce in stringScores)
            {
                int.TryParse(strScorce, out int intScore);
                integerScores.Add(intScore);
            }

            var sortedScores = integerScores.OrderByDescending(i => i);
            topScore = sortedScores.First();

            return topScore;
        }
    }
}
