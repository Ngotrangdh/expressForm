using expressForm.Core.Models.Forms;
using expressForm.Web.Extensions;
using expressForm.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                    question = await AddNewQuestion(form);
                }
            }
            else
            {
                if (questionId == 0) // this logic should be moved to another action
                {
                    question = await AddNewQuestion(form);
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

            var viewModel = new FormQuestionViewModel(form, question);

            return View("Edit", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int? formId, int? questionId, bool? newOption, FormQuestionViewModel viewModel)
        {
            if (formId == null || questionId == null)
            {
                return NotFound();
            }

            var form = await _formRepository.FindAsync(formId.Value);

            if (form == null)
            {
                return NotFound();
            }

            // this logic should be moved to its own action
            if (newOption.HasValue)
            {
                viewModel.Question.Options.Add(string.Empty);
            }

            var question = form.Questions.SingleOrDefault(question => question.Id == questionId);

            if (question == null)
            {
                return NotFound();
            }

            var model = new FormQuestionViewModel(form, question);

            if (ModelState.IsValid)
            {
                try
                {
                    question.Text = viewModel.Question.Text;
                    question.Type = viewModel.Question.Type.ToQuestionType();
                    question.IsRequired = viewModel.Question.IsRequired;
                    question.Options = JsonConvert.SerializeObject(viewModel.Question.Options);

                    _formRepository.Update(form);
                    await _formRepository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int? formId, int? questionId)
        {
            if (formId == null || questionId == null)
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

        private async Task<Question> AddNewQuestion(Form form)
        {
            Question question = new Question { Text = "Untitled Question", Type = QuestionType.MutipleChoice };
            form.Questions.Add(question);
            _formRepository.Update(form); // is this necessary?
            await _formRepository.SaveChangesAsync();
            return question;
        }
    }
}
