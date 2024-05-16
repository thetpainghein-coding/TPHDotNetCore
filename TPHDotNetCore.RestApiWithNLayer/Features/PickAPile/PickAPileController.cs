using System.Globalization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace TPHDotNetCore.RestApiWithNLayer.Features.PickAPile
{
    [Route("api/[controller]")]
    [ApiController]
    public class PickAPileController : ControllerBase
    {

        private PickAPile _data;

        private async Task<PickAPile> GetDataAsync()
        {
            string jsonStr = await System.IO.File.ReadAllTextAsync("PickAPile.json");
            var model = JsonConvert.DeserializeObject<PickAPile>(jsonStr);
            return model;
        }

        [HttpGet]
        public async Task<IActionResult> AllQuestions()
        {
            var model = await GetDataAsync();
            return Ok(model.Questions);
        }

        [HttpGet("{questionId}")]
        public async Task<IActionResult> Question(int questionId)
        {
            var model = await GetDataAsync();
            return Ok(model.Questions.FirstOrDefault(x => x.QuestionId == questionId));
        }

        [HttpGet("{questionNo}/{pileNo}")]
        public async Task<IActionResult> Answer(int questionNo, int pileNo)
        {
            var model = await GetDataAsync();

            string pileName;
            if (pileNo == 1)
            {
                pileName = "Pile-1 ";
            }
            else if (pileNo == 2)
            {
                pileName = "Pile-2";
            }
            else if (pileNo == 3)
            {
                pileName = "Pile-3";
            }
            else if (pileNo == 4)
            {
                pileName = "Pile-4";
            }
            else pileName = "No pile";


            return Ok(model.Answers.FirstOrDefault(x => x.QuestionId == questionNo && x.AnswerName == pileName));

        }

    }


    public class PickAPile
    {
        public Question[] Questions { get; set; }
        public Answer[] Answers { get; set; }
    }

    public class Question
    {
        public int QuestionId { get; set; }
        public string QuestionName { get; set; }
        public string QuestionDesp { get; set; }
    }

    public class Answer
    {
        public int AnswerId { get; set; }
        public string AnswerImageUrl { get; set; }
        public string AnswerName { get; set; }
        public string AnswerDesp { get; set; }
        public int QuestionId { get; set; }
    }


}
