using expressForm.Core.Models.Forms;
using Microsoft.AspNetCore.Mvc;
using expressForm.Web.Extensions;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using expressForm.Web.Models;

namespace expressForm.Web.Controllers
{
    public class ShareController : Controller
    {
        private readonly IFormRepository _formRepository;
        private readonly IConfiguration _configuration;

        public ShareController(IFormRepository formRepository, IConfiguration configuration)
        {
            _formRepository = formRepository;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index(int? formId)
        {
            if (formId == null)
            {
                return NotFound();
            }

            var form = await _formRepository.FindAsync(formId.Value);

            if (formId == null)
            {
                return NotFound();
            }

            return View(form.ToViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendForm(EmailViewModel viewModel)
        {
            if (viewModel.FormId == null)
            {
                return NotFound();
            }

            var form = await _formRepository.FindAsync(int.Parse(viewModel.FormId));

            if (form == null)
            {
                return NotFound();
            }
            //check viewModel valid

            var response =  await SendEmail(viewModel);
            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            viewModel.FormTitle = form.Title;

            return View("Success", viewModel);
        }

        private async Task<SendGrid.Response> SendEmail(EmailViewModel viewModel)
        {
            var apiKey = _configuration.GetSection("SendgridApiKey").Value;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("expressform@techie.com");
            var subject = viewModel.Subject;
            var to = new EmailAddress(viewModel.Email);
            var plainTextContent = viewModel.Message;
            var htmlContent = viewModel.Message;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
            return response;
        }
    }
}

