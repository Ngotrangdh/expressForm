using expressForm.Core.Models.Forms;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace expressForm.Web.Controllers
{
    public class ResponsesController : Controller
    {
        private readonly IFormRepository _formRepository;

        public ResponsesController(IFormRepository formRepository)
        {
            _formRepository = formRepository;
        }

        public async Task<IActionResult> Index(int? formId)
        {
            if (formId == null)
            {
                return NotFound();
            }

            var form = await _formRepository.FindAsync(formId.Value);

            return View(form.Responses);
        }
    }
}
