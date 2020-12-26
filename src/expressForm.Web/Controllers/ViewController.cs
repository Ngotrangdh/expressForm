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

            var model = form.ToViewModel();
            return View(model);
        }
    }
}
