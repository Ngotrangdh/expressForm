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

            var response = new ResponseViewModel
            {
                FormTitle = form.Title,
                FormDescription = form.Description,
                Answers = form.Questions
                    .Select(q => new AnswerViewModel { Question = q.ToViewModel()})
                    .ToList()
            };
        
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Preview(int? formId, ResponseViewModel viewModel)
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

             var response = new Response
            {
                Answers = viewModel.Answers
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
            return View("Success", form.ToViewModel());
        }

        public async Task<IActionResult> View(Guid guid)
        {
            var form = await _formRepository.FindAsync(guid);

            if (form == null)
            {
                return NotFound();
            }

            return RedirectToAction("preview", new { formId = form.Id });
        }
    }
}
