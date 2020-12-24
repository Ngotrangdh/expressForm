using expressForm.Core.Models.Forms;
using expressForm.Web.Models;

namespace expressForm.Web.Extensions
{
    public static class QuestionExtension
    {
        public static QuestionViewModel ToQuestionViewModel(this Question question)
        {
            return new QuestionViewModel
            {
                Id = question.Id,
                Text = question.Text,
                Type = question.Type.ToQuestionTypeViewModel(),
                IsRequired = question.IsRequired,
                Options = question.Options,
                Answers = question.Answers,
            };
        }
    }
}
