using FluentAssertions;
using System.Linq;
using System.Text;
using WebApi.Data;
using WebApi.Models;
using Xunit;

namespace WebApi.Tests.Data.Text2ListStudents
{
    public class ToStudentsTests
    {
        private readonly IText2ListStudents service;

        private readonly Student studentExpected = new Student
        {
            Name = "Peter Parker",
            Gender = "M",
            Age = 26,
            Study = "biochemistry",
            AcademicYear = 2
        };

        public ToStudentsTests()
        {
            service = new WebApi.Data.Text2ListStudents();
        }

        [Fact]
        public void When_SendCorrectStructure_Then_ReturnCorrectModel()
        {
            var studentText = "Peter Parker,M,26,biochemistry,2";
            var students = service.ToStudents(studentText);
            students.Count().Should().Be(1);
            students.First().Should().BeEquivalentTo(studentExpected);
        }

        [Fact]
        public void When_SendMultiplesCorrectStructure_Then_ReturnCorrectListModels()
        {
            var studentsText = new StringBuilder();
            studentsText.AppendLine("Peter Parker,M,26,biochemistry,2");
            studentsText.AppendLine("Jean Grey,F,28,psychology,2");
            var secondStudent = new Student
            {
                Name = "Jean Grey",
                Gender = "F",
                Age = 28,
                Study = "psychology",
                AcademicYear = 2
            };

            var students = service.ToStudents(studentsText.ToString());

            students.Count().Should().Be(2);
            students.First().Should().BeEquivalentTo(studentExpected);
            students.Last().Should().BeEquivalentTo(secondStudent);
        }

        [Theory]
        [InlineData("Peter Parker,O,26,biochemistry,2", "Current value O but only accept value M or F")]
        [InlineData(",M,26,biochemistry,2", "Value required, current value is empty")]
        [InlineData("44,M,26,biochemistry,2", "Value required, current value is a number")]
        [InlineData("Peter Parker,M,no age,biochemistry,2", "Value isn't number")]
        [InlineData("Peter Parker,M,0,biochemistry,2", "Value must be greater than zero")]
        [InlineData("Peter Parker,M,-3,biochemistry,2", "Value must be greater than zero")]
        [InlineData("Peter Parker,M,26,,2", "Value required, current value is empty")]
        [InlineData("Peter Parker,M,26,biochemistry,0", "Value must be greater than zero")]
        [InlineData("Peter Parker,M,26,biochemistry,-3", "Value must be greater than zero")]
        public void When_SendIncorrectValue_Then_ReturnThrowException(string studentText, string messageException)
        {
            var exception = Assert.Throws<System.IO.FileLoadException>(() => service.ToStudents(studentText));
            exception.Message.Should().Be(messageException);
        }

        [Theory]
        [InlineData("name,genre,age,study", "Structure line isn't correct")]
        [InlineData("name,genre,age,study,academicYear,phone", "Structure line isn't correct")]
        public void When_SendIncorrectItems_Then_ReturnThrowException(string studentText, string message)
        {
            var exception = Assert.Throws<System.IO.FileLoadException>(() => service.ToStudents(studentText));
            exception.Message.Should().Be(message);
        }
    }
}