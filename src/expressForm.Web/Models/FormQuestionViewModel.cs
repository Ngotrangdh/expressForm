using expressForm.Core.Models.Forms;
using expressForm.Web.Extensions;
using expressForm.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace expressForm.Web.Models
{
    public class FormQuestionViewModel
    {
        public FormQuestionViewModel()
        {
        }

        public FormQuestionViewModel(Form form, Question question)
        {
            Form = form?.ToViewModel() ?? throw new ArgumentNullException(nameof(form));
            Question = question?.ToViewModel() ?? throw new ArgumentNullException(nameof(question));
            Questions = form.Questions.Select(q => q.ToViewModel()).ToList();
        }

        public IEnumerable<QuestionViewModel> Questions { get; set; }
        public FormViewModel Form { get; set; }
        public QuestionViewModel Question { get; set; }
        public bool HasOptions => ((Question==null)?false: _hasOptionTypes.Contains(Question.Type));

        private static readonly HashSet<QuestionTypeViewModel> _hasOptionTypes = new HashSet<QuestionTypeViewModel>
        {
            QuestionTypeViewModel.Checkboxes,
            QuestionTypeViewModel.Dropdown,
            QuestionTypeViewModel.MutipleChoice
        };
    }
}
