using System.Linq;
using WebApi.Models;

namespace WebApi.Data
{
    public interface IText2ListSurvey
    {
        IQueryable<Survey> ToSurveys(string value);
    }
}