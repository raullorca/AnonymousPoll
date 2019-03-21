using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Moq;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Tests.Services.SurveyServices
{
    public class GetResultsTests
    {
        [Fact]
        public void When_ReceiveASurvey_Then_WillReturnStudentsByCase()
        {
            var studentRepository = new Mock<IStudentRepository>();
            studentRepository.Setup(x => x.GetAll()).Returns(FakeValues.StudentsList);

            var text2listSurvey = new Text2ListSurvey();
            var surveyRepository = new SurveyRepository(text2listSurvey);
            var service = new WebApi.Services.SurveyService(studentRepository.Object, surveyRepository);

            var results = service.GetResults(FakeValues.Surveys);

            results.Should().Be(FakeValues.Cases);

        }

        
    }
}
