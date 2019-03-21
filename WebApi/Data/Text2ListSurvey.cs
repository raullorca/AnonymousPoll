using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using WebApi.Models;

namespace WebApi.Data
{
    public interface IText2ListSurvey
    {
        IQueryable<Survey> ToSurveys(string value);
    }
    public class Text2ListSurvey : Text2List, IText2ListSurvey
    {
        const int positionGender = 0;
        const int positionAge = 1;
        const int positionStudy = 2;
        const int positionAcademicYear = 3;
        const int fieldsQuantity = 4;

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