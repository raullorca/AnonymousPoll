using System.Linq;
using System.Text;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Services
{
    public class SurveyService : ISurveyService
    {
        private IStudentRepository _studentRepository;
        private ISurveyRepository _surveyRepository;
        private IQueryable<Student> _students;

        public SurveyService(IStudentRepository studentRepository, ISurveyRepository surveyRepository)
        {
            _studentRepository = studentRepository;
            _surveyRepository = surveyRepository;
            _students = _studentRepository.GetAll();
        }

        public string GetResults(string data)
        {
            var surveys = _surveyRepository.GetAll(data);
            int caseNumber = 1;
            var result = new StringBuilder();

            foreach (var survey in surveys)
            {
                var students = GetNames(survey).OrderBy(x => x.Name);
                var text = GetStudentText(students);

                result.AppendLine($"Case #{caseNumber}: {text}");
                caseNumber++;
            }

            return result.ToString();
        }

        private IQueryable<Student> GetNames(Survey survey)
        {
            return _students
                .Where(x => x.AcademicYear == survey.AcademicYear &&
                            x.Age == survey.Age &&
                            x.Gender == survey.Gender &&
                            x.Study == survey.Study);
        }

        private string GetStudentText(IOrderedQueryable<Models.Student> students)
        {
            if (!students.Any())
            {
                return "NONE";
            }

            return string.Join(",", students.Select(x => x.Name).ToArray());
        }
    }
}