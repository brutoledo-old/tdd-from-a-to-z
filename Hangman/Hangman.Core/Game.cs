using System;
using System.Collections.Generic;
using Hangman.Core.Extensions;

namespace Hangman.Core
{
    public class Game
    {
        public Game()
        {
            Topics = new Dictionary<string, List<string>>();
            Topics.Add("Fruits", new List<string>() { "APPLE", "WATERMELON", "BANANA", "GRAPE", "BLUEBERRY" });
            Topics.Add("Animals", new List<string>() { "TIGER", "CAT", "DOG", "WHALE", "LION" });
            Topics.Add("Countries", new List<string>() { "DENMARK", "GERMANY", "BRAZIL", "PORTUGAL", "UNITED STATES" });
        }

        public Dictionary<string, List<string>> Topics { get; internal set; }

        public string SecretWord { get; internal set; }

        public double RemainingAttempts { get; set; }

        public List<char> RemainingSecretLetters { get; internal set; }

        public List<char> UsedLetters { get; set; }

        public void SelectTopic(string topic)
        {
            if (!Topics.ContainsKey(topic))
                throw new ArgumentException($"Could not find a topic with name: {topic}.");

            SecretWord = Topics[topic].PickRandom();
            RefreshGame();
        }

        private void RefreshGame()
        {
            RemainingSecretLetters = new List<char>(SecretWord);
            RemainingAttempts = 6;
            UsedLetters = new List<char>();
        }

        public void PickLetter(char c)
        {
            if (RemainingAttempts < 1)
                throw new InvalidOperationException($"You cannot pick any more letter as you have no more remaining attempts");

            if (RemainingSecretLetters.Count < 1)
                throw new InvalidOperationException($"You cannot pick any more letter as there are no remaining letters");

            UsedLetters.Add(c);

            var upperChar = char.ToUpper(c);
            if (SecretWord.Contains(upperChar))
            {
                RemainingSecretLetters.Remove(upperChar);
            }
            else
            {
                RemainingAttempts--;
            }
        }
    }
}