using System;
using System.Web.Http;
using WebApi.Services;

namespace WebApi.Controllers
{
    public class SurveyController : ApiController
    {
        private ISurveyService _surveyService;

        public SurveyController(ISurveyService surveyService)
        {
            _surveyService = surveyService;
        }

        [HttpGet]
        public IHttpActionResult GetSurvey(string data)
        {
            try
            {
                var result = _surveyService.GetResults(data);
                return Ok(result);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
    }
}