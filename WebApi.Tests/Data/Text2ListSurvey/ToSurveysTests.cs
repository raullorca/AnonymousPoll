using FluentAssertions;
using System.Linq;
using System.Text;
using WebApi.Data;
using WebApi.Models;
using Xunit;

namespace WebApi.Tests.Data.Text2ListSurvey
{
    public class ToSurveysTests
    {
        private readonly IText2ListSurvey service;

        private readonly Survey surveyExpected = new Survey
        {
            Gender = "M",
            Age = 26,
            Study = "biochemistry",
            AcademicYear = 2
        };

        public ToSurveysTests()
        {
            service = new WebApi.Data.Text2ListSurvey();
        }

        [Fact]
        public void When_SendCorrectStructure_Then_ReturnCorrectModel()
        {
            var surveyText = "M,26,biochemistry,2";
            var text = SetNumberLine(1, surveyText);
            var surveys = service.ToSurveys(text);
            surveys.Count().Should().Be(1);
            surveys.First().Should().BeEquivalentTo(surveyExpected);
        }

        [Fact]
        public void When_SendMultiplesCorrectStructure_Then_ReturnCorrectListModels()
        {
            var surveyText = new StringBuilder();
            surveyText.AppendLine("2");
            surveyText.AppendLine("M,26,biochemistry,2");
            surveyText.AppendLine("F,28,psychology,2");
            var secondSurvey = new Survey
            {
                Gender = "F",
                Age = 28,
                Study = "psychology",
                AcademicYear = 2
            };

            var surveys = service.ToSurveys(surveyText.ToString());

            surveys.Count().Should().Be(2);
            surveys.First().Should().BeEquivalentTo(surveyExpected);
            surveys.Last().Should().BeEquivalentTo(secondSurvey);
        }

        [Theory]
        [InlineData("O,26,biochemistry,2", "Current value O but only accept value M or F")]
        [InlineData("M,no age,biochemistry,2", "Value isn't number")]
        [InlineData("M,0,biochemistry,2", "Value must be greater than zero")]
        [InlineData("M,-3,biochemistry,2", "Value must be greater than zero")]
        [InlineData("M,26,,2", "Value required, current value is empty")]
        [InlineData("M,26,biochemistry,0", "Value must be greater than zero")]
        [InlineData("M,26,biochemistry,-3", "Value must be greater than zero")]
        public void When_SendIncorrectValue_Then_ReturnThrowException(string surveyText, string messageException)
        {
            string text = SetNumberLine(1, surveyText);
            var exception = Assert.Throws<System.IO.FileLoadException>(() => service.ToSurveys(text));
            exception.Message.Should().Be(messageException);
        }

        [Theory]
        [InlineData("genre,age,study", "Structure line isn't correct")]
        [InlineData("genre,age,study,academicYear,phone", "Structure line isn't correct")]
        public void When_SendIncorrectItems_Then_ReturnThrowException(string surveyText, string message)
        {
            var text = SetNumberLine(1, surveyText);
            var exception = Assert.Throws<System.IO.FileLoadException>(() => service.ToSurveys(text));
            exception.Message.Should().Be(message);
        }

        [Fact]
        public void When_SendWithoutNumberLine_Then_ReturnThrowException()
        {
            var surveyText = "M,26,biochemistry,2";
            var exception = Assert.Throws<System.IO.FileLoadException>(() => service.ToSurveys(surveyText));
            exception.Message.Should().Be("NumberLine not defined");
        }

        [Fact]
        public void When_SendWrongNumberLine_Then_ReturnThrowException()
        {
            var surveyText = "M,26,biochemistry,2";
            var text = SetNumberLine(2, surveyText);
            var exception = Assert.Throws<System.IO.FileLoadException>(() => service.ToSurveys(text));
            exception.Message.Should().Be("NumberLine wrong");
        }

        [Fact]
        public void When_SendNumberLineGreaterThan100_Then_ReturnThrowException()
        {
            var surveyText = "M,26,biochemistry,2";
            var text = SetNumberLine(101, surveyText);
            var exception = Assert.Throws<System.IO.FileLoadException>(() => service.ToSurveys(text));
            exception.Message.Should().Be("NumberLine too long");
        }

        private string SetNumberLine(int quantity, string surveyText)
        {
            var text = new StringBuilder();
            text.AppendLine(quantity.ToString());
            text.AppendLine(surveyText);
            return text.ToString();
        }
    }
}