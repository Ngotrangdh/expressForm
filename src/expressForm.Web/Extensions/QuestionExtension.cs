using expressForm.Core.Models.Forms;
using expressForm.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace expressForm.Web.Extensions
{
    public static class QuestionExtension
    {
        public static QuestionViewModel ToViewModel(this Question question)
        {
            var options = JsonConvert.DeserializeObject<List<string>>(question.Options ?? string.Empty) ?? new List<string>();

            return new QuestionViewModel
            {
                Id = question.Id,
                Text = question.Text,
                Type = question.Type.ToViewModel(),
                IsRequired = question.IsRequired,
                Options = options.Select(option => new SelectListItem(option, option)).ToList(),
                SelectedOptions = options,
                Answers = question.Answers,
                Form = question.Form.ToViewModel()
            };
        }
    }
}
