using expressForm.Core.Models.Forms;
using expressForm.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace expressForm.Web.Controllers
{
    public class QuestionsController : Controller
    {
        private readonly IFormRepository _formRepository;

        public QuestionsController(IFormRepository formRepository)
        {
            _formRepository = formRepository ?? throw new System.ArgumentNullException(nameof(formRepository));
        }

        public async Task<IActionResult> Index(int formId)
        {
            var form = await _formRepository.FindAsync(formId);

            var questions = form.Questions;

            var model = new FormQuestionViewModel
            {
                Questions = questions,
                Form = form,
                HasQuestions = questions != null
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int? formId, FormQuestionViewModel viewModel)
        {
            if (formId == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var form = await _formRepository.FindAsync(formId.Value);
                var question = new Question { Text = viewModel.Question.Text };
                if (form.Questions == null)
                {
                    form.Questions = new List<Question>();
                }
                form.Questions.Add(question);
                _formRepository.Update(form);
                await _formRepository.SaveChangesAsync();
                return RedirectToAction("Index", new { formId = formId.Value });
            }
            return View();
        }

        public async Task<IActionResult> QuestionTypeChange(int? formId, int? questionId, FormQuestionViewModel viewModel)
        {
            if (formId == null)
            {
                return NotFound();
            }


            var form = await _formRepository.FindAsync(formId.Value);
            viewModel.Form = form;
            return View("Index", viewModel);
        }

        public async Task<IActionResult> QuestionRequired(int? formId, FormQuestionViewModel viewModel)
        {
            if (formId == null)
            {
                return NotFound();
            }

            var form = await _formRepository.FindAsync(formId.Value);
            viewModel.Form = form;
            return View("Index", viewModel);
        }
    }

    public class FormQuestionViewModel
    {
        public List<Question> Questions { get; set; }
        public Form Form { get; set; }
        public bool HasQuestions { get; set; }
        public QuestionViewModel Question { get; set; }
    }
}
