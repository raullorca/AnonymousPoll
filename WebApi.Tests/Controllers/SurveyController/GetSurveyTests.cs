using FluentAssertions;
using Moq;
using System.Web.Http.Results;
using WebApi.Data;
using WebApi.Services;
using Xunit;

namespace WebApi.Tests.Controllers.SurveyController
{
    public class GetSurveyTests
    {
        [Fact]
        public void When_ReceiveASurvey_Then_WillReturnStudentsByCase()
        {
            var studentRepository = new Mock<IStudentRepository>();
            studentRepository.Setup(x => x.GetAll()).Returns(FakeValues.StudentsList);

            var text2listSurvey = new Text2ListSurvey();
            var surveyRepository = new SurveyRepository(text2listSurvey);

            var service = new SurveyService(studentRepository.Object, surveyRepository);
            var controller = new WebApi.Controllers.SurveyController(service);

            var result = controller.GetSurvey(FakeValues.Surveys);
            var content = result as OkNegotiatedContentResult<string>;
            content.Content.Should().Be(FakeValues.Cases);
        }

        [Fact]
        public void Integration_When_ReceiveASurvey_Then_WhillReturnStudentByCase()
        {
            
            var text2ListStudents = new Text2ListStudents();
            var studentRepository = new StudentRepository(text2ListStudents);
            var text2listSurvey = new Text2ListSurvey();
            var surveyRepository = new SurveyRepository(text2listSurvey);

            var service = new SurveyService(studentRepository, surveyRepository);
            var controller = new WebApi.Controllers.SurveyController(service);

            var result = controller.GetSurvey(FakeValues.Surveys);
            var content = result as OkNegotiatedContentResult<string>;
            content.Content.Should().Be(FakeValues.Cases);
        }
    }
}