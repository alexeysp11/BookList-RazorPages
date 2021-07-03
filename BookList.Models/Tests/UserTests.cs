using System;
using Xunit;
using BookList.Models; 

namespace Tests
{
    public class UserTests
    {
        [Fact]
        public void Initilize_SetProperties_GetSameValues()
        {
            // Arrange. 
            string fullname = "DefaultUser"; 
            string country = "SomeCountry"; 
            string city = "SomeCity"; 

            // Act. 
            User user = new User() 
            {
                Fullname = fullname, Country = country, City = city
            };

            // Assert. 
            Assert.Equal(user.Fullname, fullname); 
            Assert.Equal(user.Country, country); 
            Assert.Equal(user.City, city); 
        }
    }
}
