using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simon.Models
{
    public class SimonResponseData
    {
        public string UserName { get; set; }
        public DifficultyLevel DifficultyLevel { get; set; }
        public int Score { get; set; }
    }
}
