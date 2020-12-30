using expressForm.Core.Models.Forms;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace expressForm.Web.Models
{
    public class QuestionViewModel
    {
        public int Id { get; set; }
        public QuestionTypeViewModel Type { get; set; }

        [Required(ErrorMessage = "Question cannot be empty")]
        [MaxLength(500, ErrorMessage = "Question cannot exceed 500 characters.")]
        [Display(Prompt = "Question")]
        public string Text { get; set; }
        public bool IsRequired { get; set; }

        [Display(Prompt = "Option")]
        public List<string> Options { get; set; } = new List<string>();

        public FormViewModel Form { get; set; }
        public IEnumerable<Answer> Answers { get; set; }
    }
}
