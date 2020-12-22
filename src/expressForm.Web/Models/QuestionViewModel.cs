using expressForm.Core.Models.Forms;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace expressForm.Web.Models
{
    public class QuestionViewModel
    {
        public int Id { get; set; }
        public QuestionType Type { get; set; }

        [Display(Prompt = "Question")]
        public string Text { get; set; }
        public bool IsRequired { get; set; }

        [Display(Prompt = "Option")]
        public string Options { get; set; }

        public Form Form { get; set; }
        public List<Answer> Answers { get; set; }
    }
}
