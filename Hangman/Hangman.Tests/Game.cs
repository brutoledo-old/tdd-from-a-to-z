using System.Collections.Generic;

namespace Hangman.Tests
{
    public class Game
    {
        public Game()
        {
            Topics = new Dictionary<string, List<string>>();
            Topics.Add("Fruits", new List<string>() { "Apple", "Watermelon", "Banana", "Grape", "Blueberry" });
            Topics.Add("Animals", new List<string>() { "Tiger", "Cat", "Dog", "Whale", "Lion" });
            Topics.Add("Countries", new List<string>() { "Denmark", "Germany", "Brazil", "Portugal", "United States" });
        }

        public Dictionary<string, List<string>> Topics { get; internal set; }
    }
}