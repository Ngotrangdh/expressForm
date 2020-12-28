using expressForm.Core.Models.Forms;
using expressForm.Web.Extensions;
using expressForm.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
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
            _formRepository = formRepository ?? throw new ArgumentNullException(nameof(formRepository));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? formId, int? questionId)
        {
            // alternative {
            if (!formId.HasValue || await _formRepository.FindAsync(formId.Value) is not Form form1)
            {
                return NotFound();
            }
            // }

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
                    question = await AddNewQuestion(form, new List<Question>());
                }
            }
            else
            {
                if (questionId == 0) // this logic should be moved to another action
                {
                    question = await AddNewQuestion(form, form.Questions); 
                }
                else
                {
                    question = form.Questions.SingleOrDefault(question => question.Id == questionId);

                    if (question == null)
                    {
                        return NotFound();
                    }
                }
            }

            // this logic can be encapsulated to FormQuestionViewModel's ctor
            var model = new FormQuestionViewModel // rename to viewModel to be consistent
            {
                Form = form.ToViewModel(),
                Question = question.ToViewModel(),
                Questions = form.Questions.Select(question => question.ToViewModel()),
                HasOptions = IsMultiOptions(question.Type.ToViewModel())
            }; // viewModel to be consistent

            return View("Index", model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int? formId, int? questionId, bool? newOption, FormQuestionViewModel viewModel)
        {
            if (formId == null) // || questionId == null
            {
                return NotFound();
            }

            var form = await _formRepository.FindAsync(formId.Value);

            if (form == null)
            {
                return NotFound();
            }

            // this logic should be moved to its own action {
            if (newOption != null) // if (newOption.HasValue)
            {
                if (newOption.Value) // not needed
                {
                    viewModel.Question.Options.Add(string.Empty);
                }
            }
            // }

            var question = form.Questions.SingleOrDefault(question => question.Id == questionId);

            // what if question is null?

            question.Text = viewModel.Question.Text;
            question.Type = viewModel.Question.Type.ToQuestionType();
            question.IsRequired = viewModel.Question.IsRequired;
            question.Options = JsonConvert.SerializeObject(viewModel.Question.Options);

            _formRepository.Update(form);
            await _formRepository.SaveChangesAsync();

            // this logic can be encapsulated to FormQuestionViewModel's ctor
            var model = new FormQuestionViewModel // viewModel to be consistent
            {
                Form = form.ToViewModel(),
                Question = question.ToViewModel(),
                Questions = form.Questions.Select(question => question.ToViewModel()),
                HasOptions = IsMultiOptions(question.Type.ToViewModel())
            };

            return View("Index", model); // View name should be Edit to be consistent
        }

        public async Task<IActionResult> Delete(int? formId, int? questionId)
        {
            if (formId == null) // || questionId is null
            {
                return NotFound();
            }

            var form = await _formRepository.FindAsync(formId.Value);

            if (form == null)
            {
                return NotFound();
            }

            var question = form.Questions.SingleOrDefault(question => question.Id == questionId);

            if (question == null)
            {
                return NotFound();
            }

            form.Questions.Remove(question);

            _formRepository.Update(form);
            await _formRepository.SaveChangesAsync();

            return RedirectToAction("Edit", new { formId });
        }

        private async Task<Question> AddNewQuestion(Form form, List<Question> questions) // questions is not needed
        {
            Question question = new Question { Text = "Untitled Question", Type = QuestionType.MutipleChoice };
            questions.Add(question);
            form.Questions = questions;
            _formRepository.Update(form); // is this necessary?
            await _formRepository.SaveChangesAsync();
            return question;
        }

        // this logic should be encapsulated to FormQuestionViewModel
        private bool IsMultiOptions(QuestionTypeViewModel questionType) // make static and rename to HasOptions
        {
            // options is enuf
            // Also, this should be a private field of HashSet
            var optionList = new List<QuestionTypeViewModel> 
            {
                QuestionTypeViewModel.Checkboxes,
                QuestionTypeViewModel.Dropdown,
                QuestionTypeViewModel.MutipleChoice
            };

            return optionList.Contains(questionType);
        }
    }
}
