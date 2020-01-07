using System;
using Hangman.Core;
using NUnit.Framework;

namespace Hangman.Tests
{
    [TestFixture]
    public class HangmanTests
    {
        //
        //   ________
        //  |       |
        //  |       O
        //  |     / | \
        //  |      / \
        //  |
        //vv|vvvvvvvvvvvvvvvvv
        //
        //  _   _   _   _   _
        //
        // Game Steps:
        // - Select a Topic (Fruits, Animals, Countries)
        // - Game randomly generates a word (from Topic) to be discovered
        // - Game exposes the number of letters (length)
        // - The player has 6 attempts to find the correct word otherwise the man is hanged in high
        //
        // Rules:
        // - The game must expose the already used letters (correct or wrong)

        [Test]
        [TestCase("Fruits")]
        [TestCase("Animals")]
        [TestCase("Countries")]
        public void StartGame_GameIsInCorrectState(string topic)
        {
            Game game = new Game();
            Assert.AreEqual(3, game.Topics.Count);
            Assert.IsTrue(game.Topics.ContainsKey(topic));
        }

        [Test]
        [TestCase("Fruits")]
        [TestCase("Animals")]
        [TestCase("Countries")]
        public void StartGame_EachTopicHasAtLeast5DifferentWords(string topic)
        {
            Game game = new Game();

            Assert.GreaterOrEqual(5, game.Topics[topic].Count);
        }

        [Test]
        [TestCase("Fruits")]
        [TestCase("Animals")]
        [TestCase("Countries")]
        public void Game_RandomlyGeneratesTheTopicWord(string topic)
        {
            Game game = new Game();

            game.SelectTopic(topic);

            Assert.IsNotEmpty(game.SecretWord);
            Assert.Contains(game.SecretWord, game.Topics[topic]);
        }

        [Test]
        public void Game_SelectTopic_InvalidTopic_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Game game = new Game();

                game.SelectTopic("Cars");
            });
        }


        [Test]
        public void Game_SelectTopic_Refreshes_The_Game()
        {
            Game game = new Game();
            
            game.SelectTopic("Fruits");
            game.PickLetter('Z');
            Assert.Contains('Z', game.UsedLetters);

            game.SelectTopic("Animals");
            Assert.IsEmpty(game.UsedLetters);
            Assert.AreEqual(6, game.RemainingAttempts);
            Assert.AreEqual(game.SecretWord.Length, game.RemainingSecretLetters.Count);
        }
        
        [Test]
        public void Game_PickLetter_Updates_UsedLetters()
        {
            Game game = new Game();
            game.SelectTopic("Fruits");
            game.PickLetter('Z');

            Assert.Contains('Z', game.UsedLetters);
        }

        [Test]
        public void Game_PickLetter_Invalid_Letter()
        {
            Game game = new Game();
            game.SelectTopic("Fruits");
            game.PickLetter('Z');

            Assert.AreEqual(5, game.RemainingAttempts);
            Assert.Contains('Z', game.UsedLetters);
        }

        [Test]
        public void Game_PickLetter_Case_Insentitive()
        {
            Game game = new Game();
            game.SelectTopic("Fruits");

            var capitalLetter = game.SecretWord[0];
            game.PickLetter(Char.ToUpper(capitalLetter));
            game.PickLetter(Char.ToLower(capitalLetter));

            Assert.AreEqual(6, game.RemainingAttempts);
            Assert.AreEqual(2, game.UsedLetters.Count);
        }

        [Test]
        public void Game_PickLetter_Without_Remaining_Attempts()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                Game game = new Game();
                game.SelectTopic("Fruits");
                game.PickLetter('Z');
                game.PickLetter('X');
                game.PickLetter('J');
                game.PickLetter('V');
                game.PickLetter('S');
                game.PickLetter('C');
                game.PickLetter('K');
            });
        }
        
        [Test]
        public void Game_PickLetter_Valid_Letter()
        {
            Game game = new Game();
            game.SelectTopic("Fruits");

            var capitalLetter = game.SecretWord[0];
            game.PickLetter(capitalLetter);

            Assert.AreEqual(6, game.RemainingAttempts);
            Assert.Less(game.RemainingSecretLetters.Count, game.SecretWord.Length);
        }

        [Test]
        public void Game_PickLetter_Without_RemainingSecretLetters()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                Game game = new Game();
                game.SelectTopic("Fruits");

                for (int i = 0; i < game.SecretWord.Length; i++)
                {
                    game.PickLetter(game.SecretWord[i]);
                }

                game.PickLetter('A');
            });
        }

    }
}