using System.Linq;
using WebApi.Models;

namespace WebApi.Data
{
    public class SurveyRepository : ISurveyRepository
    {
        private IText2ListSurvey _text2ListSurvey;

        public SurveyRepository(IText2ListSurvey text2ListSurvey)
        {
            _text2ListSurvey = text2ListSurvey;
        }

        public IQueryable<Survey> GetAll(string data)
        {
            return _text2ListSurvey.ToSurveys(data);
        }
    }
}