using expressForm.Core.Models.Forms;
using expressForm.Web.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace expressForm.Web.Controllers
{
    public class QuestionsController : Controller
    {
        private readonly IFormRepository _formRepository;

        public QuestionsController(IFormRepository formRepository)
        {
            //TODO: move System. to using
            _formRepository = formRepository ?? throw new System.ArgumentNullException(nameof(formRepository));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? formId, int? questionId)
        {
            if (formId == null)
            {
                return NotFound();
            }

            var form = await _formRepository.FindAsync(formId.Value);

            if (form == null)
            {
                return NotFound();
            }

            Question question = null;

            if (questionId == null)
            {
                question = form.Questions.FirstOrDefault();

                if (question == null)
                {
                    // IsRequired initialization is not needed
                    question = new Question { Text = "Untitled Question", Type = QuestionType.MutipleChoice, IsRequired = false };
                    form.Questions = new List<Question> { question };
                    _formRepository.Update(form);//need?
                    await _formRepository.SaveChangesAsync();
                }
            }
            else
            {
                question = form.Questions.SingleOrDefault(question => question.Id == questionId);
            }

            // viewModel is enuf
            var formQuestionViewModel = new FormQuestionViewModel
            {
                Form = form.ToFormViewModel(),
                Question = question.ToQuestionViewModel(), // NW: question is nullable
                Questions = form.Questions.Select(question => question.ToQuestionViewModel()),
                HasQuestions = form.Questions.Any()
            };

            return View("Index", formQuestionViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int? formId, int? questionId, FormQuestionViewModel viewModel)
        {
            if (formId == null)
            {
                return NotFound();
            }

            var form = await _formRepository.FindAsync(formId.Value);
            if (form == null)
            {
                return NotFound();
            }

            var question = form.Questions.SingleOrDefault(question => question.Id == questionId);
            question.Text = viewModel.Question.Text;
            question.Type = viewModel.Question.Type.ToQuestionType();
            question.IsRequired = viewModel.Question.IsRequired;
            _formRepository.Update(form);
            await _formRepository.SaveChangesAsync();

            var formQuestionViewModel = new FormQuestionViewModel
            {
                Form = form.ToFormViewModel(),
                Question = question.ToQuestionViewModel(),
                Questions = form.Questions.Select(question => question.ToQuestionViewModel()),
                HasQuestions = form.Questions != null
            };

            return View("Index", formQuestionViewModel);
        }
    }
}
