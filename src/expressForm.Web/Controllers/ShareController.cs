using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace expressForm.Web.Controllers
{
    public class ShareController : Controller
    {
        public IActionResult Index(int formId)
        {
            var model = new FormShareViewModel()
            {
                FormId = formId,
                Title = "First Form",
                Link = "https://docs.microsoft.com/en-us/aspnet/core/mvc/views/tag-helpers/built-in/anchor-tag-helper?view=aspnetcore-5.0"
            };
            return View(model);
        }
    }

    public class FormShareViewModel
    {
        public int FormId { get; set; }
        public String Title { get; set; }
        public string Link { get; set; }
    }
}

