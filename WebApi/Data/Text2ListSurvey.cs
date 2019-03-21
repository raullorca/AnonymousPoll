using System.Collections.Generic;
using System.IO;
using System.Linq;
using WebApi.Models;

namespace WebApi.Data
{
    public class Text2ListSurvey : Text2List, IText2ListSurvey
    {
        private const int positionGender = 0;
        private const int positionAge = 1;
        private const int positionStudy = 2;
        private const int positionAcademicYear = 3;
        private const int fieldsQuantity = 4;

        public IQueryable<Survey> ToSurveys(string value)
        {
            var surveyList = new List<Survey>();
            Survey survey;
            bool firstline = true;
            int cases = 0;

            using (var reader = new StringReader(value))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (firstline)
                    {
                        firstline = false;
                        if (!IsNumeric(line))
                        {
                            GenerateThrow("NumberLine not defined");
                        }
                        cases = int.Parse(line);
                        continue;
                    }
                    var item = line.Split(',');
                    if (item.Length != fieldsQuantity)
                    {
                        GenerateThrow("Structure line isn't correct");
                    }
                    survey = new Survey
                    {
                        Gender = SetGender(item, positionGender),
                        Age = SetNumeric(item, positionAge),
                        Study = SetString(item, positionStudy),
                        AcademicYear = SetNumeric(item, positionAcademicYear)
                    };
                    surveyList.Add(survey);
                }
            }
            if (cases != surveyList.Count)
            {
                GenerateThrow("NumberLine wrong");
            }
            return surveyList.AsQueryable();
        }
    }
}