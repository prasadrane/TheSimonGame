using System.Collections.Generic;

namespace Simon.Models
{
    public class Score
    {
        public int Id { get; set; }
        public DifficultyLevel DifficultyLevel { get; set; }
        public string PreviousScores { get; set; }

        public Score()
        {

        }

        public Score(DifficultyLevel difficultyLevel, string scores)
        {
            DifficultyLevel = difficultyLevel;
            PreviousScores = scores;
        }

        public Score(int id, DifficultyLevel difficultyLevel, string scores)
            : this(difficultyLevel, scores)
        {
            Id = id;
        }
    }
}