using System.Collections.Generic;
using System.IO;
using System.Linq;
using WebApi.Models;

namespace WebApi.Data
{
    public class Text2ListStudents : Text2List, IText2ListStudents
    {
        private const int positionName = 0;
        private const int positionGender = 1;
        private const int positionAge = 2;
        private const int positionStudy = 3;
        private const int positionAcademicYear = 4;
        private const int fieldsQuantity = 5;

        public string LoadFile(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException(@"File not found
                The path and name of the file can be found in web.config
                in key *studentsFile*
                by default it is located in c:\resources\students");
            }
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
                        Gender = SetGender(item, positionGender),
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