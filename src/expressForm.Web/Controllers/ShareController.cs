using expressForm.Core.Models.Forms;
using Microsoft.AspNetCore.Mvc;
using expressForm.Web.Extensions;
using System.Threading.Tasks;

namespace expressForm.Web.Controllers
{
    public class ShareController : Controller
    {
        private readonly IFormRepository _formRepository;

        public ShareController(IFormRepository formRepository)
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

            if (formId == null)
            {
                return NotFound();
            }

            return View(form.ToViewModel());
        }
    }
}

