using NUnit.Framework;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace SplitString.Test
{
    [TestFixture]
    public class SplitStringTest
    {
        [Test]
        public void BasicTests()
        {
            Assert.AreEqual(new string[] { "ab", "c_" }, SplitString("abc"));
            Assert.AreEqual(new string[] { "ab", "cd", "ef" }, SplitString("abcdef"));
        }

        [Test]
        public void ExtendedTests()
        {
            var pairs = SplitString("cdabefg");

            Assert.IsNotNull(pairs, "solution did not return a value");
            Assert.AreEqual(4, pairs.Length, "solution did not return an array with enough pairs");
            Assert.AreEqual("cd", pairs[0], "solution did not return pairs with correct values");
            Assert.AreEqual("g_", pairs[3], "solution did not return pairs with correct values");

            pairs = SplitString("abcd");

            Assert.IsNotNull(pairs, "solution did not return a value");
            Assert.AreEqual(2, pairs.Length, "solution did not return an array with correct number of pairs");
            Assert.AreEqual("cd", pairs[1], "last pair in solution is not correct");
        }

        [Test]
        public void RandomTests()
        {
            var rand = new Random();

            Func<string, string[]> mySolution = delegate (string str)
            {
                if (str.Length % 2 == 1)
                {
                    str += "_";
                }
                var arr = new string[str.Length / 2];

                for (var i = 0; i < str.Length; i += 2)
                {
                    arr[i / 2] = str.Substring(i, 2);
                }

                return arr;
            };

            for (int r = 0; r < 40; r++)
            {
                var length = rand.Next(1, 15);
                var str = string.Concat(Enumerable.Range(0, length).Select(a => (char)rand.Next(65, 91)));
                Assert.AreEqual(string.Join(", ", mySolution(str)), string.Join(", ", SplitString(str)));
            }
        }

        public static string[] SplitString(string str)
        {
            return Regex.Matches(str + "_", "..").ToArray().Select(x => x.ToString()).ToArray(); ;
        }
    }
}