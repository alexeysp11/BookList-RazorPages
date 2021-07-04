using System;
using Xunit;
using BookList.Models; 

namespace Tests.BookList.Models
{
    public class BookTests
    {
        [Fact]
        public void Initilize_UseParameterizedConstructor_ValuesTheSame()
        {
            //Given
            string name = "Default name"; 
            string author = "Default author"; 
            string description = "Default description"; 
            
            //When
            Book book = new Book(name, author, description); 
            
            //Then
            Assert.True(book != null); 
            Assert.Equal(book.Name, name); 
            Assert.Equal(book.Author, author); 
            Assert.Equal(book.Desciption, description); 
        }

        [Fact]
        public void Initilize_UseDefaultConstructor_ValuesTheSame()
        {
            //Given
            string name = "Default name"; 
            string author = "Default author"; 
            string description = "Default description"; 
            
            //When
            Book book = new Book();
            book.Name = name; 
            book.Author = author;
            book.Desciption = description;  
            
            //Then
            Assert.True(book != null); 
            Assert.Equal(book.Name, name); 
            Assert.Equal(book.Author, author); 
            Assert.Equal(book.Desciption, description); 
        }
    }
}