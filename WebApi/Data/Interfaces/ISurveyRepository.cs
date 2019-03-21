using System.Linq;
using WebApi.Models;

namespace WebApi.Data
{
    public interface ISurveyRepository
    {
        IQueryable<Survey> GetAll(string data);
    }
}