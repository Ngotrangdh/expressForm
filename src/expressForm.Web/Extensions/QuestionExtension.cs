using expressForm.Core.Models.Forms;
using expressForm.Web.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace expressForm.Web.Extensions
{
    public static class QuestionExtension
    {
        public static QuestionViewModel ToViewModel(this Question question)
        {
            return new QuestionViewModel
            {
                Id = question.Id,
                Text = question.Text,
                Type = question.Type.ToViewModel(),
                IsRequired = question.IsRequired,
                Options = JsonConvert.DeserializeObject<List<string>>(question.Options ?? string.Empty) ?? new List<string>(),
                Answers = question.Answers,
                Form = question.Form.ToViewModel()
            };
        }
    }
}
