using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace expressForm.Web.Models
{
    public enum QuestionTypeViewModel
    {
        [Display(Name = "Short Answer")]
        ShortAnswer,

        [Display(Name = "Paragraph")]
        Paragraph,

        [Display(Name = "Multiple Choice")]
        MutipleChoice,

        [Display(Name = "Checkboxes")]
        Checkboxes,

        [Display(Name = "Dropdown List")]
        Dropdown,

        [Display(Name = "Date Time")]
        Date
    }
}
