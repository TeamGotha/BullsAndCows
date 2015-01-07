namespace Engine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using BullsAndCows.Utils;

    public class Scoreboard
    {
        private Dictionary<string, int> results;

        public Scoreboard()
        {
            this.Results = new Dictionary<string, int>();
        }

        public Dictionary<string, int> Results
        {
            get
            {
                return SortResults(this.results.ToList());
            }
            private set { this.results = value; }
        }

        private Dictionary<string, int> SortResults(List<KeyValuePair<string, int>> results)
        {
            results = this.results.ToList();
            results.SortList();
            return results.ToDictionary(k => k.Key, v => v.Value);
        }

        public void AddScore(string name, int score)
        {
            AddScoreInternal(name, score);
        }

        private void AddScoreInternal(string name, int score)
        {
            this.CheckScore(score);
            if (!this.Results.ContainsKey(name))
            {
                this.results.Add(name, score);
            }
            else
            {
                var playerScore = this.Results[name];
                this.results[name] = (playerScore > score ? playerScore : score);
            }
            
        }

        private void CheckScore(int score)
        {
            if (score < 0)
            {
                throw new ArgumentException("Score value must be positive number.");
            }
        }

    }
}
