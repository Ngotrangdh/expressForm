using expressForm.Web.Models;
using System.Collections.Generic;

namespace expressForm.Web.Controllers
{
    public class FormQuestionViewModel
    {
        public IEnumerable<QuestionViewModel> Questions { get; set; }
        public FormViewModel Form { get; set; }
        public bool HasOptions { get; set; }
        public QuestionViewModel Question { get; set; }
    }
}
