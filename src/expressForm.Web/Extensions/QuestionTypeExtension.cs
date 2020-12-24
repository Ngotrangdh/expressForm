using expressForm.Core.Models.Forms;
using expressForm.Web.Models;

namespace expressForm.Web.Extensions
{
    public static class QuestionTypeExtension
    {
        public static QuestionTypeViewModel ToQuestionTypeViewModel(this QuestionType questionType)
        {
            return (QuestionTypeViewModel)questionType;
        }

        public static QuestionType ToQuestionType(this QuestionTypeViewModel questionTypeViewModel)
        {
            return (QuestionType)questionTypeViewModel;
        }
    }
}
