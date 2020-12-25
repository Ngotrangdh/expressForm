using expressForm.Core.Models.Forms;
using expressForm.Web.Models;
using System.Collections.Generic;
using System.Linq;

namespace expressForm.Web.Extensions
{
    public static class FormExtension
    {
        public static FormViewModel ToViewModel(this Form form)
        {
            var questions = form.Questions;
            if (questions == null)
            {
                questions = new List<Question>();
            }

            var formViewModel = new FormViewModel
            {
                Id = form.Id,
                Title = form.Title,
                Description = form.Description,
                Link = form.Link,
                Questions = form.Questions.Select(question => question.ToViewModel()),
                Responses = form.Responses,
                CreatedDate = form.CreatedDate,
                UpdatedDate = form.UpdatedDate,
                User = form.User
            };
            return formViewModel;
        }
    }
}
