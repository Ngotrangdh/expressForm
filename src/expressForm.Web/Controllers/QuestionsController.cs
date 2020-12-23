using expressForm.Core.Models.Forms;
using expressForm.Web.Models;
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
            _formRepository = formRepository ?? throw new System.ArgumentNullException(nameof(formRepository));
        }

        public async Task<IActionResult> Index(int? formId)
        {
            if (formId == null)
            {
                return NotFound();
            }

            var form = await _formRepository.FindAsync(formId.Value);

            var questions = form.Questions;

            var model = new FormQuestionViewModel
            {
                Questions = questions.Select(question => question.ToQuestionViewModel()),
                Form = form.ToFormViewModel(),
                HasQuestions = questions != null,
                Question = new QuestionViewModel { Id = 0, Type = QuestionTypeViewModel.MutipleChoice}
            };

            return View(model);
        }

        //public async Task<IActionResult> Index(int? formId, FormQuestionViewModel viewModel)
        //{
        //    if (formId == null)
        //    {
        //        return NotFound();
        //    }


        //    return View();
        //}
        
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

                var question = new Question 
                {
                    Text = viewModel.Question.Text, 
                    Options = viewModel.Question.Options,
                    Type = viewModel.Question.Type.ToQuestionType(), 
                    IsRequired = viewModel.Question.IsRequired  
                };

                if (form.Questions == null)
                {
                    form.Questions = new List<Question>();
                }

                form.Questions.Add(question);
                _formRepository.Update(form);
                await _formRepository.SaveChangesAsync();
                return RedirectToAction("Index", new { formId = formId.Value, questionId = question.Id });
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
            if (form == null)
            {
                return NotFound();
            }

            var question = form.Questions.SingleOrDefault(question => question.Id == questionId);

            if (question != null)
            {
                question.Type = viewModel.Question.Type.ToQuestionType();
                await _formRepository.SaveChangesAsync();
            }
            else
            {
                question = new Question { Id = 0, Type = viewModel.Question.Type.ToQuestionType() };
            }

            var formQuestionViewModel = new FormQuestionViewModel
            {
                Form = form.ToFormViewModel(),
                Question = question.ToQuestionViewModel(),
                Questions = form.Questions.Select(question => question.ToQuestionViewModel()),
                HasQuestions = form.Questions != null
            };
        
            return View("Index", formQuestionViewModel);
        }

        public async Task<IActionResult> QuestionRequired(int? formId, int? questionId, FormQuestionViewModel viewModel)
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

            if (question != null)
            {
                question.IsRequired = viewModel.Question.IsRequired;
                await _formRepository.SaveChangesAsync();
            }
            else
            {
                question = new Question { Id = 0, IsRequired = viewModel.Question.IsRequired };
            }

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

    public static class FormViewModelExtension
    {
        public static FormViewModel ToFormViewModel(this Form form)
        {
            var questions = form.Questions;
            if (questions == null)
            {
                questions = new List<Question>();
            }

            var formViewModel =  new FormViewModel
            {
                Id = form.Id,
                Title = form.Title,
                Description = form.Description,
                Link = form.Link,
                Questions = form.Questions.Select(question => question.ToQuestionViewModel()),
                Responses = form.Responses,
                CreatedDate = form.CreatedDate,
                UpdatedDate = form.UpdatedDate,
                User = form.User
            };
            return formViewModel;
        }
    }
}
