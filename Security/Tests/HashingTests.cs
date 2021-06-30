using NUnit.Framework;
using Security.Models; 

namespace Tests.Security.Models
{
    /// <summary>
    /// Allows to test methods of Hashing class. 
    /// </summary>
    public class HashingTests 
    {
        #region Members
        /// <summary>
        /// Private field for storing an instance of Hashing class. 
        /// </summary>
        private Hashing hashing; 
        #endregion  // Members

        [SetUp]
        public void Setup()
        {
            hashing = new Hashing(); 
        }

        [Test]
        public void HashFunc_OneStringPassedTwice_SameOutput()
        {
            // Assign. 
            string input = "StringForTestingHashFunc"; 

            // Act. 
            string output1 = hashing.HashFunc(input); 
            string output2 = hashing.HashFunc(input); 

            // Assert. 
            Assert.AreEqual(output1, output2);
        }

        [Test]
        public void HashFunc_SameStringPassed_SameOutput()
        {
            // Assign. 
            string input1 = "StringForTestingHashFunc"; 
            string input2 = "StringForTestingHashFunc"; 

            // Act. 
            string output1 = hashing.HashFunc(input1); 
            string output2 = hashing.HashFunc(input2); 

            // Assert. 
            Assert.AreEqual(output1, output2);
        }

        [Test]
        public void HashFunc_DifferentStringsPassed_DifferentOutput()
        {
            // Assign. 
            string input1 = "StringForTestingHashFunc1"; 
            string input2 = "DiffernetStringForTesting"; 
            string input3 = "AnotherString"; 

            // Act. 
            string output1 = hashing.HashFunc(input1); 
            string output2 = hashing.HashFunc(input2); 
            string output3 = hashing.HashFunc(input3); 

            // Assert. 
            Assert.AreNotEqual(output1, output2);
            Assert.AreNotEqual(output1, output3);
            Assert.AreNotEqual(output2, output3);
        }
    }
}