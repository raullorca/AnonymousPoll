using System.Linq;
using System.Web.Configuration;
using WebApi.Models;

namespace WebApi.Data
{
    public class StudentRepository : IStudentRepository
    {
        private IText2ListStudents _text2ListStudents;

        public StudentRepository(IText2ListStudents text2ListStudents)
        {
            _text2ListStudents = text2ListStudents;
        }

        public IQueryable<Student> GetAll()
        {
            var path = WebConfigurationManager.AppSettings["studentsFile"];
            var value = _text2ListStudents.LoadFile(path);
            var students = _text2ListStudents.ToStudents(value);
            return students;
        }
    }
}