using expressForm.Core.Forms;
using expressForm.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace expressForm.Web.Controllers
{
    public class QuestionsController : Controller
    {
        public IActionResult Index(int formId)
        {
            var questions = new List<Question>()
            {
                new Question {Id = 0, Title= "Question 1"},
                new Question {Id = 1, Title= "Question 2"},
                new Question {Id = 2, Title= "Question 3"},
                new Question {Id = 3, Title= "Question 4"},
                new Question {Id = 4, Title= "Question 5"},
            };

            var model = new FormQuestionViewModel
            {
                Questions = questions,
                Form = new FormViewModel { Id = formId, Title = "First Form", Description = "Description" }
            };

            return View(model);
        }
    }

    public class Question
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }

    public class FormQuestionViewModel
    {
        public List<Question> Questions { get; set; }
        public FormViewModel Form { get; set; }
    }
}
