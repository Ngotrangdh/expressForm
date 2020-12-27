using expressForm.Core.Models.Forms;
using expressForm.Web.Extensions;
using expressForm.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace expressForm.Web.Controllers
{
    public class ViewController : Controller
    {
        private readonly IFormRepository _formRepository;

        public ViewController(IFormRepository formRepository)
        {
            _formRepository = formRepository;
        }

        public async Task<IActionResult> Preview(int? formId)
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

            var response = new Response();

            form.Responses.Add(response);
            _formRepository.Update(form);
            await _formRepository.SaveChangesAsync();
            var answers = new List<AnswerViewModel>();

            answers = form.Questions.Select(q => new AnswerViewModel { Question = q.ToViewModel(), Response = response }).ToList();
            return View(answers);
        }

        [HttpPost]
        public async Task<string> Preview(int? formId, int? responseId, List<AnswerViewModel> viewModel)
        {
            if (formId == null)
            {
                //return NotFound();
            }

            var form = await _formRepository.FindAsync(formId.Value);

            if (form == null)
            {
                //return NotFound();
            }

            if (responseId == null)
            {
                //return NotFound();
            }

            var response = new Response
            {
                Answers = viewModel
                    .Select(a => new Answer
                    {
                        Text = a.Text,
                        Question = form.Questions.SingleOrDefault(q => q.Id == a.Question.Id)
                    })
                    .ToList(),
                Form = form
            };

            form.Responses.Add(response);

            _formRepository.Update(form);
            await _formRepository.SaveChangesAsync();
            return "Successful";
        }
    }

    public class AnswerViewModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public QuestionViewModel Question { get; set; }
        public Response Response { get; set; }
    }



}
