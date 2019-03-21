using System.Collections.Generic;
using System.IO;
using System.Linq;
using WebApi.Models;

namespace WebApi.Data
{
    public interface IText2ListStudents
    {
        string LoadFile(string path);
        IQueryable<Student> ToStudents(string value);
    }
    public class Text2ListStudents : Text2List,IText2ListStudents
    {
        const int positionName = 0;
        const int positionGender = 1;
        const int positionAge = 2;
        const int positionStudy = 3;
        const int positionAcademicYear = 4;
        const int fieldsQuantity = 5;

        public string LoadFile(string path)
        {
            return File.ReadAllText(path);
        }

        public IQueryable<Student> ToStudents(string value)
        {
            var studentList = new List<Student>();
            Student student;

            using (var reader = new StringReader(value))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var item = line.Split(',');
                    if (item.Length != fieldsQuantity)
                    {
                        GenerateThrow("Structure line isn't correct");
                    }

                    student = new Student
                    {
                        Name = SetString(item, positionName),
                        Gender = SetGender(item,positionGender),
                        Age = SetNumeric(item, positionAge),
                        Study = SetString(item, positionStudy),
                        AcademicYear = SetNumeric(item, positionAcademicYear)
                    };
                    studentList.Add(student);
                }
            }

            return studentList.AsQueryable();
        }


    }
}