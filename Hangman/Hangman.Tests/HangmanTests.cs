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
        public void StartGame_GameIsInCorrectState()
        {
            Game game = new Game();
            Assert.AreEqual(game.Topics.Count, 3);
        }

        [Test]
        [TestCase("Fruits")]
        [TestCase("Animals")]
        [TestCase("Countries")]
        public void StartGame_HasTheCorrectTopics(string topic)
        {
            Game game = new Game();
            Assert.IsTrue(game.Topics.ContainsKey(topic));
        }

        [Test]
        [TestCase("Fruits")]
        [TestCase("Animals")]
        [TestCase("Countries")]
        public void StartGame_EachTopicHasAtLeast5DifferentWords(string topic)
        {
            Game game = new Game();
            Assert.GreaterOrEqual(game.Topics[topic].Count, 5);

        }
    }
}